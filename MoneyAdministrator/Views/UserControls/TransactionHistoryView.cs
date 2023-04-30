using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Common.Enums;
using MoneyAdministrator.Common.Utilities.TypeTools;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Utilities;
using MoneyAdministrator.Utilities.ControlTools;
using MoneyAdministrator.Utilities.Disposable;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;

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
        private int _focusRow = 0;

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
        public int FocusRow
        {
            get => _focusRow;
            set => _focusRow = value;
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

                var row = 0;
                foreach (var year in datasource.OrderByDescending(x => x.Date).Select(x => x.Date.Year).Distinct())
                    for (var month = 12; month >= 1; month--)
                    {
                        List<TransactionViewDto> monthTransactions = datasource
                            .Where(x => x.Date.Year == year && x.Date.Month == month).OrderByDescending(x => x.Date.Day).ThenBy(x => x.Description).ToList();

                        if (monthTransactions.Count == 0)
                            continue;

                        //Determino los separadores
                        DateTime separatorDate = new DateTime(year, month, 1);
                        AddGrdMonthSeparator(_grd, ref row, separatorDate.ToString("yyyy"), separatorDate.ToString("(MM) MMM"));
                        PaintGrdMonthSeparator(_grd, row, year, month);

                        //Obtengo los detalles pasivos
                        var passive = monthTransactions.Where(x => x.Amount < 0)
                            .OrderByDescending(x => x.TransactionType).ToList();

                        //Obtengo los detalles activos
                        var assets = monthTransactions.Where(x => x.Amount >= 0)
                            .OrderByDescending(x => x.TransactionType).ToList();

                        //Añado los detalles services pasivos
                        AddGrdRows(_grd, ref row, passive, "Pasivos", true);

                        //Añado los detalles services activos
                        AddGrdRows(_grd, ref row, assets, "Activos", false);
                    }
            }

            GrdSetScroll(_grd, _focusRow);
        }

        private void AddGrdRows(DataGridView grd, ref int row, List<TransactionViewDto> dto, string separatorText, bool isPasive)
        {
            for (int i = 0; i < dto.Count; i++)
            {
                //Añado separador de servicios
                if (i == 0)
                {
                    AddGrdValueSeparator(grd, ref row, separatorText);
                    PaintGrdValueSeparator(grd, row, isPasive);
                }

                AddGrdRow(grd, ref row, dto[i]);
            }
        }

        private void AddGrdRow(DataGridView grd, ref int row, TransactionViewDto dto)
        {
            row = grd.Rows.Add(new object[]
            {
                dto.Id,
                dto.TransactionType,
                dto.Frequency,
                dto.Date.ToString("yyyy-MM-dd"),
                dto.EntityName,
                dto.Description,
                dto.Installment,
                dto.CurrencyName,
                dto.Amount.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")),
                dto.Concider,
                dto.Paid,
            });

            //Pinto el monto segun corresponda
            PaintDgvCells.PaintDecimal(grd, row, "amount");
        }

        private void AddGrdMonthSeparator(DataGridView grd, ref int row, string dateRow, string entityRow)
        {
            row = grd.Rows.Add(new object[]
            {
                -1,
                0,
                0,
                dateRow,
                entityRow,
                "",
                "",
                "",
                "",
                false,
                false,
            });
        }

        private void AddGrdValueSeparator(DataGridView grd, ref int row, string text)
        {
            row = grd.Rows.Add(new object[]
            {
                -2,
                0,
                0,
                "",
                text,
                "",
                "",
                "",
                "",
                false,
                false,
            });
        }

        private void PaintGrdMonthSeparator(DataGridView grd, int row, int year, int month)
        {
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

            //Pinto el separador
            if (year > DateTime.Now.Year)
                PaintDgvCells.PaintSeparator(grd, row, sepFutureBackColor, sepFutureForeColor);

            else if (year == DateTime.Now.Year && month == DateTime.Now.Month)
                PaintDgvCells.PaintSeparator(grd, row, sepCurrentMonthBackColor, sepCurrentForeColor);

            else if (year == DateTime.Now.Year)
                PaintDgvCells.PaintSeparator(grd, row, sepCurrentBackColor, sepCurrentForeColor);

            else if (year < DateTime.Now.Year)
                PaintDgvCells.PaintSeparator(grd, row, sepOldestBackColor, sepOldestForeColor);
        }

        private void PaintGrdValueSeparator(DataGridView grd, int row, bool isPassive)
        {
            //Separador Pasivos
            Color sepPassivesBackColor = Color.FromArgb(244, 204, 204);
            Color sepPassivesForeColor = Color.Black;
            //Separador Activos
            Color sepAssetsBackColor = Color.FromArgb(230, 255, 113);
            Color sepAssetsForeColor = Color.Black;

            if (isPassive)
                PaintDgvCells.PaintSeparator(grd, row, sepPassivesBackColor, sepPassivesForeColor);
            else
                PaintDgvCells.PaintSeparator(grd, row, sepAssetsBackColor, sepAssetsForeColor);
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

        private void GrdSetScroll(DataGridView grd, int detailId = 0)
        {
            int rowIndex = -1;

            //Obtengo la lista de filas con los valores que necesito para buscar
            int distanceToSeparator = 0;

            var values = new List<RowItem>();
            foreach (DataGridViewRow row in grd.Rows)
            {
                distanceToSeparator++;

                //Ignoro los separadores
                if ((int)row.Cells["id"].Value == -1)
                {
                    distanceToSeparator = 0;
                    continue;
                }
                else if ((int)row.Cells["id"].Value == -2)
                    continue;

                values.Add(new RowItem
                {
                    RowId = grd.Rows.IndexOf(row),
                    DetailId = (int)row.Cells["id"].Value,
                    Date = DateTimeTools.Convert((string)row.Cells["date"].Value, "yyyy-MM-dd"),
                    DistanceToSeparator = distanceToSeparator,
                });
            }

            if (detailId == 0)
            {
                var findedValue = values
                    .Where(x => x.Date.Year == DateTime.Now.Year && x.Date.Month == DateTime.Now.Month)
                    .FirstOrDefault();

                if (findedValue is null)
                    return;

                rowIndex = findedValue.RowId - findedValue.DistanceToSeparator;
            }
            else
            {
                var findedValue = values
                    .Where(x => x.DetailId == detailId)
                    .LastOrDefault();

                if (findedValue is null)
                    return;

                rowIndex = findedValue.RowId - findedValue.DistanceToSeparator;

                grd.ClearSelection();
                grd.Rows[findedValue.RowId].Selected = true;
            }

            DataGridViewTools.ScrollToRow(grd, rowIndex, -1);
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
            _focusRow = 0;

            ClearInputs();
        }

        private void ClearInputs()
        {
            _txtEntityName.Text = "";
            _txtDescription.Text = "";
            _dtpDate.Value = DateTime.Now;
            _txtAmount.Text = "0";
            _cbCurrency.SelectedIndex = _cbCurrency.FindStringExact("ARS");

            _ckbInstallments.Checked = false;
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
            if ((int)grd.Rows[e.RowIndex].Cells["id"].Value < 0)
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
            var isSeparator = (int)_grd.Rows[e.RowIndex].Cells["id"].Value < 0;

            if (isSeparator)
            {
                // Dibuja el contenido predeterminado de la celda
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

                //Evito que se muestren checkboxes en los separadores
                if (e.ColumnIndex == 9 || e.ColumnIndex == 10)
                    e.PaintBackground(e.CellBounds, true);

                // Pinto el borde derecho de la fecha
                if (e.ColumnIndex == 3)
                    DataGridViewTools.PaintCellBorder(e, cellBorder, DataGridViewBorder.RightBorder);

                // Pinto el borde inferior
                DataGridViewTools.PaintCellBorder(e, cellBorder, DataGridViewBorder.BottomBorder);

                e.Handled = true;
            }
            else
            {
                // Dibuja el contenido predeterminado de la celda
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

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

    internal class RowItem
    {
        public int RowId { get; set; }
        public int DetailId { get; set; }
        public DateTime Date { get; set; }
        public int DistanceToSeparator { get; set; }
    }
}
