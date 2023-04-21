using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Import.Summary.Utilities.TypeTools
{
    internal class StringTools
    {
        public static string GetNumbersFromString(string input)
        {
            return new string(input.Where(c => char.IsDigit(c) || c == '-' || c == '.' || c == ',').ToArray());
        }
    }
}
