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

        [Required(ErrorMessage = "Se requiere ingresar la tarjeta de credito")]
        public int CreditCardId { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar el periodo")]
        public DateTime Period { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar la fecha")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar la fecha de vencimiento")]
        public DateTime Expiration { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar la fecha de proximo cierre")]
        public DateTime NextDate { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar la fecha de priximo vencimiento")]
        public DateTime NextExpiration { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar el pago minimo")]
        public decimal MinimumPayment { get; set; }

        //foreign keys
        public virtual CreditCard CreditCard { get; set; }

        //foreign keys all constraints
        public virtual IEnumerable<CCSummaryDetail> CCSummaryDetails { get; set; }
    }
}
