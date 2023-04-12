using MoneyAdministrator.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.ImportPdfSummary.Banks.Supervielle
{
    public static class SupervielleImporter
    {
        public static CreditCardSummaryDto ImportFromSupervielle(List<string> pages)
        {
            var lines = CleanContent.FilterTrash(pages);
            return GetValuesFromString.GetSummaryData(lines);
        }
    }
}
