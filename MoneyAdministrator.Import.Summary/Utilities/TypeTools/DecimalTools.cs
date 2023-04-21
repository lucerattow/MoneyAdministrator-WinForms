using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoneyAdministrator.Import.Summary.Utilities.TypeTools
{
    internal class DecimalTools
    {
        public static decimal Convert(string input)
        {
            input = input.Trim();
            input = StringTools.GetNumbersFromString(input);

            if (string.IsNullOrEmpty(input))
                return 0;

            string decimalSeparator = input.Substring(input.Length - 3, 1);
            string thousandSeparator = decimalSeparator == "." ? "," : ".";

            input = input.Replace(thousandSeparator, string.Empty);

            // Verificar si el símbolo negativo está presente y eliminar los posibles símbolos negativos adicionales
            bool isNegative = input.StartsWith("-");
            input = Regex.Replace(input, "-", string.Empty);

            if (isNegative)
            {
                input = "-" + input;
            }

            if (decimal.TryParse(input, NumberStyles.Float, new CultureInfo("en-US") { NumberFormat = { NumberDecimalSeparator = decimalSeparator } }, out decimal result))
            {
                return result;
            }

            return 0;
        }
    }
}
