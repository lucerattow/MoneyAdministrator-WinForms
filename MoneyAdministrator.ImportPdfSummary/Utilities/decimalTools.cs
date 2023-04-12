using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Module.ImportHsbcSummary.Utilities
{
    public static class decimalTools
    {
        public static decimal ToDecimal(string input)
        {
            decimal result = 0;

            input = input.Replace(",", ".");
            if (!string.IsNullOrEmpty(input) && decimal.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
                return result;
            else
                return 0;
        }
    }
}
