using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Module.ImportHsbcSummary.Utilities
{
    public static class stringUtilities
    {
        public static int GetNumbersFromString(string input)
        {
            var numbers = new string(input.Where(c => char.IsDigit(c) || c == '-').ToArray());

            if (numbers.Length > 0)
                return int.Parse(numbers);
            else
                return 0;
        }
    }
}
