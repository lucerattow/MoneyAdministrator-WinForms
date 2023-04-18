using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.ImportPdfSummary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.ImportPdfSummary.Banks.Supervielle
{
    public class SpvImporter
    {
        private string _brandName;

        public SpvImporter(string brandName)
        {
            _brandName = brandName;
        }

        public static CreditCardSummaryDto ImportFromSupervielle(string pdfFilePath, string brandName)
        {
            if (!ImportConfigs.SupervielleBrands.Contains(brandName))
                throw new Exception($"La marca de tarjeta de credito {brandName} no esta soportada para tarjetas Supervielle");

            //Inicializo las clases necesarias
            var importer = new SpvImporter(brandName);
            var getStringFromPdf = new SpvGetStringFromPdf(pdfFilePath, new SpvExtractionRegion());

            //Inicializo el dto para guardar los textos
            var tableDto = new TransactionsTableDto();
            tableDto.AllText = getStringFromPdf.GetAllTextFromPdf();
            tableDto.Date = getStringFromPdf.GetDateFromPdf();
            tableDto.Ars = getStringFromPdf.GetArsFromPdf();
            tableDto.Usd = getStringFromPdf.GetUsdFromPdf();

            return importer.ImportFromText(tableDto);
        }

        public CreditCardSummaryDto ImportFromText(TransactionsTableDto table)
        {
            //Inicializo las clases necesarias
            var cleanContent = new SpvCleanContent(_brandName);

            table.AllText = cleanContent.NormalizePages(table.AllText);
            table.Date = cleanContent.NormalizeDatePages(table.Date);
            table.Ars = cleanContent.NormalizeArsPages(table.Ars);
            table.Usd = cleanContent.NormalizeUsdPages(table.Usd);

            return SpvGetValuesFromString.GetSummaryData(_brandName, table);
        }
    }
}
