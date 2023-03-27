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
        //Properties
        [DisplayName("Entity ID")]
        [Display(AutoGenerateField = false)]
        public int Id { get; set; }

        [DisplayName("Name")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Entity name must be between 3 and 25 characters")]
        [Required(ErrorMessage = "Entity name is required")]
        public string Name { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
