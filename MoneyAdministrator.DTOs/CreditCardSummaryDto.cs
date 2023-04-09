using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.DTOs
{
    public class CreditCardSummaryDto
    {
        public int Id { get; set; }
        public DateTime Period { get; set; }
        public DateTime Date { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime NextDate { get; set; }
        public DateTime NextExpiration { get; set; }
        public decimal minimumPayment { get; set; }
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
