using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Module.ImportHsbcSummary.Utilities
{
    public static class decimalTools
    {
        public static decimal ToDecimal(string input)
        {
            input = input.Replace(",", ".");
            if (!string.IsNullOrEmpty(input) && decimal.TryParse(input, out decimal result))
                return result;
            else
                return 0;
        }
    }
}
