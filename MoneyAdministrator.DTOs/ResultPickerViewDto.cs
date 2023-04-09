using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.DTOs
{
    public class ResultPickerViewDto
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Registros")]
        public string Field1 { get; set; }
    }
}
