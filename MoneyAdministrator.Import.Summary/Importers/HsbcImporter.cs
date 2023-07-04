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
            //Extraigo el texto de los pdf
            var textExtractor = new HsbcTextExtractor(filename, brandName);
            var dto = textExtractor.GetDataFromPDF();

            //Compruebo si el resumen corresponde al banco HSBC
            var bank = dto.AllText.Where(x => x.ToLower().Contains(Compatibility.HSBC.Name)).FirstOrDefault()?.Trim().ToLower();
            if (bank is null || Compatibility.Banks.Where(x => x.Name.Contains(bank)) == null)
                throw new Exception("El resumen importado no es del banco HSBC");

            //Compruebo si el resumen corresponde a la marca seleccionada
            var brand = dto.AllText.Where(x => x.ToLower().Contains(brandName)).FirstOrDefault()?.Trim().ToLower();
            if (brand is null)
                throw new Exception($"El resumen importado no es de una tarjeta {brandName}");

            //Filtro texto basura o que no es relevante
            var cleanText = new HsbcTextCleaner(brandName);
            dto.AllText = cleanText.CleanAllText(dto.AllText);

            //Proceso toda la data y la represento en un objeto
            var textProcessor = new HsbcTextProcessor(brandName);
            return textProcessor.GetSummaryData(dto);
        }
    }
}
