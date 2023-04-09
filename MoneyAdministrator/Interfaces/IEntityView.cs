using MoneyAdministrator.DTOs;
using MoneyAdministrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Interfaces
{
    public interface IEntityView
    {
        //field
        /// <summary>Obtengo el ID seleccionado</summary>
        int SelectedId { get; set; }
        /// <summary>Obtengo el nombre del registro</summary>
        string EntityName { get; set; }
        /// <summary>Obtengo el tipo de entidad</summary>
        EntityType SelectedEntityType { get; set; }
        /// <summary>Retorna el resultado del ShowDialog</summary>
        DialogResult DialogResult { get; set; }

        //methods
        /// <summary>Envio los datos de los tipos de entidad al ComboBox</summary>
        /// <param name="datasource">Lista de tipos de entidad</param>
        void SetEntityTypeList(List<EntityType> datasource);
        /// <summary>Envio los datos genericos al GRD de ResultPicker</summary>
        /// <param name="transactions">DTOs para rellenar la GRD de ResultPicker</param>
        void GrdRefreshData(List<EntityDto> dataSource);
        /// <summary>Ejecuto la logica de botones</summary>
        void ButtonsLogic();

        //functions
        /// <summary>Muestra el formulario como Dialogo</summary>
        DialogResult ShowDialog();

        //events
        event EventHandler GrdDoubleClick;
        event EventHandler ButtonSelectClick;
        event EventHandler ButtonInsertClick;
        event EventHandler ButtonUpdateClick;
        event EventHandler ButtonDeleteClick;
    }
}
