using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.ImportPdfSummary.Banks.Supervielle;
using MoneyAdministrator.ImportPdfSummary.Dtos;

namespace MoneyAdministrator.ImportPdfSummary.Banks.Hsbc
{
    public static class HsbcImporter
    {
        public static CreditCardSummaryDto ImportFromHsbc(string pdfPathFile, string brandName)
        {
            var getStringFromPdf = new HsbcGetStringFromPdf(pdfPathFile);
            var pages = getStringFromPdf.GetAllTextFromPdf();
            var lines = HsbcCleanContent.FilterTrash(pages);
            return HsbcGetValuesFromString.GetSummaryData(lines);
        }
    }
}
