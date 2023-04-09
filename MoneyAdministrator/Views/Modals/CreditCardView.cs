using MoneyAdministrator.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Services.Utilities;
using MoneyAdministrator.Utilities;
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
        public string EntityName
        {
            get => _txtEntityName.Text;
            set => _txtEntityName.Text = value;
        }
        public CreditCardType SelectedCreditCardType
        {
            get => (CreditCardType)_cbCreditCardType.SelectedItem;
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
                foreach (var dto in dataSource.OrderBy(x => x.BankEntityName).ThenBy(x => x.CreditCardTypeName).ThenBy(x => x.LastFourNumbers))
                {
                    row = _grd.Rows.Add(new object[]
                    {
                        dto.Id,
                        dto.BankEntityName,
                        dto.CreditCardTypeName,
                        dto.LastFourNumbers,
                    });
                }
            }
        }

        public void SetCreditCardTypeList(List<CreditCardType> datasource)
        {
            _cbCreditCardType.DataSource = datasource;
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
            _txtEntityName.Text = "";
            _cbCreditCardType.SelectedIndex = 0;
            _txtLastFourNumbers.Text = "";
            ButtonsLogic();
        }

        private void GrdSetup()
        {
            ControlConfig.DataGridViewSetup(_grd);

            //Configuracion de columnas
            _grd.Columns.Add(new DataGridViewColumn() //0 Id
            {
                Name = "id",
                HeaderText = "Id",
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
                Visible = false,
            });
            _grd.Columns.Add(new DataGridViewColumn() //1 Banco
            {
                Name = "bankEntity",
                HeaderText = "Banco",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = 200,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
            });
            _grd.Columns.Add(new DataGridViewColumn() //2 Tipo de tarjeta
            {
                Name = "type",
                HeaderText = "Tipo de tarjeta",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
            });
            _grd.Columns.Add(new DataGridViewColumn() //3 Numeros
            {
                Name = "lastFourNumbers",
                HeaderText = "Numeros",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = 180,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
            });
        }

        private void AssosiateEvents()
        {
            _grd.CellMouseDoubleClick += (sender, e) => GrdDoubleClick?.Invoke(sender, e);
            _tsbSelect.Click += (sender, e) => ButtonSelectClick?.Invoke(sender, e);
            _btnEntitySearch.Click += (sender, e) => ButtonEntitySearchClick?.Invoke(sender, e);
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
