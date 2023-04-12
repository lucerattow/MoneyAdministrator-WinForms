using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Models
{
    public class Currency
    {
        //Properties
        public int Id { get; set; }

        [StringLength(3, MinimumLength = 3, ErrorMessage = "El nombre de la moneda debe tener 3 caracteres")]
        [Required(ErrorMessage = "Falta ingresar el nombre de la moneda")]
        public string Name { get; set; }

        //foreign keys all constraints
        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual ICollection<CurrencyValue> CurrencyValues { get; set; }

        public virtual ICollection<Salary> Salaries { get; set; }
    }
}
