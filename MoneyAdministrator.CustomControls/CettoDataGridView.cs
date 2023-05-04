using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Svg;

namespace MoneyAdministrator.CustomControls
{
    public class CettoDataGridView : DataGridView
    {
        //fields
        private int _expandColumnHeight;

        public int ExpandColumnHeight
        {
            get => _expandColumnHeight;
            set
            {
                if (Columns.Count >= 4)
                {
                    Columns[3].Width = _expandColumnHeight;
                }
                _expandColumnHeight = value;
            }
        }

        public CettoDataGridView()
        {
            _expandColumnHeight = 30;

            this.CellClick += CettoDataGridView_CellClick;

            ColumnsClear();
        }

        //methods
        public void ColumnsAdd(DataGridViewColumn col)
        {
            Columns.Add(col);
        }

        public void ColumnsClear()
        {
            Columns.Clear();
            SetBaseColumns();
        }

        public int RowsInsert(int row, object[] data, bool IsGroupHeader, int GroupLevel, bool IsCollapsed)
        {
            return RowsAdd(data, IsGroupHeader, GroupLevel, IsCollapsed, row);
        }

        public int RowsAdd(object[] data, bool IsGroupHeader, int GroupLevel, bool IsCollapsed)
        {
            return RowsAdd(data, IsGroupHeader, GroupLevel, IsCollapsed, -1);
        }

        private int RowsAdd(object[] data, bool IsGroupHeader, int GroupLevel, bool IsCollapsed, int row = -1)
        {
            var expandIcon = IsCollapsed ? Files.Images.expand_collapse_gray : Files.Images.expand_expanded_gray;
            object[] InitialCells = new object[] { IsGroupHeader, GroupLevel, IsCollapsed, IsGroupHeader ? expandIcon : Files.Images.empty };
            object[] AllCells = new object[InitialCells.Length + data.Length];
            InitialCells.CopyTo(AllCells, 0);
            data.CopyTo(AllCells, InitialCells.Length);

            //Compruebo si la fila se debe insertar o añadir normalmente
            var originalRow = row;
            if (row == -1)
                row = Rows.Add(AllCells);
            else
            {
                Rows.Insert(row, AllCells);
                row++;
            }

            Rows[originalRow].Cells[3].Style.BackColor = this.BackgroundColor;
            Rows[originalRow].Cells[3].Style.ForeColor = this.BackgroundColor;
            Rows[originalRow].Cells[3].Style.SelectionBackColor = this.BackgroundColor;
            Rows[originalRow].Cells[3].Style.SelectionForeColor = this.BackgroundColor;

            UpdateRowVisibility();

            return row;
        }

        public void RowsClear()
        {
            Rows.Clear();
        }

        private void SetBaseColumns()
        {
            //Configuracion de columnas
            Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "IsGroupHeader",
                HeaderText = "",
                ReadOnly = true,
                Visible = false,
            }); //0 IsGroupHeader
            Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "GroupLevel",
                HeaderText = "",
                ReadOnly = true,
                Visible = false,
            }); //1 GroupLevel
            Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "IsCollapsed",
                HeaderText = "",
                ReadOnly = true,
                Visible = false,
            }); //2 IsCollapsed
            Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewImageCell(),
                Name = "CollapseImage",
                HeaderText = "",
                Width = _expandColumnHeight,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter, },
                ReadOnly = true,
            }); //3 CollapseImage
        }

        private void UpdateRowVisibility()
        {
            int groupLevel = -1;
            bool collapse = false;

            foreach (DataGridViewRow row in this.Rows)
            {
                if (row == null) continue;

                if ((bool)row.Cells["IsGroupHeader"].Value)
                {
                    groupLevel = (int)row.Cells["GroupLevel"].Value;
                    collapse = (bool)row.Cells["IsCollapsed"].Value;
                }
                else if ((int)row.Cells["GroupLevel"].Value > groupLevel)
                {
                    row.Visible = !collapse;
                }
            }
        }

        //function
        private void CettoDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (this.Rows[e.RowIndex] == null || !(bool)this.Rows[e.RowIndex].Cells["IsGroupHeader"].Value) return;

            this.Rows[e.RowIndex].Cells["IsCollapsed"].Value = !(bool)this.Rows[e.RowIndex].Cells["IsCollapsed"].Value;
            this.Rows[e.RowIndex].Cells["CollapseImage"].Value = 
                (bool)this.Rows[e.RowIndex].Cells["IsCollapsed"].Value ? Files.Images.expand_collapse_gray : Files.Images.expand_expanded_gray;
            
            UpdateRowVisibility();
        }
    }
}
