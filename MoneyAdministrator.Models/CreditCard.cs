using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Models
{
    public class CreditCard
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Falta especificar el banco emisor de la tarjeta de crédito")]
        public int EntityId { get; set; }

        [Required(ErrorMessage = "Falta especificar la marca de la tarjeta de crédito")]
        public int CreditCardBrandId { get; set; }

        [Required(ErrorMessage = "Falta ingresar los últimos 4 números de la tarjeta de crédito")]
        public int LastFourNumbers { get; set; }

        [DefaultValue(false)]
        public bool Deleted { get; set; }

        //foreign keys
        public virtual Entity Entity { get; set; }

        public virtual CreditCardBrand CreditCardBrand { get; set; }

        //foreign keys all constraints
        public virtual IEnumerable<CCSummary> CCSumaries { get; set; }
    }
}
