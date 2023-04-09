using MoneyAdministrator.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Utilities;
using MoneyAdministrator.Utilities.Disposable;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MoneyAdministrator.Views
{
    public partial class CreditCardResumesView : UserControl, ICreditCardResumesView
    {
        //fields
        private int _selectedId = 0;
        private const int _colWidthDate = 90;
        private const int _colWidthEntity = 210;
        private const int _colWidthInstall = 60;
        private const int _colWidthCurrency = 70;
        private const int _colWidthAmount = 120;
        private const int _colWidthTotal = _colWidthDate + _colWidthEntity + _colWidthInstall + _colWidthCurrency + _colWidthAmount;

        //properties
        public int SelectedId
        {
            get => _selectedId;
            set => _selectedId = value;
        }

        public CreditCardResumesView()
        {
            this.Visible = false;

            Dock = DockStyle.Fill;
            InitializeComponent();
            AssosiateEvents();
            ControlsSetup();
            ButtonsLogic();

            //Muestro la ventana ya cargada
            this.Visible = true;
        }

        //methods
        public void GrdRefreshData(List<TransactionViewDto> transactions)
        {
            //using (new CursorWait())
            //using (new DataGridViewHide(_grd))
            //{
            ////Limpio la grilla y el yearPicker
            //_grd.Rows.Clear();

            ////Filtro las transacciones por el año seleccionado
            //transactions = transactions.Where(x => x.Date.Year == _ypYearPage.Value).ToList();

            //if (transactions.Count <= 0)
            //    return;

            //var row = 0;
            //for (var i = 12; i >= 1; i--)
            //{
            //    List<TransactionViewDto> monthTransactions = transactions
            //        .Where(x => x.Date.Month == i).OrderByDescending(x => x.Date.Day).ToList();

            //    if (monthTransactions.Count != 0)
            //    {
            //        DateTime separatorDate = new DateTime(_ypYearPage.Value, i, 1);
            //        //Añado un separador
            //        row = _grd.Rows.Add(new object[]
            //        {
            //            -1,
            //            separatorDate.ToString("yyyy"),
            //            separatorDate.ToString("(MM) MMM"),
            //            "",
            //            "",
            //            "",
            //            "",
            //        });

            //        //Pinto el separador
            //        foreach (DataGridViewCell cell in _grd.Rows[row].Cells)
            //        {
            //            cell.Style.BackColor = Color.FromArgb(75, 135, 230);
            //            cell.Style.ForeColor = Color.White;
            //            cell.Style.SelectionBackColor = cell.Style.BackColor;
            //            cell.Style.SelectionForeColor = cell.Style.ForeColor;
            //        }

            //        //Caso contrario añado los registros a la tabla
            //        foreach (var transaction in monthTransactions)
            //        {
            //            row = _grd.Rows.Add(new object[]
            //            {
            //                transaction.Id,
            //                transaction.Date.ToString("yyyy-MM-dd"),
            //                transaction.EntityName,
            //                transaction.Description,
            //                transaction.Installment,
            //                transaction.CurrencyName,
            //                transaction.Amount.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")),
            //            });

            //            //Pinto el monto segun corresponda
            //            if (transaction.Amount > 0)
            //                _grd.Rows[row].Cells["amount"].Style.ForeColor = Color.Green;
            //            else if (transaction.Amount < 0)
            //                _grd.Rows[row].Cells["amount"].Style.ForeColor = Color.FromArgb(150, 0, 0);
            //            else
            //                _grd.Rows[row].Cells["amount"].Style.ForeColor = Color.FromArgb(80, 80, 80);
            //        }
            //    }
            //}
            //}
        }

        public void SetCurrenciesList(List<Currency> currencies)
        {
            //_cbCurrency.DataSource = currencies;
            //_cbCurrency.DisplayMember = "Name";
        }

        public void ButtonsLogic()
        {
            //_tsbInsert.Enabled = _selectedId == 0;
            //_tsbUpdate.Enabled = _selectedId != 0;
            //_tsbDelete.Enabled = _selectedId != 0;
        }

        private void ClearInputs()
        {
            //_selectedId = 0;
            //_txtEntityName.Text = "";
            //_txtDescription.Text = "";
            //_dtpPeriod.Value = DateTime.Now;
            //_txtAmount.Text = "0";
            //_cbCurrency.SelectedIndex = _cbCurrency.FindStringExact("ARS");
            //_txtInstallmentCurrent.Text = "";
            //_txtInstallments.Text = "";
            //_ckbService.Checked = false;
            //_cbFrequency.SelectedIndex = _cbFrequency.FindString("1");

            //Editing = false;
            ButtonsLogic();
        }

        private void AssosiateEvents()
        {
            //_btnEntitySearch.Click += (sender, e) => ButtonEntitySearchClick?.Invoke(sender, e);
            //_tsbExit.Click += (sender, e) => ButtonExitClick?.Invoke(sender, e);
            //_ypYearPage.ButtonNextClick += (sender, e) => SelectedYearChange?.Invoke(sender, e);
            //_ypYearPage.ButtonPreviousClick += (sender, e) => SelectedYearChange?.Invoke(sender, e);
            //_ypYearPage.ValueChange += (sender, e) => SelectedYearChange?.Invoke(sender, e);
        }

        private void ControlsSetup()
        {
            //_txtEntityName.MaxLength = 25;
            //_dtpPeriod.CustomFormat = ConfigurationManager.AppSettings["DateFormat"];
            //_txtDescription.MaxLength = 150;
            //_txtAmount.TextAlign = HorizontalAlignment.Right;
            //_txtAmount.Text = "0";
            //_txtInstallmentCurrent.MaxLength = 2;
            //_txtInstallmentCurrent.TextAlign = HorizontalAlignment.Center;
            //_txtInstallments.MaxLength = 2;
            //_txtInstallments.TextAlign = HorizontalAlignment.Center;

            //_cbFrequency.Enabled = false;
            //_cbFrequency.DataSource = _frequencies;

            GrdSetup();
        }

        private void GrdSetup()
        {
            ControlConfig.DataGridViewSetup(_grd);

            //Configuracion de columnas
            _grd.Columns.Add(new DataGridViewColumn() //0 id
            {
                Name = "id",
                HeaderText = "Id",
                CellTemplate = new DataGridViewTextBoxCell(),
                Visible = false,
            });
            _grd.Columns.Add(new DataGridViewColumn() //1 Fecha
            {
                Name = "date",
                HeaderText = "Fecha",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colWidthDate,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
            });
            _grd.Columns.Add(new DataGridViewColumn() //2 entity
            {
                Name = "entity",
                HeaderText = "Entidad",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colWidthEntity,
            });
            _grd.Columns.Add(new DataGridViewColumn() //3 description
            {
                Name = "description",
                HeaderText = "Descripcion",
                CellTemplate = new DataGridViewTextBoxCell(),
            });
            _grd.Columns.Add(new DataGridViewColumn() //4 installments
            {
                Name = "installments",
                HeaderText = "Cuotas",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colWidthInstall,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter },
            });
            _grd.Columns.Add(new DataGridViewColumn() //5 currency
            {
                Name = "currency",
                HeaderText = "Moneda",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colWidthCurrency,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter },
            });
            _grd.Columns.Add(new DataGridViewColumn() //6 amount
            {
                Name = "amount",
                HeaderText = "Monto",
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
                Width = _colWidthAmount
            });
        }

        //events
        
        public event EventHandler GrdDoubleClick;
        public event EventHandler ButtonInsertClick;
        public event EventHandler ButtonUpdateClick;
        public event EventHandler ButtonDeleteClick;
        public event EventHandler ButtonExitClick;
        public event EventHandler SelectedYearChange;
        public event EventHandler ButtonEntitySearchClick;
    }
}
