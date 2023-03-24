using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Interfaces
{
    public interface ITransactionHistoryView
    {
        string EntityName { get; set; }
        DateTime Date { get; set; }
        string Description { get; set; }
        int CurrencyId { get; set; }
        decimal Value { get; set; }
        int Installments { get; set; }
    }
}
