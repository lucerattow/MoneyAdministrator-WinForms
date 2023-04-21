using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoneyAdministrator.Import.Summary.Importers.TextCleaners
{
    internal class HsbcTextCleaner
    {
        private string _brandName;

        public HsbcTextCleaner(string brandName)
        {
            _brandName = brandName;
        }

        public List<string> CleanAllText(List<string> lines)
        {
            var result = new List<string>();

            foreach (var line in lines)
            {
                if (line.Contains("Debitaremos de su c.ahorro") || line.Contains("el importe de su pago mínimo actual"))
                    break;

                result.Add(line);
            }

            result = CleanSeparatorLines(result);
            result = CleanUnnecesaryData(result);
            result = CleanHeaderData(result);

            return result;
        }

        private List<string> CleanSeparatorLines(List<string> lines)
        {
            var result = new List<string>();

            foreach (var line in lines)
            {
                if (line.Contains("-----------------------") || line.Contains("______________________"))
                    continue;

                result.Add(line);
            }

            return result;
        }

        private List<string> CleanUnnecesaryData(List<string> lines)
        {
            var result = new List<string>();

            foreach (var line in lines)
            {
                if (line.Contains("SALDO PENDIENTE") ||
                    line.Contains("TOTAL CONSUMOS DEL MES") ||
                    line.Contains("SALDO ACTUAL") ||
                    line.Contains("PAGO MINIMO") ||
                    line.Contains("TOTAL TITULAR") ||
                    line.Contains("DETALLE DEL MES"))
                    continue;

                if (line.Contains("RESUMEN CONSOLIDADO"))
                {
                    result.Add("::SUMMARY::");
                    continue;
                }

                if (line.Contains("SUBTOTAL"))
                {
                    result.Add("::TAXES::");
                    continue;
                }

                if (line.Contains("COMPRAS DEL MES"))
                {
                    result.Add("::DETAILS::");
                    continue;
                }

                if (line.Contains("DEBITOS AUTOMATICOS"))
                {
                    result.Add("::AUTODEBITS::");
                    continue;
                }

                if (line.Contains("CUOTAS DEL MES"))
                {
                    result.Add("::INSTALLMENTS::");
                    continue;
                }

                result.Add(line);
            }

            return result;
        }

        private List<string> CleanHeaderData(List<string> lines)
        {
            var result = new List<string>();

            bool copy = false;
            for (int i = 0; i < lines.Count; i++)
            {
                if (!(i == 0 || i == 2 || i == 4 || i >= 6))
                    continue;

                var line = Regex.Replace(lines[i], @"\s+", " ");

                if (i == 0)
                {
                    var date = line.Split(" ")[0];
                    result.Add($"DATE:{date}");
                }

                if (i == 2)
                {
                    var dateExp = line.Split(" ")[0];
                    result.Add($"DATE_EXP:{dateExp}");

                    var minPay = line.Split(" ")[1];
                    result.Add($"MIN_PAY:{minPay}");
                }

                if (i == 4)
                {
                    var dateNext = line.Split(" ")[3];
                    result.Add($"DATE_NEXT:{dateNext}");
                }

                if (i == 6)
                {
                    var dateNextExp = line.Substring(line.Length - 9, 9);
                    result.Add($"DATE_NEXT_EXP:{dateNextExp}");
                }

                if (lines[i].Contains("::SUMMARY::"))
                    copy = true;

                if (!copy)
                    continue;

                result.Add(lines[i]);
            }

            return result;
        }
    }
}
