using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Common.DTOs
{
    public class CreditCardDto
    {
        public int Id { get; set; }
        public string BankEntityName { get; set; }
        public string CreditCardTypeName { get; set; }
        public string LastFourNumbers { get; set; }
    }
}
