using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.ImportPdfSummary.Banks.Hsbc
{
    public class HsbcExtractionRegion : IRegion
    {
        public Rectangle GetDateRegion(PdfPage page)
        {
            throw new NotImplementedException("Las tarjetas de credito HSBC no obtienen las fechas mediante una region");
        }

        public Rectangle GetArsRegion(PdfPage page)
        {
            var height = page.GetPageSize().GetHeight();
            return new Rectangle(440, 0, 20, height);
        }

        public Rectangle GetUsdRegion(PdfPage page)
        {
            var height = page.GetPageSize().GetHeight();
            return new Rectangle(480, 0, 80, height);
        }
    }
}
