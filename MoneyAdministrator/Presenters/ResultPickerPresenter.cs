using MoneyAdministrator.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Views.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Presenters
{
    public class ResultPickerPresenter
    {
        //fields
        private readonly IResultPickerView? _resultPickerView;
        private List<ResultPickerViewDto> _dataSource;

        public ResultPickerPresenter(List<ResultPickerViewDto> resultPickerData)
        {
            _dataSource = resultPickerData;
            _resultPickerView = new ResultPickerView();
            _resultPickerView.GrdDoubleClick += GrdDoubleClick;
            _resultPickerView.ButtonSelectClick += ButtonSelectClick;
            _resultPickerView.ButtonSearchClick+= ButtonSearchClick;
            _resultPickerView.ButtonClearClick += ButtonClearClick;
            _resultPickerView.TxtNameTextChange += TxtNameTextChange;
        }

        //methods
        public int Show()
        {
            if (_resultPickerView == null)
                throw new Exception("Ocurrio un error al intentar abrir el popup");

            _resultPickerView.GrdRefreshData(_dataSource);

            if (_resultPickerView.ShowDialog() == DialogResult.OK)
                return _resultPickerView.SelectedId;
            else
                return -1;
        }

        //events
        private void GrdDoubleClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            _resultPickerView.SelectedId = (int)((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value;
            _resultPickerView.SelectedName = (string)((DataGridView)sender).Rows[e.RowIndex].Cells[1].Value;
            _resultPickerView.ButtonsLogic();
        }
        private void ButtonSelectClick(object? sender, EventArgs e)
        {
            _resultPickerView.DialogResult = DialogResult.OK;
        }
        private void ButtonSearchClick(object? sender, EventArgs e)
        {
            var search = _dataSource.Where(data => data.Field1.Contains(_resultPickerView.SelectedName)).ToList();
            _resultPickerView.GrdRefreshData(search);
        }
        private void ButtonClearClick(object? sender, EventArgs e)
        {
            _resultPickerView.SelectedId = 0;
            _resultPickerView.SelectedName = "";
            _resultPickerView.GrdRefreshData(_dataSource);
            _resultPickerView.ButtonsLogic();
        }
        private void TxtNameTextChange(object? sender, EventArgs e)
        {
            var resultId = _dataSource.Where(data => data.Field1 == _resultPickerView.SelectedName).FirstOrDefault()?.Id;

            if (resultId != null)
                _resultPickerView.SelectedId = (int)resultId;
            else
                _resultPickerView.SelectedId = 0;

            _resultPickerView.ButtonsLogic();
        }
    }
}
