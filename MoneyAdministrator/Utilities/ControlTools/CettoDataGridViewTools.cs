using MoneyAdministrator.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Utilities.ControlTools
{
    public class CettoDataGridViewTools
    {
        public static void PaintSeparator(CettoDataGridView grd, int row, Color backColor, Color foreColor)
        {
            for (int col = 4; col < grd.Rows[row].Cells.Count; col++)
            {
                var cell = grd.Rows[row].Cells[col];
                cell.Style.BackColor = backColor;
                cell.Style.ForeColor = foreColor;
                cell.Style.SelectionBackColor = cell.Style.BackColor;
                cell.Style.SelectionForeColor = cell.Style.ForeColor;
            }
        }
    }
}
