using MoneyAdministrator.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;

namespace MoneyAdministrator.Views
{
    public partial class TransactionHistoryView : UserControl, ITransactionHistoryView
    {
        //fields
        private int _selectedId = 0;

        #region properties
        public string EntityName 
        { 
            get => _tbEntity.Text; 
            set => _tbEntity.Text = value; 
        }
        public DateTime Date 
        { 
            get => _dtpDate.Value; 
            set => _dtpDate.Value = value; 
        }
        public string Description
        {
            get => _tbDescription.Text;
            set => _tbDescription.Text = value;
        }
        public int CurrencyId 
        { 
            get => _cbCurrency.SelectedIndex; 
            set => _cbCurrency.SelectedIndex = value;
        }
        public decimal Value
        {
            get => decimal.Parse(_tbValue.Text);
            set => _tbValue.Text = value.ToString();
        }
        public int Installments
        {
            get => int.Parse(_tbInstallments.Text);
            set => _tbInstallments.Text = value.ToString();
        }
        #endregion

        //events
        public event EventHandler SelectedYearChange;
        public event EventHandler ButtonEntitySearchClick;

        public TransactionHistoryView()
        {
            this.Visible = false;
            Dock = DockStyle.Fill;
            InitializeComponent();
            AssosiateEvents();

            //Textbox setup
            _tbValue.TextAlign = HorizontalAlignment.Right;
            _tbInstallments.MaxLength = 2;
            _tbEntity.MaxLength = 25;
            _tbDescription.MaxLength = 150;

            //Configuro la grilla
            GrdConfigure();

            //Muestro la ventana ya cargada
            this.Visible = true;
        }

        #region methods
        //private
        private void AssosiateEvents()
        {
            _ypYearPage.ButtonNextClick += delegate
            {
                SelectedYearChange?.Invoke(_ypYearPage, EventArgs.Empty);
            };
            _ypYearPage.ButtonPreviousClick += delegate
            {
                SelectedYearChange?.Invoke(_ypYearPage, EventArgs.Empty);
            };
            _ypYearPage.ValueChange += delegate
            {
                SelectedYearChange?.Invoke(_ypYearPage, EventArgs.Empty);
            };
            _btnEntitySearch.Click += delegate
            { 
                ButtonEntitySearchClick?.Invoke(_btnEntitySearch, EventArgs.Empty);
            };
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
        private void ButtonsLogic()
        {
            _tsbInsert.Enabled = _selectedId == 0;
            _tsbUpdate.Enabled = _selectedId != 0;
            _tsbDelete.Enabled = _selectedId != 0;
        }
        //public
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

                //Añado un separador
                row = _grd.Rows.Add(new object[]
                {
                    -1,
                    monthTransactions[0].Date.ToString("yyyy"),
                    monthTransactions[0].Date.ToString("(MM) MMM"),
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
        #endregion

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
