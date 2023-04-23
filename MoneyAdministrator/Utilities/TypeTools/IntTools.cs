using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoneyAdministrator.Utilities.TypeTools
{
    internal class IntTools
    {
        public static int Convert(string input)
        {
            input = input.Trim();
            input = StringTools.GetNumbersFromString(input);

            if (string.IsNullOrEmpty(input))
                return 0;

            // Verificar si el símbolo negativo está presente y eliminar los posibles símbolos negativos adicionales
            bool isNegative = input.StartsWith("-");
            input = Regex.Replace(input, "-", string.Empty);

            if (isNegative)
            {
                input = "-" + input;
            }

            if (int.TryParse(input, out int result))
            {
                return result;
            }

            return 0;
        }
    }
}
