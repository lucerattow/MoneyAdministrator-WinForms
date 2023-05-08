using MoneyAdministrator.Common.DTOs.Views;
using MoneyAdministrator.Common.Enums;
using MoneyAdministrator.Common.Utilities.TypeTools;
using MoneyAdministrator.CustomControls;
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
        private TransactionHistoryDto? _selectedDto;
        private TransactionHistoryDto? _checkBoxChangeDto;
        private int _focusRow = 0;

        //properties
        public TransactionHistoryDto? SelectedDto
        {
            get => _selectedDto;
            set => _selectedDto = value;
        }
        public TransactionHistoryDto? CheckBoxChangeDto
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
            get => _dtpDate.Value.Date;
            set => _dtpDate.Value = value.Date;
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

        public void GrdRefreshData(List<TransactionHistoryDto> datasource)
        {
            using (new CursorWait())
            using (new DataGridViewHide(_cettogrd))
            {
                //Limpio la grilla
                _cettogrd.Rows.Clear();

                if (datasource.Count <= 0)
                    return;

                var row = 0;
                foreach (var year in datasource.OrderByDescending(x => x.Date).Select(x => x.Date.Year).Distinct())
                    for (var month = 12; month >= 1; month--)
                    {
                        List<TransactionHistoryDto> monthTransactions = datasource
                            .Where(x => x.Date.Year == year && x.Date.Month == month).OrderByDescending(x => x.Date.Day).ThenBy(x => x.Description).ToList();

                        if (monthTransactions.Count == 0)
                            continue;

                        //Determino los separadores
                        DateTime separatorDate = new DateTime(year, month, 1);
                        GrdInsertMonthSeparator(ref row, separatorDate);

                        //Obtengo los detalles pasivos
                        var passive = monthTransactions.Where(x => x.Amount < 0)
                            .OrderByDescending(x => x.TransactionType).ToList();

                        //Obtengo los detalles activos
                        var assets = monthTransactions.Where(x => x.Amount >= 0)
                            .OrderByDescending(x => x.TransactionType).ToList();

                        //Añado los detalles services pasivos
                        if (passive.Count > 0)
                        {
                            GrdInsertAmountSeparator(ref row, true);
                            foreach (var dto in passive)
                                GrdAddRow(ref row, dto);
                        }

                        //Añado los detalles services activos
                        if (assets.Count > 0)
                        {
                            GrdInsertAmountSeparator(ref row, false);
                            foreach (var dto in assets)
                                GrdAddRow(ref row, dto);
                        }
                    }
            }
        }

        public void GrdInsertRows(List<TransactionHistoryDto> dtos)
        {
            foreach (var dto in dtos)
                GrdInsertRow(dto);
        }

        public void GrdInsertRow(TransactionHistoryDto dto)
        {
            DateTime date = dto.Date;

            int initGroupIndex = _cettogrd.Rows.Count;
            int endGroupIndex = _cettogrd.Rows.Count;

            //Obtengo la posicion del separador con el año y mes iguales a mi dto
            for (int index = 0; index < _cettogrd.Rows.Count; index++)
            {
                //Si no es un separador, ignoro la fila
                if ((int)_cettogrd.Rows[index].Cells["id"].Value != -1)
                    continue;

                var year = IntTools.Convert(_cettogrd.Rows[index].Cells["date"].Value.ToString());
                var month = IntTools.Convert(_cettogrd.Rows[index].Cells["entity"].Value.ToString());

                DateTime separatorDate = new DateTime(year, month, 1);
                DateTime dtoDate = new DateTime(date.Year, date.Month, 1);

                //Ontengo el id del separador actual
                if (dtoDate == separatorDate)
                {
                    initGroupIndex = index + 1;
                }

                //obtengo el id del proximo separador
                if (dtoDate > separatorDate)
                {
                    endGroupIndex = index;
                    break;
                }
            }

            //Si no se encontro el separador, seteo el inicio en la misma fila que el final
            if (endGroupIndex < initGroupIndex) 
                initGroupIndex = endGroupIndex;

            //Determino si el dto es pasivo o activo
            var isPasive = dto.Amount < 0;

            //Compruebo que existan separadores por valor
            if (initGroupIndex != endGroupIndex)
            {
                //Obtengo las row index de los separadores de valor
                var passiveIndex = -1;
                var assetsIndex = -1;
                for (int index = initGroupIndex; index < endGroupIndex; index++)
                {
                    if ((int)_cettogrd.Rows[index].Cells["id"].Value != -2)
                        continue;

                    if (_cettogrd.Rows[index].Cells["entity"].Value.ToString() == "Pasivos")
                        passiveIndex = index;

                    if (_cettogrd.Rows[index].Cells["entity"].Value.ToString() == "Activos")
                    {
                        assetsIndex = index;
                        break;
                    }
                }

                //Determino si es necesario insertar un separador y termino de definir los rangos del grupo de celdas
                if (isPasive)
                {
                    //Si no existe el separador lo inserto
                    if (passiveIndex == -1)
                    {
                        GrdInsertAmountSeparator(ref initGroupIndex, isPasive, true);
                        initGroupIndex = initGroupIndex + 1;
                        assetsIndex = assetsIndex != -1 ? assetsIndex + 1 : -1;
                    }
                    else
                        initGroupIndex = passiveIndex + 1;

                    //Guardo la ultima row del grupo
                    endGroupIndex = assetsIndex != -1 ? assetsIndex : endGroupIndex;
                }
                else
                {
                    if (assetsIndex == -1)
                    {
                        GrdInsertAmountSeparator(ref endGroupIndex, isPasive, true);
                        initGroupIndex = endGroupIndex + 1;
                        endGroupIndex = endGroupIndex + 1;
                    }
                    else
                        initGroupIndex = assetsIndex + 1;
                }
            }
            //Si no habia separador por mes, entonces añado el separador por valor directamente
            else
            {
                //Añado el separador
                GrdInsertMonthSeparator(ref initGroupIndex, date, true);
                initGroupIndex++;
                GrdInsertAmountSeparator(ref initGroupIndex, isPasive, true);
                initGroupIndex++;
            }

            //Si se insertaron separadores actualizo el endGroupIndex
            if (endGroupIndex < initGroupIndex)
                endGroupIndex = initGroupIndex;

            //Determinar en que posicion añadir al dto, de modo que quede filtrado por typo y fecha
            var insertIndex = endGroupIndex;
            for (int index = initGroupIndex ; index < endGroupIndex; index++)
            {
                DateTime rowDate = DateTimeTools.Convert((string)_cettogrd.Rows[index].Cells["date"].Value, "yyyy-MM-dd");
                TransactionType type = (TransactionType)_cettogrd.Rows[index].Cells["type"].Value;
                string description = _cettogrd.Rows[index].Cells["description"].Value.ToString();

                if (dto.TransactionType > type) 
                {
                    insertIndex = index;
                    break;
                }
                else if (dto.TransactionType >= type)
                {
                    if (dto.Date > rowDate)
                    {
                        insertIndex = index;
                        break;
                    }
                    else if (dto.Date >= rowDate)
                    {
                        if (String.Compare(dto.Description, description) < 0)
                        {
                            insertIndex = index;
                            break;
                        }
                    }
                }
            }

            //Añado el detalle
            GrdAddRow(ref insertIndex, dto, true);

            //Si el detalle se encuentra en la fecha ingresada por el usuario le hago focus
            if (dto.Date.Year == Date.Year && dto.Date.Month == Date.Month)
            {
                _cettogrd.ClearSelection();
                _cettogrd.Rows[insertIndex].Selected = true;
            }
        }

        private void GrdInsertMonthSeparator(ref int row, DateTime date, bool insert = false)
        {
            var isCollapsed = true;

            if (date.Year == DateTime.Now.Year && date.Month == DateTime.Now.Month)
                isCollapsed = false;

            //Inserto el mes
            if (insert)
                row = _cettogrd.RowsInsert(row, new object[]
                {
                    date.ToString("yyyy"),
                    date.ToString("(MM) MMM"),
                    "",
                    "",
                    "",
                    "",
                    false,
                    false,
                    -1,
                    0,
                    0,
                    0,
                }, true, 0, false);
            else
                row = _cettogrd.RowsAdd(new object[]
                {
                    date.ToString("yyyy"),
                    date.ToString("(MM) MMM"),
                    "",
                    "",
                    "",
                    "",
                    false,
                    false,
                    -1,
                    0,
                    0,
                    0,
                }, true, 0, isCollapsed);

            //Separador futuro
            Color sepFutureBackColor = Color.FromArgb(252, 229, 205);
            Color sepFutureForeColor = Color.Black;
            //Separador año actual
            Color sepCurrentBackColor = Color.FromArgb(255, 153, 0);
            Color sepCurrentForeColor = Color.White;
            //Separador mes actual
            Color sepCurrentMonthBackColor = Color.FromArgb(178, 107, 0);
            //Separador antiguo
            Color sepOldestBackColor = Color.FromArgb(217, 217, 217);
            Color sepOldestForeColor = Color.Black;

            //Pinto el separador
            if (date.Year > DateTime.Now.Year)
                CettoDataGridViewTools.PaintSeparator(_cettogrd, row, sepFutureBackColor, sepFutureForeColor);

            else if (date.Year == DateTime.Now.Year && date.Month == DateTime.Now.Month)
                CettoDataGridViewTools.PaintSeparator(_cettogrd, row, sepCurrentMonthBackColor, sepCurrentForeColor);

            else if (date.Year == DateTime.Now.Year)
                CettoDataGridViewTools.PaintSeparator(_cettogrd, row, sepCurrentBackColor, sepCurrentForeColor);

            else if (date.Year < DateTime.Now.Year)
                CettoDataGridViewTools.PaintSeparator(_cettogrd, row, sepOldestBackColor, sepOldestForeColor);
        }

        private void GrdInsertAmountSeparator(ref int row, bool isPasive, bool insert = false)
        {
            var text = isPasive ? "Pasivos" : "Activos";

            if (insert)
                row = _cettogrd.RowsInsert(row, new object[]
                {
                    "",
                    text,
                    "",
                    "",
                    "",
                    "",
                    false,
                    false,
                    -2,
                    0,
                    0,
                    0,
                }, false, 1, false);
            else
                row = _cettogrd.RowsAdd(new object[]
                {
                    "",
                    text,
                    "",
                    "",
                    "",
                    "",
                    false,
                    false,
                    -2,
                    0,
                    0,
                    0,
                }, false, 1, false);

            //Separador Pasivos
            Color sepPassivesBackColor = Color.FromArgb(244, 204, 204);
            Color sepPassivesForeColor = Color.Black;
            //Separador Activos
            Color sepAssetsBackColor = Color.FromArgb(230, 255, 113);
            Color sepAssetsForeColor = Color.Black;

            if (isPasive)
                CettoDataGridViewTools.PaintSeparator(_cettogrd, row, sepPassivesBackColor, sepPassivesForeColor);
            else
                CettoDataGridViewTools.PaintSeparator(_cettogrd, row, sepAssetsBackColor, sepAssetsForeColor);
        }

        private void GrdAddRow(ref int row, TransactionHistoryDto dto, bool insert = false)
        {
            if (insert)
                row = _cettogrd.RowsInsert(row, new object[]
                {
                    dto.Date.ToString("yyyy-MM-dd"),
                    dto.EntityName,
                    dto.Description,
                    dto.Installment,
                    dto.CurrencyName,
                    dto.Amount.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")),
                    dto.Concider,
                    dto.Paid,
                    dto.Id,
                    dto.TransactionId,
                    dto.TransactionType,
                    dto.Frequency,
                }, false, 1, false);
            else
                row = _cettogrd.RowsAdd(new object[]
                {
                    dto.Date.ToString("yyyy-MM-dd"),
                    dto.EntityName,
                    dto.Description,
                    dto.Installment,
                    dto.CurrencyName,
                    dto.Amount.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")),
                    dto.Concider,
                    dto.Paid,
                    dto.Id,
                    dto.TransactionId,
                    dto.TransactionType,
                    dto.Frequency,
                }, false, 1, false);

            //Pinto el monto segun corresponda
            GrdPaintDetail(row, dto.Concider);
        }

        private void GrdPaintDetail(int row, bool active)
        {
            //Colores
            Color ForeColor = Color.Black;

            if (!active)
                ForeColor = Color.FromArgb(200, 200, 200);

            //Pinto el color de la fuente en cada celda
            for (int i = 4; i < _cettogrd.Rows[row].Cells.Count; i++)
            {
                _cettogrd.Rows[row].Cells[i].Style.ForeColor = ForeColor;
            }

            //Si esta activo, pinto el monto
            if (active)
                DataGridViewTools.PaintDecimal(_cettogrd, row, "amount");
        }

        public void GrdDeleteSelected(int transactionId, bool deleteSeparators = true)
        {
            var indexes = new List<int>();
            for (int index = 0; index < _cettogrd.Rows.Count; index++)
            {
                //Si es un separador continuo
                if ((int)_cettogrd.Rows[index].Cells["id"].Value < 0)
                    continue;

                //Añado el row index a la lista de filas a borrar
                if ((int)_cettogrd.Rows[index].Cells["transactionId"].Value == transactionId)
                    indexes.Add(index);
            }

            GrdDeleteSelected(indexes, deleteSeparators);
        }

        public void GrdDeleteSelectedService(int transactionId, TransactionType type, DateTime date, bool deleteCurrentMonth = false, bool deleteSeparators = true)
        {
            var indexes = new List<int>();
            for (int index = 0; index < _cettogrd.Rows.Count; index++)
            {
                //Si es un separador continuo
                if ((int)_cettogrd.Rows[index].Cells["id"].Value < 0)
                    continue;

                //Obtengo la fecha del servicio
                DateTime currentDate = DateTimeTools.Convert(_cettogrd.Rows[index].Cells["date"].Value.ToString(), "yyyy-MM-dd");

                if (deleteCurrentMonth)
                    //Ignoro meses futuros
                    if (currentDate > date)
                        continue;

                //ignoro meses pasados
                if (currentDate < date)
                    break;

                //Añado el row index a la lista de filas a borrar
                if ((int)_cettogrd.Rows[index].Cells["transactionId"].Value == transactionId)
                    indexes.Add(index);
            }

            GrdDeleteSelected(indexes, deleteSeparators);
        }

        private void GrdDeleteSelected(List<int> indexes, bool deleteSeparators = true)
        {
            //Ordeno de mayor a menor las filas y las elimino
            indexes = indexes.OrderByDescending(x => x).ToList();
            foreach (var index in indexes)
            {
                var upIsDetail = false;
                var downIsDetail = false;

                //Compruebo si arriba hay un detalle
                var previousIndex = index - 1;
                if (previousIndex != -1 && (int)_cettogrd.Rows[previousIndex].Cells["id"].Value > 0)
                    upIsDetail = true;

                //Compruebo si abajo hay un detalle
                var nextIndex = index + 1;
                if (nextIndex < _cettogrd.Rows.Count && (int)_cettogrd.Rows[nextIndex].Cells["id"].Value > 0)
                    downIsDetail = true;

                //Elimino el registro
                _cettogrd.RowDelete(index);

                //Si se marco que no se deben eliminar separadores, termino el codigo aqui
                if (!deleteSeparators)
                    continue;

                //Comparo si es necesario eliminar los separadores
                if (!upIsDetail && !downIsDetail)
                {
                    //Consulto 2 culumnas arriba esta el separador de mes
                    var monthSeparatorIndex = (int)_cettogrd.Rows[previousIndex - 1].Cells["id"].Value == -1;

                    //Si arriba es "Pasivos" y abajo NO es "Activos" o no hay mas filas
                    if ((_cettogrd.Rows[previousIndex].Cells["entity"].Value.ToString() == "Pasivos" && index < _cettogrd.Rows.Count &&
                        _cettogrd.Rows[index].Cells["entity"].Value.ToString() != "Activos") ||
                        (_cettogrd.Rows[previousIndex].Cells["entity"].Value.ToString() == "Pasivos" && index >= _cettogrd.Rows.Count))
                    {
                        _cettogrd.RowDelete(previousIndex);
                        _cettogrd.RowDelete(previousIndex - 1);
                    }
                    //Si arriba es "Activos" y no hay pasivos
                    else if (monthSeparatorIndex && _cettogrd.Rows[previousIndex].Cells["entity"].Value.ToString() == "Activos")
                    {
                        _cettogrd.RowDelete(previousIndex);
                        _cettogrd.RowDelete(previousIndex - 1);
                    }
                    //Elimino unicamente el separador de valor
                    else
                    {
                        _cettogrd.RowDelete(previousIndex);
                    }
                }
            }
        }

        private void GrdSetup()
        {
            DataGridViewTools.DataGridViewSetup(_cettogrd);

            //Configuracion de columnas
            //El CettoDataGridView inserta 4 columnas automaticamente, por lo que los demas indices se corren luego del 3
            _cettogrd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
                Name = "date",
                HeaderText = "Fecha",
                Width = _colWidthDate,
                ReadOnly = true,
            }); //4 date
            _cettogrd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "entity",
                HeaderText = "Entidad",
                Width = _colWidthEntity,
                ReadOnly = true,
            }); //5 entity
            _cettogrd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "description",
                HeaderText = "Descripcion",
                ReadOnly = true,
            }); //6 description
            _cettogrd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter },
                Name = "installments",
                HeaderText = "Cuotas",
                Width = _colWidthInstall,
                ReadOnly = true,
            }); //7 installments
            _cettogrd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter },
                Name = "currency",
                HeaderText = "Moneda",
                Width = _colWidthCurrency,
                ReadOnly = true,
            }); //8 currency
            _cettogrd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
                Name = "amount",
                HeaderText = "Monto",
                Width = _colWidthAmount,
                ReadOnly = true,
            }); //9 amount
            _cettogrd.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                CellTemplate = new CettoDataGridViewCheckBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter },
                Name = "concider",
                HeaderText = "Sumar",
                Width = _colCheckBox,
            }); //10 concider
            _cettogrd.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                CellTemplate = new CettoDataGridViewGreenCheckBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter },
                Name = "paid",
                HeaderText = "Pagado",
                Width = _colCheckBox,
            }); //11 paid
            _cettogrd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "id",
                HeaderText = "Id",
                ReadOnly = true,
                Visible = false,
            }); //12 id
            _cettogrd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "transactionId",
                HeaderText = "Transaction Id",
                ReadOnly = true,
                Visible = false,
            }); //13 transactionId
            _cettogrd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "type",
                HeaderText = "Tipo",
                ReadOnly = true,
                Visible = false,
            }); //14 type
            _cettogrd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "frequency",
                HeaderText = "Frecuencia",
                ReadOnly = true,
                Visible = false,
            }); //15 frequency
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
            var isService = _selectedDto?.TransactionType == TransactionType.Service;

            _tsbInsert.Enabled = _selectedDto == null;
            _tsbUpdate.Enabled = _selectedDto != null && !isCreditCardRest;
            _tsbDelete.Enabled = _selectedDto != null;
            _tsbNewPay.Enabled = _selectedDto != null && isCreditCardRest;

            _dtpDate.Enabled = !(isCreditCardRest || isService);

            _ckbInstallments.Enabled = _selectedDto == null;
            _ckbService.Enabled = _selectedDto == null;
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
        }

        private void _tsbUpdate_Click(object sender, EventArgs e)
        {
            var rowIndex = _cettogrd.FirstDisplayedScrollingRowIndex;

            ButtonUpdateClick.Invoke(sender, e);
            Clear();
            
            _cettogrd.FirstDisplayedScrollingRowIndex = rowIndex;
        }

        private void _tsbDelete_Click(object sender, EventArgs e)
        {
            ButtonDeleteClick.Invoke(sender, e);
            Clear();
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

        private void _grd_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var grd = sender as DataGridView;

            if (e.RowIndex < 0)
                return;

            //Si es un separador
            if ((int)grd.Rows[e.RowIndex].Cells["id"].Value <= 0)
                return;

            //Si no se esta editando un detalle, actualizo la fecha para crear una transaccion
            if (_selectedDto is null)
                this.Date = DateTimeTools.Convert((string)grd.Rows[e.RowIndex].Cells["date"].Value, "yyyy-MM-dd");
        }

        private void _grd_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var grd = sender as DataGridView;

            //Si es un separador
            if ((int)grd.Rows[e.RowIndex].Cells["id"].Value < 0)
                return;

            //Si el doble click es en los checkbox
            if (e.ColumnIndex == 10 || e.ColumnIndex == 11)
                return;

            _selectedDto = new TransactionHistoryDto
            {
                Id = (int)grd.Rows[e.RowIndex].Cells["id"].Value,
                TransactionId = (int)grd.Rows[e.RowIndex].Cells["transactionId"].Value,
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

        private void _grd_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var grd = sender as DataGridView;

            //Si es un separador
            if ((int)grd.Rows[e.RowIndex].Cells["id"].Value < 0)
                return;

            //si el click NO es en los checkbox
            if (e.ColumnIndex != 10 && e.ColumnIndex != 11)
                return;

            _checkBoxChangeDto = new TransactionHistoryDto
            {
                Id = (int)grd.Rows[e.RowIndex].Cells["id"].Value,
                TransactionId = (int)grd.Rows[e.RowIndex].Cells["transactionId"].Value,
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

            //Repinto la fila
            GrdPaintDetail(e.RowIndex, (bool)grd.Rows[e.RowIndex].Cells["concider"].Value);
        }

        private void _grd_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //Si la fila es la de los headers
            if (e.RowIndex < 0)
                return;

            Color cellBorder = Color.FromArgb(50, 50, 50);

            //Consulto si la fila es un separador
            var isSeparator = (int)_cettogrd.Rows[e.RowIndex].Cells["id"].Value < 0;

            if (e.ColumnIndex == 3)
            {
                // Dibuja el contenido predeterminado de la celda
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);
                // Pinto el borde derecho de la fecha
                DataGridViewTools.PaintCellBorder(e, cellBorder, DataGridViewBorder.RightBorder);
                e.Handled = true;
                return;
            }

            if (isSeparator)
            {
                // Dibuja el contenido predeterminado de la celda
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

                //Evito que se muestren checkboxes en los separadores
                if (e.ColumnIndex == 10 || e.ColumnIndex == 11)
                    e.PaintBackground(e.CellBounds, true);

                // Pinto el borde derecho de la fecha
                if (e.ColumnIndex == 4)
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
                if (e.ColumnIndex == 4)
                    DataGridViewTools.PaintCellBorder(e, cellBorder, DataGridViewBorder.RightBorder);

                // Indica que hemos manejado el evento y no se requiere el dibujo predeterminado
                e.Handled = true;
            }
        }

        private void _grd_Resize(object sender, EventArgs e)
        {
            _cettogrd.Columns["description"].Width = _cettogrd.Width - _cettogrd.ExpandColumnHeight - _colWidthTotal - 19;
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
