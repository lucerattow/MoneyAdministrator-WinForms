using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Utilities
{
    public class PaintDgvCells
    {
        public static void PaintSeparator(DataGridView grd, int row, Color backColor, Color foreColor)
        {
            foreach (DataGridViewCell cell in grd.Rows[row].Cells)
            {
                cell.Style.BackColor = backColor;
                cell.Style.ForeColor = foreColor;
                cell.Style.SelectionBackColor = cell.Style.BackColor;
                cell.Style.SelectionForeColor = cell.Style.ForeColor;
            }
        }

        public static void PaintDecimal(DataGridView grd, int row, int col, bool inverted = false)
        {
            if (grd.Rows[row].Cells[col].Value == null)
                return;

            var strValue = string.Concat(grd.Rows[row].Cells[col].Value.ToString()
                .Where(x => char.IsDigit(x) || x == ',' || x == '-'));

            Color positive = Color.Green;
            Color negative = Color.FromArgb(150, 0, 0);

            if (decimal.TryParse(strValue, out decimal value))
            {
                if (value > 0)
                    grd.Rows[row].Cells[col].Style.ForeColor = inverted ? negative : positive;
                else if (value < 0)
                    grd.Rows[row].Cells[col].Style.ForeColor = inverted ? positive : negative;
                else
                    grd.Rows[row].Cells[col].Style.ForeColor = Color.FromArgb(80, 80, 80);
            }
        }

        public static void PaintDecimal(DataGridView grd, int row, string col, bool inverted = false)
        {
            var index = grd.Columns[col].Index;
            PaintDecimal(grd, row, index, inverted);
        }

        public static void PaintCurrentDate(DataGridView grd, int row, int col, bool inverted = false)
        {
            string Date = grd.Rows[row].Cells[col].Value.ToString();
            if (Date.Length >= 7)
            {
                int yyyy = int.Parse(Date.Split('-')[0]);
                int MM = int.Parse(Date.Split('-')[1]);
                var period = new DateTime(yyyy, MM, 1);

                if (period == new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1))
                {
                    grd.Rows[row].Cells[col].Style.BackColor = Color.Black;
                    grd.Rows[row].Cells[col].Style.ForeColor = Color.White;
                }
            }
        }
    }
}
