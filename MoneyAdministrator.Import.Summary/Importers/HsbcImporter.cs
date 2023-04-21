using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Import.Summary.DTOs;
using MoneyAdministrator.Import.Summary.Importers.TextCleaners;
using MoneyAdministrator.Import.Summary.Importers.TextExtractors;
using MoneyAdministrator.Import.Summary.Importers.TextProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Import.Summary.Importers
{
    internal static class HsbcImporter
    {
        public static CreditCardSummaryDto ExtractData(string filename, string brandName)
        {
            var result = new CreditCardSummaryDto();
            var cleanText = new HsbcTextCleaner(brandName);

            var AllText = HsbcTextExtractor.GetTextFromPDF(filename);

            //Compruebo si el resumen corresponde al banco HSBC
            var bank = AllText.Where(x => x.ToLower().Contains(Compatibility.HSBC.Name)).FirstOrDefault()?.Trim().ToLower();
            if (bank is null || Compatibility.Banks.Where(x => x.Name.Contains(bank)) == null)
                throw new Exception("El resumen importado no es del banco HSBC");

            //Compruebo si el resumen corresponde a la marca seleccionada
            var brand = AllText.Where(x => x.ToLower().Contains(brandName)).FirstOrDefault()?.Trim().ToLower();
            if (brand is null)
                throw new Exception($"El resumen importado no es de una tarjeta {brandName}");

            //Obtengo los textos con los datos necesarios
            var table = new TextExtractionDto();
            table.AllText = cleanText.CleanAllText(AllText);

            return HsbcTextProcessor.GetSummaryData(table);
        }
    }
}
