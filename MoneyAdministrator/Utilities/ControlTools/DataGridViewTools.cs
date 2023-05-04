using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Utilities.ControlTools
{
    public class DataGridViewTools
    {
        public static void DataGridViewSetup(DataGridView _grd)
        {
            //Activo el dobuble buffering
            var flags = BindingFlags.Instance | BindingFlags.NonPublic;
            _grd.GetType().GetProperty("DoubleBuffered", flags).SetValue(_grd, true);

            //Bloqueo al usuario para editar el grd
            _grd.AllowUserToAddRows = false;
            _grd.AllowUserToDeleteRows = false;
            _grd.AllowUserToResizeRows = false;
            _grd.AllowUserToOrderColumns = false;
            _grd.AllowUserToResizeColumns = false;

            //Personalizo el comportamiento
            _grd.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Personalizo la apariencia
            _grd.RowHeadersVisible = false;
            _grd.BorderStyle = BorderStyle.FixedSingle;
            _grd.CellBorderStyle = DataGridViewCellBorderStyle.None;
            _grd.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            _grd.BackgroundColor = Color.FromArgb(235, 235, 235);
            _grd.DefaultCellStyle = new DataGridViewCellStyle()
            {
                SelectionBackColor = Color.FromArgb(150, 220, 220),
                SelectionForeColor = Color.FromArgb(40, 40, 40),
            };
            _grd.RowTemplate.Height = 23;
        }

        public static void PaintCellBorder(DataGridViewCellPaintingEventArgs e, Color color, DataGridViewBorder border)
        {
            Point p1 = new Point(1, 1);
            Point p2 = new Point(1, 1);

            switch (border)
            {
                case DataGridViewBorder.TopBorder:
                    p1 = DataGridViewPoints.GetTopLeft(e);
                    p2 = DataGridViewPoints.GetTopRight(e);
                    break;
                case DataGridViewBorder.BottomBorder:
                    p1 = DataGridViewPoints.GetBottomLeft(e);
                    p2 = DataGridViewPoints.GetBottomRight(e);
                    break;
                case DataGridViewBorder.LeftBorder:
                    p1 = DataGridViewPoints.GetTopLeft(e);
                    p1 = DataGridViewPoints.GetBottomLeft(e);
                    break;
                case DataGridViewBorder.RightBorder:
                    p1 = DataGridViewPoints.GetTopRight(e);
                    p2 = DataGridViewPoints.GetBottomRight(e);
                    break;
                default:
                    break;
            }

            // Dibuja una línea en la direccion indicada
            using (Pen pen = new Pen(color, 1))
            {
                e.Graphics.DrawLine(pen, p1, p2);
            }
        }

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

    public enum DataGridViewBorder
    { 
        TopBorder,
        BottomBorder,
        LeftBorder,
        RightBorder,
    }

    internal class DataGridViewPoints
    { 
        public static Point GetTopRight(DataGridViewCellPaintingEventArgs e) => new Point(e.CellBounds.Right - 1, e.CellBounds.Top);

        public static Point GetTopLeft(DataGridViewCellPaintingEventArgs e) => new Point(e.CellBounds.Left, e.CellBounds.Top);

        public static Point GetBottomRight(DataGridViewCellPaintingEventArgs e) => new Point(e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);

        public static Point GetBottomLeft(DataGridViewCellPaintingEventArgs e) => new Point(e.CellBounds.Left, e.CellBounds.Bottom - 1);
    }
}
