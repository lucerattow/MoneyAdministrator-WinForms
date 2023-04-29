using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Common.Enums;
using MoneyAdministrator.Common.Utilities.TypeTools;
using MoneyAdministrator.DTOs.Enums;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services;
using MoneyAdministrator.Utilities;
using MoneyAdministrator.Utilities.Disposable;
using MoneyAdministrator.Views;
using System.Configuration;
using System.Globalization;
using System.Text;

namespace MoneyAdministrator.Presenters
{
    public class CreditCardSummaryPresenter
    {
        //fields
        private ICreditCardSummaryView _view;
        private string _databasePath;
        private Action _closeView;

        //properties
        public ICreditCardSummaryView View
        {
            get { return _view; }
        }

        public CreditCardSummaryPresenter(string databasePath, Action closeView)
        {
            _view = new CreditCardResumesView();
            _databasePath = databasePath;
            _closeView = closeView;

            AssosiateEvents();
        }

        //methods
        private void AssosiateEvents()
        {
            _view.ButtonImportClick += ButtonImportClick;
            _view.ButtonNewPayClick += ButtonNewPayClick;
            _view.ButtonInsertClick += ButtonInsertClick;
            _view.ButtonDeleteClick += ButtonDeleteClick;
            _view.ButtonExitClick += ButtonExitClick;
            _view.ButtonSearchCreditCardClick += ButtonSearchCreditCardClick;
            _view.SummaryListNodeClick += SummaryListNodeClick;
        }

        private void TvRefreshData(CreditCard creditCard)
        {
            var creditCardService = new CreditCardService(_databasePath);
            creditCard = creditCardService.Get(creditCard.Id);

            var CCSumaries = new List<TreeViewSummaryListDto>();

            if (creditCard.CCSumaries.Count() != 0)
            {
                creditCard.CCSumaries.ToList().ForEach(x =>
                {
                    var summaryDto = new TreeViewSummaryListDto
                    {
                        Id = x.Id,
                        Period = x.Period,
                        Imported = x.Imported,
                    };
                    CCSumaries.Add(summaryDto);
                });
            }

            _view.TvRefreshData(CCSumaries);
        }

        private void OpenCreditCardSummary(CreditCardSummaryDto dto)
        {
            using (new CursorWait())
            {
                _view.Period = dto.Period;
                _view.Date = dto.Date;
                _view.Expiration = dto.DateExpiration;
                _view.NextDate = dto.DateNext;
                _view.NextExpiration = dto.DateNextExpiration;

                _view.TotalArs = dto.TotalArs;
                _view.TotalUsd = dto.TotalUsd;
                _view.minimumPayment = dto.MinimumPayment;
                _view.OutstandingArs = dto.OutstandingArs;

                _view.CCSummaryDetailDtos = dto.CreditCardSummaryDetails;
            }
        }

        private void GrdPaymentsRefreshData(int TransactionPayId)
        {
            var transactionService = new TransactionService(_databasePath);
            var transaction = transactionService.Get(TransactionPayId);

            if (transaction == null || transaction.TransactionDetails.Count == 0)
                return;

            List<CreditCardPayDto> dtos = new List<CreditCardPayDto>();
            foreach (var detail in transaction.TransactionDetails.OrderByDescending(x => x.Date.Day).ThenBy(x => x.Transaction.Description))
            {
                dtos.Add(new CreditCardPayDto
                {
                    Id = detail.Id,
                    Date = detail.Date,
                    EntityName = detail.Transaction.Entity.Name,
                    Description = detail.Transaction.Description,
                    AmountArs = detail.Amount,
                });
            }
            this._view.GrdPaymentsRefreshData(dtos);
        }

