using MoneyAdministrator.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services;
using MoneyAdministrator.Views.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Presenters
{
    public class TransactionHistoryPresenter
    {
        private ITransactionHistoryView _transactionHistoryView;
        private string _databasePath;

        public TransactionHistoryPresenter(ITransactionHistoryView transactionHistoryView, string databasePath)
        {
            this._databasePath = databasePath;
            this._transactionHistoryView = transactionHistoryView;
            this._transactionHistoryView.SelectedYearChange += SelectedYearChange;
            this._transactionHistoryView.EntitySearch += EntitySearch;

            SetCurrenciesList();
            GrdRefreshData();
        }

        #region events
        private void SelectedYearChange(object? sender, EventArgs e)
        {
            GrdRefreshData();
        }
        private void EntitySearch(object? sender, EventArgs e)
        {
            var entities = new EntityService(_databasePath).GetAll();
            var selectedId = new ResultPickerPresenter(_databasePath).Show(entities);

            if (selectedId >= 0)
                _transactionHistoryView.EntityName = entities.Where(x => x.Id == selectedId).FirstOrDefault().Name;
        }
        #endregion

        #region methods
        private void SetCurrenciesList()
        {
            var currencyService = new CurrencyService(_databasePath);
            var currencies = currencyService.GetAll();
            this._transactionHistoryView.SetCurrenciesList(currencies);
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
            this._transactionHistoryView.GrdRefreshData(dtos);
        }
        #endregion
    }
}
