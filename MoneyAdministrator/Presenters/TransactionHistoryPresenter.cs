using MoneyAdministrator.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services;
using MoneyAdministrator.Utilities;
using MoneyAdministrator.Views;
using System.Configuration;

namespace MoneyAdministrator.Presenters
{
    public class TransactionHistoryPresenter
    {
        //fields
        private ITransactionHistoryView _view;
        private string _databasePath;
        private Action _closeForm;

        //properties
        public ITransactionHistoryView View 
        { 
            get { return _view; } 
        }

        public TransactionHistoryPresenter(string databasePath, Action closeView)
        {
            _databasePath = databasePath;
            _closeForm = closeView;
            _view = new TransactionHistoryView();

            AssosiateEvents();
            SetCurrenciesList();
            GrdRefreshData();
        }

        //methods
        private void AssosiateEvents()
        {
            _view.GrdDoubleClick += GrdDoubleClick;
            _view.ButtonInsertClick += ButtonInsertClick;
            _view.ButtonUpdateClick += ButtonUpdateClick;
            _view.ButtonDeleteClick += ButtonDeleteClick;
            _view.ButtonClearClick += ButtonClearClick;
            _view.ButtonExitClick += ButtonExitClick;
            _view.SelectedYearChange += SelectedYearChange;
            _view.ButtonEntitySearchClick += ButtonEntitySearchClick;
        }

        private void SetCurrenciesList()
        {
            var currencyService = new CurrencyService(_databasePath);
            var currencies = currencyService.GetAll();
            this._view.SetCurrenciesList(currencies);
        }

        private void GrdRefreshData()
        { 
            var transactionDetailService = new TransactionDetailService(_databasePath);
            var transactionDetails = transactionDetailService.GetAll();

            List<TransactionViewDto> dtos = new List<TransactionViewDto>();
            foreach (var transactionDetail in transactionDetails)
            {
                //Obtengo el valor maximo de la propiedad installment para esta transaccion
                int maxInstallment = transactionDetails
                    .Where(x => x.TransactionId == transactionDetail.TransactionId)
                    .Max(x => x.Installment);
                //Genero el string "1 / 6" que indica el numero de cuotas
                string installments = transactionDetail.Installment != 0 ? $"{transactionDetail.Installment} / {maxInstallment}" : "";

                dtos.Add(new TransactionViewDto() 
                { 
                    Id = transactionDetail.Id,
                    Date = transactionDetail.Date,
                    EntityName = transactionDetail.Transaction.Entity.Name,
                    Description = transactionDetail.Transaction.Description,
                    Installment = installments,
                    CurrencyName = transactionDetail.Transaction.Currency.Name,
                    Amount = transactionDetail.Amount
                });
            }
            this._view.GrdRefreshData(dtos);
        }

        private void SelectTransaction()
        {
            var entity = new TransactionDetailService(_databasePath).Get(_view.SelectedId);

            _view.EntityName = entity.Transaction.Entity.Name;
            _view.Date = entity.Date;
            _view.Description = entity.Transaction.Description;
            _view.Amount = entity.Amount;
            _view.CurrencyId = entity.Transaction.CurrencyId;
            _view.InstallmentCurrent = entity.Installment;
            _view.InstallmentMax = entity.Transaction.TransactionDetails.Max(t => t.Installment);
        }

        //events
        private void GrdDoubleClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            _view.SelectedId = (int)(sender as DataGridView).Rows[e.RowIndex].Cells[0].Value;
            _view.ButtonsLogic();
            SelectTransaction();
        }

        private void ButtonInsertClick(object? sender, EventArgs e) 
        {
            try
            {
                var entity = new Entity() { Name = _view.EntityName };
                var date = _view.Date;
                var description = _view.Description;
                var amount = _view.Amount;
                var currencyId = _view.CurrencyId;
                var installments = _view.InstallmentMax;

                //Compruebo si la entidad ya existe, si no existe la inserto
                var entityService = new EntityService(_databasePath);
                var searchEntity = entityService.GetByName(entity.Name);
                if (searchEntity == null)
                    entityService.Insert(entity);
                else
                    entity.Id = searchEntity.Id;

                //Inserto la transaccion
                var transactionService = new TransactionService(_databasePath);
                var transaction = new Transaction()
                {
                    EntityId = entity.Id,
                    CurrencyId = currencyId,
                    Description = description,
                };
                transactionService.Insert(transaction);

                //Inserto los detalles de la transaccion
                var transactionDetailService = new TransactionDetailService(_databasePath);

                for (int installment = 1; installment <= installments; installment++)
                {
                    var transactionDetail = new TransactionDetail()
                    {
                        TransactionId = transaction.Id,
                        Date = date,
                        Amount = amount,
                        Installment = installment
                    };
                    date = date.AddMonths(1);
                    transactionDetailService.Insert(transactionDetail);
                }
            }
            catch (Exception ex)
            {
                CommonMessageBox.errorMessageShow(ex.Message, MessageBoxButtons.OK);
            }
            ButtonClearClick();
            GrdRefreshData();
        }

        private void ButtonUpdateClick(object? sender, EventArgs e) 
        {
            ButtonClearClick();
            GrdRefreshData();
        }

        private void ButtonDeleteClick(object? sender, EventArgs e)
        {
            try
            {
                var transactionService = new TransactionService(_databasePath);
                var transactionDetailService = new TransactionDetailService(_databasePath);

                //Compruebo que el transactionDetail existe
                var transactionDetail = transactionDetailService.Get(_view.SelectedId);
                if (transactionDetail == null)
                {
                    MessageBox.Show("Ocurrio un error al intentar borrar la transaccion seleccionada",
                        ConfigurationManager.AppSettings["AppTitle"] + " : Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (var tDetail in transactionDetail.Transaction.TransactionDetails)
                {
                    transactionDetailService.Delete(tDetail);
                }
                transactionService.Delete(transactionDetail.Transaction);
            }
            catch (Exception ex)
            {
                CommonMessageBox.errorMessageShow(ex.Message, MessageBoxButtons.OK);
            }
            ButtonClearClick();
            GrdRefreshData();
        }

        private void ButtonClearClick()
        {
            ButtonClearClick(null, EventArgs.Empty);
        }
        private void ButtonClearClick(object? sender, EventArgs e) 
        {
            _view.SelectedId = 0;
            _view.EntityName = "";
            _view.Description = "";
            _view.Date = DateTime.Now;
            _view.Amount = 0;
            _view.CurrencyId = 1; //ARS ID = 1
            _view.InstallmentCurrent = 0;
            _view.InstallmentMax = 0;
            _view.ButtonsLogic();
        }

        private void ButtonExitClick(object? sender, EventArgs e)
        {
            _closeForm();
        }

        private void SelectedYearChange(object? sender, EventArgs e)
        {
            GrdRefreshData();
        }

        private void ButtonEntitySearchClick(object? sender, EventArgs e)
        {
            var entities = new EntityService(_databasePath).GetAll();
            var resultPickerViewDtos = entities.Select(entity => new ResultPickerViewDto
            {
                Id = entity.Id,
                Field1 = entity.Name
            }).ToList();

            var selectedId = new ResultPickerPresenter(resultPickerViewDtos).Show();

            if (selectedId > 0)
                _view.EntityName = entities.Where(x => x.Id == selectedId).FirstOrDefault().Name;
        }
    }
}
