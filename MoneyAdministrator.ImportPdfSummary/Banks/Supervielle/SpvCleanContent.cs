using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Geom;
using System.Text.RegularExpressions;

namespace MoneyAdministrator.ImportPdfSummary.Banks.Supervielle
{
    public class SpvCleanContent
    {
        private string _brandName;

        public SpvCleanContent(string brandName) 
        {
            _brandName = brandName;
        }

        #region normalizacion de datos

        public List<string> NormalizePages(List<string> pages)
        {
            //Divido las paginas en lineas, convinando todo en una misma lista
            pages = pages.SelectMany(page => page.Split('\n')).ToList();

            //Elimino datos innecesarios
            return CleanText(pages);
        }

        private List<string> CleanText(List<string> lines)
        {
            List<string> result = new List<string>();

            foreach (var line in lines)
            {
                if (line.Contains("PAGO MINIMO") || line.Contains("SALDO ACTUAL"))
                    break;

                result.Add(line);
            }

            return result;
        }

        public List<string> NormalizeDatePages(List<string> datePages)
        {
            //Divido las paginas en lineas, convinando todo en una misma lista
            datePages = datePages.SelectMany(page => page.Split('\n')).ToList();

            //Elimino datos innecesarios
            return CleanTextDate(datePages);
        }

        private List<string> CleanTextDate(List<string> dateLines)
        {
            switch (_brandName)
            {
                case "visa":
                    return CleanTextDateVisa(dateLines);
                case "mastercard":
                    return CleanTextDateMaster(dateLines);
                default:
                    throw new Exception(ImportConfigs.SupervielleBrandNotCompatible.Replace("<brandName>", _brandName));
            }
        }

        private List<string> CleanTextDateVisa(List<string> dateLines)
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

        private List<string> CleanTextDateMaster(List<string> dateLines)
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

        public List<string> NormalizeArsPages(List<string> arsPages)
        {
            //Divido las paginas en lineas, convinando todo en una misma lista
            arsPages = arsPages.SelectMany(page => page.Split('\n')).ToList();

            //Elimino datos innecesarios
            return CleanTextArs(arsPages);
        }

        private List<string> CleanTextArs(List<string> arsLines)
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
                        result.Add("::TAXESANDMAINTENANCE::");
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

        public List<string> NormalizeUsdPages(List<string> usdPages)
        {
            //Divido las paginas en lineas, convinando todo en una misma lista
            usdPages = usdPages.SelectMany(page => page.Split('\n')).ToList();

            //Elimino datos innecesarios
            return CleanTextUsd(usdPages);
        }

        private List<string> CleanTextUsd(List<string> usdLines)
        {
            switch (_brandName)
            {
                case "visa":
                    return CleanTextUsdVisa(usdLines);
                case "mastercard":
                    return CleanTextUsdMaster(usdLines);
                default:
                    throw new Exception(ImportConfigs.SupervielleBrandNotCompatible.Replace("<brandName>", _brandName));
            }
        }

        private List<string> CleanTextUsdVisa(List<string> usdLines)
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
                        result.Add("::TAXESANDMAINTENANCE::");
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

        private List<string> CleanTextUsdMaster(List<string> usdLines)
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
                        result.Add("::TAXESANDMAINTENANCE::");
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

            //Añado el pago minimo de dolares
            result.Add("0,00");

            return result;
        }

        #endregion

        #region Summary Meta Data

        public List<string> GetSummaryMetaData(List<string> lines)
        {
            var results = new List<string>();

            int index = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                if (i == 0 || i == 6 || i == 8 || i == 12)
                    results.Add(lines[i]);
            }

