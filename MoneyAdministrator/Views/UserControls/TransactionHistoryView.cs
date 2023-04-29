﻿using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
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
using MoneyAdministrator.Common.Utilities.TypeTools;
using MoneyAdministrator.Utilities.ControlTools;
using System.Configuration;
using System.Reflection.Metadata.Ecma335;
using MoneyAdministrator.Common.Enums;

namespace MoneyAdministrator.Views.UserControls
{
    public partial class TransactionHistoryView : UserControl, ITransactionHistoryView
    {
        //grd columns width
        private const int _colWidthDate = 90;
        private const int _colWidthEntity = 210;
        private const int _colWidthInstall = 60;
        private const int _colWidthCurrency = 70;
        private const int _colWidthAmount = 120;
        private const int _colCheckBox = 70;
        private const int _colWidthTotal = _colWidthDate + _colWidthEntity + _colWidthInstall + _colWidthCurrency + _colWidthAmount + (_colCheckBox * 2);

        //fields
        private TransactionViewDto? _selectedDto;
        private TransactionViewDto? _checkBoxChangeDto;

        //properties
        public TransactionViewDto? SelectedDto
        {
            get => _selectedDto;
            set => _selectedDto = value;
        }
        public TransactionViewDto? CheckBoxChangeDto
        {
            get => _checkBoxChangeDto;
        }

        //properties fields
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
        public decimal Amount
        {
            get
            {
                var numbers = new string(_txtAmount.Text.Where(char.IsDigit).ToArray());
                var value = decimal.Parse(numbers) / 100;

                var oper = _txtAmount.OperatorSymbol;
                if (oper == "-" && value > 0 || oper == "+" && value < 0)
                    value *= -1;

                return value;
            }
            set
            {
                if (value >= 0)
                    _txtAmount.OperatorSymbol = "+";
                else
                    _txtAmount.OperatorSymbol = "-";

                _txtAmount.Text = value.ToString("N2");
            }
        }
        public Currency Currency
        {
            get => (Currency)_cbCurrency.SelectedItem;
            set => _cbCurrency.SelectedIndex = _cbCurrency.FindStringExact(value.Name);
        }

        //properties installments
        public bool IsInstallment
        {
            get => _ckbInstallments.Checked;
            set => _ckbInstallments.Checked = value;
        }
        public int InstallmentCurrent
        {
            get => IntTools.Convert(_txtInstallmentCurrent.Text);
            set => _txtInstallmentCurrent.Text = value > 0 ? value.ToString() : "";
        }
        public int InstallmentMax
        {
            get => IntTools.Convert(string.IsNullOrEmpty(_txtInstallments.Text) ? "1" : _txtInstallments.Text);
            set => _txtInstallments.Text = value > 0 ? value.ToString() : "";
        }

        //properties service
        public bool IsService
        {
            get => _ckbService.Checked;
            set => _ckbService.Checked = value;
        }
        public int Frequency
        {
            get
            {
                var fre = IntTools.Convert(_cbFrequency.SelectedItem is null ? "" : _cbFrequency.SelectedItem.ToString());
                return fre > 0 ? fre : 1;
            }
            set
            {
                if (value < 1) value = 1;
                _cbFrequency.SelectedIndex = _cbFrequency.FindString(value.ToString());
            }
        }

        public TransactionHistoryView()
        {
            using (new CursorWait())
            {
                Dock = DockStyle.Fill;
                InitializeComponent();
                ControlsSetup();
                ButtonsLogic();
            }
        }

        //methods
        public void SetCurrenciesList(List<Currency> datasource)
        {
            _cbCurrency.DataSource = datasource;
            _cbCurrency.DisplayMember = "Name";
        }

