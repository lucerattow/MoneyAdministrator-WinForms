using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.ImportPdfSummary
{
    public static class ImportConfigs
    {
        public static int dayStartPeriod = 16;

        //Supervielle
        public static string[] SupervielleBrands = new string[] { "visa", "mastercard" };
        public static string SupervielleBrandNotCompatible = "El resumen ingresado no corresponde a una tarjeta Supervielle <brandName>";
    }
}
