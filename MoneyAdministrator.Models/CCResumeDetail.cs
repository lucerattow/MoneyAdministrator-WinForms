using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Models
{
    public class CCResumeDetail
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Credit card resume is required")]
        public int CCResumeId { get; set; }

        //foreign keys
        public virtual CCResume CCResume { get; set; }
    }
}
