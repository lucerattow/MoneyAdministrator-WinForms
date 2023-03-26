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
        private bool resultPickerIsOpen = false;

        public ResultPickerPresenter(string databasePath)
        {
            _resultPickerView = new ResultPickerView();
            _databasePath = databasePath;
            _resultPickerView.FormClosed += FormClosed;
        }

        public void Show<T>(List<T> resultPickerData) where T : class
        {
            if (resultPickerIsOpen)
            {
                _resultPickerView?.BringToFront();
            }
            else
            {
                resultPickerIsOpen = true;
                _resultPickerView?.Show();
                _resultPickerView.TopMost = true;
                _resultPickerView?.GrdRefreshData<T>(resultPickerData);
            }
        }
        private void FormClosed(object? sender, EventArgs e)
        { 
            resultPickerIsOpen = false;
        }
    }
}
