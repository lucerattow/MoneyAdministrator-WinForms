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

        [StringLength(25, MinimumLength = 3, ErrorMessage = "El tipo de entidad debe tener entre 3 y 25 caracteres")]
        [Required(ErrorMessage = "Falta indicar el nombre del tipo de entidad")]
        public string Name { get; set; }

        [DefaultValue(false)]
        public bool Deleted { get; set; }

        //foreign keys all constraints
        public virtual IEnumerable<Entity> Entities { get; set; }
    }
}
