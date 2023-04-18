using iText.Kernel.Geom;
using iText.Kernel.Pdf;

namespace MoneyAdministrator.ImportPdfSummary
{
    public interface IRegion
    {
        Rectangle GetDateRegion(PdfPage page);
        Rectangle GetArsRegion(PdfPage page);
        Rectangle GetUsdRegion(PdfPage page);
    }
}
