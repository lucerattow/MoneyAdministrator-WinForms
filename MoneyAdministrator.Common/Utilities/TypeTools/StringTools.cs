using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Common.Utilities.TypeTools
{
    public class StringTools
    {
        public static string GetDecimalFromString(string input)
        {
            return new string(input.Where(c => char.IsDigit(c) || c == '-' || c == '.' || c == ',').ToArray());
        }

        public static string GetNumbersFromString(string input)
        {
            return new string(input.Where(c => char.IsDigit(c) || c == '-').ToArray());
        }
    }
}
