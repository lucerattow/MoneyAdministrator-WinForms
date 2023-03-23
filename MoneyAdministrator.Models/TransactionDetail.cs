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
        //Fields
        private int _id;
        private int _transactionId;
        private DateTime _date;
        private decimal _value;
        private int _installment;

        private Transaction _transaction;

        //Columnas
        [DisplayName("Transaction detail ID")]
        [Required(ErrorMessage = "Transaction detail ID is required")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DisplayName("Transaction ID")]
        [Required(ErrorMessage = "Transaction ID is required")]
        public int TransactionId
        {
            get { return _transactionId; }
            set { _transactionId = value; }
        }

        [DisplayName("Transaction date")]
        [Required(ErrorMessage = "Transaction date is required")]
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        [DisplayName("Value")]
        [Required(ErrorMessage = "Transaction value is required")]
        public decimal Value
        {
            get { return _value; }
            set { _value = value; }
        }

        [DisplayName("Installment")]
        public int Installment
        {
            get { return _installment; }
            set { _installment = value; }
        }

        public Transaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }
    }
}
