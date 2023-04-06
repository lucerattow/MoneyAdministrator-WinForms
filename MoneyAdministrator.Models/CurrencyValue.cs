using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Models
{
    public class CurrencyValue
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Currency is required")]
        public int CurrencyId { get; set; }

        public decimal Value { get; set; }

        //foraign keys
        public virtual Currency Currency { get; set; }
    }
}
