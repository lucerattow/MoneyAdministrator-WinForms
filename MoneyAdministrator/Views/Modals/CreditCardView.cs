using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services.Utilities;
using MoneyAdministrator.Utilities;
using MoneyAdministrator.Utilities.ControlTools;
using MoneyAdministrator.Utilities.Disposable;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

namespace MoneyAdministrator.Views.Modals
{
    public partial class CreditCardView : Form, ICreditCardView
    {
        //fields
        private int _selectedId = 0;

        //properties
        public int SelectedId
        {
            get => _selectedId;
            set => _selectedId = value;
        }
        public Entity Entity
        {
            get
            {
                var bank = (Entity)_cbBank.SelectedItem;
                if (bank == null)
                {
                    bank = new Entity
                    {
                        Name = _cbBank.Text,
                        EntityTypeId = 2, //Tipo Banco
                    };
                }
                return bank;
            }
            set
            {
                var index = _cbBank.FindStringExact(value.Name);
                _cbBank.SelectedIndex = _cbBank.FindStringExact(value.Name);

                if (index != -1)
                    _cbBank.Text = value.Name;
            }
        }
        public CreditCardBrand CreditCardBrand
        {
            get => (CreditCardBrand)_cbCreditCardType.SelectedItem;
            set => _cbCreditCardType.SelectedIndex = _cbCreditCardType.FindStringExact(value.Name);
        }
        public int LastFourNumbers
        {
            get => stringUtilities.GetNumbersFromString(_txtLastFourNumbers.Text);
            set => _txtLastFourNumbers.Text = value.ToString();
        }

        public new DialogResult DialogResult
        {
            get => base.DialogResult;
            set => base.DialogResult = value;
        }

        public CreditCardView()
        {
            using (new CursorWait())
            {
                InitializeComponent();

                this.Text = $"{ConfigurationManager.AppSettings["AppTitle"]} : Seleccionar tarjeta de credito";

                GrdSetup();
                AssosiateEvents();
                ButtonsLogic();
            }
        }

        //methods
        public void GrdRefreshData(List<CreditCardDto> dataSource)
        {
            using (new CursorWait())
            using (new DataGridViewHide(_grd))
            {
                _grd.Rows.Clear();

                int row = 0;
                foreach (var dto in dataSource.OrderBy(x => x.BankEntityName).ThenBy(x => x.CreditCardBrandName).ThenBy(x => x.LastFourNumbers))
                {
                    row = _grd.Rows.Add(new object[]
                    {
                        dto.Id,
                        dto.BankEntityName,
                        dto.CreditCardBrandName,
                        dto.LastFourNumbers,
                    });
                }
            }
        }

        public void CreditCardEntityRefreshData(List<Entity> datasource)
        {
            _cbBank.DataSource = datasource.OrderBy(x => x.Name).ToList();
            _cbBank.DisplayMember = "Name";
        }

        public void CreditCardBrandRefreshData(List<CreditCardBrand> datasource)
        {
            _cbCreditCardType.DataSource = datasource.OrderBy(x => x.Name).ToList();
            _cbCreditCardType.DisplayMember = "Name";
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
            _cbBank.Text = "";
            _cbCreditCardType.SelectedIndex = 0;
            _txtLastFourNumbers.Text = "";
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
                Name = "bankEntity",
                HeaderText = "Banco",
                Width = 200,
                ReadOnly = true,
            }); //1 bankEntity
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
                Name = "type",
                HeaderText = "Tipo de tarjeta",
                Width = 150,
                ReadOnly = true,
            }); //2 type
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
                Name = "lastFourNumbers",
                HeaderText = "Numeros",
                Width = 180,
                ReadOnly = true,
            }); //3 lastFourNumbers
        }

        private void AssosiateEvents()
        {
            _grd.CellMouseDoubleClick += (sender, e) => GrdDoubleClick?.Invoke(sender, e);
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
        public event EventHandler ButtonEntitySearchClick;
    }
}
