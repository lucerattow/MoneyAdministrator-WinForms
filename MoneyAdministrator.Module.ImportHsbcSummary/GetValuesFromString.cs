using iText.Layout.Properties;
using MoneyAdministrator.DTOs;
using MoneyAdministrator.Module.ImportHsbcSummary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Module.ImportHsbcSummary
{
    public static class GetValuesFromString
    {
        public static List<string> FilteredHeaderAndFooter(List<string> pages)
        {
            List<string> result = new List<string>();

            foreach (var page in pages)
            {
                var filtered = FilteredFooter(page);
                filtered = FilteredHeader(filtered);

                result.Add(filtered);
            }

            return result;
        }

        private static string FilteredFooter(string page)
        {
            List<string> result = new List<string>();
            var lines = page.Split("\n").ToList();

            foreach (var line in lines)
            {
                if (line.Contains("031 305 8 031 305 8 CERATTO LUCAS EZEQUIEL"))
                    break;

                result.Add(line);
            }

            return string.Join("\n", result.Where(x => !string.IsNullOrEmpty(x)));
        }

        private static string FilteredHeader(string page)
        {
            List<string> result = new List<string>();
            var lines = page.Split("\n").ToList();

            bool copy = !lines.Where(x => x.Contains("031 . 305 . 8")).Any();

            foreach (var line in lines)
            {
                if (line.Contains("031 . 305 . 8"))
                {
                    copy = true;
                    continue;
                }

                if (!copy)
                    continue;

                result.Add(line);
            }

            return string.Join("\n", result.Where(x => !string.IsNullOrEmpty(x)));
        }

        public static CreditCardSummaryDto GetGeneralSummaryData(string pageOne)
        {
            var ccSummary = new CreditCardSummaryDto();
            var lines = pageOne.Split("\n");

            int index = 0;
            foreach (var line in lines)
            {
                //Obtengo la fecha de cierre
                if (index == 0)
                {
                    var date = line.Split(" ").ToList().Where(x => !string.IsNullOrEmpty(x)).ToList()[0];
                    ccSummary.Date = DateTimeTools.ConvertToDateTime(date);
                }

                //Obtengo la fecha de vencimiento
                if (index == 2)
                {
                    var date = line.Split(" ").ToList().Where(x => !string.IsNullOrEmpty(x)).ToList()[0];
                    ccSummary.Expiration = DateTimeTools.ConvertToDateTime(date);
                }

                //Obtengo la proxima fecha de cierre
                if (index == 4)
                {
                    var date = line.Split(" ").ToList().Where(x => !string.IsNullOrEmpty(x)).ToList()[3];
                    ccSummary.NextDate = DateTimeTools.ConvertToDateTime(date);
                }

                //Obtengo la proxima fecha de vencimiento y el pago minimo
                if (index == 6)
                {
                    var datePayment = line.Split(" ").ToList().Where(x => !string.IsNullOrEmpty(x)).ToList()[1];

                    //Fecha de vencimiento
                    var date = datePayment.Substring(datePayment.Length - 9);
                    ccSummary.NextExpiration = DateTimeTools.ConvertToDateTime(date);

                    //Pago minimo
                    var minimumPayment = datePayment.Substring(0, datePayment.Length - 9);
                    ccSummary.minimumPayment = decimalTools.ToDecimal(minimumPayment);
                }

                index++;
            }

            return ccSummary;
        }

        public static string GetConsolidatedSummary(string pageOne)
        {
            var lines = pageOne.Split("\n").ToList();
            var result = new List<string>();

            bool copy = false;
            int index = 0;
            foreach (var line in lines)
            {
                if (line.Contains("RESUMEN CONSOLIDADO"))
                    copy = true;

                if (line.Contains("DETALLE DEL MES"))
                    break;

                if (!copy)
                    continue;

                result.Add(line);

                index++;
            }

            return string.Join("\n", result.Where(x => !string.IsNullOrEmpty(x)));
        }
    }
}
