using MoneyAdministrator.DTOs;
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
        //properties
        /// <summary>Obtengo el ID seleccionado</summary>
        int SelectedId { get; set; }
        /// <summary>Obtengo la fecha</summary>
        DateTime Date { get; set; }
        /// <summary>Obtengo el nombre de la entidad</summary>
        string EntityName { get; set; }
        /// <summary>Obtengo la descripcion</summary>
        string Description { get; set; }
        /// <summary>Obtengo el monto</summary>
        decimal Amount { get; set; }
        /// <summary>Obtengo la moneda seleccioanda</summary>
        Currency Currency { get; set; }

        /// <summary>Obtengo la cuota actual del registro</summary>
        int InstallmentCurrent { get; set; }
        /// <summary>Obtengo la cantidad de cuotas maxima</summary>
        int InstallmentMax { get; set; }
        /// <summary>Obtengo la frecuencia de repeticion del servicio</summary>
        int Frequency { get; set; }

        /// <summary>Indica si la transaccion es un servicio</summary>
        bool IsService { get; set; }
        /// <summary>Indica si la transaccion es nueva o si se esta editando</summary>
        bool Editing { get; set; }

        //methods
        /// <summary>Envio los datos de las monedas al ComboBox de transaccionHistory</summary>
        /// <param name="datasource">Lista de monedas</param>
        void SetCurrenciesList(List<Currency> datasource);
        /// <summary>Envio los datos de las transacciones al GRD de transaccionHistory</summary>
        /// <param name="datasource">DTOs para rellenar la GRD de transaccionHistory</param>
        void GrdRefreshData(List<TransactionDto> datasource);
        /// <summary>Ejecuto la logica de botones</summary>
        void ButtonsLogic();

        //events
        event EventHandler GrdDoubleClick;
        event EventHandler ButtonInsertClick;
        event EventHandler ButtonUpdateClick;
        event EventHandler ButtonDeleteClick;
        event EventHandler ButtonExitClick;
        event EventHandler SelectedYearChange;
        event EventHandler ButtonEntitySearchClick;
    }
}
