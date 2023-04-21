using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Import.Summary.Importers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Import.Summary
{
    public class Execute
    {
        private string _bankName;
        private string _brandName;

        public Execute(string bankName, string brandName)
        {
            _bankName = bankName.ToLower();
            _brandName = brandName.ToLower();
        }

        public CreditCardSummaryDto ExtractDataFromPDF(string filename)
        {
            //Compruebo si es posible importar resumenes del banco seleccionado
            var bank = Compatibility.Banks.Where(x => x.Name == _bankName).FirstOrDefault();
            if (bank is null)
                throw new Exception($"No es posible importar tarjetas del banco {_bankName}");

            //Compruebo si es posible importar resumenes de la marca para este banco
            if (!bank.Brands.Contains(_brandName))
                throw new Exception($"El banco {_brandName} no soporta tarjetas {_brandName}");

            //Importo el resumen
            if (_bankName == Compatibility.HSBC.Name)
                return HsbcImporter.ExtractData(filename, _brandName);
            if (_bankName == Compatibility.Supervielle.Name)
                return SpvImporter.ExtractData(filename, _brandName);
            else
                throw new Exception($"No es posible importar tarjetas del banco {_bankName}");
        }
    }
}
