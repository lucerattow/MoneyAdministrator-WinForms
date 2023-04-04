using MoneyAdministrator.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Interfaces
{
    public interface IResultPickerView
    {
        //field
        /// <summary>Obtengo el ID seleccionado</summary>
        int SelectedId { get; set; }
        /// <summary>Obtengo el nombre del registro</summary>
        string SelectedName { get; set; }
        /// <summary>Retorna el resultado del ShowDialog</summary>
        DialogResult DialogResult { get; set; }

        //methods
        /// <summary>Envio los datos genericos al GRD de ResultPicker</summary>
        /// <param name="transactions">DTOs para rellenar la GRD de ResultPicker</param>
        void GrdRefreshData(List<ResultPickerViewDto> dataSource);
        /// <summary>Ejecuto la logica de botones</summary>
        void ButtonsLogic();

        //functions
        /// <summary>Muestra el formulario como Dialogo</summary>
        DialogResult ShowDialog();

        //events
        event EventHandler<DataGridViewCellMouseEventArgs> GrdDoubleClick;
        event EventHandler ButtonSelectClick;
        event EventHandler ButtonSearchClick;
        event EventHandler ButtonClearClick;
        event EventHandler TxtNameTextChange;
    }
}
