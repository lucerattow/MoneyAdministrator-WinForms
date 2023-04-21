using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoneyAdministrator.Import.Summary.Importers.TextCleaners
{
    internal class SpvTextCleaner
    {
        private string _brandName;

        public SpvTextCleaner(string brandName)
        {
            _brandName = brandName;
        }

        public List<string> CleanAllText(List<string> lines)
        {
            var Result = new List<string>();

            foreach (var line in lines)
            {
                if (line.Contains("PAGO MINIMO") || line.Contains("SALDO ACTUAL"))
                    break;

                Result.Add(line);
            }

            Result = ReplaceTableHeaders(Result);
            Result = CleanUnnecesaryData(Result);
            Result = CleanHeaderData(Result);
            Result = FixDateBug(Result);

            return Result;
        }

        private List<string> ReplaceTableHeaders(List<string> lines)
        {
            return _brandName == Brand.Visa ? ReplaceTableHeadersVisa(lines) : ReplaceTableHeadersMaster(lines);
        }

        private List<string> ReplaceTableHeadersVisa(List<string> lines)
        {
            var result = new List<string>();

            int count = 0;
            foreach (var line in lines)
            {
                if (line.Contains("FECHA DETALLE DE TRANSACCION IMPORTE EN PESOS IMPORTE EN DOLARES"))
                {
                    if (count == 0)
                        result.Add("::SUMMARY::");
                    if (count == 1)
                        result.Add("::TAXES::");
                    count++;
                    continue;
                }

                if (line.Contains("FECHA COMPROBANTE DETALLE DE TRANSACCION IMPORTE EN PESOS IMPORTE EN DOLARES"))
                {
                    result.Add("::DETAILS::");
                    continue;
                }

                result.Add(line);
            }

            return result;
        }

        private List<string> ReplaceTableHeadersMaster(List<string> lines)
        {
            var result = new List<string>();

            int count = 0;
            foreach (var line in lines)
            {
                if (line.Contains("FECHA DETALLE DE TRANSACCION IMPORTE EN PESOS IMPORTE EN DOLARES"))
                {
                    if (count == 0)
                        result.Add("::SUMMARY::");
                    if (count == 1)
                        result.Add("::TAXES::");
                    count++;
                    continue;
                }

                if (line.Contains("FECHA COMPROBANTE COD. OPERACION DETALLE DE TRANSACCION IMPORTE EN PESOS IMPORTE EN DOLARES"))
                {
                    result.Add("::DETAILS::");
                    continue;
                }

                result.Add(line);
            }

            return result;
        }

        private List<string> CleanUnnecesaryData(List<string> lines)
        {
            var result = new List<string>();

            foreach (var line in lines)
            {
                if (line.Contains("Movimientos") ||
                    line.Contains("TOTAL TITULAR") ||
                    line.Contains("Total Consumos de"))
                    continue;

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
                if (!(i == 3 || i == 8 || i >= 12))
                    continue;

                var line = Regex.Replace(lines[i], @"\s+", " ");

                if (i == 3)
                {
                    Match match = Regex.Match(line, @"\d{4}-\d{2}-\d{2}");
                    result.Add($"DATE:{match.Value}");
                }

                if (i == 8)
                {
                    var dateExp = line.Split(" ")[0];
                    result.Add($"DATE_EXP:{dateExp}");

                    var minPay = line.Split(" ")[3];
                    result.Add($"MIN_PAY:{minPay}");
                }

                if (i == 12)
                {
                    var lastIndex = line.LastIndexOf(":");

                    var dateNextContainer = line.Substring(0, lastIndex);
                    Match match = Regex.Match(dateNextContainer, @"\d{4}-\d{2}-\d{2}");
                    result.Add($"DATE_NEXT:{match.Value}");

                    var dateExp = line.Substring(lastIndex + 1);
                    Match dateExpMatch = Regex.Match(dateExp, @"\d{4}-\d{2}-\d{2}");
                    result.Add($"DATE_NEXT_EXP:{dateExpMatch.Value}");
                }

                if (lines[i].Contains("::SUMMARY::"))
                    copy = true;

                if (!copy)
                    continue;

                result.Add(lines[i]);
            }

            return result;
        }

        private List<string> FixDateBug(List<string> lines)
        {
            var result = new List<string>();
            var dateReg = new Regex(@"^\d{4}-\d{2}-\d{2}$");

            var previousLineDate = false;
            var process = false;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains("::DETAILS::"))
                {
                    process = true;
                }

                if (!process)
                {
                    result.Add(lines[i]);
                    continue;
                }

                //Compruebo si la linea actual es unicamente una fecha
                if (dateReg.IsMatch(lines[i].Trim()))
                {
                    previousLineDate = true;
                    continue;
                }

                //Si la linea anterior contiene solo una fecha, ejemplo:
                //linea 1: "2022-04-04" -> fecha solitaria
                //linea 2: "00210 4 RAPPI 499,00" -> transaccion sin fecha
                if (previousLineDate && dateReg.IsMatch(lines[i - 1].Trim()))
                {
                    result.Add(lines[i - 1] + " " + lines[i]);
                    previousLineDate = false;
                    continue;
                }

                //Si la linea tiene su fecha + informacion de transaccion
                //Ejemplo: "2022-04-04 00210 4 RAPPI 499,00"
                else if (lines[i].Length > 12 && dateReg.IsMatch(lines[i].Substring(0, 10)))
                {
                    result.Add(lines[i]);
                    continue;
                }

                //Si falta la fecha, y esta se encuentra una linea por debajo, ejemplo:
                //linea 1: "00210 4 RAPPI 499,00" -> transaccion sin fecha
                //linea 2: "2022-04-04" -> fecha solitaria
                else if (i + 1 < lines.Count && dateReg.IsMatch(lines[i + 1].Trim()))
                {
                    result.Add(lines[i + 1] + " " + lines[i]);
                    i++; //skipeo la siguiente linea para no activar "previousLineDate"
                    continue;
                }

                //Añado lineas normales sin fecha
                result.Add(lines[i]);
            }

            return result;
        }

        //Funciones para Date
        public List<string> CleanDate(List<string> dateLines)
        {
            return _brandName == Brand.Visa ? CleanDateVisa(dateLines) : CleanDateMaster(dateLines);
        }

        private List<string> CleanDateVisa(List<string> dateLines)
        {
            List<string> result = new List<string>();

            int count = 0;
            foreach (var line in dateLines)
            {
                if (line.Contains("FECHA CI") || line.Contains("FECHA PR") || line.Contains("Movimiento"))
                    continue;

                if (line.Contains("FECHA"))
                {
                    if (count == 0)
                        result.Add("::SUMMARY::");
                    if (count == 1)
                        result.Add("::DETAILS::");
                    if (count == 2)
                        break;
                    count++;
                    continue;
                }

                //Si es menor o igual a 1, continua
                if (count <= 0) continue;

                result.Add(line);
            }

            return result;
        }

        private List<string> CleanDateMaster(List<string> dateLines)
        {
            List<string> result = new List<string>();

            int count = 0;
            foreach (var line in dateLines)
            {
                if (line.Contains("FECHA CI") || line.Contains("FECHA PR") || line.Contains("Movimiento"))
                    continue;

                if (line.Contains("FECHA"))
                {
                    if (count == 0)
                        result.Add("::SUMMARY::");
                    if (count == 1)
                        result.Add("::DETAILS::");
                    if (count == 2)
                        break;
                    count++;
                    continue;
                }

                //Si es menor o igual a 1, continua
                if (count <= 0) continue;

                result.Add(line);
            }

            return result;
        }

        //Funciones para Amount Ars
        public List<string> CleanAmountArs(List<string> arsLines)
        {
            List<string> result = new List<string>();

            int count = 0;
            foreach (var line in arsLines)
            {
                if (line.Contains("IMPORTE EN PESOS"))
                {
                    if (count == 0)
                        result.Add("::SUMMARY::");
                    if (count == 1)
                        result.Add("::DETAILS::");
                    if (count == 2)
                        result.Add("::TAXES::");
                    if (count == 3)
                        break;
                    count++;
                    continue;
                }

                if (count == 0)
                    continue;

                if (Regex.IsMatch(line, @"[a-zA-Z]+"))
                    break;

                result.Add(line);
            }

            //Elimino el valor del pago minimo
            result.RemoveAt(result.Count - 1);

            return result;
        }

        //Funciones para Amount Usd
        public List<string> CleanAmountUsd(List<string> usdLines)
        {
            return _brandName == Brand.Visa ? CleanAmountUsdVisa(usdLines) : CleanAmountUsdMaster(usdLines);
        }

        private List<string> CleanAmountUsdVisa(List<string> usdLines)
        {
            List<string> result = new List<string>();

            int count = 0;
            foreach (var line in usdLines)
            {
                if (line.Contains("IMPORTE EN DOLARES"))
                {
                    if (count == 0)
                        result.Add("::SUMMARY::");
                    if (count == 1)
                        result.Add("::DETAILS::");
                    if (count == 2)
                        result.Add("::TAXES::");
                    if (count == 3)
                        break;
                    count++;
                    continue;
                }

                if (count == 0)
                    continue;

                if (Regex.IsMatch(line, @"[a-zA-Z]+"))
                    break;

                result.Add(line);
            }

            //Elimino el valor del pago minimo
            result.RemoveAt(result.Count - 1);

            return result;
        }

        private List<string> CleanAmountUsdMaster(List<string> usdLines)
        {
            List<string> result = new List<string>();

            int count = 0;
            foreach (var line in usdLines)
            {
                if (line.Contains("IMPORTE EN DOLARES"))
                {
                    if (count == 0)
                        result.Add("::SUMMARY::");
                    if (count == 1)
                        result.Add("::DETAILS::");
                    if (count == 2)
                        result.Add("::TAXES::");
                    if (count == 3)
                        break;
                    count++;
                    continue;
                }

                if (count == 0)
                    continue;

                if (Regex.IsMatch(line, @"[a-zA-Z]+"))
                    break;

                result.Add(line);
            }

            return result;
        }
    }
}
