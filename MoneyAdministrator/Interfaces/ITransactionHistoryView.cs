using MoneyAdministrator.DTOs;
using MoneyAdministrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Interfaces
{
    public interface ITransactionHistoryView
    {
        //Properties
        DateTime Date { get; set; }
        string EntityName { get; set; }
        string Description { get; set; }
        decimal Value { get; set; }
        int CurrencyId { get; set; }
        int Installments { get; set; }

        //Events
        event EventHandler SelectedYearChange;
        event EventHandler ButtonEntitySearchClick;

        //Methods
        void SetCurrenciesList(List<Currency> currencies);
        void GrdRefreshData(List<TransactionViewDto> transactions);
    }
}
