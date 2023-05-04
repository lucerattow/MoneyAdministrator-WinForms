using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.CustomControls
{
    public class CettoDataGridViewCheckBoxColumn : DataGridViewCheckBoxColumn
    {
        public CettoDataGridViewCheckBoxColumn()
        {
            this.CellTemplate = new CettoDataGridViewCheckBoxCell();
        }
    }
}
