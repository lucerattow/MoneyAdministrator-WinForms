using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [Required(ErrorMessage = "Se requiere generar una transaccion para cargar el nuevo resumen")]
        public int TransactionId { get; set; }

        [DefaultValue(0)]
        public int TransactionPayId { get; set; }

        [Required(ErrorMessage = "Falta ingresar el periodo del resumen")]
        public DateTime Period { get; set; }

        [Required(ErrorMessage = "Falta ingresar la fecha de emisión del resumen")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Falta ingresar la fecha de vencimiento del resumen")]
        public DateTime DateExpiration { get; set; }

        [Required(ErrorMessage = "Falta ingresar la fecha del próximo resumen")]
        public DateTime DateNext { get; set; }

        [Required(ErrorMessage = "Falta ingresar la fecha del próximo vencimiento")]
        public DateTime DateNextExpiration { get; set; }

        [Required(ErrorMessage = "Falta ingresar el total en ARS")]
        public decimal TotalArs { get; set; }

        [Required(ErrorMessage = "Falta ingresar el total en USD")]
        public decimal TotalUsd { get; set; }

        [Required(ErrorMessage = "Falta ingresar el pago mínimo")]
        public decimal MinimumPayment { get; set; }

        //foreign keys
        public virtual Transaction Transaction { get; set; }

        public virtual CreditCard CreditCard { get; set; }

        //foreign keys all constraints
        public virtual IEnumerable<CCSummaryDetail> CCSummaryDetails { get; set; }
    }
}
