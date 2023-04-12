using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Models
{
    public class CCSummary
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Falta especificar la tarjeta de crédito del resumen")]
        public int CreditCardId { get; set; }

        [Required(ErrorMessage = "Falta ingresar el periodo del resumen")]
        public DateTime Period { get; set; }

        [Required(ErrorMessage = "Falta ingresar la fecha de emisión del resumen")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Falta ingresar la fecha de vencimiento del resumen")]
        public DateTime Expiration { get; set; }

        [Required(ErrorMessage = "Falta ingresar la fecha del próximo resumen")]
        public DateTime NextDate { get; set; }

        [Required(ErrorMessage = "Falta ingresar la fecha del próximo vencimiento")]
        public DateTime NextExpiration { get; set; }

        [Required(ErrorMessage = "Falta ingresar el pago mínimo")]
        public decimal MinimumPayment { get; set; }

        //foreign keys
        public virtual CreditCard CreditCard { get; set; }

        //foreign keys all constraints
        public virtual IEnumerable<CCSummaryDetail> CCSummaryDetails { get; set; }
    }
}
