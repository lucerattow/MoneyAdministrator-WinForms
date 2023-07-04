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
        //Visa

        /// <summary>Obtiene la region que se desea extraer del pdf (Esta pensado para la primera pagina de Mastercard)</summary>
        public static Rectangle GetPageVisaFirst(float width) => new(0, 130, width, 560);

        /// <summary>Obtiene la region que se desea extraer del pdf (Esta pensado para las paginas PAR de Mastercard)</summary>
        public static Rectangle GetPageVisa(float width) => new(0, 0, width, 780);
        //Mastercard

        /// <summary>Obtiene la region que se desea extraer del pdf (Esta pensado para la primera pagina de Mastercard)</summary>
        public static Rectangle GetPageMasterFirst(float width) => new(0, 130, width, 480);

        /// <summary>Obtiene la region que se desea extraer del pdf (Esta pensado para las paginas PAR de Mastercard)</summary>
        public static Rectangle GetPageMasterEven(float width) => new(0, 0, width, 780);

        /// <summary>Obtiene la region que se desea extraer del pdf (Esta pensado para las paginas INPAR de Mastercard)</summary>
        public static Rectangle GetPageMasterOdd(float width) => new(0, 0, width, 620);
    }
}
