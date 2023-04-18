using iText.Svg.Renderers.Impl;
using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.DTOs.Enums;
using MoneyAdministrator.ImportPdfSummary.Dtos;
using MoneyAdministrator.Module.ImportHsbcSummary.Utilities;
using System.Text.RegularExpressions;

namespace MoneyAdministrator.ImportPdfSummary.Banks.Supervielle
{
    public class SpvGetValuesFromString
    {
        private string _brandName;

        private SpvGetValuesFromString(string brandName)
        {
            _brandName = brandName;
        }

        public static CreditCardSummaryDto GetSummaryData(string brandName, TransactionsTableDto table)
        {
            var getValuesFromString = new SpvGetValuesFromString(brandName);
            var summary = getValuesFromString.GetSummaryMetaData(table);
            summary.AddDetailDto(getValuesFromString.GetPrincipalSummaryData(table));
            summary.AddDetailDto(getValuesFromString.GetDetailsData(table));
            summary.AddDetailDto(getValuesFromString.GetTaxesAndMaintenanceData(table));

            return summary;
        }

        private CreditCardSummaryDto GetSummaryMetaData(TransactionsTableDto table)
        {
            var summary = new CreditCardSummaryDto();
            var lines = new SpvCleanContent(_brandName).GetSummaryMetaData(table.AllText);

            for (int i = 0; i < lines.Count; i++)
            {
                //Compruebo: La marca de la tarjeta
                if (i == 0)
                    if (!lines[i].ToLower().Contains(_brandName))
                        throw new Exception($"El resumen ingresado no corresponde a una tarjeta Supervielle {_brandName}");

                //Obtengo: Fecha cierre, Periodo
                if (i == 1)
                {
                    //Fecha de cierre
                    var index = lines[i].IndexOf(":");
                    var date = lines[i].Substring(index + 1).Trim();
                    summary.Date = DateTimeTools.ConvertToDateTime(date, "yyyy-MM-dd");

                    //Periodo
                    int month = (summary.Date.Day >= ImportConfigs.dayStartPeriod) ? summary.Date.Month + 1 : summary.Date.Month;
                    int year = summary.Date.Year;

                    if (month == 13)
                    {
                        month = 1;
                        year++;
                    }

                    summary.Period = new DateTime(year, month, 1);
                }

                //Obtengo: Fecha vencimiento, Pago minimo
                if (i == 2)
                {
                    //Fecha vencimiento
                    var date = lines[i].Split(" ").ToList().Where(x => !string.IsNullOrEmpty(x)).ToList()[0].Trim();
                    summary.Expiration = DateTimeTools.ConvertToDateTime(date, "yyyy-MM-dd");

                    //Pago minimo
                    var minimumPayment = lines[i].Split(" ").ToList().Where(x => !string.IsNullOrEmpty(x)).ToList()[3].Trim();
                    summary.MinimumPayment = decimalTools.ParseDecimal(minimumPayment);
                }

                //Obtengo: Proximo cierre, Proximo vencimiento
                if (i == 3)
                {
                    var index = lines[i].IndexOf(":");
                    var nextDate = lines[i].Substring(index + 1, 11).Trim();
                    summary.NextDate = DateTimeTools.ConvertToDateTime(nextDate, "yyyy-MM-dd");

                    index = lines[i].LastIndexOf(":");
                    var nextExpiration = lines[i].Substring(index + 1).Trim();
                    summary.NextExpiration = DateTimeTools.ConvertToDateTime(nextExpiration, "yyyy-MM-dd");
                }
            }

            return summary;
        }

