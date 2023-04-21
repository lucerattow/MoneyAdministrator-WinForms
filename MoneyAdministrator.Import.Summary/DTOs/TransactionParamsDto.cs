using MoneyAdministrator.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Import.Summary.DTOs
{
    internal class TransactionParamsDto
    {
        public CreditCardSummaryDetailType Type { get; set; }
        public string Date { get; set; }
        public string DateFormat { get; set; }
        public string Description { get; set; }
        public string Installments { get; set; }
        public string AmountArs { get; set; }
        public string AmountUsd { get; set; }
    }
}
