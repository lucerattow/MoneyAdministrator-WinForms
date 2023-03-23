using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Models
{
    public class Transaction
    {
        //Fields
        private int _id;
        private int _entityId;
        private int _currencyId;
        private string _description;

        private Entity _entity;
        private Currency _currency;
        private ICollection<TransactionDetail> _transactionDetail;

        //Properties
        [DisplayName("Transaction ID")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DisplayName("Entity")]
        [Required(ErrorMessage = "Entity ID is required")]
        public int EntityId
        {
            get { return _entityId; }
            set { _entityId = value; }
        }

        [DisplayName("Currency")]
        [Required(ErrorMessage = "Currency ID is required")]
        public int CurrencyId
        {
            get { return _currencyId; }
            set { _currencyId = value; }
        }

        [DisplayName("Description")]
        [StringLength(150, ErrorMessage = "Transaction description cannot exceed 150 characters.")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Entity Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }

        public Currency Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        public ICollection<TransactionDetail> TransactionDetails
        {
            get { return _transactionDetail; }
            set { _transactionDetail = value; }
        }
    }
}
