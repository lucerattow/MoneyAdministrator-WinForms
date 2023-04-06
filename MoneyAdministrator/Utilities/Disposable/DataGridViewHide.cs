using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Utilities.Disposable
{
    internal class DataGridViewHide : IDisposable
    {
        private DataGridView _grd;

        public DataGridViewHide(DataGridView grd)
        {
            _grd = grd;
            _grd.Visible = false;
        }

        public void Dispose()
        {
            _grd.Visible = true;
        }
    }
}
