using MoneyAdministrator.DTOs;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Interfaces
{
    public interface ICreditCardView
    {
        //methods
        /// <summary>Envio los datos de los tipos de tarjeta de credito al ComboBox</summary>
        /// <param name="datasource">Lista de tarjetas de credito</param>
        void SetCreditCardTypeList(List<CreditCardType> datasource);
        /// <summary>Envio los datos de las tarjetas de credito al GRD</summary>
        /// <param name="dataSource">DTOs para rellenar la GRD</param>
        void GrdRefreshData(List<CreditCardDto> dataSource);

        //functions
        /// <summary>Obtengo el ID seleccionado</summary>
        int SelectedId { get; set; }
        /// <summary>Obtengo el nombre del banco</summary>
        string EntityName { get; set; }
        /// <summary>Obtengo el tipo de tarjeta de credito</summary>
        CreditCardType SelectedCreditCardType { get; set; }
        /// <summary>Obtengo los ultimos 4 numeros de la tarjeta</summary>
        int LastFourNumbers { get; set; }
        /// <summary>Retorna el resultado del ShowDialog</summary>
        DialogResult DialogResult { get; set; }

        //functions
        /// <summary>Muestra el formulario como Dialogo</summary>
        DialogResult ShowDialog();

        //events
        event EventHandler GrdDoubleClick;
        event EventHandler ButtonSelectClick;
        event EventHandler ButtonInsertClick;
        event EventHandler ButtonUpdateClick;
        event EventHandler ButtonDeleteClick;
        event EventHandler ButtonEntitySearchClick;
    }
}
