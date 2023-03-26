using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Utilities
{
    public static class ControlConfig
    {
        public static void DataGridViewSetup(DataGridView _grd)
        {
            _grd.AllowUserToAddRows = false;
            _grd.AllowUserToDeleteRows = false;
            _grd.AllowUserToResizeRows = false;
            _grd.AllowUserToOrderColumns = false;
            _grd.AllowUserToResizeColumns = false;

            _grd.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _grd.EditMode = DataGridViewEditMode.EditProgrammatically;

            _grd.RowHeadersVisible = false;
            _grd.BorderStyle = BorderStyle.FixedSingle;
            _grd.CellBorderStyle = DataGridViewCellBorderStyle.None;
            _grd.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            _grd.BackgroundColor = Color.FromArgb(230, 230, 230);
            _grd.DefaultCellStyle = new DataGridViewCellStyle()
            {
                SelectionBackColor = Color.FromArgb(240, 240, 240),
                SelectionForeColor = Color.FromArgb(40, 40, 40),
            };
            _grd.RowTemplate.Height = 20;
        }
    }
}