        private void GenerateInstallmentOnlySummaries()
        {
            var creditCard = new CreditCardService(_databasePath).Get(_view.CreditCard.Id);
            if (creditCard is null || creditCard.CCSumaries.Count() == 0)
                return;

            //Recorro todos los resumenes desde el mas antiguo hasta el mas actual
            var summaries = creditCard.CCSumaries.Where(x => x.Imported = true).OrderBy(x => x.Period).ToList();
            foreach (var summary in summaries)
            {
                var installments = summary.CCSummaryDetails.Where(x => x.Type == CreditCardSummaryDetailType.Installments).ToList();
                if (installments.Count == 0)
                    continue;

                //Obtengo el perido actual y el siguiente
                var period = summary.Period;
                var nextPeriod = period.AddMonths(1);

                //Compruebo en ciclo si el siguiente periodo fue importado
                var nextSummary = summaries.Where(x => x.Period == nextPeriod).FirstOrDefault();
                while ((nextSummary is null || nextSummary.Imported == false) && installments.Count > 0)
                {
                    //Si el resumen no fue importado (osea, generado mediante este metodo) lo elimino para renovarlo
                    if (nextSummary != null && nextSummary.Imported == false)
                        new CCSummaryService(_databasePath).Delete(nextSummary);

                    //Añado los detalles del resumen
                    List<CCSummaryDetail> details = new List<CCSummaryDetail>();
                    foreach (var detail in installments)
                    {
                        //Calculo las cuotas
                        var install = detail.Installments.Replace(" ", "");
                        var parts = install.Split('/');
                        var current = IntTools.Convert(parts[0]);
                        var total = IntTools.Convert(parts[1]);

                        //Si es la ultima cuota
                        if (current == total)
                            continue;

                        details.Add(new CCSummaryDetail
                        {
                            CCSummaryId = summary.Id,
                            Type = detail.Type,
                            Date = detail.Date,
                            Description = detail.Description,
                            Installments = $"{current + 1} / {total}",
                            AmountArs = detail.AmountArs,
                            AmountUsd = detail.AmountUsd,
                        });
                    }
                    installments = details;

                    //Creo el resumen de la tarjeta
                    var newSummary = new CCSummary
                    {
                        CreditCardId = creditCard.Id,
                        Period = nextPeriod,
                        Date = new DateTime(1, 1, 1),
                        DateExpiration = new DateTime(1, 1, 1),
                        DateNext = new DateTime(1, 1, 1),
                        DateNextExpiration = new DateTime(1, 1, 1),
                        TotalArs = installments.Sum(x => x.AmountArs),
                        TotalUsd = installments.Sum(x => x.AmountUsd),
                        MinimumPayment = 0,
                        Imported = false,
                    };

                    //Inserto el resumen y sus detalles
                    if (details.Count > 0)
                        InsertNewSummary(newSummary, details);

                    //Añado 1 mes al proximo periodo y vuelvo a consultar si ya existe un resumen para ese periodo
                    nextPeriod = nextPeriod.AddMonths(1);
                    nextSummary = summaries.Where(x => x.Period == nextPeriod).FirstOrDefault();
                    //Si nextSummary sigue siendo null, se seguira ejecutando el ciclo
                }
            }
        }

