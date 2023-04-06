using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Models
{
    public class CreditCardType
    {
        public int Id { get; set; }

        [StringLength(25, MinimumLength = 3, ErrorMessage = "Credit card type must be between 3 and 25 characters")]
        [Required(ErrorMessage = "Credit card Type name is required")]
        public string Name { get; set; }

        //foreign keys all constraints
        public virtual IEnumerable<CreditCard> CreditCards { get; set; }
    }
}
