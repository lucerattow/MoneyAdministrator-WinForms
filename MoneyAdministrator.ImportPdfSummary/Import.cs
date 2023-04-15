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
        private readonly string _brandName;

        public Import(string pdfFilePath, string bankName, string brandName) 
        {
            _pdfFilePath = pdfFilePath;
            _bankName = bankName.ToLower();
            _brandName = brandName.ToLower();
        }

        public CreditCardSummaryDto ExtractTextFromPdf()
        {
            switch (_bankName)
            {
                case "hsbc":
                    return HsbcImporter.ImportFromHsbc(_pdfFilePath, _brandName);
                case "supervielle":
                    return SpvImporter.ImportFromSupervielle(_pdfFilePath, _brandName);
                default:
                    throw new Exception($"No es posible importar los resumenes de su tarjeta para el banco {_bankName}");
            }
        }
    }
}
