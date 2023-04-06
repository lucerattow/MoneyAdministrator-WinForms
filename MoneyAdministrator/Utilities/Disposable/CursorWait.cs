using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Utilities.Disposable
{
    internal class CursorWait : IDisposable
    {
        private Cursor _previousCursor;

        public CursorWait() 
        {
            _previousCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
        }

        public void Dispose()
        {
            Cursor.Current = _previousCursor;
        }
    }
}