        private List<CreditCardSummaryDetailDto> GetPrincipalSummaryData(TransactionsTableDto table)
        {
            var results = new List<CreditCardSummaryDetailDto>();

            var spvCleanContent = new SpvCleanContent(_brandName);
            var lines = spvCleanContent.GetPrincipalSummaryDataFromString(table.AllText);
            var dateLines = spvCleanContent.GetPrincipalSummaryDataFromSectionsString(table.Date);
            var arsLines = spvCleanContent.GetPrincipalSummaryDataFromSectionsString(table.Ars);
            var usdLines = spvCleanContent.GetPrincipalSummaryDataFromSectionsString(table.Usd);

            var previousLineDate = false;
            for (int i = 0; i < lines.Count; i++)
            {
                var skipNextLine = false;
                var validDate = false;
                var lastIndex = 0;
                var date = "";
                var value = "";
                var aux = "";
                var dateReg = new Regex("^\\d{4}-\\d{2}-\\d{2}$");

                //Compruebo si la linea actual es una fecha mal impresa:
                if (dateReg.IsMatch(lines[i].Trim()))
                {
                    previousLineDate = true;
                    continue;
                }

                var detailDto = new CreditCardSummaryDetailDto();
                detailDto.Type = CreditCardSummaryDetailType.Summary;

                //Obtengo: valor USD
                lastIndex = lines[i].LastIndexOf(" ");
                value = lines[i].Substring(lastIndex).Trim();
                aux = lines[i].Substring(0, lastIndex);

                //Seteo: valor USD
                if (usdLines.Count > 0 && usdLines[0] == value)
                {
                    detailDto.AmountUsd = decimalTools.ParseDecimal(value);
                    usdLines.Remove(usdLines[0]);

                    //Obtengo el valor de ARS
                    lastIndex = aux.LastIndexOf(" ");
                    value = aux.Substring(lastIndex + 1).Trim();
                }

                //Obtengo: valor ARS
                if (arsLines.Count > 0 && arsLines[0] == value)
                {
                    detailDto.AmountArs = decimalTools.ParseDecimal(value);
                    arsLines.Remove(arsLines[0]);
                    aux = lines[i].Substring(0, lastIndex);
                }

                //saldo anterior
                if (i == 0)
                {
                    //Obtengo: Descripcion
                    detailDto.Description = aux.Trim();

                    detailDto.Date = new DateTime(1, 1, 1);
                    detailDto.Installments = "";

                    results.Add(detailDto);
                    continue;
                }

                //Obtengo: Fecha
                if (previousLineDate && dateReg.IsMatch(lines[i - 1].Trim()))
                {
                    validDate = true;
                    date = lines[i - 1].Trim();
                    previousLineDate = false;
                }
                else if (dateReg.IsMatch(lines[i].Substring(0, 10)))
                {
                    validDate = true;
                    date = aux.Substring(0, 10).Trim();
                    aux = aux.Substring(10).Trim();
                }
                else if (dateReg.IsMatch(lines[i + 1].Trim()))
                {
                    validDate = true;
                    skipNextLine = true;
                    if ((i + 1) <= lines.Count)
                        date = lines[i + 1].Trim();
                    else
                        detailDto.Date = new DateTime(1, 1, 1);
                }

                if (validDate && dateLines.Count > 0 && dateLines[0] == date)
                {
                    detailDto.Date = DateTimeTools.ConvertToDateTime(date, "yyyy-MM-dd");
                    dateLines.Remove(dateLines[0]);
                }

                //Obtengo: Descripcion
                detailDto.Description = aux.Trim();

                detailDto.Installments = "";

                results.Add(detailDto);

                if (skipNextLine)
                    i++;
            }

            return results;
        }

