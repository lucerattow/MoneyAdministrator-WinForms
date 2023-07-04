using iText.Kernel.Pdf.Canvas.Parser.Filter;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyAdministrator.Import.Summary.Importers.PdfRegions;
using MoneyAdministrator.Import.Summary.DTOs;

namespace MoneyAdministrator.Import.Summary.Importers.TextExtractors
{
    internal class HsbcTextExtractor
    {
        private string _filename;
        private string _brandname;

        public HsbcTextExtractor(string filename, string brandname)
        { 
            _filename = filename;
            _brandname = brandname;
        }

        public TextExtractionDto GetDataFromPDF()
        {
            var result = new TextExtractionDto();

            if (Brand.Mastercard == _brandname)
                result.AllText = GetTextFromPDF(GetZoneFilterMaster);
            else 
                result.AllText = GetTextFromPDF(GetZoneFilterVisa);

            return result;
        }

        private List<string> GetTextFromPDF(Func<int, float, float, TextRegionEventFilter> getZoneFilterMaster)
        {
            var pagesText = new List<string>();

            using (var pdfDocument = new PdfDocument(new PdfReader(_filename)))
            {
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
                {
                    PdfPage page = pdfDocument.GetPage(i);

                    //Inicializo el filtro de extraccion del texto
                    var filter = new IEventFilter[1];
                    var width = page.GetPageSize().GetWidth();
                    var height = page.GetPageSize().GetHeight();

                    filter[0] = getZoneFilterMaster(i, width, height);

                    var filteredTextEventListener = new FilteredTextEventListener(new LocationTextExtractionStrategy(), filter);
                    pagesText.Add(PdfTextExtractor.GetTextFromPage(page, filteredTextEventListener));
                }
            }

            return GetPagesInLines(pagesText);
        }

        private TextRegionEventFilter GetZoneFilterMaster(int page, float width, float height)
        {
            //Configuro la zona de extraccion de texto
            if (page == 1)
            {
                //Primera pagina
                var region = HsbcPdfRegion.GetPageMasterFirst(width);
                return new TextRegionEventFilter(region);
            }
            else if (page % 2 == 0)
            {
                //Paginas Par
                var region = HsbcPdfRegion.GetPageMasterEven(width);
                return new TextRegionEventFilter(region);
            }
            else
            {
                //Paginas Impar
                var region = HsbcPdfRegion.GetPageMasterOdd(width);
                return new TextRegionEventFilter(region);
            }
        }

        private TextRegionEventFilter GetZoneFilterVisa(int page, float width, float height)
        {
            //Configuro la zona de extraccion de texto
            if (page == 1)
            {
                //Primera pagina
                var region = HsbcPdfRegion.GetPageVisaFirst(width);
                return new TextRegionEventFilter(region);
            }
            else
            {
                //Paginas siguientes
                var region = HsbcPdfRegion.GetPageVisa(width);
                return new TextRegionEventFilter(region);
            }
        }

        private List<string> GetPagesInLines(List<string> pages)
        {
            var result = new List<string>();

            foreach (var page in pages)
                result.AddRange(page.Split("\n"));

            return result;
        }
    }
}
