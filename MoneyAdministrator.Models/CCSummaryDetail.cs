using MoneyAdministrator.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Models
{
    public class CCSummaryDetail
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Se requiere un resumen de tarjeta de credito")]
        public int CCSummaryId { get; set; }

        [Required(ErrorMessage = "Se requiere el tipo de detalle")]
        public CreditCardSummaryDetailDtoType Type { get; set; }

        [Required(ErrorMessage = "Se requiere la fecha del detalle")]
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string Installments { get; set; }

        public decimal AmountArs { get; set; }

        public decimal AmountUsd { get; set; }

        //foreign keys
        public virtual CCSummary CCSummary { get; set; }
    }
}
