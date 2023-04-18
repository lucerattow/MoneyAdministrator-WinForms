using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using iText.Kernel.Geom;
using iText.Pdfa;

namespace MoneyAdministrator.ImportPdfSummary.Banks.Supervielle
{
    public class SpvGetStringFromPdf
    {
        private readonly string _filePath;
        private readonly IRegion _regions;

        public SpvGetStringFromPdf(string filePath, IRegion regions)
        {
            _filePath = filePath;
            _regions = regions;
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

        public List<string> GetDateFromPdf()
        {
            var result = new List<string>();

            using (var reader = new PdfReader(_filePath))
            using (var pdfDocument = new PdfDocument(reader))
            {
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
                {
                    PdfPage page = pdfDocument.GetPage(i);
                    Rectangle rec = _regions.GetDateRegion(page);
                    result.Add(GetRegionFromPdf(page, rec));
                }
            }

            return result;
        }

        public List<string> GetArsFromPdf()
        {
            var result = new List<string>();

            using (var reader = new PdfReader(_filePath))
            using (var pdfDocument = new PdfDocument(reader))
            {
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
                {
                    PdfPage page = pdfDocument.GetPage(i);
                    Rectangle rec = _regions.GetArsRegion(page);
                    result.Add(GetRegionFromPdf(page, rec));
                }
            }

            return result;
        }

        public List<string> GetUsdFromPdf()
        {
            var result = new List<string>();

            using (var reader = new PdfReader(_filePath))
            using (var pdfDocument = new PdfDocument(reader))
            {
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
                {
                    PdfPage page = pdfDocument.GetPage(i);
                    Rectangle rec = _regions.GetUsdRegion(page);
                    result.Add(GetRegionFromPdf(page, rec));
                }
            }

            return result;
        }

        private string GetRegionFromPdf(PdfPage page, Rectangle region)
        {
            var filter = new IEventFilter[1];
            filter[0] = new TextRegionEventFilter(region);
            var filteredTextEventListener = new FilteredTextEventListener(new LocationTextExtractionStrategy(), filter);

            return PdfTextExtractor.GetTextFromPage(page, filteredTextEventListener);
        }
    }
}
