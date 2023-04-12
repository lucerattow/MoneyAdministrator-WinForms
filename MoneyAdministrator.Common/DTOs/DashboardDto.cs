using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Common.DTOs
{
    public class DashboardDto
    {
        public DateTime Period { get; set; }
        public decimal UsdValue { get; set; }
        public decimal UsdSalary { get; set; }
        public decimal SalaryArs { get; set; }
        public decimal SalaryUsd { get; set; }
        public decimal Assets { get; set; }
        public decimal Passives { get; set; }
        public decimal Balance { get; set; }
    }
}
