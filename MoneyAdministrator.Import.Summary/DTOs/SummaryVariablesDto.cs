using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Import.Summary.DTOs
{
    internal class SummaryVariablesDto
    {
        public string DateFormat { get; set; }
        public string Period { get; set; }
        public string Date { get; set; }
        public string DateExpiration { get; set; }
        public string DateNext { get; set; }
        public string DateNextExpiration { get; set; }
        public string TotalArs { get; set; }
        public string TotalUsd { get; set; }
        public string MinimumPayment { get; set; }
        public string OutstandingArs { get; set; }
    }
}
