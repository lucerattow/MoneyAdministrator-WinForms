using iText.Kernel.Geom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Import.Summary.Importers.PdfRegions
{
    internal static class HsbcPdfRegion
    {
        public static Rectangle GetPageFirst(float width) => new(0, 130, width, 480);
        //Par
        public static Rectangle GetPageEven(float width) => new(0, 0, width, 780);
        //Impar
        public static Rectangle GetPageOdd(float width) => new(0, 0, width, 620);
    }
}
