using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.ImportPdfSummary.Banks.Hsbc;
using MoneyAdministrator.ImportPdfSummary.Banks.Supervielle;
using MoneyAdministrator.ImportPdfSummary.Enums;

namespace MoneyAdministrator.ImportPdfSummary
{
    public class Import
    {
        private readonly string _pdfFilePath;
        private readonly string _bankName;
        private readonly ImportFromBank _importFromBank;

        public Import(string pdfFilePath, string importFromBank) 
        {
            _pdfFilePath = pdfFilePath;
            _bankName = importFromBank;

            importFromBank = importFromBank.Replace(" ", "").ToLower();

            if (string.IsNullOrEmpty(importFromBank))
                throw new ArgumentNullException("Se requiere un nombre de banco");

            else if (importFromBank == ImportFromBank.HSBC.ToString().ToLower())
                _importFromBank = ImportFromBank.HSBC;

            else if (importFromBank == ImportFromBank.Supervielle.ToString().ToLower())
                _importFromBank = ImportFromBank.Supervielle;
        }

        public CreditCardSummaryDto ExtractTextFromPdf()
        {
            var pages = GetPageLinesFromPdf();

            switch (_importFromBank)
            {
                case ImportFromBank.HSBC:
                    return HsbcImporter.ImportFromHsbc(pages);
                case ImportFromBank.Supervielle:
                    return SupervielleImporter.ImportFromSupervielle(pages);
                default:
                    throw new Exception($"No es posible importar los resumenes de su tarjeta para el banco {_bankName}");
            }
        }

        private List<string> GetPageLinesFromPdf()
        {
            var result = new List<string>();

            using (PdfReader reader = new PdfReader(_pdfFilePath))
            using (PdfDocument pdfDocument = new PdfDocument(reader))
            {
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
                {
                    PdfPage page = pdfDocument.GetPage(i);
                    result.Add(PdfTextExtractor.GetTextFromPage(page));
                }
            }

            return result;
        }
    }
}
