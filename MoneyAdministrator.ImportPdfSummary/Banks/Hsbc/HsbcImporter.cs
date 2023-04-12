using MoneyAdministrator.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.ImportPdfSummary.Banks.Hsbc
{
    public static class HsbcImporter
    {
        public static CreditCardSummaryDto ImportFromHsbc(List<string> pages)
        {
            var lines = CleanContent.FilterTrash(pages);
            return GetValuesFromString.GetSummaryData(lines);
        }
    }
}
