using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Models
{
    public class CreditCard
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Emisor bank is required")]
        public int EntityId { get; set; }

        [Required(ErrorMessage = "Emisor enterprise is required")]
        public int CreditCardTypeId { get; set; }

        [Required(ErrorMessage = "Last four numbers is required")]
        public int LastFourNumbers { get; set; }

        //foreign keys
        public virtual Entity Entity { get; set; }

        public virtual CreditCardType CreditCardType { get; set; }

        //foreign keys all constraints
        public virtual IEnumerable<CCResume> CCResume { get; set; }
    }
}
