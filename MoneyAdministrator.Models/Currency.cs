using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Models
{
    public class Currency
    {
        //Fields
        private int _id;
        private string _name;

        private ICollection<Transaction> _transactions;

        //Properties
        [DisplayName("Currency ID")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DisplayName("Name")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency name need 3 cacarteres")]
        [Required(ErrorMessage = "Currency name is required")]
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
