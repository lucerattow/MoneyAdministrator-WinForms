using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.ImportPdfSummary.Banks.Supervielle
{
    public class SpvExtractionRegion : IRegion
    {
        public Rectangle GetDateRegion(PdfPage page)
        {
            var height = page.GetPageSize().GetHeight();
            return new Rectangle(35, 0, 35, height);
        }

        public Rectangle GetInstallmentsRegion(PdfPage page)
        {
            throw new NotImplementedException("Las tarjetas Supervielle no deben obtener las cuotas mediante una region");
        }

        public Rectangle GetArsRegion(PdfPage page)
        {
            var height = page.GetPageSize().GetHeight();
            return new Rectangle(400, 0, 80, height);
        }

        public Rectangle GetUsdRegion(PdfPage page)
        {
            var height = page.GetPageSize().GetHeight();
            return new Rectangle(480, 0, 80, height);
        }
    }
}
