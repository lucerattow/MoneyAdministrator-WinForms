using MoneyAdministrator.Import.Summary.Utilities.TypeTools;
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
            return Brand.Visa == _brandName ? CleanAllTextVisa(lines) : CleanAllTextMaster(lines);
        }

        //Mastercard

        private List<string> CleanAllTextMaster(List<string> lines)
        {
            var result = new List<string>();

            foreach (var line in lines)
            {
                if (line.Contains("Debitaremos de su c.ahorro") || line.Contains("el importe de su pago mínimo actual"))
                    break;

                result.Add(line);
            }

            result = CleanSeparatorLinesMaster(result);
            result = CleanUnnecesaryDataMaster(result);
            result = CleanHeaderDataMaster(result);

            return result;
        }

        private List<string> CleanSeparatorLinesMaster(List<string> lines)
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

        private List<string> CleanUnnecesaryDataMaster(List<string> lines)
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

        private List<string> CleanHeaderDataMaster(List<string> lines)
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

                    var totalArs = line.Split(" ")[1];
                    result.Add($"TOTAL_ARS:{totalArs}");

                    var totalUsd = line.Split(" ")[2];
                    result.Add($"TOTAL_USD:{totalUsd}");
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

        //Visa

        private List<string> CleanAllTextVisa(List<string> lines)
        {
            var result = new List<string>();

            foreach (var line in lines)
            {
                if (line.Contains("Plan V: abonando el pago") || line.Contains("usted puede cancelar"))
                    break;

                result.Add(line.Trim());
            }

            result = CleanSeparatorLinesVisa(result);
            result = CleanUnnecesaryDataVisa(result);
            result = CleanHeaderDataVisa(result);

            return result;
        }

        private List<string> CleanSeparatorLinesVisa(List<string> lines)
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

        private List<string> CleanUnnecesaryDataVisa(List<string> lines)
        {
            var result = new List<string>();

            var summaryCopy = true;
            foreach (var line in lines)
            {
                if (line.Contains("SALDO ANTERIOR") ||
                    line.Contains("PAGINA") ||
                    line.Contains("TITULAR DE CUENTA") ||
                    line.Contains("PAGO MINIMO") ||
                    line.Contains("SALDO ACTUAL"))
                    continue;

                if (line.Contains("DETALLE DE TRANSACCION"))
                {
                    if (!summaryCopy)
                        continue;

                    result.Add("::DETAILS::");
                    summaryCopy = false;
                    continue;
                }

                if (line.Contains("TARJETA 2680 Total Consumos de"))
                {
                    result.Add("::TAXES::");
                    continue;
                }

                result.Add(line);
            }

            return result;
        }

        private List<string> CleanHeaderDataVisa(List<string> lines)
        {
            var result = new List<string>();

            bool copy = false;
            for (int i = 0; i < lines.Count; i++)
            {
                if (!(i == 0 || i == 2 || i == 5 || i == 7 || i == 9 || i >= 15))
                    continue;

                var line = Regex.Replace(lines[i], @"\s+", " ");

                if (i == 0)
                {
                    //Obtengo la fecha del string
                    Regex regex = new Regex("[0-9]{2} [A-Za-z]{3} [0-9]{2}");
                    MatchCollection matches = regex.Matches(line);
                    var date_exp = matches[0].Value.Replace(" ", "-");

                    result.Add($"DATE_EXP:{date_exp}");
                }

                if (i == 2)
                {
                    //Obtengo la fecha del string
                    Regex regex = new Regex("[0-9]{2} [A-Za-z]{3} [0-9]{2}");
                    MatchCollection matches = regex.Matches(line);
                    var date = matches[0].Value.Replace(" ", "-");

                    result.Add($"DATE:{date}");
                }

                if (i == 5)
                {
                    //Obtengo el numero del string
                    var total_ars = DecimalTools.Convert(line);
                    result.Add($"TOTAL_ARS:{total_ars}");
                }

                if (i == 7)
                {
                    //Obtengo el numero del string
                    var total_usd = DecimalTools.Convert(line);
                    result.Add($"TOTAL_USD:{total_usd}");
                }

                if (i == 9)
                {
                    //Obtengo el numero del string
                    var min_pay = DecimalTools.Convert(line);
                    result.Add($"MIN_PAY:{min_pay}");
                }


                if (i == 15)
                {
                    //Obtengo la ultima fecha del string
                    Regex regex = new Regex("[0-9]{2} [A-Za-z]{3} [0-9]{2}");
                    MatchCollection matches = regex.Matches(line);
                    var date_next = matches[matches.Count - 1].Value.Replace(" ", "-");

                    result.Add($"DATE_NEXT:{date_next}");
                }

                if (i == 16)
                {
                    //Obtengo la ultima fecha del string
                    Regex regex = new Regex("[0-9]{2} [A-Za-z]{3} [0-9]{2}");
                    MatchCollection matches = regex.Matches(line);
                    var date_next_exp = matches[matches.Count - 1].Value.Replace(" ", "-");

                    result.Add($"DATE_NEXT_EXP:{date_next_exp}");
                }

                if (lines[i].Contains("::DETAILS::"))
                    copy = true;

                if (!copy)
                    continue;

                result.Add(lines[i]);
            }

            return result;
        }

    }
}
