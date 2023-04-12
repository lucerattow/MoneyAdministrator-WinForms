using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Models
{
    public class CreditCardBank
    {
        public int Id { get; set; }

        [StringLength(25, MinimumLength = 3, ErrorMessage = "El banco emisor debe tener entre 3 y 25 caracteres")]
        [Required(ErrorMessage = "Falta ingresar el nombre del banco emisor")]
        public string Name { get; set; }

        [DefaultValue(false)]
        public bool ImportSupport { get; set; }

        //foreign keys all constraints
        public virtual IEnumerable<CreditCard> CreditCards { get; set; }
    }
}