        private List<CreditCardSummaryDetailDto> GetDetailsData(TransactionsTableDto table)
        {
            var results = new List<CreditCardSummaryDetailDto>();

            var spvCleanContent = new SpvCleanContent(_brandName);
            var lines = spvCleanContent.GetDetailsDataFromString(table.AllText);
            var dateLines = spvCleanContent.GetDetailsDataFromCurrencyString(table.Date);
            var arsLines = spvCleanContent.GetDetailsDataFromCurrencyString(table.Ars);
            var usdLines = spvCleanContent.GetDetailsDataFromCurrencyString(table.Usd);

            var previousLineDate = false;
            for (int i = 0; i < lines.Count; i++)
            {
                var skipNextLine = false;
                var validDate = false;
                var lastIndex = 0;
                var date = "";
                var value = "";
                var aux = "";
                var dateReg = new Regex("^\\d{4}-\\d{2}-\\d{2}$");

                var detailDto = new CreditCardSummaryDetailDto();
                detailDto.Type = CreditCardSummaryDetailType.Details;

                //Obtengo: valor USD
                lastIndex = lines[i].LastIndexOf(" ");
                value = lines[i].Substring(lastIndex).Trim();
                aux = lines[i].Substring(0, lastIndex);

                if (usdLines.Count > 0 && usdLines[0] == value)
                {
                    detailDto.AmountUsd = decimalTools.ParseDecimal(value);
                    usdLines.Remove(usdLines[0]);

                    //Obtengo el valor de ARS
                    lastIndex = aux.LastIndexOf(" ");
                    value = aux.Substring(lastIndex + 1).Trim();
                }

                //Obtengo: valor ARS
                if (arsLines.Count > 0 && arsLines[0] == value)
                {
                    detailDto.AmountArs = decimalTools.ParseDecimal(value);
                    arsLines.Remove(arsLines[0]);
                }

                //Obtengo: Fecha
                if (previousLineDate && dateReg.IsMatch(lines[i - 1].Trim()))
                {
                    validDate = true;
                    date = lines[i - 1].Trim();
                    previousLineDate = false;
                }
                else if (dateReg.IsMatch(lines[i].Substring(0, 10)))
                {
                    validDate = true;
                    date = aux.Substring(0, 10).Trim();
                    aux = aux.Substring(10).Trim();
                }
                else if (dateReg.IsMatch(lines[i + 1]))
                {
                    validDate = true;
                    skipNextLine = true;
                    if ((i + 1) <= lines.Count)
                        date = lines[i + 1].Trim();
                    else
                        detailDto.Date = new DateTime(1, 1, 1);
                }

                if (validDate && dateLines.Count > 0 && dateLines[0] == date)
                {
                    detailDto.Date = DateTimeTools.ConvertToDateTime(date, "yyyy-MM-dd");
                    dateLines.Remove(dateLines[0]);
                }

                //Obtengo: Descripcion
                detailDto.Description = aux.Trim();

                detailDto.Installments = "";

                results.Add(detailDto);

                if (skipNextLine)
                    i++;
            }

            return results;
        }

        private List<CreditCardSummaryDetailDto> GetTaxesAndMaintenanceData(TransactionsTableDto table)
        {
            var results = new List<CreditCardSummaryDetailDto>();

            var spvCleanContent = new SpvCleanContent(_brandName);
            var lines = spvCleanContent.GetTaxesAndMaintenanceDataFromString(table.AllText);
            var arsLines = spvCleanContent.GetTaxesAndMaintenanceDataFromCurrencyString(table.Ars);
            var usdLines = spvCleanContent.GetTaxesAndMaintenanceDataFromCurrencyString(table.Usd);

            for (int i = 0; i < lines.Count; i++)
            {
                var lastIndex = 0;
                var value = "";
                var aux = "";
                var dateReg = new Regex("^\\d{4}-\\d{2}-\\d{2}$");

                var detailDto = new CreditCardSummaryDetailDto();
                detailDto.Type = CreditCardSummaryDetailType.TaxesAndMaintenance;

                //Compruebo si la linea final es la linea de SALDO TOTAL
                if (i + 1 == lines.Count)
                {
                    if (lines[i].Contains(arsLines.Last()))
                        continue;
                }

                //Compruebo si la linea actual es una fecha mal impresa:
                if (dateReg.IsMatch(lines[i].Trim()))
                    continue;

                //Obtengo: valor USD
                lastIndex = lines[i].LastIndexOf(" ");
                value = lines[i].Substring(lastIndex).Trim();
                aux = lines[i].Substring(0, lastIndex);

                if (usdLines.Count > 0 && usdLines[0] == value)
                {
                    detailDto.AmountUsd = decimalTools.ParseDecimal(value);
                    usdLines.Remove(usdLines[0]);

                    //Obtengo el valor de ARS
                    lastIndex = aux.LastIndexOf(" ");
                    value = aux.Substring(lastIndex + 1).Trim();
                }

                //Obtengo: valor ARS
                if (arsLines.Count > 0 && arsLines[0] == value)
                {
                    detailDto.AmountArs = decimalTools.ParseDecimal(value);
                    arsLines.Remove(arsLines[0]);
                }

                //Obtengo: Descripcion
                detailDto.Description = aux.Trim();

                detailDto.Date = new DateTime(1, 1, 1);
                detailDto.Installments = "";

                results.Add(detailDto);
            }

            return results;
        }
    }
}
