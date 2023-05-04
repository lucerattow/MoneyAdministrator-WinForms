using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Utilities;
using MoneyAdministrator.Utilities.ControlTools;
using MoneyAdministrator.Utilities.Disposable;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;

namespace MoneyAdministrator.Views.Modals
{
    public partial class EntityView : Form, IEntityView
    {
        //fields
        private int _selectedId = 0;

        //properties
        public int SelectedId
        {
            get => _selectedId;
            set => _selectedId = value;
        }
        public string EntityName
        {
            get => _txtName.Text;
            set => _txtName.Text = value;
        }
        public EntityType EntityType
        {
            get
            {
                var entity = (EntityType)_cbEntityType.SelectedItem;
                if (entity == null)
                {
                    entity = new EntityType
                    {
                        Name = _cbEntityType.Text,
                    };
                }
                return entity;
            }
            set
            {
                var index = _cbEntityType.FindStringExact(value.Name);
                _cbEntityType.SelectedIndex = _cbEntityType.FindStringExact(value.Name);

                if (index != -1)
                    _cbEntityType.Text = value.Name;
            }
        }

        public new DialogResult DialogResult
        {
            get => base.DialogResult;
            set => base.DialogResult = value;
        }

        public EntityView()
        {
            using (new CursorWait())
            {
                InitializeComponent();

                this.Text = $"{ConfigurationManager.AppSettings["AppTitle"]} : Seleccionar entidad";

                GrdSetup();
                AssosiateEvents();
                ButtonsLogic();
            }
        }

        //methods
        public void GrdRefreshData(List<EntityDto> dataSource)
        {
            using (new CursorWait())
            using (new DataGridViewHide(_grd))
            {
                _grd.Rows.Clear();

                int row = 0;
                foreach (var dto in dataSource.OrderBy(x => x.EntityTypeName).ThenBy(x => x.Name))
                {
                    row = _grd.Rows.Add(new object[]
                    {
                        dto.Id,
                        dto.Name,
                        dto.EntityTypeName,
                    });
                }
            }
        }

        public void SetEntityTypeList(List<EntityType> datasource)
        {
            _cbEntityType.DataSource = datasource.OrderBy(x => x.Name).ToList();
            _cbEntityType.ValueMember = "Id";
            _cbEntityType.DisplayMember = "Name";
        }

        public void ButtonsLogic()
        {
            bool enabled = _selectedId > 0;
            _tsbSelect.Enabled = enabled;
            _tsbInsert.Enabled = !enabled;
            _tsbUpdate.Enabled = enabled;
            _tsbDelete.Enabled = enabled;
        }

        private void ClearInputs()
        {
            _selectedId = 0;
            _txtName.Text = "";
            _cbEntityType.Text = "";
            ButtonsLogic();
        }

        private void GrdSetup()
        {
            DataGridViewTools.DataGridViewSetup(_grd);

            //Configuracion de columnas
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
                Name = "id",
                HeaderText = "Id",
                ReadOnly = true,
                Visible = false,
            }); //0 id
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
                Name = "entityName",
                HeaderText = "Nombre",
                Width = 200,
                ReadOnly = true,
            }); //1 entityName
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
                Name = "type",
                HeaderText = "Tipo de entidad",
                Width = 150,
                ReadOnly = true,
            }); //2 type
        }

        private void AssosiateEvents()
        {
            _tsbSelect.Click += (sender, e) => ButtonSelectClick?.Invoke(sender, e);
        }

        //events
        private void _grd_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _selectedId = (int)(sender as DataGridView).Rows[e.RowIndex].Cells[0].Value;
            GrdDoubleClick?.Invoke(sender, e);
            ButtonsLogic();
        }

        private void _tsbInsert_Click(object sender, EventArgs e)
        {
            ButtonInsertClick?.Invoke(sender, e);
            ClearInputs();
        }

        private void _tsbUpdate_Click(object sender, EventArgs e)
        {
            ButtonUpdateClick?.Invoke(sender, e);
            ClearInputs();
        }

        private void _tsbDelete_Click(object sender, EventArgs e)
        {
            ButtonDeleteClick?.Invoke(sender, e);
            ClearInputs();
        }

        private void _tsbClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        public event EventHandler GrdDoubleClick;
        public event EventHandler ButtonSelectClick;
        public event EventHandler ButtonInsertClick;
        public event EventHandler ButtonUpdateClick;
        public event EventHandler ButtonDeleteClick;
    }
}
