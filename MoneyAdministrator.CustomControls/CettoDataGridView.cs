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
        /// <summary>
        /// Añade una columna
        /// </summary>
        /// <param name="col"></param>
        public void ColumnsAdd(DataGridViewColumn col)
        {
            Columns.Add(col);
        }

        /// <summary>
        /// Elimina todas las columnas
        /// </summary>
        public void ColumnsClear()
        {
            Columns.Clear();
            SetBaseColumns();
        }

        /// <summary>
        /// Inserta una fila con los valores indicados
        /// </summary>
        /// <param name="row">ID donde se insertara la columna</param>
        /// <param name="data">Array de objetos con los valores de cada columna</param>
        /// <param name="IsGroupHeader">Indica si la fila sera un separador</param>
        /// <param name="GroupLevel">Indica el nivel del separador</param>
        /// <param name="IsCollapsed">Indica si el grupo esta colapsado o no</param>
        /// <returns></returns>
        public int RowsInsert(int row, object[] data, bool IsGroupHeader, int GroupLevel, bool IsCollapsed)
        {
            return RowsAdd(data, IsGroupHeader, GroupLevel, IsCollapsed, row);
        }

        /// <summary>
        /// Añade una fila con los valores indicados
        /// </summary>
        /// <param name="data">Array de objetos con los valores de cada columna</param>
        /// <param name="IsGroupHeader">Indica si la fila sera un separador</param>
        /// <param name="GroupLevel">Indica el nivel del separador</param>
        /// <param name="IsCollapsed">Indica si el grupo esta colapsado o no</param>
        /// <returns></returns>
        public int RowsAdd(object[] data, bool IsGroupHeader, int GroupLevel, bool IsCollapsed)
        {
            return RowsAdd(data, IsGroupHeader, GroupLevel, IsCollapsed, -1);
        }

        /// <summary>
        /// Elimina una fila
        /// </summary>
        /// <param name="rowIndex">ID de la fila que se desea eliminar</param>
        public void RowDelete(int rowIndex)
        {
            this.Rows.RemoveAt(rowIndex);
        }

        /// <summary>
        /// Limpia las filas del DataGridView
        /// </summary>
        public void RowsClear()
        {
            Rows.Clear();
        }

        private int RowsAdd(object[] data, bool IsGroupHeader, int GroupLevel, bool IsCollapsed, int row = -1)
        {
            var expandIcon = IsCollapsed ? Files.Images.expand_collapse_gray : Files.Images.expand_expanded_gray;
            object[] InitialCells = new object[] { IsGroupHeader, GroupLevel, IsCollapsed, IsGroupHeader ? expandIcon : Files.Images.empty };
            object[] AllCells = new object[InitialCells.Length + data.Length];
            InitialCells.CopyTo(AllCells, 0);
            data.CopyTo(AllCells, InitialCells.Length);

            //Compruebo si la fila se debe insertar o añadir normalmente
            if (row == -1)
                row = Rows.Add(AllCells);
            else
            {
                Rows.Insert(row, AllCells);
            }

            Rows[row].Cells[3].Style.BackColor = this.BackgroundColor;
            Rows[row].Cells[3].Style.ForeColor = this.BackgroundColor;
            Rows[row].Cells[3].Style.SelectionBackColor = this.BackgroundColor;
            Rows[row].Cells[3].Style.SelectionForeColor = this.BackgroundColor;

            UpdateRowVisibility();

            return row;
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
