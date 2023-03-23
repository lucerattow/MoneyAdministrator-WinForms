using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Models
{
    public class Entity
    {
        //Fields
        private int _id;
        private string _name;

        private ICollection<Transaction> _transactions;

        //Properties
        [DisplayName("Entity ID")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DisplayName("Name")]
        [StringLength(25, ErrorMessage = "Entity name must be between 3 and 25 characters")]
        [Required(ErrorMessage = "Entity name is required")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public ICollection<Transaction> Transactions
        {
            get { return _transactions; }
            set { _transactions = value; }
        }
    }
}
