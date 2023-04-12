using MoneyAdministrator.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.DTOs.Interfaces
{
    public interface IPluginImportSummary
    {
        string Name { get; }
        string BankName { get; }
        string Description { get; }
        string Version { get; }
        CreditCardSummaryDto GetDataFromPdf(string pdfFilePath);
    }
}
