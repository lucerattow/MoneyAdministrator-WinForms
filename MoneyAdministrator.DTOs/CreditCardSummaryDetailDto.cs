using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.DTOs
{
    public class CreditCardSummaryDetailDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ItemName { get; set; }
        public string NroCupon { get; set; }
        public decimal AmountArs { get; set; }
        public decimal AmountUsd { get; set; }
    }
}
