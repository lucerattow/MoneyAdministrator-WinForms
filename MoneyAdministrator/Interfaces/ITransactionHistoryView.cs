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
        //properties
        int SelectedId { get; set; }
        DateTime Date { get; set; }
        string EntityName { get; set; }
        string Description { get; set; }
        decimal Amount { get; set; }
        Currency Currency { get; set; }

        int InstallmentCurrent { get; set; }
        int InstallmentMax { get; set; }
        int Frequency { get; set; }

        bool IsService { get; set; }
        bool Editing { get; set; }

        //methods
        void SetCurrenciesList(List<Currency> currencies);
        void GrdRefreshData(List<TransactionViewDto> transactions);
        void ButtonsLogic();

        //events
        event EventHandler GrdDoubleClick;
        event EventHandler ButtonInsertClick;
        event EventHandler ButtonUpdateClick;
        event EventHandler ButtonDeleteClick;
        event EventHandler ButtonExitClick;
        event EventHandler SelectedYearChange;
        event EventHandler ButtonEntitySearchClick;
    }
}
