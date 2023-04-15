using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.ImportPdfSummary.Dtos
{
    public class TransactionsTableDto
    {
        public List<string> AllText { get; set; }
        public List<string> Installments { get; set; }
        public List<string> Date { get; set; }
        public List<string> Ars { get; set; }
        public List<string> Usd { get; set; }

        public TransactionsTableDto() 
        { 
            AllText = new List<string>();
            Installments = new List<string>();
            Date = new List<string>();
            Ars = new List<string>();
            Usd = new List<string>();
        }
    }
}
