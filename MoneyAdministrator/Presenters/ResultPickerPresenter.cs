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
        private readonly IResultPickerView? _resultPickerView;
        private string _databasePath;

        public ResultPickerPresenter(string databasePath)
        {
            _resultPickerView = new ResultPickerView();
            _databasePath = databasePath;
            _resultPickerView.GrdDoubleClick += GrdDoubleClick;
            _resultPickerView.ButtonSelectClick += ButtonSelectClick;
        }

        //public methods
        public int Show<T>(List<T> resultPickerData) where T : class
        {
            if (_resultPickerView == null)
                throw new Exception("Ocurrio un error al intentar abrir el popup");

            _resultPickerView.GrdRefreshData(resultPickerData);

            if (_resultPickerView.ShowDialog() == DialogResult.OK)
                return _resultPickerView.SelectedId;
            else
                return -1;
        }

        //view events
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
    }
}
