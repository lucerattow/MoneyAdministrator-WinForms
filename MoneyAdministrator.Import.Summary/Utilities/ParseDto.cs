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
            DateTime? date = null;
            decimal? amountArs = null;
            decimal? amountUsd = null;

            if (DateTimeTools.TestDate(dto.Date, dto.DateFormat))
                date = DateTimeTools.Convert(dto.Date, dto.DateFormat);

            if (!string.IsNullOrEmpty(dto.AmountArs))
                amountArs = DecimalTools.Convert(dto.AmountArs);

            if (!string.IsNullOrEmpty(dto.AmountUsd))
                amountUsd = DecimalTools.Convert(dto.AmountUsd);

            //Creo el dto y asigno los valores
            var summaryDto = new CreditCardSummaryDetailDto();
            summaryDto.Type = dto.Type;

            summaryDto.Description = dto.Description;
            summaryDto.Installments = dto.Installments;

            if (date != null)
                summaryDto.Date = (DateTime)date;

            if (amountArs != null)
                summaryDto.AmountArs = (decimal)amountArs;

            if (amountUsd != null)
                summaryDto.AmountUsd = (decimal)amountUsd;

            return summaryDto;
        }
    }
}
