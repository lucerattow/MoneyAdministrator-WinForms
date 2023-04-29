using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.DTOs.Enums;
using MoneyAdministrator.Import.Summary.Utilities.TypeTools;
using MoneyAdministrator.Import.Summary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MoneyAdministrator.Import.Summary.DTOs;
using MoneyAdministrator.Import.Summary.Importers.TextFilters;

namespace MoneyAdministrator.Import.Summary.Importers.TextProcessors
{
    internal static class SpvTextProcessor
    {
        public static CreditCardSummaryDto GetSummaryData(TextExtractionDto table)
        {
            var summary = GetSummaryVariablesData(table.AllText);
            summary.AddDetailDto(GetAllData(table));

            return summary;
        }

        private static CreditCardSummaryDto GetSummaryVariablesData(List<string> lines)
        {
            var dto = new SummaryVariablesDto();
            dto.DateFormat = "yyyy-MM-dd";

            for (int i = 0; i < 7; i++)
            {
                var key = lines[i].Split(":")[0];
                var value = lines[i].Split(":")[1];

                switch (key)
                {
                    case "DATE":
                        //Fecha de resumen
                        dto.Date = value;
                        continue;
                    case "DATE_EXP":
                        //Fecha de vencimiento
                        dto.DateExpiration = value;
                        continue;
                    case "DATE_NEXT":
                        //Proximo cierre
                        dto.DateNext = value;
                        continue;
                    case "DATE_NEXT_EXP":
                        //Proximo vencimiento
                        dto.DateNextExpiration = value;
                        continue;
                    case "TOTAL_ARS":
                        //Saldo total Ars
                        dto.TotalArs = value;
                        continue;
                    case "TOTAL_USD":
                        //Saldo total Usd
                        dto.TotalUsd = value;
                        continue;
                    case "MIN_PAY":
                        //Pago minimo
                        dto.MinimumPayment = value;
                        continue;
                }
            }

            return ParseDto.GetCreditCardSummaryDto(dto);
        }

        private static List<CreditCardSummaryDetailDto> GetAllData(TextExtractionDto table)
        {
            var results = new List<CreditCardSummaryDetailDto>();

            //Variables necesarias para procesar el texto
            Regex dateRegex = new Regex(@"^(\d{4}-\d{2}-\d{2})?");
            var detailType = CreditCardSummaryDetailType.Summary;

            var DateList = table.Date;
            var AmountArsList = table.AmountArs;
            var AmountUsdList = table.AmountUsd;

            //Analizo cada linea
            bool process = false;
            foreach (var line in table.AllText)
            {
                //Compruebo si la linea es un separador
                switch (line)
                {
                    case "::SUMMARY::":
                        detailType = CreditCardSummaryDetailType.Summary;
                        process = true;

                        DateList = SpvTextFilter.FilterText(table.Date, line);
                        AmountArsList = SpvTextFilter.FilterText(table.AmountArs, line);
                        AmountUsdList = SpvTextFilter.FilterText(table.AmountUsd, line);
                        continue;
                    case "::TAXES::":
                        detailType = CreditCardSummaryDetailType.Taxes;

                        DateList = SpvTextFilter.FilterText(table.Date, line);
                        AmountArsList = SpvTextFilter.FilterText(table.AmountArs, line);
                        AmountUsdList = SpvTextFilter.FilterText(table.AmountUsd, line);
                        continue;
                    case "::DETAILS::":
                        detailType = CreditCardSummaryDetailType.Details;

                        DateList = SpvTextFilter.FilterText(table.Date, line);
                        AmountArsList = SpvTextFilter.FilterText(table.AmountArs, line);
                        AmountUsdList = SpvTextFilter.FilterText(table.AmountUsd, line);
                        continue;
                }

                if (!process)
                    continue;

                //Compruebo si el ultimo valor corresponde al total en ars y usd
                if (detailType == CreditCardSummaryDetailType.Taxes)
                {
                    if (line.StartsWith(AmountArsList.Last()))
                        continue;
                }

                //Variable para almacenar el texto que va sobrando
                string restOfText = "";

                string date = "";
                string description = "";
                string installments = "";
                string amountArs = "";
                string amountUsd = "";

                //Obtengo: valor USD
                var AmountIndex = line.LastIndexOf(" ");
                var AmountValue = line.Substring(AmountIndex).Trim();
                restOfText = line.Substring(0, AmountIndex);

                //Seteo: valor USD
                if (AmountUsdList.Count > 0 && AmountUsdList[0] == AmountValue)
                {
                    amountUsd = AmountValue;
                    AmountUsdList.RemoveAt(0);

                    //Obtengo el valor de ARS
                    AmountIndex = restOfText.LastIndexOf(" ");
                    AmountValue = restOfText.Substring(AmountIndex + 1).Trim();
                }

                //Obtengo: valor ARS
                if (AmountArsList.Count > 0 && AmountArsList[0] == AmountValue)
                {
                    amountArs = AmountValue;
                    AmountArsList.RemoveAt(0);
                    restOfText = line.Substring(0, AmountIndex);
                }

                //Obtengo: Fecha
                Match dateMatch = dateRegex.Match(restOfText);
                if (DateList.Count > 0 && DateList[0] == dateMatch.Value)
                {
                    date = dateMatch.Value;
                    DateList.RemoveAt(0);
                    restOfText = restOfText.Substring(date.Length, restOfText.Length - date.Length);
                }

                //Obtengo: Cuotas
                Regex installRegex1 = new Regex(@" \d{2}/\d{2} ");
                Regex installRegex2 = new Regex(@"C\.\d{2}/\d{2} ");
                Regex installRegex3 = new Regex(@" \d{1,2}-\d{2} ");

                Match installMatch1 = installRegex1.Match($" {restOfText} ");
                Match installMatch2 = installRegex2.Match($" {restOfText} ");
                Match installMatch3 = installRegex3.Match($" {restOfText} ");

                if (detailType == CreditCardSummaryDetailType.Details)
                {
                    if (installMatch1.Success)
                    {
                        installments = installMatch1.Value;
                        restOfText = restOfText.Replace(installments.Trim(), "");
                    }
                    else if (installMatch2.Success)
                    {
                        installments = installMatch2.Value;
                        restOfText = restOfText.Replace(installments.Trim(), "");
                    }
                    else if (installMatch3.Success)
                    { 
                        installments = installMatch3.Value;
                        restOfText = restOfText.Replace(installments.Trim(), "");
                    }
                }

                //Obtengo: Descripcion
                description = restOfText.Trim();

                //Se installment type
                CreditCardSummaryDetailType type = detailType;
                if (detailType == CreditCardSummaryDetailType.Details && !string.IsNullOrEmpty(installments))
                    type = CreditCardSummaryDetailType.Installments;

                var summary = new TransactionParamsDto()
                {
                    Type = type,
                    Date = date,
                    DateFormat = "yyyy-MM-dd",
                    Description = description,
                    Installments = installments,
                    AmountArs = amountArs,
                    AmountUsd = amountUsd
                };

                results.Add(ParseDto.GetCreditCardSummaryDetailDto(summary));
            }

            return results;
        }
    }
}
