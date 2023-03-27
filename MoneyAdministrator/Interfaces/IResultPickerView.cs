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
        int SelectedId { get; set; }
        string SelectedName { get; set; }
        DialogResult DialogResult { get; set; }

        //events
        event EventHandler<DataGridViewCellMouseEventArgs> GrdDoubleClick;
        event EventHandler ButtonSelectClick;
        event EventHandler ButtonSearchClick;
        event EventHandler ButtonInsertClick;
        event EventHandler ButtonUpdateClick;
        event EventHandler ButtonDeleteClick;
        event EventHandler ButtonClearClick;

        //methods
        void GrdRefreshData<T>(List<T> dataSource) where T : class;
        void ButtonsLogic();

        //functions
        DialogResult ShowDialog();
    }
}
