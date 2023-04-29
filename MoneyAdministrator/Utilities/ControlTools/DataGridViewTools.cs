using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Utilities.ControlTools
{
    public class DataGridViewTools
    {
        public static void ScrollToRow(DataGridView grd, int rowToShow, int offSet)
        {
            if (rowToShow != -1 && rowToShow < grd.Rows.Count)
            {
                while (rowToShow <= Math.Abs(offSet))
                {
                    if (offSet > 0)
                        offSet--;
                    else if (offSet < 0) 
                        offSet++;
                }
                rowToShow += offSet;

                if (rowToShow >= 0 && rowToShow < grd.RowCount)
                {
                    var countVisible = grd.DisplayedRowCount(false);
                    var firstVisible = grd.FirstDisplayedScrollingRowIndex;
                    if (rowToShow < firstVisible)
                    {
                        grd.FirstDisplayedScrollingRowIndex = rowToShow;
                    }
                    else if (rowToShow >= firstVisible + countVisible)
                    {
                        grd.FirstDisplayedScrollingRowIndex = rowToShow - 1;
                    }
                }
            }
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

        public static Point GetBottomRight(DataGridViewCellPaintingEventArgs e) => new Point(e.CellBounds.Right - 1, e.CellBounds.Bottom);

        public static Point GetBottomLeft(DataGridViewCellPaintingEventArgs e) => new Point(e.CellBounds.Left, e.CellBounds.Bottom);
    }
}
