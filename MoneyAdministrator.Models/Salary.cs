using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Models
{
    public class Salary
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Falta ingresar la fecha")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Falta especificar la moneda")]
        public int CurrencyId { get; set; }

        public decimal Value { get; set; }

        //foraign keys
        public virtual Currency Currency { get; set; }
    }
}