            return results;
        }

        public List<string> GetSummaryPropertiesSectionFromString(List<string> lines)
        {
            var results = new List<string>();

            //Obtengo las lineas
            int index = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                if (i == 0 || i == 2 || i == 4 || i == 6)
                    results.Add(lines[i]);
            }

            return results;
        }

        #endregion

        #region Principal Summary Data

        public List<string> GetPrincipalSummaryDataFromString(List<string> lines)
        {
            var results = new List<string>();

            //Obtengo las lineas
            bool copy = false;
            foreach (var line in lines)
            {
                if (line.Contains("SALDO ANTERIOR"))
                    copy = true;

                if (line.Contains("Movimientos"))
                    break;

                if (!copy)
                    continue;

                results.Add(line);
            }

            return results.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public List<string> GetPrincipalSummaryDataFromSectionsString(List<string> lines)
        {
            var results = new List<string>();

            //Obtengo las lineas
            for (int index = 0; index < lines.Count; index++)
            {
                if (lines[index].Contains("::SUMMARY::"))
                {
                    continue;
                }

                if (lines[index].Contains("::DETAILS::"))
                    break;

                results.Add(lines[index]);
            }

            return results.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        #endregion

        #region Details Data

        public List<string> GetDetailsDataFromString(List<string> lines)
        {
            switch (_brandName)
            {
                case "visa":
                    return GetDetailsDataFromStringVisa(lines);
                case "mastercard":
                    return GetDetailsDataFromStringMaster(lines);
                default:
                    throw new Exception(ImportConfigs.SupervielleBrandNotCompatible.Replace("<brandName>", _brandName));
            }
        }

        public List<string> GetDetailsDataFromStringVisa(List<string> lines)
        {
            var results = new List<string>();

            //Obtengo las lineas
            bool copy = false;
            foreach (var line in lines)
            {
                if (line.Contains("FECHA COMPROBANTE DETALLE DE TRANSACCION IMPORTE EN PESOS IMPORTE EN DOLARES"))
                {
                    copy = true;
                    continue;
                }

                if (line.Contains("Total Consumos de LUCAS EZEQU CERATTO"))
                    break;

                if (!copy)
                    continue;

                results.Add(line);
            }

            return results.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public List<string> GetDetailsDataFromStringMaster(List<string> lines)
        {
            var results = new List<string>();

            //Obtengo las lineas
            bool copy = false;
            foreach (var line in lines)
            {
                if (line.Contains("FECHA COMPROBANTE COD. OPERACION DETALLE DE TRANSACCION IMPORTE EN PESOS IMPORTE EN DOLARES"))
                {
                    copy = true;
                    continue;
                }

                if (line.Contains("TOTAL TITULAR CERATTO LUCAS EZEQUIEL"))
                    break;

                if (!copy)
                    continue;

                results.Add(line);
            }

            return results.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public List<string> GetDetailsDataFromCurrencyString(List<string> lines)
        {
            var results = new List<string>();

            //Obtengo las lineas
            var copy = false;
            for (int index = 0; index < lines.Count; index++)
            {
                if (lines[index].Contains("::DETAILS::"))
                {
                    copy = true;
                    continue;
                }

                if (lines[index].Contains("::TAXESANDMAINTENANCE::"))
                    break;

                if (!copy)
                    continue;

                results.Add(lines[index]);
            }

            //Elimino el ultimo valor (subtotal)
            var lastItem = results.LastOrDefault();
            if (lastItem != null && results.Count > 0)
            {
                results.Remove(lastItem);
            }

            return results.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        #endregion

        #region Taxes And Maintenance Data

        public List<string> GetTaxesAndMaintenanceDataFromString(List<string> lines)
        {
            var results = new List<string>();

            //Obtengo las lineas
            bool copy = false;
            int count = 0;
            foreach (var line in lines)
            {
                if (line.Contains("FECHA DETALLE DE TRANSACCION IMPORTE EN PESOS IMPORTE EN DOLARES"))
                {
                    count++;
                    copy = true;
                    continue;
                }

                if (count <= 1) 
                    continue;

                if (line.Contains("SALDO ACTUAL"))
                    break;

                if (!copy)
                    continue;

                results.Add(line);
            }

            return results.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public List<string> GetTaxesAndMaintenanceDataFromCurrencyString(List<string> lines)
        {
            var results = new List<string>();

            //Obtengo las lineas
            var copy = false;
            for (int index = 0; index < lines.Count; index++)
            {
                if (lines[index].Contains("::TAXESANDMAINTENANCE::"))
                {
                    copy = true;
                    continue;
                }

                if (!copy)
                    continue;

                results.Add(lines[index]);
            }

            //Elimino los ultimos 2 valor (total y minimo)
            results.RemoveRange(results.Count - 1, 1);

            return results.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        #endregion
    }
}