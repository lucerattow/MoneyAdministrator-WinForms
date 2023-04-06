using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Models
{
    public class CCResume
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Credit card is required")]
        public int CreditCardId { get; set; }

        [Required(ErrorMessage = "Period is required")]
        public DateTime Period { get; set; }

        //foreign keys
        public virtual CreditCard CreditCard { get; set; }

        //foreign keys all constraints
        public virtual IEnumerable<CCResumeDetail> CCResumeDetails { get; set; }
    }
}
