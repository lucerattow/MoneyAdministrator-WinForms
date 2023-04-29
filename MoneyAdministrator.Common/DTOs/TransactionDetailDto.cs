using MoneyAdministrator.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Common.DTOs
{
    public class TransactionDetailDto
    {
        //Transaction
        public int EntityId { get; set; }
        public int CurrencyId { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Description { get; set; }
        //details
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
        public int Frequency { get; set; }
    }
}
