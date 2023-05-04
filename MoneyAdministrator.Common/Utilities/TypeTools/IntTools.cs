using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoneyAdministrator.Common.Utilities.TypeTools
{
    public class IntTools
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

        public static List<int> GetIntermediateNumbers(int init, int end)
        {
            //Unifico la lista de años
            var numbers = new List<int>();

            for (int year = init; year <= end; year++)
            {
                numbers.Add(year);
            }

            return numbers;
        }
    }
}
