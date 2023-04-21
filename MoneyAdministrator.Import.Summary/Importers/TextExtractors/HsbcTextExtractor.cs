using iText.Kernel.Pdf.Canvas.Parser.Filter;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyAdministrator.Import.Summary.Importers.PdfRegions;

namespace MoneyAdministrator.Import.Summary.Importers.TextExtractors
{
    internal static class HsbcTextExtractor
    {
        public static List<string> GetTextFromPDF(string filename)
        {
            var pagesText = new List<string>();

            using (var pdfDocument = new PdfDocument(new PdfReader(filename)))
            {
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
                {
                    PdfPage page = pdfDocument.GetPage(i);

                    if (i == 1)
                    {
                        var filter = new IEventFilter[1];
                        var width = page.GetPageSize().GetWidth();
                        var region = HsbcPdfRegion.GetPageFirst(width);
                        filter[0] = new TextRegionEventFilter(region);
                        var filteredTextEventListener = new FilteredTextEventListener(new LocationTextExtractionStrategy(), filter);

                        pagesText.Add(PdfTextExtractor.GetTextFromPage(page, filteredTextEventListener));
                    }
                    else if (i % 2 == 0)
                    {
                        var filter = new IEventFilter[1];
                        var width = page.GetPageSize().GetWidth();
                        var region = HsbcPdfRegion.GetPageEven(width);
                        filter[0] = new TextRegionEventFilter(region);
                        var filteredTextEventListener = new FilteredTextEventListener(new LocationTextExtractionStrategy(), filter);

                        pagesText.Add(PdfTextExtractor.GetTextFromPage(page, filteredTextEventListener));
                    }
                    else if (i % 2 != 0)
                    {
                        var filter = new IEventFilter[1];
                        var width = page.GetPageSize().GetWidth();
                        var region = HsbcPdfRegion.GetPageOdd(width);
                        filter[0] = new TextRegionEventFilter(region);
                        var filteredTextEventListener = new FilteredTextEventListener(new LocationTextExtractionStrategy(), filter);

                        pagesText.Add(PdfTextExtractor.GetTextFromPage(page, filteredTextEventListener));
                    }
                }
            }

            return GetPagesInLines(pagesText);
        }

        private static List<string> GetPagesInLines(List<string> pages)
        {
            var result = new List<string>();

            foreach (var page in pages)
                result.AddRange(page.Split("\n"));

            return result;
        }
    }
}
