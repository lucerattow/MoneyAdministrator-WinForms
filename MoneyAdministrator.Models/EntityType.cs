using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Models
{
    public class EntityType
    {

        public int Id { get; set; }

        [StringLength(25, MinimumLength = 3, ErrorMessage = "Entity name must be between 3 and 25 characters")]
        [Required(ErrorMessage = "Name for the entity type is required")]
        public string Name { get; set; }

        [DefaultValue(false)]
        public bool Deleted { get; set; }

        //foreign keys all constraints
        public virtual IEnumerable<Entity> Entities { get; set; }
    }
}
