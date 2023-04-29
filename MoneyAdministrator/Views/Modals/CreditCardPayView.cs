using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Utilities.Disposable;
using MoneyAdministrator.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MoneyAdministrator.Models;
using System.Configuration;

namespace MoneyAdministrator.Views.Modals
{
    public partial class CreditCardPayView : Form, ICreditCardPayView
    {
        //grd columns width
        private const int _colWidthDate = 90;
        private const int _colWidthEntity = 210;
        private const int _colWidthAmount = 120;
        private const int _colWidthTotal = _colWidthDate + _colWidthEntity + _colWidthAmount;

        //fields
        private int _transactionOutstandingId = 0;
        private int _selectedTransactionDetail = 0;
        private DateTime _constDatePay = DateTime.Now;

        //properties
        public int TransactionOutstandingId
        {
            get => _transactionOutstandingId;
            set => _transactionOutstandingId = value;
        }
        public int SelectedTransactionDetail
        {
            get => _selectedTransactionDetail;
            set => _selectedTransactionDetail = value;
        }
        public string CreditCardDescription
        {
            set => _txtCreditCard.Text = value;
        }
        public DateTime PayDay
        {
            get => _dtpDate.Value;
            set
            {
                _constDatePay = value;
                _dtpDate.Value = value;
            }
        }
        public decimal AmountPay
        {
            get
            {
                var numbers = new string(_txtAmountPay.Text.Where(char.IsDigit).ToArray());
                var value = decimal.Parse(numbers) / 100;

                var oper = _txtAmountPay.OperatorSymbol;
                if (oper == "-" && value > 0 || oper == "+" && value < 0)
                    value *= -1;

                return value;
            }
            set
            {
                if (value >= 0)
                    _txtAmountPay.OperatorSymbol = "+";
                else
                    _txtAmountPay.OperatorSymbol = "-";

                _txtAmountPay.Text = value.ToString("N2");
            }
        }

        public CreditCardPayView()
        {
            using (new CursorWait())
            {
                InitializeComponent();

                this.Text = $"Nuevo pago - Periodo:";

                ButtonsLogic();
                GrdSetup();
            }
        }

        //methods
        public void GrdRefreshData(List<CreditCardPayDto> datasource, DateTime period)
        {
            this.Text = $"Nuevo pago - Periodo: {period.ToString(ConfigurationManager.AppSettings["DateFormat"])}";

            using (new CursorWait())
            using (new DataGridViewHide(_grd))
            {
                //Limpio la grilla y el yearPicker
                _grd.Rows.Clear();

                if (datasource.Count <= 0)
                    return;

                var row = 0;
                var transactions = datasource.OrderByDescending(x => x.Date.Day).ToList();

                if (transactions.Count != 0)
                {
                    foreach (var transaction in transactions)
                    {
                        row = _grd.Rows.Add(new object[]
                        {
                        transaction.Id,
                        transaction.Date.ToString("yyyy-MM-dd"),
                        transaction.EntityName,
                        transaction.Description,
                        transaction.AmountArs.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")),
                        });

                        //Pinto el monto segun corresponda
                        PaintDgvCells.PaintDecimal(_grd, row, "amount");
                    }
                }
            }
        }

        public void ButtonsLogic()
        {
            _tsbInsert.Enabled = _selectedTransactionDetail == 0;
            _tsbUpdate.Enabled = _selectedTransactionDetail != 0 && _selectedTransactionDetail != _transactionOutstandingId;
            _tsbDelete.Enabled = _selectedTransactionDetail != 0 && _selectedTransactionDetail != _transactionOutstandingId;
        }

        private void ClearInputs()
        {
            _txtAmountPay.Text = "0";
            _dtpDate.Value = new DateTime(_constDatePay.Year, _constDatePay.Month, 1);
            _selectedTransactionDetail = 0;
            ButtonsLogic();
        }

        private void GrdSetup()
        {
            ControlConfig.DataGridViewSetup(_grd);

            //Configuracion de columnas
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "id",
                HeaderText = "Id",
                ReadOnly = true,
                Visible = false,
            }); //0 id
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
                Name = "date",
                HeaderText = "Fecha",
                Width = _colWidthDate,
                ReadOnly = true,
            }); //1 date
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "entity",
                HeaderText = "Entidad",
                Width = _colWidthEntity,
                ReadOnly = true,
            }); //2 entity
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "description",
                HeaderText = "Descripcion",
                Width = _grd.Width - _colWidthTotal - 19,
                ReadOnly = true,
            }); //3 description
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
                Name = "amount",
                HeaderText = "Monto",
                Width = _colWidthAmount,
                ReadOnly = true,
            }); //4 amount
        }

        private void RestrictDateToDayOnly(DateTimePicker dtp)
        {
            var newDate = dtp.Value;

            if (newDate.Year != _constDatePay.Year || newDate.Month != _constDatePay.Month)
                dtp.Value = new DateTime(_constDatePay.Year, _constDatePay.Month, dtp.Value.Day);
        }

        //events
        private void _tsbInsert_Click(object sender, EventArgs e)
        {
            ButtonInsertClick.Invoke(sender, e);
            ButtonsLogic();
        }

        private void _tsbUpdate_Click(object sender, EventArgs e)
        {
            ButtonUpdateClick.Invoke(sender, e);
            ClearInputs();
            ButtonsLogic();
        }

        private void _tsbDelete_Click(object sender, EventArgs e)
        {
            ButtonDeleteClick.Invoke(sender, e);
            ClearInputs();
            ButtonsLogic();
        }

        private void _tsbClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
            ButtonsLogic();
        }

        private void _txtCreditCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void _dtpDate_ValueChanged(object sender, EventArgs e)
        {
            RestrictDateToDayOnly(sender as DateTimePicker);
        }

        private void _grd_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _selectedTransactionDetail = (int)(sender as DataGridView).Rows[e.RowIndex].Cells[0].Value;
            GrdDoubleClick?.Invoke(sender, e);
            ButtonsLogic();
        }

        private void _txtAmountPay_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((TextBox)sender).Text += "+";
        }

        private void _txtAmountPay_KeyDown(object sender, KeyEventArgs e)
        {
            // Hacer handle de los caracteres "+"
            if (e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Add)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        public event EventHandler GrdDoubleClick;
        public event EventHandler ButtonInsertClick;
        public event EventHandler ButtonUpdateClick;
        public event EventHandler ButtonDeleteClick;
    }
}
