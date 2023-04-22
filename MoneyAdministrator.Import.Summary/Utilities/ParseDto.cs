using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Import.Summary.DTOs;
using MoneyAdministrator.Import.Summary.Utilities.TypeTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Import.Summary.Utilities
{
    internal class ParseDto
    {
        public static CreditCardSummaryDetailDto GetCreditCardSummaryDetailDto(TransactionParamsDto dto)
        {
            //Los valores de moneda se invierten por la siguiente razon:
            //Los consumos vienen en positivo, pero realmente debe expresarse en negativo ya que son gastos
            //Los pagos vienen en negativo, pero deben mostrarse como positivos ya que estamos normalizando la deuda
            var result = new CreditCardSummaryDetailDto();

            DateTime? date = null;
            decimal? amountArs = null;
            decimal? amountUsd = null;

            if (DateTimeTools.TestDate(dto.Date, dto.DateFormat))
                date = DateTimeTools.Convert(dto.Date, dto.DateFormat);

            if (!string.IsNullOrEmpty(dto.AmountArs))
                amountArs = DecimalTools.Convert(dto.AmountArs) * -1;

            if (!string.IsNullOrEmpty(dto.AmountUsd))
                amountUsd = DecimalTools.Convert(dto.AmountUsd) * -1;

            //Creo el dto y asigno los valores
            result.Type = dto.Type;

            result.Description = dto.Description;
            result.Installments = dto.Installments;

            if (date != null)
                result.Date = (DateTime)date;

            if (amountArs != null)
                result.AmountArs = (decimal)amountArs;

            if (amountUsd != null)
                result.AmountUsd = (decimal)amountUsd;

            return result;
        }

        public static CreditCardSummaryDto GetCreditCardSummaryDto(SummaryVariablesDto dto)
        {
            //Los valores de moneda se invierten por la siguiente razon:
            //Los consumos vienen en positivo, pero realmente debe expresarse en negativo ya que son gastos
            //Los pagos vienen en negativo, pero deben mostrarse como positivos ya que estamos normalizando la deuda
            DateTime? period = null;
            DateTime? date = null;
            DateTime? dateExpiration = null;
            DateTime? dateNext = null;
            DateTime? dateNextExpiration = null;
            decimal? totalArs = null;
            decimal? totalUsd = null;
            decimal? minimumPayment = null;
            decimal? outstandingArs = null;

            //Parseo la fecha de cierre
            if (DateTimeTools.TestDate(dto.Date, dto.DateFormat))
                date = DateTimeTools.Convert(dto.Date, dto.DateFormat);

            //Obtengo el periodo en base a la fecha de cierre
            if (date != null)
            {
                DateTime dtoDate = ((DateTime)date);
                int month = dtoDate.Date.Day >= ModuleConstants.DayStartPeriod ? dtoDate.Date.Month + 1 : dtoDate.Date.Month;
                int year = dtoDate.Date.Year;
                if (month == 13)
                {
                    month = 1;
                    year++;
                }
                period = new DateTime(year, month, 1);
            }

            //Parseo la fecha de cierre
            if (DateTimeTools.TestDate(dto.DateExpiration, dto.DateFormat))
                dateExpiration = DateTimeTools.Convert(dto.DateExpiration, dto.DateFormat);

            //Parseo la fecha de cierre
            if (DateTimeTools.TestDate(dto.DateNext, dto.DateFormat))
                dateNext = DateTimeTools.Convert(dto.DateNext, dto.DateFormat);

            //Parseo la fecha de cierre
            if (DateTimeTools.TestDate(dto.DateNextExpiration, dto.DateFormat))
                dateNextExpiration = DateTimeTools.Convert(dto.DateNextExpiration, dto.DateFormat);

            //Parseo el total en pesos
            if (!string.IsNullOrEmpty(dto.TotalArs))
                totalArs = DecimalTools.Convert(dto.TotalArs) * -1;

            //Parseo el total en dolares
            if (!string.IsNullOrEmpty(dto.TotalUsd))
                totalUsd = DecimalTools.Convert(dto.TotalUsd) * -1;
            
            //Parseo el pago minimo
            if (!string.IsNullOrEmpty(dto.MinimumPayment))
                totalUsd = DecimalTools.Convert(dto.MinimumPayment) * -1;

            //Genero el objeto final
            var result = new CreditCardSummaryDto();

            if (period != null)
                result.Period = (DateTime)period;

            if (date != null)
                result.Date = (DateTime)date;

            if (dateExpiration != null)
                result.DateExpiration = (DateTime)dateExpiration;

            if (dateNext != null)
                result.DateNext = (DateTime)dateNext;

            if (dateNextExpiration != null)
                result.DateNextExpiration = (DateTime)dateNextExpiration;

            if (totalArs != null)
            {
                result.TotalArs = (decimal)totalArs;
                result.OutstandingArs = (decimal)totalArs;
            }

            if (totalUsd != null)
                result.TotalUsd = (decimal)totalUsd;

            if (minimumPayment != null)
                result.MinimumPayment = (decimal)minimumPayment;


            return result;
        }
    }
}
