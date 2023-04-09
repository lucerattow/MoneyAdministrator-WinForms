using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using MoneyAdministrator.DTOs;
using System.Globalization;
using MoneyAdministrator.Module.ImportHsbcSummary.Utilities;

namespace MoneyAdministrator.Module.ImportHsbcSummary
{
    public class Import
    {
        private readonly string _pdfFilePath;

        private Import(string pdfFilePath) 
        {
            _pdfFilePath = pdfFilePath;
        }

        private CreditCardSummaryDto ExtractTextFromPdf()
        {
            var ccSummary = new CreditCardSummaryDto();

            using (PdfReader reader = new PdfReader(_pdfFilePath))
            using (PdfDocument pdfDocument = new PdfDocument(reader))
            {
                List<string> pagesContent = new List<string>();
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
                {
                    PdfPage page = pdfDocument.GetPage(i);
                    pagesContent.Add(PdfTextExtractor.GetTextFromPage(page));
                }

                ccSummary = GetCreditCardSummary(pagesContent);
            }

            return ccSummary;
        }

        private CreditCardSummaryDto GetCreditCardSummary(List<string> pages)
        {
            var ccSummary = new CreditCardSummaryDto();
            pages = GetValuesFromString.FilteredHeaderAndFooter(pages);

            ccSummary = GetValuesFromString.GetGeneralSummaryData(pages[0]);

            var consolidateSummary = GetValuesFromString.GetConsolidatedSummary(pages[0]);

            return ccSummary;
        }

        //tengo que usar este metodo
        public static CreditCardSummaryDto GetCreditCardSummaryDto(string pdfFilePath)
        {
            return new Import(pdfFilePath).ExtractTextFromPdf();
        }
    }

    public class CreditCardSummaryDto2
    {
        public int Id { get; set; }
        public DateTime Period { get; set; }
        public DateTime Date { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime NextDate { get; set; }
        public DateTime NextExpiration { get; set; }
        public decimal minimumPayment { get; set; }
        public List<CreditCardSummaryDetailDto> CreditCardSummaries { get; set; }
    }
}
