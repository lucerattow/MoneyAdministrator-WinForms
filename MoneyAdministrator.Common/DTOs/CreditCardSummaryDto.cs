using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Common.DTOs
{
    public class CreditCardSummaryDto
    {
        public int Id { get; set; }
        public DateTime Period { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateExpiration { get; set; }
        public DateTime DateNext { get; set; }
        public DateTime DateNextExpiration { get; set; }
        public decimal TotalArs { get; set; }
        public decimal TotalUsd { get; set; }
        public decimal MinimumPayment { get; set; }
        public decimal OutstandingArs { get; set; }
        public List<CreditCardSummaryDetailDto> CreditCardSummaryDetails { get; set; }

        public void AddDetailDto(List<CreditCardSummaryDetailDto> creditCardSummaryDetailDto)
        {
            creditCardSummaryDetailDto.ForEach(x => AddDetailDto(x));
        }

        public void AddDetailDto(CreditCardSummaryDetailDto creditCardSummaryDetailDto)
        {
            if (CreditCardSummaryDetails == null)
                CreditCardSummaryDetails = new List<CreditCardSummaryDetailDto>();

            CreditCardSummaryDetails.Add(creditCardSummaryDetailDto);
        }
    }
}
