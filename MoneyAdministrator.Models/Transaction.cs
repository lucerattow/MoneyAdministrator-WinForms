using MoneyAdministrator.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Models
{
    public class Transaction
    {
        //Properties
        public int Id { get; set; }

        [Required(ErrorMessage = "Falta especificar el tipo de entidad")]
        public int EntityId { get; set; }

        [Required(ErrorMessage = "Falta especificar la moneda")]
        public int CurrencyId { get; set; }

        [Required(ErrorMessage = "Falta especificar el tipo de transaccion")]
        public TransactionType TransactionType { get; set; }

        [StringLength(150, ErrorMessage = "La descripción no puede superar los 150 caracteres")]
        public string Description { get; set; }

        //foraign keys
        public virtual Entity Entity { get; set; }

        public virtual Currency Currency { get; set; }

        //foreign keys all constraints
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }

        public virtual ICollection<CCSummary> CCSummaries { get; set; }
    }
}
