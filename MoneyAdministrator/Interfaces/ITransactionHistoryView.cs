using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Common.Utilities.TypeTools;
using MoneyAdministrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Interfaces
{
    public interface ITransactionHistoryView
    {
        //properties//properties
        TransactionViewDto? SelectedDto { get; set; }
        TransactionViewDto? CheckBoxChangeDto { get; }

        //properties fields
        string EntityName { get; set; }
        DateTime Date { get; set; }
        string Description { get; set; }
        decimal Amount { get; set; }
        Currency Currency { get; set; }

        //properties installments
        bool IsInstallment { get; set; }
        int InstallmentCurrent { get; set; }
        int InstallmentMax { get; set; }

        //properties service
        bool IsService { get; set; }
        int Frequency { get; set; }

        //methods
        /// <summary>Envio los datos de las monedas al ComboBox de transaccionHistory</summary>
        /// <param name="datasource">Lista de monedas</param>
        void SetCurrenciesList(List<Currency> datasource);
        /// <summary>Envio los datos de las transacciones al GRD de transaccionHistory</summary>
        /// <param name="datasource">DTOs para rellenar la GRD de transaccionHistory</param>
        void GrdRefreshData(List<TransactionViewDto> datasource);
        /// <summary>Envio los datos de la nueva transaccion insertada</summary>
        /// <param name="datasource">DTO a insertar en la GRD de transaccionHistory</param>
        void GrdInsertRows(List<TransactionViewDto> dto);
        /// <summary>Elimina el detalle seleccionado y los relacionados</summary>
        void GrdDeleteSelected(bool deleteSeparators = true);

        //events
        event EventHandler ButtonInsertClick;
        event EventHandler ButtonNewPayClick;
        event EventHandler ButtonUpdateClick;
        event EventHandler ButtonDeleteClick;
        event EventHandler ButtonExitClick;
        event EventHandler ButtonEntitySearchClick;
        event EventHandler GrdDoubleClick;
        event EventHandler GrdValueChange;
    }
}
