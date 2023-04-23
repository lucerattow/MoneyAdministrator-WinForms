using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.DTOs.Enums;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services;
using MoneyAdministrator.Utilities;
using MoneyAdministrator.Utilities.Disposable;
using MoneyAdministrator.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

            if (transaction == null && transaction.TransactionDetails.Count == 0)
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
            var ccSummary = ccSummaryService.GetAll()
                .Where(x => x.CreditCardId == _view.CreditCard.Id && x.Period == _view.Period).FirstOrDefault();

            if (ccSummary != null)
            {
                //Si ya existe el resumen
                string message = "Ya existe un resumen cargado para este periodo, deseas reemplazarlo?";
                string title = "Confirmar reemplazo de resumen";

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

            //Creo la transaccion
            var descripcion = $"Saldo pendiente {_view.CreditCard.CreditCardBrand.Name} -" +
                $" ●●●● ●●●● ●●●● {_view.CreditCard.LastFourNumbers} - Vencimiento: {_view.Expiration:yyyy-MM-dd}";
            var newTransaction = new Transaction
            { 
                EntityId = _view.CreditCard.EntityId,
                CurrencyId = 1, //ARS
                Description = descripcion,
            };
            transactionService.Insert(newTransaction);

            //Creo el detalle de la transaccion
            var transactionDetail = new TransactionDetail
            {
                TransactionId = newTransaction.Id,
                Date = _view.Period,
                Amount = _view.TotalArs,
                Installment = 0,
                Frequency = 1,
            };
            transactionDetailService.Insert(transactionDetail);

            //Creo el resumen de la tarjeta
            ccSummary = new CCSummary
            {
                CreditCardId = _view.CreditCard.Id,
                TransactionId = newTransaction.Id,
                Period = _view.Period,
                Date = _view.Date,
                DateExpiration = _view.Expiration,
                DateNext = _view.NextDate,
                DateNextExpiration = _view.NextExpiration,
                TotalArs = _view.TotalArs,
                TotalUsd = _view.TotalUsd,
                MinimumPayment = _view.minimumPayment,
            };
            ccSummaryService.Insert(ccSummary);

            //Añado los detalles del resumen
            foreach (var CCSummaryDetailDto in _view.CCSummaryDetailDtos)
            {
                var ccSummaryDetail = new CCSummaryDetail
                {
                    CCSummaryId = ccSummary.Id,
                    Type = CCSummaryDetailDto.Type,
                    Date = CCSummaryDetailDto.Date,
                    Description = CCSummaryDetailDto.Description,
                    Installments = CCSummaryDetailDto.Installments,
                    AmountArs = CCSummaryDetailDto.AmountArs,
                    AmountUsd = CCSummaryDetailDto.AmountUsd,
                };
                ccSummaryDetailService.Insert(ccSummaryDetail);
            }

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
