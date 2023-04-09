using iText.Kernel.Geom;
using iText.Layout.Properties;
using MoneyAdministrator.DTOs;
using MoneyAdministrator.DTOs.Enums;
using MoneyAdministrator.Module.ImportHsbcSummary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoneyAdministrator.Module.ImportHsbcSummary
{
    public static class GetValuesFromString
    {
        public static CreditCardSummaryDto GetSummaryData(List<string> lines)
        {
            var ccSummary = new CreditCardSummaryDto();

            GetSummaryVariablesData(ref ccSummary, lines);

            ccSummary.AddDetailDto(GetConsolidatedData(lines));
            ccSummary.AddDetailDto(GetConsumptionsData(lines));
            return ccSummary;
        }

        private static void GetSummaryVariablesData(ref CreditCardSummaryDto ccSummary, List<string> lines)
        {
            lines = CleanContent.GetSummaryPropertiesSectionString(lines);

            for (int i = 0; i < lines.Count; i++)
            {
                //Obtengo la fecha de cierre
                if (i == 0)
                {
                    var date = lines[i].Split(" ").ToList().Where(x => !string.IsNullOrEmpty(x)).ToList()[0];
                    ccSummary.Date = DateTimeTools.ConvertToDateTime(date);
                }

                //Obtengo la fecha de vencimiento
                if (i == 1)
                {
                    var date = lines[i].Split(" ").ToList().Where(x => !string.IsNullOrEmpty(x)).ToList()[0];
                    ccSummary.Expiration = DateTimeTools.ConvertToDateTime(date);
                }

                //Obtengo la proxima fecha de cierre
                if (i == 2)
                {
                    var date = lines[i].Split(" ").ToList().Where(x => !string.IsNullOrEmpty(x)).ToList()[3];
                    ccSummary.NextDate = DateTimeTools.ConvertToDateTime(date);
                }

                //Obtengo la proxima fecha de vencimiento y el pago minimo
                if (i == 3)
                {
                    var datePayment = lines[i].Split(" ").ToList().Where(x => !string.IsNullOrEmpty(x)).ToList()[1];

                    //Fecha de vencimiento
                    var date = datePayment.Substring(datePayment.Length - 9);
                    ccSummary.NextExpiration = DateTimeTools.ConvertToDateTime(date);

                    //Pago minimo
                    var minimumPayment = datePayment.Substring(0, datePayment.Length - 9);
                    ccSummary.minimumPayment = decimalTools.ToDecimal(minimumPayment);
                }
            }
        }

        public static List<CreditCardSummaryDetailDto> GetConsolidatedData(List<string> lines)
        {
            var results = new List<CreditCardSummaryDetailDto>();
            var resultType = CreditCardSummaryDetailDtoType.Summary;

            string data = CleanContent.GetConsolidatedSectionString(lines);
            var dataLines = data.Split("\n");

            for (int i = 0; i < dataLines.Count(); i++)
            {
                if (dataLines.Contains("SUBTOTAL"))
                {
                    resultType = CreditCardSummaryDetailDtoType.TaxesAndMaintenance;
                    continue;
                }

                var ccSummaryDetail = new CreditCardSummaryDetailDto();
                ccSummaryDetail.Type = resultType;

                //Obtengo la fecha
                if (!dataLines[i].StartsWith(" "))
                {
                    var date = dataLines[i].Substring(0, 9);
                    ccSummaryDetail.Date = DateTimeTools.ConvertToDateTime(date);
                }

                //Elimino el espacio de la fecha
                dataLines[i] = dataLines[i].Substring(22, dataLines[i].Length - 22);

                //Obtengo la descripcion
                int index = dataLines[i].IndexOf("  ");
                ccSummaryDetail.Description = dataLines[i].Substring(0, index);

                //Elimino la descripcion
                dataLines[i] = dataLines[i].Substring(index);

                //Obtengo las monedas
                var length = dataLines[i].Length > 12 ? 12 : dataLines[i].Length;

                ccSummaryDetail.AmountArs = decimalTools.ToDecimal(dataLines[i].Substring(0, length));

                if (length > 12)
                    ccSummaryDetail.AmountUsd = decimalTools.ToDecimal(dataLines[i].Substring(length));

                results.Add(ccSummaryDetail);
            }

            return results;
        }

        public static List<CreditCardSummaryDetailDto> GetConsumptionsData(List<string> lines)
        {
            var results = new List<CreditCardSummaryDetailDto>();
            var resultType = CreditCardSummaryDetailDtoType.Details;

            string data = CleanContent.GetConsolidatedSectionString(lines);

            var dataLines = data.Split("\n");

            for (int i = 0; i < dataLines.Count(); i++)
            {
                var ccSummaryDetail = new CreditCardSummaryDetailDto();
                ccSummaryDetail.Type = resultType;

                results.Add(ccSummaryDetail);
            }

            return results;
        }
    }
}
