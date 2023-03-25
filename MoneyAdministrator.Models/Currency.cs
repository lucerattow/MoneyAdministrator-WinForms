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
        //Properties
        [DisplayName("Currency ID")]
        public int Id { get; set; }

        [DisplayName("Name")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency name need 3 cacarteres")]
        [Required(ErrorMessage = "Currency name is required")]
        public string Name { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
