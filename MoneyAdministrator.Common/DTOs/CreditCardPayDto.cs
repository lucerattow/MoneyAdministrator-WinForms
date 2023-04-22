using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Common.DTOs
{
    public class CreditCardPayDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string EntityName { get; set; }
        public string Description { get; set; }
        public decimal AmountArs { get; set; }
    }
}
