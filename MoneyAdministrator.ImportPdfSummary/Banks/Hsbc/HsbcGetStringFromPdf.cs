using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using iText.Kernel.Geom;

namespace MoneyAdministrator.ImportPdfSummary.Banks.Hsbc
{
    public class HsbcGetStringFromPdf
    {
        private readonly string _filePath;

        public HsbcGetStringFromPdf(string filePath)
        {
            _filePath = filePath;
        }

        public List<string> GetAllTextFromPdf()
        {
            var result = new List<string>();

            using (var reader = new PdfReader(_filePath))
            using (var pdfDocument = new PdfDocument(reader))
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
