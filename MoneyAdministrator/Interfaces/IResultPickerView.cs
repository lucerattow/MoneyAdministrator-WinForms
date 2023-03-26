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
        //fields
        bool TopMost { get; set; }

        //events
        event EventHandler FormClosed;

        //methods
        void BringToFront();
        void Show();
        void GrdRefreshData<T>(List<T> dataSource) where T : class;
    }
}
