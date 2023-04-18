using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Module.ImportHsbcSummary.Utilities
{
    internal class DateTimeTools
    {
        /// <summary>Recibe una fecha con formato "dd-MMM-yy" y la convierte a "dd-MM-yy",
        /// Ejemplo: input: "28-Ene-22" output: "28-01-22"</summary>
        public static string ConvertDateFormat(string date)
        {
            var dateParts = date.Split("-");

            switch (dateParts[1])
            {
                case "Ene":
                    dateParts[1] = "01";
                    break;
                case "Feb":
                    dateParts[1] = "02";
                    break;
                case "Mar":
                    dateParts[1] = "03";
                    break;
                case "Abr":
                    dateParts[1] = "04";
                    break;
                case "May":
                    dateParts[1] = "05";
                    break;
                case "Jun":
                    dateParts[1] = "06";
                    break;
                case "Jul":
                    dateParts[1] = "07";
                    break;
                case "Ago":
                    dateParts[1] = "08";
                    break;
                case "Sep":
                    dateParts[1] = "09";
                    break;
                case "Oct":
                    dateParts[1] = "10";
                    break;
                case "Nov":
                    dateParts[1] = "10";
                    break;
                case "Dic":
                    dateParts[1] = "12";
                    break;
                default:
                    break;
            }

            return string.Join("-", dateParts);
        }

        public static DateTime ConvertToDateTime(string date, string format)
        {
            if (format == "dd-MMM-yy")
            {
                date = ConvertDateFormat(date);
                format = "dd-MM-yy";
            }
            return DateTime.ParseExact(date, format, CultureInfo.GetCultureInfo("es-ES"));
        }

        public static bool TestDate(string date, string format)
        {
            try
            {
                var _ = ConvertToDateTime(date, format);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
