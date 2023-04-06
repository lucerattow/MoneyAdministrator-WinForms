using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Services.Utilities
{
    public static class stringUtilities
    {
        public static int GetNumbersFromString(string input)
        {
            var numbers = new string(input.Where(char.IsDigit).ToArray());

            if (numbers.Length > 0)
                return int.Parse(numbers);
            else
                return 0;
        }
    }
}
