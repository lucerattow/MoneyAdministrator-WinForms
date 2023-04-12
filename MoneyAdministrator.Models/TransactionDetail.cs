using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Models
{
    public class TransactionDetail
    {
        //Columnas
        public int Id { get; set; }

        [Required(ErrorMessage = "Falta especificar la transacción")]
        public int TransactionId { get; set; }

        [Required(ErrorMessage = "Falta ingresar la fecha de la transacción")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Falta ingresar el monto de la transacción")]
        public decimal Amount { get; set; }

        public int Installment { get; set; }

        public int Frequency { get; set; }

        //foreign keys
        public virtual Transaction Transaction { get; set; }
    }
}
