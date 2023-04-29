using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoneyAdministrator.Utilities
{
    public class ControlConfig
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

            _grd.BackgroundColor = Color.FromArgb(235,235,235);
            _grd.DefaultCellStyle = new DataGridViewCellStyle()
            {
                SelectionBackColor = Color.FromArgb(240, 240, 240),
                SelectionForeColor = Color.FromArgb(40, 40, 40),
            };
            _grd.RowTemplate.Height = 23;
        }
    }
}