        private void InsertNewSummary(CCSummary summary, List<CCSummaryDetail> details)
        {
            //Creo la transaccion
            var descripcion = $"{_view.CreditCard.CreditCardBrand.Name} - *{_view.CreditCard.LastFourNumbers} :: Saldo pendiente";
            descripcion += summary.Imported ? 
                $" (pago minimo: {summary.MinimumPayment.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES"))})" : " (resumen incompleto)";

            var transaction = new Transaction
            {
                TransactionType = TransactionType.CreditCardOutstanding,
                EntityId = _view.CreditCard.EntityId,
                CurrencyId = 1, //ARS
                Description = descripcion,
            };
            new TransactionService(_databasePath).Insert(transaction);

            //Creo el detalle de la transaccion
            var transactionDetail = new TransactionDetail
            {
                TransactionId = transaction.Id,
                Date = summary.Period,
                EndDate = summary.Period,
                Amount = summary.TotalArs,
                Frequency = 1,
                Concider = true,
                Paid = false,
            };
            new TransactionDetailService(_databasePath).Insert(transactionDetail);

            //Inserto el resumen padre
            summary.TransactionId = transaction.Id;
            new CCSummaryService(_databasePath).Insert(summary);

            //Inserto los detalles del resumen
            foreach (var detail in details)
            {
                var ccDetail = new CCSummaryDetail
                {
                    CCSummaryId = summary.Id,
                    Type = detail.Type,
                    Date = detail.Date,
                    Description = detail.Description,
                    Installments = detail.Installments,
                    AmountArs = detail.AmountArs,
                    AmountUsd = detail.AmountUsd,
                };
                new CCSummaryDetailService(_databasePath).Insert(ccDetail);
            }
        }

        //events
        private void ButtonImportClick(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog()
            {
                Filter = $"{ConfigurationManager.AppSettings["AppTitle"]} Credit Card Summary (*.pdf)|*.pdf",
                Title = $"Abrir {ConfigurationManager.AppSettings["AppTitle"]} Credit Card Summary",
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (new CursorWait())
                {
                    try
                    {
                        string filePath = openFileDialog.FileName;
                        string bankName = _view.CreditCard.Entity.Name;
                        string brandName = _view.CreditCard.CreditCardBrand.Name;
                        var importer = new Import.Summary.Execute(bankName, brandName);
                        var summaryDto = importer.ExtractDataFromPDF(filePath);
                        OpenCreditCardSummary(summaryDto);
                        _view.SummaryImported = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR: " + ex.Message, ConfigurationManager.AppSettings["AppTitle"],
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ButtonNewPayClick(object sender, EventArgs e)
        {
            var creditCardPayPresenter = new CreditCardPayPresenter(_databasePath, _view.CCSummaryId);
            creditCardPayPresenter.Show();

            var ccSummaryService = new CCSummaryService(_databasePath).Get(_view.CCSummaryId);
            if (ccSummaryService != null)
                GrdPaymentsRefreshData(ccSummaryService.TransactionPayId);
        }

        private void ButtonInsertClick(object sender, EventArgs e)
        {
            //Inicializo los servicios
            var transactionService = new TransactionService(_databasePath);
            var transactionDetailService = new TransactionDetailService(_databasePath);
            var ccSummaryService = new CCSummaryService(_databasePath);
            var ccSummaryDetailService = new CCSummaryDetailService(_databasePath);

            //Consulto si el resumen ya existe para este periodo y tarjeta
            var summary = ccSummaryService.GetAll()
                .Where(x => x.CreditCardId == _view.CreditCard.Id && x.Period == _view.Period).FirstOrDefault();

            if (summary != null)
            {
                //Si ya existe el resumen
                string message = "Ya existe un resumen cargado para este periodo, deseas reemplazarlo?";
                string title = "Confirmar reemplazo de resumen";

                if (CommonMessageBox.warningMessageShow(message, MessageBoxButtons.YesNo, title) != DialogResult.Yes)
                    return;

                //Elimino la transaccion asociada
                var transaction = transactionService.Get(summary.TransactionId);
                if (transaction != null)
                    //Al eliminar la transaccion, se elimina el resumen en cascada
                    transactionService.Delete(transaction);
                else
                    ccSummaryService.Delete(summary);
            }

            //Creo el resumen de la tarjeta
            summary = new CCSummary
            {
                CreditCardId = _view.CreditCard.Id,
                Period = _view.Period,
                Date = _view.Date,
                DateExpiration = _view.Expiration,
                DateNext = _view.NextDate,
                DateNextExpiration = _view.NextExpiration,
                TotalArs = _view.TotalArs,
                TotalUsd = _view.TotalUsd,
                MinimumPayment = _view.minimumPayment,
                Imported = true,
            };

            //Añado los detalles del resumen
            List<CCSummaryDetail> details = new List<CCSummaryDetail>();
            foreach (var dto in _view.CCSummaryDetailDtos)
            {
                details.Add(new CCSummaryDetail
                {
                    CCSummaryId = summary.Id,
                    Type = dto.Type,
                    Date = dto.Date,
                    Description = dto.Description,
                    Installments = dto.Installments,
                    AmountArs = dto.AmountArs,
                    AmountUsd = dto.AmountUsd,
                });
            }

            //Inserto el resumen y sus detalles
            InsertNewSummary(summary, details);

            if (details.Where(x => x.Type == CreditCardSummaryDetailType.Installments).ToList().Count > 0)
                GenerateInstallmentOnlySummaries();

            TvRefreshData(_view.CreditCard);
        }

        private void ButtonDeleteClick(object sender, EventArgs e)
        {
            //Inicializo los servicios
            var ccSummaryService = new CCSummaryService(_databasePath);
            var transactionService = new TransactionService(_databasePath);

            //Consulto si el resumen ya existe para este periodo y tarjeta
            var ccSummary = ccSummaryService.GetAll()
                .Where(x => x.CreditCardId == _view.CreditCard.Id && x.Period == _view.Period).FirstOrDefault();
            if (ccSummary != null)
            {
                string message = "Estas seguro que deseas eliminar el resumen actual?";
                string title = "Confirmar eliminacion de resumen";

                if (CommonMessageBox.warningMessageShow(message, MessageBoxButtons.YesNo, title) != DialogResult.Yes)
                    return;

                //Elimino la transaccion asociada
                var transaction = transactionService.Get(ccSummary.TransactionId);
                if (transaction != null)
                    //Al eliminar la transaccion, se elimina el resumen en cascada
                    transactionService.Delete(transaction);
                else
                    ccSummaryService.Delete(ccSummary);
            }

            TvRefreshData(_view.CreditCard);
        }

        private void ButtonExitClick(object sender, EventArgs e)
        {
            if (_view.SummaryImported == true)
            {
                string message = "Esta seguro que desea salir sin guardar el resumen importado?";
                string title = "Confirmar descartar importacion";

                if (CommonMessageBox.warningMessageShow(message, MessageBoxButtons.YesNo, title) != DialogResult.Yes)
                    return;
            }

            _closeView();
        }

        private void ButtonSearchCreditCardClick(object sender, EventArgs e)
        {
            var selectedCreditCardId = new CreditCardPresenter(_databasePath).Show();
            var creditCard = new CreditCardService(_databasePath).Get(selectedCreditCardId);
            if (creditCard != null)
            {
                _view.CreditCard = creditCard;
                TvRefreshData(creditCard);
            }
        }

        private void SummaryListNodeClick(object sender, EventArgs e)
        {
            _view.SummaryImported = false;

            //Inicializo los servicios
            var ccSummaryService = new CCSummaryService(_databasePath);
            var ccSummaryDetailsService = new CCSummaryDetailService(_databasePath);

            var ccSummary = ccSummaryService.Get(_view.CCSummaryId);
            if (ccSummary != null)
            {
                var ccSummaryDto = new CreditCardSummaryDto
                {
                    Id = ccSummary.Id,
                    Period = ccSummary.Period,
                    Date = ccSummary.Date,
                    DateExpiration = ccSummary.DateExpiration,
                    DateNext = ccSummary.DateNext,
                    DateNextExpiration = ccSummary.DateNextExpiration,
                    TotalArs = ccSummary.TotalArs,
                    TotalUsd = ccSummary.TotalUsd,
                    MinimumPayment = ccSummary.MinimumPayment,
                    OutstandingArs = ccSummary.Transaction.TransactionDetails.FirstOrDefault().Amount,
                };

                ccSummaryDto.CreditCardSummaryDetails = new List<CreditCardSummaryDetailDto>();
                var ccSummaryDetails = ccSummaryDetailsService.GetAll().Where(x => x.CCSummaryId == ccSummary.Id).ToList();
                foreach (var ccSummaryDetail in ccSummaryDetails)
                {
                    ccSummaryDto.AddDetailDto(new CreditCardSummaryDetailDto
                    {
                        Date = ccSummaryDetail.Date,
                        Type = ccSummaryDetail.Type,
                        Description = ccSummaryDetail.Description,
                        Installments = ccSummaryDetail.Installments,
                        AmountArs = ccSummaryDetail.AmountArs,
                        AmountUsd = ccSummaryDetail.AmountUsd,
                    });
                }

                OpenCreditCardSummary(ccSummaryDto);
                GrdPaymentsRefreshData(ccSummary.TransactionPayId);
            }
        }
    }
}
