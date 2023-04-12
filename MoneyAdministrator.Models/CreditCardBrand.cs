using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Models
{
    public class CreditCardBrand
    {
        public int Id { get; set; }

        [StringLength(25, MinimumLength = 3, ErrorMessage = "La marca de la tarjeta de crédito debe tener entre 3 y 25 caracteres")]
        [Required(ErrorMessage = "Falta ingresar la marca de la tarjeta de crédito")]
        public string Name { get; set; }

        //foreign keys all constraints
        public virtual IEnumerable<CreditCard> CreditCards { get; set; }
    }
}