        public void GrdRefreshData(List<TransactionViewDto> datasource)
        {
            using (new CursorWait())
            using (new DataGridViewHide(_grd))
            {
                //Limpio la grilla
                _grd.Rows.Clear();

                if (datasource.Count <= 0)
                    return;

                //Separador futuro
                Color sepFutureBackColor = Color.FromArgb(170, 200, 255);
                Color sepFutureForeColor = Color.Black;

                //Separador año actual
                Color sepCurrentBackColor = Color.FromArgb(75, 135, 230);
                Color sepCurrentForeColor = Color.White;

                //Separador mes actual
                Color sepCurrentMonthBackColor = Color.FromArgb(40, 70, 200);

                //Separador antiguo
                Color sepOldestBackColor = Color.FromArgb(200, 200, 200);
                Color sepOldestForeColor = Color.Black;

                var row = 0;
                foreach (var year in datasource.OrderByDescending(x => x.Date).Select(x => x.Date.Year).Distinct())
                    for (var month = 12; month >= 1; month--)
                    {
                        List<TransactionViewDto> monthTransactions = datasource
                            .Where(x => x.Date.Year == year && x.Date.Month == month).OrderByDescending(x => x.Date.Day).ThenBy(x => x.Description).ToList();

                        if (monthTransactions.Count == 0)
                            continue;

                        DateTime separatorDate = new DateTime(year, month, 1);
                        //Añado un separador
                        row = _grd.Rows.Add(new object[]
                        {
                            -1,
                            0,
                            0,
                            separatorDate.ToString("yyyy"),
                            separatorDate.ToString("(MM) MMM"),
                            "",
                            "",
                            "",
                            "",
                            false,
                            false,
                        });

                        //Pinto el separador
                        if (year > DateTime.Now.Year)
                            PaintDgvCells.PaintSeparator(_grd, row, sepFutureBackColor, sepFutureForeColor);

                        else if (year == DateTime.Now.Year && month == DateTime.Now.Month)
                            PaintDgvCells.PaintSeparator(_grd, row, sepCurrentMonthBackColor, sepCurrentForeColor);

                        else if (year == DateTime.Now.Year)
                            PaintDgvCells.PaintSeparator(_grd, row, sepCurrentBackColor, sepCurrentForeColor);

                        else if (year < DateTime.Now.Year)
                            PaintDgvCells.PaintSeparator(_grd, row, sepOldestBackColor, sepOldestForeColor);

                        //Caso contrario añado los registros a la tabla
                        foreach (var transaction in monthTransactions)
                        {
                            row = _grd.Rows.Add(new object[]
                            {
                                transaction.Id,
                                transaction.TransactionType,
                                transaction.Frequency,
                                transaction.Date.ToString("yyyy-MM-dd"),
                                transaction.EntityName,
                                transaction.Description,
                                transaction.Installment,
                                transaction.CurrencyName,
                                transaction.Amount.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")),
                                transaction.Concider,
                                transaction.Paid,
                            });

                            //Pinto el monto segun corresponda
                            PaintDgvCells.PaintDecimal(_grd, row, "amount");
                        }
                    }
            }

            GrdStartingScroll();
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
                Name = "type",
                HeaderText = "Tipo",
                ReadOnly = true,
                Visible = false,
            }); //1 type
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "frequency",
                HeaderText = "Frecuencia",
                ReadOnly = true,
                Visible = false,
            }); //2 frequency
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
                Name = "date",
                HeaderText = "Fecha",
                Width = _colWidthDate,
                ReadOnly = true,
            }); //3 date
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "entity",
                HeaderText = "Entidad",
                Width = _colWidthEntity,
                ReadOnly = true,
            }); //4 entity
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "description",
                HeaderText = "Descripcion",
                ReadOnly = true,
            }); //5 description
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter },
                Name = "installments",
                HeaderText = "Cuotas",
                Width = _colWidthInstall,
                ReadOnly = true,
            }); //6 installments
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter },
                Name = "currency",
                HeaderText = "Moneda",
                Width = _colWidthCurrency,
                ReadOnly = true,
            }); //7 currency
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
                Name = "amount",
                HeaderText = "Monto",
                Width = _colWidthAmount,
                ReadOnly = true,
            }); //8 amount
            _grd.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                CellTemplate = new DataGridViewCheckBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter },
                Name = "concider",
                HeaderText = "Sumar",
                Width = _colCheckBox,
            }); //9 concider
            _grd.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                CellTemplate = new DataGridViewCheckBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter },
                Name = "paid",
                HeaderText = "Pagado",
                Width = _colCheckBox,
            }); //10 paid
        }

        private void GrdStartingScroll()
        {
            int rowIndex = -1;

            for (int i = 0; i < _grd.Rows.Count; i++)
            {
                //Compruebo que no sea un separador
                if ((int)_grd.Rows[i].Cells["id"].Value == -1)
                    continue;

                DateTime rowDate = DateTimeTools.Convert(_grd.Rows[i].Cells["date"].Value.ToString(), "yyyy-MM-dd");

                if (rowDate.Year == DateTime.Now.Year && rowDate.Month == DateTime.Now.Month)
                {
                    rowIndex = i;
                    break;
                }
            }

            DataGridViewTools.ScrollToRow(_grd, rowIndex, -3);
        }

        private void ControlsSetup()
        {
            _txtEntityName.MaxLength = 25;
            _dtpDate.CustomFormat = ConfigurationManager.AppSettings["DateFormat"];
            _txtDescription.MaxLength = 150;
            _txtAmount.TextAlign = HorizontalAlignment.Right;
            _txtAmount.Text = "0";

            _ckbInstallments.Checked = false;
            _txtInstallmentCurrent.Enabled = false;
            _txtInstallmentCurrent.MaxLength = 2;
            _txtInstallmentCurrent.TextAlign = HorizontalAlignment.Center;
            _txtInstallments.Enabled = false;
            _txtInstallments.MaxLength = 2;
            _txtInstallments.TextAlign = HorizontalAlignment.Center;

            _ckbService.Checked = false;
            _cbFrequency.Enabled = false;
            _cbFrequency.Items.Clear();
            _cbFrequency.Items.Add("1 Mes");
            _cbFrequency.Items.Add("3 Meses");
            _cbFrequency.Items.Add("6 Meses");
            _cbFrequency.Items.Add("12 Meses");
            _cbFrequency.SelectedIndex = _cbFrequency.FindString("1");

            GrdSetup();
        }

        private void ButtonsLogic()
        {
            var isCreditCardRest = _selectedDto?.TransactionType == TransactionType.CreditCardOutstanding;

            _tsbInsert.Enabled = _selectedDto == null;
            _tsbUpdate.Enabled = _selectedDto != null && !isCreditCardRest;
            _tsbDelete.Enabled = _selectedDto != null;
            _tsbNewPay.Enabled = _selectedDto != null && isCreditCardRest;
        }

        private void Clear()
        {
            _selectedDto = null;
            _checkBoxChangeDto = null;

            ClearInputs();
        }

        private void ClearInputs()
        {
            _txtEntityName.Text = "";
            _txtDescription.Text = "";
            _dtpDate.Value = DateTime.Now;
            _txtAmount.Text = "0";
            _cbCurrency.SelectedIndex = _cbCurrency.FindStringExact("ARS");

            _ckbService.Checked = false;
            _txtInstallmentCurrent.Text = "";
            _txtInstallments.Text = "";

            _ckbService.Checked = false;
            _cbFrequency.SelectedIndex = _cbFrequency.FindString("1");

            ButtonsLogic();
        }

        //events
        private void _tsbNewPay_Click(object sender, EventArgs e)
        {
            ButtonNewPayClick.Invoke(sender, e);
        }

        private void _tsbInsert_Click(object sender, EventArgs e)
        {
            ButtonInsertClick.Invoke(sender, e);
            Clear();
            ButtonsLogic();
        }

        private void _tsbUpdate_Click(object sender, EventArgs e)
        {
            ButtonUpdateClick.Invoke(sender, e);
            Clear();
            ButtonsLogic();
        }

        private void _tsbDelete_Click(object sender, EventArgs e)
        {
            ButtonDeleteClick.Invoke(sender, e);
            Clear();
            ButtonsLogic();
        }

        private void _tsbClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void _tsbExit_Click(object sender, EventArgs e)
        {
            ButtonExitClick.Invoke(sender, e);
        }

        private void _btnEntitySearch_Click(object sender, EventArgs e)
        {
            ButtonEntitySearchClick.Invoke(sender, e);
        }

        private void _ckbInstallments_CheckedChanged(object sender, EventArgs e)
        {
            _txtInstallments.Enabled = _ckbInstallments.Checked;

            if (_ckbInstallments.Checked)
                _ckbService.Checked = false;
        }

        private void _ckbService_CheckedChanged(object sender, EventArgs e)
        {
            _cbFrequency.Enabled = _ckbService.Checked;

            if (_ckbService.Checked)
                _ckbInstallments.Checked = false;
        }

        private void _grd_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var grd = sender as DataGridView;

            //Si es un separador
            if ((int)grd.Rows[e.RowIndex].Cells["id"].Value == -1)
                return;

            //Si el doble click es en los checkbox
            if (e.ColumnIndex == 9 || e.ColumnIndex == 10)
                return;

            _selectedDto = new TransactionViewDto
            {
                Id = (int)grd.Rows[e.RowIndex].Cells["id"].Value,
                TransactionType = (TransactionType)grd.Rows[e.RowIndex].Cells["type"].Value,
                Frequency = (int)grd.Rows[e.RowIndex].Cells["frequency"].Value,
                Date = DateTimeTools.Convert((string)grd.Rows[e.RowIndex].Cells["date"].Value, "yyyy-MM-dd"),
                EntityName = (string)grd.Rows[e.RowIndex].Cells["entity"].Value,
                Description = (string)grd.Rows[e.RowIndex].Cells["description"].Value,
                Installment = (string)grd.Rows[e.RowIndex].Cells["installments"].Value,
                CurrencyName = (string)grd.Rows[e.RowIndex].Cells["currency"].Value,
                Amount = DecimalTools.Convert((string)grd.Rows[e.RowIndex].Cells["amount"].Value),
                Concider = (bool)grd.Rows[e.RowIndex].Cells["concider"].Value,
                Paid = (bool)grd.Rows[e.RowIndex].Cells["paid"].Value,
            };

            ClearInputs();
            GrdDoubleClick.Invoke(sender, e);
            ButtonsLogic();
        }

        private void _grd_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //Si la fila es la de los headers
            if (e.RowIndex < 0)
                return;

            Color cellBorder = Color.FromArgb(50, 50, 50);

            //Consulto si la fila es un separador
            var isSeparator = (int)_grd.Rows[e.RowIndex].Cells["id"].Value == -1;

            //Evito que se muestren checkboxes en los separadores
            if (isSeparator && (e.ColumnIndex == 9 || e.ColumnIndex == 10))
            {
                // Establece el estilo de fondo de la celda igual al estilo de fondo del DataGridView
                e.CellStyle.BackColor = _grd.Rows[e.RowIndex].Cells["id"].Style.BackColor;
                e.CellStyle.SelectionBackColor = _grd.Rows[e.RowIndex].Cells["id"].Style.SelectionBackColor;

                // Dibuja la celda con el estilo personalizado
                e.PaintBackground(e.CellBounds, true);
                e.Handled = true;
            }
            else
            {
                // Dibuja el contenido predeterminado de la celda
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

                // Pinto el borde superior de la fecha
                if (e.RowIndex != 0 && (int)_grd.Rows[e.RowIndex - 1].Cells["id"].Value == -1)
                    DataGridViewTools.PaintCellBorder(e, cellBorder, DataGridViewBorder.TopBorder);

                // Pinto el borde derecho de la fecha
                if (e.ColumnIndex == 3)
                    DataGridViewTools.PaintCellBorder(e, cellBorder, DataGridViewBorder.RightBorder);

                // Indica que hemos manejado el evento y no se requiere el dibujo predeterminado
                e.Handled = true;
            }
        }

        private void _grd_Resize(object sender, EventArgs e)
        {
            _grd.Columns["description"].Width = _grd.Width - _colWidthTotal - 19;
        }

        private void _grd_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var grd = sender as DataGridView;

            //Si es un separador
            if ((int)grd.Rows[e.RowIndex].Cells["id"].Value == -1)
                return;

            //si el click NO es en los checkbox
            if (e.ColumnIndex != 9 && e.ColumnIndex != 10)
                return;

            _checkBoxChangeDto = new TransactionViewDto
            {
                Id = (int)grd.Rows[e.RowIndex].Cells["id"].Value,
                TransactionType = (TransactionType)grd.Rows[e.RowIndex].Cells["type"].Value,
                Frequency = (int)grd.Rows[e.RowIndex].Cells["frequency"].Value,
                Date = DateTimeTools.Convert((string)grd.Rows[e.RowIndex].Cells["date"].Value, "yyyy-MM-dd"),
                EntityName = (string)grd.Rows[e.RowIndex].Cells["entity"].Value,
                Description = (string)grd.Rows[e.RowIndex].Cells["description"].Value,
                Installment = (string)grd.Rows[e.RowIndex].Cells["installments"].Value,
                CurrencyName = (string)grd.Rows[e.RowIndex].Cells["currency"].Value,
                Amount = DecimalTools.Convert((string)grd.Rows[e.RowIndex].Cells["amount"].Value),
                Concider = (bool)grd.Rows[e.RowIndex].Cells["concider"].Value,
                Paid = (bool)grd.Rows[e.RowIndex].Cells["paid"].Value,
            };
            GrdValueChange.Invoke(sender, e);
        }

        public event EventHandler ButtonInsertClick;
        public event EventHandler ButtonNewPayClick;
        public event EventHandler ButtonUpdateClick;
        public event EventHandler ButtonDeleteClick;
        public event EventHandler ButtonExitClick;
        public event EventHandler ButtonEntitySearchClick;
        public event EventHandler GrdDoubleClick;
        public event EventHandler GrdValueChange;
    }
}
