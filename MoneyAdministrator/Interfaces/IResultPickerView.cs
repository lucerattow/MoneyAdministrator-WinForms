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

        //methods
        void GrdRefreshData(List<ResultPickerViewDto> dataSource);
        void ButtonsLogic();

        //functions
        DialogResult ShowDialog();

        //events
        event EventHandler<DataGridViewCellMouseEventArgs> GrdDoubleClick;
        event EventHandler ButtonSelectClick;
        event EventHandler ButtonSearchClick;
        event EventHandler ButtonClearClick;
        event EventHandler TxtNameTextChange;
    }
}
