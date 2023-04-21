using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.DTOs.Enums;
using MoneyAdministrator.Import.Summary.Utilities.TypeTools;
using MoneyAdministrator.Import.Summary.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MoneyAdministrator.Import.Summary.DTOs;

namespace MoneyAdministrator.Import.Summary.Importers.TextProcessors
{
    internal static class HsbcTextProcessor
    {
        public static CreditCardSummaryDto GetSummaryData(TextExtractionDto table)
        {
            var summary = GetSummaryVariablesData(table.AllText);
            summary.AddDetailDto(GetAllData(table.AllText));

            return summary;
        }

        private static CreditCardSummaryDto GetSummaryVariablesData(List<string> lines)
        {
            var summary = new CreditCardSummaryDto();

            for (int i = 0; i < 7; i++)
            {
                var key = lines[i].Split(":")[0];
                var value = lines[i].Split(":")[1];

                switch (key)
                {
                    case "DATE":
                        //Fecha de resumen
                        summary.Date = DateTimeTools.Convert(value, "dd-MMM-yy");

                        //Periodo
                        int month = summary.Date.Day >= ModuleConstants.DayStartPeriod ? summary.Date.Month + 1 : summary.Date.Month;
                        int year = summary.Date.Year;
                        if (month == 13)
                        {
                            month = 1;
                            year++;
                        }
                        summary.Period = new DateTime(year, month, 1);
                        continue;
                    case "DATE_EXP":
                        //Fecha de vencimiento
                        summary.Expiration = DateTimeTools.Convert(value, "dd-MMM-yy");
                        continue;
                    case "DATE_NEXT":
                        //Proximo cierre
                        summary.NextDate = DateTimeTools.Convert(value, "dd-MMM-yy");
                        continue;
                    case "DATE_NEXT_EXP":
                        //Proximo vencimiento
                        summary.NextExpiration = DateTimeTools.Convert(value, "dd-MMM-yy");
                        continue;
                    case "TOTAL_ARS":
                        //Saldo total Ars
                        summary.TotalArs = DecimalTools.Convert(value);
                        continue;
                    case "TOTAL_USD":
                        //Saldo total Usd
                        summary.TotalUsd = DecimalTools.Convert(value);
                        continue;
                    case "MIN_PAY":
                        //Pago minimo
                        summary.MinimumPayment = DecimalTools.Convert(value);
                        continue;
                }
            }

            return summary;
        }

        private static List<CreditCardSummaryDetailDto> GetAllData(List<string> lines)
        {
            var results = new List<CreditCardSummaryDetailDto>();

            //Variables necesarias para procesar el texto
            Regex dateRegex = new Regex(@"^(\d{2}\-[A-Za-z]{3}\-\d{2})?");
            Regex installmentRegex = new Regex(@" \d{2}/\d{2} ");
            var detailType = CreditCardSummaryDetailType.Summary;

            //Analizo cada linea
            bool process = false;
            foreach (var line in lines)
            {
                //Compruebo si la linea es un separador
                switch (line)
                {
                    case "::SUMMARY::":
                        detailType = CreditCardSummaryDetailType.Summary;
                        process = true;
                        continue;
                    case "::TAXES::":
                        detailType = CreditCardSummaryDetailType.TaxesAndMaintenance;
                        continue;
                    case "::DETAILS::":
                        detailType = CreditCardSummaryDetailType.Details;
                        continue;
                    case "::AUTODEBITS::":
                        detailType = CreditCardSummaryDetailType.AutomaticDebits;
                        continue;
                    case "::INSTALLMENTS::":
                        detailType = CreditCardSummaryDetailType.Installments;
                        continue;
                }

                if (!process)
                    continue;

                //Obtengo la fecha
                Match dateMatch = dateRegex.Match(line);
                string date = dateMatch.Groups[1].Value;

                //Obtengo los valores AmountArs y AmountUsd
                int usdStartIndex = line.Length - 12;
                int arsStartIndex = line.Length - 24;

                string firstAmount = arsStartIndex >= 0 ? line.Substring(arsStartIndex, 12).Trim() : "";
                string secondAmount = usdStartIndex >= 0 ? line.Substring(usdStartIndex, 12).Trim() : "";

                //Intento obtener el valor de AmountArs
                bool isFirstAmountValid =
                    firstAmount.Contains(",") &&
                    double.TryParse(firstAmount.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double _) ||
                    firstAmount == "";

                bool isSecondAmountValid =
                    double.TryParse(secondAmount.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double _) ||
                    secondAmount == "";

                string amountArs = "";
                string amountUsd = "";

                //Separo los valores Date, AmountArs y AmountUsd, y guardo el resto
                string restOfLine = "";
                if (isFirstAmountValid && isSecondAmountValid)
                {
                    amountArs = firstAmount;
                    amountUsd = secondAmount;
                    restOfLine = line.Substring(dateMatch.Length, arsStartIndex - dateMatch.Length + 1).Trim() + " ";
                }
                else if (isSecondAmountValid)
                {
                    amountArs = secondAmount;
                    restOfLine = line.Substring(dateMatch.Length, usdStartIndex - dateMatch.Length + 1).Trim() + " ";
                }

                string description = "";
                string installment = "";
                if (detailType == CreditCardSummaryDetailType.Installments)
                {
                    //Obtengo las cuotas
                    Match installmentMatch = installmentRegex.Match(restOfLine);
                    installment = installmentMatch.Value.Trim();

                    //Obtengo la descripcion
                    description = restOfLine.Trim();
                    if (!string.IsNullOrEmpty(installment))
                    {
                        int length = installmentMatch.Length;
                        int index = installmentMatch.Index;
                        description = restOfLine.Substring(0, index) + restOfLine.Substring(index + length);
                        description = Regex.Replace(description, @"\s+", " ").Trim();
                    }
                }
                else
                    description = Regex.Replace(restOfLine, @"\s+", " ").Trim();

                //Guardo los datos en un DTO
                var summary = new TransactionParamsDto()
                {
                    Type = detailType,
                    Date = date,
                    DateFormat = "dd-MMM-yy",
                    Description = description,
                    Installments = installment,
                    AmountArs = amountArs,
                    AmountUsd = amountUsd
                };

                //Convierto los valores del dto y lo agrego a la lista de transacciones
                var parsedDto = ParseDto.GetCreditCardSummaryDetailDto(summary);
                results.Add(parsedDto);
            }

            return results;
        }
    }
}
