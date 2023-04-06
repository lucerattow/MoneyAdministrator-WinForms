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
        [DisplayName("Transaction detail ID")]
        [Required(ErrorMessage = "Transaction detail ID is required")]
        public int Id { get; set; }

        [DisplayName("Transaction ID")]
        [Required(ErrorMessage = "Transaction ID is required")]
        public int TransactionId { get; set; }

        [DisplayName("Transaction date")]
        [Required(ErrorMessage = "Transaction date is required")]
        public DateTime Date { get; set; }

        [DisplayName("Amount")]
        [Required(ErrorMessage = "Transaction amount is required")]
        public decimal Amount { get; set; }

        [DisplayName("Installment")]
        public int Installment { get; set; }

        [DisplayName("Frequency")]
        public int Frequency { get; set; }

        //foreign keys
        public virtual Transaction Transaction { get; set; }
    }
}
