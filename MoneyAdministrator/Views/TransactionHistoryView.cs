using MoneyAdministrator.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;

namespace MoneyAdministrator.Views
{
    public partial class TransactionHistoryView : UserControl, ITransactionHistoryView
    {
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

        public TransactionHistoryView()
        {
            Dock = DockStyle.Fill;
            InitializeComponent();

            //Textbox setup
            _tbValue.TextAlign = HorizontalAlignment.Right;
            _tbInstallments.MaxLength = 2;
            _tbEntity.MaxLength = 25;
            _tbDescription.MaxLength = 150;

            //Ejecuto eventos necesarios
            //TbValue_TextChanged(_tbValue, new());

            //Configuro la grilla
            GrdConfigure();
            //GrdLoadData();

            //Muestro la ventana ya cargada
            _pnlContent.Visible = true;
        }

        private void GrdConfigure()
        {
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
            _grd.Columns.Add(new DataGridViewColumn() //6 value
            {
                Name = "value",
                HeaderText = "Valor",
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
            });

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

        private void GrdLoadData()
        {
            //Limpio la grilla
            _grd.Rows.Clear();

            ////Obtengo las transacciones
            //List<TransactionDetailDto> transactions = _transactionsController.GetAll()
            //    .OrderByDescending(x => x.Date).ToList();

            //if (transactions.Count <= 0)
            //    return;

            //Preparo variables para separar los meses
            CultureInfo culture = CultureInfo.InvariantCulture;
            DateTime previous = DateTime.MinValue;
            DateTime current = DateTime.MinValue;
            DateTime last_year = DateTime.MinValue; 
            bool monthSeparator = false;
            int row = 0;

            //Guardo el mes del registro mas nuevo
            //DateTime.TryParseExact(transactions[0].Date, "yyyy-MM-dd", culture, DateTimeStyles.None, out last_year);

            //for (int i = 0; i < transactions.Count; i++)
            //{
            //    //Guardo el mes del registro actual
            //    //DateTime.TryParseExact(transactions[i].Date, "yyyy-MM-dd", culture, DateTimeStyles.None, out current);

            //    //Comparo los meses del registro actual y el anterior
            //    //Para definir si debo añadir un nuevo separador
            //    monthSeparator = current.Month != previous.Month;

            //    if (i == 0 || monthSeparator)
            //    {
            //        //Añado un separador
            //        object[] separator = {
            //            -1,
            //            current.ToString("yyyy"),
            //            current.ToString("(MM) MMM"),
            //            "",
            //            "",
            //            "",
            //            "",
            //        };
            //        row = Grd.Rows.Add(separator);

            //        //Pinto el separador
            //        Color sepBackColor = Color.FromArgb(200, 100, 0);
            //        Color sepForeColor = Color.White;

            //        //Si no es el año actual, pintar de gris
            //        if (current.Year != last_year.Year)
            //        {
            //            sepBackColor = Color.FromArgb(200, 200, 200);
            //            sepForeColor = Color.Black;
            //        }

            //        foreach (DataGridViewCell cell in Grd.Rows[row].Cells)
            //        {
            //            cell.Style.BackColor = sepBackColor;
            //            cell.Style.ForeColor = sepForeColor;
            //            cell.Style.SelectionBackColor = cell.Style.BackColor;
            //            cell.Style.SelectionForeColor = cell.Style.ForeColor;
            //        }

            //        previous = current;
            //        monthSeparator = false;
            //    }

            //    //Añado la transaccion al Grd
            //    //string installments = $"{transactions[i].InstallmentCurrent}/{transactions[i].InstallmentTotal}";
            //    //if (transactions[i].InstallmentTotal == 0)
            //    //    installments = "";

            //    object[] newrow = {
            //        //transactions[i].Id,
            //        //transactions[i].Date,
            //        //transactions[i].Origin,
            //        //transactions[i].Description,
            //        //installments,
            //        //transactions[i].Money,
            //        //transactions[i].Value.ToString("N2")
            //    };
            //    row = Grd.Rows.Add(newrow);

            //    //Pinto la fila segun corresponda
            //    if (transactions[i].Value >= 0)
            //        Grd.Rows[row].Cells["valor"].Style.ForeColor = Color.FromArgb(10, 130, 65);
            //    else
            //        Grd.Rows[row].Cells["valor"].Style.ForeColor = Color.FromArgb(200, 60, 40);
            //}
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
