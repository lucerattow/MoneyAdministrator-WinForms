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
        //Properties
        [DisplayName("Transaction ID")]
        public int Id { get; set; }

        [DisplayName("Entity")]
        [Required(ErrorMessage = "Entity ID is required")]
        public int EntityId { get; set; }

        [DisplayName("Currency")]
        [Required(ErrorMessage = "Currency ID is required")]
        public int CurrencyId { get; set; }

        [DisplayName("Description")]
        [StringLength(150, ErrorMessage = "Transaction description cannot exceed 150 characters.")]
        public string Description { get; set; }

        //ForaignKeys
        public virtual Entity Entity { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
