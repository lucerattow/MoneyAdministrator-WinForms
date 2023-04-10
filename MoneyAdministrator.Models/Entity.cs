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
        public int Id { get; set; }

        [Required(ErrorMessage = "Falta indicar el tipo de entidad")]
        public int EntityTypeId { get; set; }

        [StringLength(25, MinimumLength = 3, ErrorMessage = "El nombre de la entidad debe tener entre 3 y 25 caracteres")]
        [Required(ErrorMessage = "Falta indicar el nombre de la entidad")]
        public string Name { get; set; }

        [DefaultValue(false)]
        public bool Deleted { get; set; }

        //foreign keys
        public virtual EntityType EntityType { get; set; }

        //foreign keys all constraints
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
