using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services;
using MoneyAdministrator.Utilities;
using MoneyAdministrator.Utilities.Disposable;
using MoneyAdministrator.Views;
using MoneyAdministrator.ImportPdfSummary;
using System;
using System.Collections.Generic;
using System.Configuration;
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
                _view.Expiration = dto.Expiration;
                _view.NextDate = dto.NextDate;
                _view.NextExpiration = dto.NextExpiration;

                _view.TotalArs = dto.CreditCardSummaryDetails.Select(x => x.AmountArs).Sum();
                _view.TotalUsd = dto.CreditCardSummaryDetails.Select(x => x.AmountUsd).Sum();
                _view.minimumPayment = dto.MinimumPayment;

                _view.CCSummaryDetailDtos = dto.CreditCardSummaryDetails;
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
                        string bankName = _view.CreditCard.CreditCardBank.Name;
                        string brandName = _view.CreditCard.CreditCardBrand.Name;
                        var creditCardSummaryDto = new Import(filePath, bankName, brandName);
                        OpenCreditCardSummary(creditCardSummaryDto.ExtractTextFromPdf());
                        _view.ImportedSummary = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR: " + ex.Message, ConfigurationManager.AppSettings["AppTitle"],
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ButtonInsertClick(object sender, EventArgs e)
        {
            //Inicializo los servicios
            var ccSummaryService = new CCSummaryService(_databasePath);
            var ccSummaryDetailService = new CCSummaryDetailService(_databasePath);

            //Consulto si el resumen ya existe para este periodo y tarjeta
            var ccSummary = ccSummaryService.GetAll()
                .Where(x => x.CreditCardId == _view.CreditCard.Id && x.Period == _view.Period).FirstOrDefault();
            if (ccSummary != null)
            {
                string message = "Ya existe un resumen cargado para este periodo, deseas reemplazarlo?";
                string title = "Confirmar reemplazo de resumen";

                if (CommonMessageBox.warningMessageShow(message, MessageBoxButtons.YesNo, title) != DialogResult.Yes)
                    return;

                //Los detalles se eliminan en cascada
                ccSummaryService.Delete(ccSummary);
            }

            ccSummary = new CCSummary
            {
                CreditCardId = _view.CreditCard.Id,
                Period = _view.Period,
                Date = _view.Date,
                Expiration = _view.Expiration,
                NextDate = _view.NextDate,
                NextExpiration = _view.NextExpiration,
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

            //Consulto si el resumen ya existe para este periodo y tarjeta
            var ccSummary = ccSummaryService.GetAll()
                .Where(x => x.CreditCardId == _view.CreditCard.Id && x.Period == _view.Period).FirstOrDefault();
            if (ccSummary != null)
            {
                string message = "Estas seguro que deseas eliminar el resumen actual?";
                string title = "Confirmar eliminacion de resumen";

                if (CommonMessageBox.warningMessageShow(message, MessageBoxButtons.YesNo, title) != DialogResult.Yes)
                    return;

                //Los detalles se eliminan en cascada
                ccSummaryService.Delete(ccSummary);
            }

            TvRefreshData(_view.CreditCard);
        }

        private void ButtonExitClick(object sender, EventArgs e)
        {
            if (_view.ImportedSummary == true)
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
            _view.ImportedSummary = false;

            //Inicializo los servicios
            var ccSummaryService = new CCSummaryService(_databasePath);
            var ccSummaryDetailsService = new CCSummaryDetailService(_databasePath);

            var ccSummary = ccSummaryService.Get(_view.SelectedSummaryId);
            if (ccSummary != null)
            {
                var ccSummaryDto = new CreditCardSummaryDto
                {
                    Id = ccSummary.Id,
                    Period = ccSummary.Period,
                    Date = ccSummary.Date,
                    Expiration = ccSummary.Expiration,
                    NextDate = ccSummary.NextDate,
                    NextExpiration = ccSummary.NextExpiration,
                    MinimumPayment = ccSummary.MinimumPayment,
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
            }
        }
    }
}
