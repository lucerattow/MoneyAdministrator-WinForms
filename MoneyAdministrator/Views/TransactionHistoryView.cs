using Microsoft.Extensions.Options;
using MoneyAdministrator.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;

namespace MoneyAdministrator.Views
{
    public partial class TransactionHistoryView : UserControl, ITransactionHistoryView
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
        public DateTime Date 
        { 
            get => _dtpDate.Value; 
            set => _dtpDate.Value = value; 
        }
        public string Description
        {
            get => _txtDescription.Text;
            set => _txtDescription.Text = value;
        }
        public int CurrencyId 
        { 
            get => _cbCurrency.SelectedIndex + 1; 
            set => _cbCurrency.SelectedIndex = value - 1;
        }
        public decimal Amount
        {
            get => decimal.Parse(_txtAmount.Text);
            set => _txtAmount.Text = value.ToString();
        }
        public int InstallmentCurrent
        {
            get => int.Parse(_txtInstallmentCurrent.Text);
            set
            {
                if (value > 0)
                    _txtInstallmentCurrent.Text = value.ToString();
                else
                    _txtInstallmentCurrent.Text = "";
            }
        }
        public int InstallmentMax
        {
            get
            {
                var installments = _txtInstallments.Text;
                if (string.IsNullOrEmpty(installments) || installments == "0")
                    installments = "1";

                return int.Parse(installments);
            }
            set
            {
                if (value > 0)
                    _txtInstallments.Text = value.ToString();
                else
                    _txtInstallments.Text = "";
            }
        }

        //events
        public event EventHandler<DataGridViewCellMouseEventArgs> GrdDoubleClick;
        public event EventHandler ButtonInsertClick;
        public event EventHandler ButtonUpdateClick;
        public event EventHandler ButtonDeleteClick;
        public event EventHandler ButtonClearClick;
        public event EventHandler ButtonExitClick;
        public event EventHandler SelectedYearChange;
        public event EventHandler ButtonEntitySearchClick;

        public TransactionHistoryView()
        {
            this.Visible = false;

            Dock = DockStyle.Fill;
            InitializeComponent();
            AssosiateEvents();
            ConfigureFields();
            GrdConfigure();
            ButtonsLogic();

            //Muestro la ventana ya cargada
            this.Visible = true;
        }

        //public methods
        public void GrdRefreshData(List<TransactionViewDto> transactions)
        {
            //Limpio la grilla y el yearPicker
            _grd.Rows.Clear();
            _ypYearPage.AvailableYears = transactions.Select(x => x.Date.Year).Distinct().ToList();

            //Filtro las transacciones por el año seleccionado
            transactions = transactions.Where(x => x.Date.Year == _ypYearPage.Value).ToList();

            if (transactions.Count <= 0)
                return;

            var row = 0;
            for (var i = 12; i >= 1; i--)
            {
                List<TransactionViewDto> monthTransactions = transactions
                    .Where(x => x.Date.Month == i).OrderByDescending(x => x.Date.Day).ToList();

                DateTime separatorDate = new DateTime(_ypYearPage.Value, i, 1);
                //Añado un separador
                row = _grd.Rows.Add(new object[]
                {
                    -1,
                    separatorDate.ToString("yyyy"),
                    separatorDate.ToString("(MM) MMM"),
                    "",
                    "",
                    "",
                    "",
                });

                //Pinto el separador
                Color sepBackColor = Color.FromArgb(75, 135, 230);
                Color sepForeColor = Color.White;
                foreach (DataGridViewCell cell in _grd.Rows[row].Cells)
                {
                    cell.Style.BackColor = sepBackColor;
                    cell.Style.ForeColor = sepForeColor;
                    cell.Style.SelectionBackColor = cell.Style.BackColor;
                    cell.Style.SelectionForeColor = cell.Style.ForeColor;
                }

                if (monthTransactions.Count == 0)
                {
                    //Si no hay registros en este mes, añado una fila en blanco
                    row = _grd.Rows.Add(new object[] { "", "", "", "", "", "", "" });
                }
                else
                {
                    //Caso contrario añado los registros a la tabla
                    foreach (var transaction in monthTransactions)
                    {
                        row = _grd.Rows.Add(new object[]
                        {
                            transaction.Id,
                            transaction.Date.ToString("yyyy-MM-dd"),
                            transaction.EntityName,
                            transaction.Description,
                            transaction.Installment,
                            transaction.CurrencyName,
                            transaction.Amount.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")),
                        });

                        //Pinto el monto segun corresponda
                        if (transaction.Amount > 0)
                            _grd.Rows[row].Cells["amount"].Style.ForeColor = Color.FromArgb(40, 150, 60);
                        else if (transaction.Amount < 0)
                            _grd.Rows[row].Cells["amount"].Style.ForeColor = Color.FromArgb(200, 60, 40);
                        else
                            _grd.Rows[row].Cells["amount"].Style.ForeColor = Color.FromArgb(80, 80, 80);
                    }
                }
            }
        }
        public void SetCurrenciesList(List<Currency> currencies)
        {
            _cbCurrency.DataSource = currencies;
            _cbCurrency.DisplayMember = "Name";
        }
        public void ButtonsLogic()
        {
            _tsbInsert.Enabled = _selectedId == 0;
            _tsbUpdate.Enabled = _selectedId != 0;
            _tsbDelete.Enabled = _selectedId != 0;
        }

        //private methods
        private void AssosiateEvents()
        {
            _grd.CellMouseDoubleClick += (sender, e) => GrdDoubleClick?.Invoke(sender, e);

            _tsbInsert.Click += (sender, e) => ButtonInsertClick?.Invoke(sender, e);
            _tsbUpdate.Click += (sender, e) => ButtonUpdateClick?.Invoke(sender, e);
            _tsbDelete.Click += (sender, e) => ButtonDeleteClick?.Invoke(sender, e);
            _tsbClear.Click += (sender, e) => ButtonClearClick?.Invoke(sender, e);
            _tsbExit.Click += (sender, e) => ButtonExitClick?.Invoke(sender, e);

            _btnEntitySearch.Click += (sender, e) => ButtonEntitySearchClick?.Invoke(sender, e);

            _ypYearPage.ButtonNextClick += (sender, e) => SelectedYearChange?.Invoke(sender, e);
            _ypYearPage.ButtonPreviousClick += (sender, e) => SelectedYearChange?.Invoke(sender, e);
            _ypYearPage.ValueChange += (sender, e) => SelectedYearChange?.Invoke(sender, e);
        }
        private void GrdConfigure()
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
                Width = 70,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
            });
            _grd.Columns.Add(new DataGridViewColumn() //2 entity
            {
                Name = "entity",
                HeaderText = "Entidad",
                CellTemplate = new DataGridViewTextBoxCell(),
            });
            _grd.Columns.Add(new DataGridViewColumn() //3 description
            {
                Name = "description",
                HeaderText = "Descripcion",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = 300,
            });
            _grd.Columns.Add(new DataGridViewColumn() //4 installments
            {
                Name = "installments",
                HeaderText = "Cuotas",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = 60,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter },
            });
            _grd.Columns.Add(new DataGridViewColumn() //5 currency
            {
                Name = "currency",
                HeaderText = "Moneda",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = 60,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter },
            });
            _grd.Columns.Add(new DataGridViewColumn() //6 amount
            {
                Name = "amount",
                HeaderText = "Monto",
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
            });
        }
        private void ConfigureFields()
        {
            _txtEntityName.MaxLength = 25;
            _dtpDate.CustomFormat = ConfigurationManager.AppSettings["DateFormat"];
            _txtDescription.MaxLength = 150;
            _txtAmount.TextAlign = HorizontalAlignment.Right;
            _txtAmount.Text = "0,00";
            _txtInstallmentCurrent.MaxLength = 2;
            _txtInstallmentCurrent.TextAlign = HorizontalAlignment.Center;
            _txtInstallments.MaxLength = 2;
            _txtInstallments.TextAlign = HorizontalAlignment.Center;
        }

        #region Eventos

        //private void TbValue_TextChanged(object sender, EventArgs e)
        //{
        //    TextBox? tb = sender as TextBox;
        //    // Verifica si el valor es un número válido
        //    if (decimal.TryParse(tb?.Text, out decimal value))
        //    {
        //        // Formatea el valor utilizando la cadena de formato "#0.00"
        //        tb.Text = value.ToString("#0.00");
        //    }
        //}

        //private void TbValue_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    TextBox? tb = sender as TextBox;
        //    // Permitir solo números, caracteres de control (como retroceso) y el separador decimal
        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
        //    {
        //        e.Handled = true;
        //    }

        //    // Permitir solo un separador decimal
        //    if (e.KeyChar == '.' && tb.Text.IndexOf('.') > -1)
        //    {
        //        e.Handled = true;
        //    }

        //    // Permitir solo dos dígitos decimales
        //    if (!char.IsControl(e.KeyChar))
        //    {
        //        if (tb?.Text.IndexOf('.') > -1 && tb.Text.Substring(tb.Text.IndexOf('.')).Length > 2)
        //        {
        //            e.Handled = true;
        //        }
        //    }
        //}

        //private void TbInstallments_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }
        //}

        //private void BtnCreate_Click(object sender, EventArgs e)
        //{
        //    _ = int.TryParse(_tbInstallments.Text, out int installment);
        //    _ = decimal.TryParse(_tbValue.Text, out decimal value);
        //    //string money = _rbCurrencyArs.Checked ? _rbCurrencyArs.Text : _rbCurrencyUsd.Text;

        //    try
        //    {
        //        //ValidateTransaction();
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        if (ex.ParamName == _tbEntity.Name)
        //            _lbOrigin.ForeColor = Color.Red;
        //        else
        //            _lbOrigin.ForeColor = Color.Black;
        //        return;
        //    }

        //    _lbOrigin.ForeColor = Color.Black;

        //    //var transaction = new TransactionDetailDto()
        //    //{
        //    //    //Date = DpDate.Value.ToString("yyyy-MM-dd"),
        //    //    //Origin = TbOrigin.Text,
        //    //    //Description = TbDescription.Text,
        //    //    //Value = value,
        //    //    //Money = money,
        //    //    //InstallmentCurrent = 1,
        //    //    //InstallmentTotal = installment
        //    //};

        //    //_transactionsController.Create(transaction);
        //    GrdLoadData();
        //}
         
        #endregion

    }
}
