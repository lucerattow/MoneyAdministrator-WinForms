using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Utilities;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using MoneyAdministrator.Utilities.Disposable;
using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.Services;
using MoneyAdministrator.Utilities.ControlTools;
using MoneyAdministrator.Common.Utilities.TypeTools;

namespace MoneyAdministrator.Views.UserControls
{
    public partial class DashboardView : UserControl, IDashboardView
    {
        private DateTime? _selectedRecordPeriod;

        public DateTime? SelectedRecordPeriod
        {
            get => _selectedRecordPeriod?.Date;
            set
            {
                _selectedRecordPeriod = value?.Date;
                if (value != null)
                    _dtpDate.Value = ((DateTime)value).Date;
                else
                    _dtpDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
        }
        public DateTime Period
        {
            get => _dtpDate.Value.Date;
        }
        public decimal UsdValue
        {
            get
            {
                var numbers = new string(_txtUsdValue.Text.Where(char.IsDigit).ToArray());
                var value = decimal.Parse(numbers) / 100;

                var oper = _txtUsdValue.OperatorSymbol;
                if (oper == "-" && value > 0 || oper == "+" && value < 0)
                    value *= -1;

                return value;
            }
            set
            {
                if (value >= 0)
                    _txtUsdValue.OperatorSymbol = "+";
                else
                    _txtUsdValue.OperatorSymbol = "-";

                _txtUsdValue.Text = value.ToString("N2");
            }
        }
        public decimal SalaryArs
        {
            get
            {
                var numbers = new string(_txtSalaryArs.Text.Where(char.IsDigit).ToArray());
                var value = decimal.Parse(numbers) / 100;

                var oper = _txtSalaryArs.OperatorSymbol;
                if (oper == "-" && value > 0 || oper == "+" && value < 0)
                    value *= -1;

                return value;
            }
            set
            {
                if (value >= 0)
                    _txtSalaryArs.OperatorSymbol = "+";
                else
                    _txtSalaryArs.OperatorSymbol = "-";

                _txtSalaryArs.Text = value.ToString("N2");
            }
        }
        public decimal SalaryUsd
        {
            get
            {
                var numbers = new string(_txtSalaryUsd.Text.Where(char.IsDigit).ToArray());
                var value = decimal.Parse(numbers) / 100;

                var oper = _txtSalaryUsd.OperatorSymbol;
                if (oper == "-" && value > 0 || oper == "+" && value < 0)
                    value *= -1;

                return value;
            }
            set
            {
                if (value >= 0)
                    _txtSalaryUsd.OperatorSymbol = "+";
                else
                    _txtSalaryUsd.OperatorSymbol = "-";

                _txtSalaryUsd.Text = value.ToString("N2");
            }
        }

        //Configuracion de los anchos de columnas
        private int _colPeriodWidth = 90;
        private int _colUsdCompareWidth = 180;
        private int _colWalletWidth = 750;

        public DashboardView()
        {
            this.Visible = false;

            using (new CursorWait())
            {
                Dock = DockStyle.Fill;
                InitializeComponent();
                ControlsSetup();
                AssosiateEvents();
            }

            //Muestro la ventana ya cargada
            this.Visible = true;
        }

        //methods
        public void GrdRefreshData(List<DashboardDto> dashboardDtos)
        {
            using (new CursorWait())
            using (new DataGridViewHide(_grd))
            {
                //Limpio la grilla y el yearPicker
                _grd.Rows.Clear();

                if (dashboardDtos.Count <= 0)
                    return;

                var years = dashboardDtos.Select(x => x.Period.Year).Distinct().ToList();

                var row = 0;

                //Separador futuro
                Color sepFutureBackColor = Color.FromArgb(220, 190, 255);
                Color sepFutureForeColor = Color.Black;

                //Separador actual
                Color sepCurrentBackColor = Color.FromArgb(153, 0, 255);
                Color sepCurrentForeColor = Color.White;

                //Separador antiguo
                Color sepOldestBackColor = Color.FromArgb(200, 200, 200);
                Color sepOldestForeColor = Color.Black;

                foreach (var year in years)
                {
                    var yearDashboarDtos = dashboardDtos.Where(x => x.Period.Year == year).ToList();

                    if (yearDashboarDtos.Count == 0)
                        continue;

                    DateTime separatorDate = new DateTime(yearDashboarDtos[0].Period.Year, yearDashboarDtos[0].Period.Month, 1);
                    //Añado un separador
                    row = _grd.RowsAdd(new object[]
                    {
                        yearDashboarDtos[0].Period.ToString("'Año: 'yyyy"),
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                    }, true, 0, year != DateTime.Now.Year);

                    //Pinto el separador
                    if (year > DateTime.Now.Year)
                        CettoDataGridViewTools.PaintSeparator(_grd, row, sepFutureBackColor, sepFutureForeColor);
                    else if (year == DateTime.Now.Year)
                        CettoDataGridViewTools.PaintSeparator(_grd, row, sepCurrentBackColor, sepCurrentForeColor);
                    else if (year < DateTime.Now.Year)
                        CettoDataGridViewTools.PaintSeparator(_grd, row, sepOldestBackColor, sepOldestForeColor);

                    //Añado los registros a la tabla
                    foreach (var dashboardDto in yearDashboarDtos)
                    {
                        row = _grd.RowsAdd(new object[]
                        {
                            dashboardDto.Period.ToString("yyyy-MM"),
                            dashboardDto.UsdValue.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")),
                            dashboardDto.UsdSalary.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")),
                            dashboardDto.SalaryArs.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")),
                            dashboardDto.SalaryUsd.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")),
                            dashboardDto.Assets.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")),
                            dashboardDto.Passives.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")),
                            dashboardDto.Balance.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")),
                        }, false, 1, false);

                        //Pinto el monto segun corresponda
                        for (int col = 4; col < _grd.Rows[row].Cells.Count; col++)
                        {
                            //Pinto el periodo actual
                            if (col == 4)
                                DataGridViewTools.PaintCurrentDate(_grd, row, col);

                            //Pinto los valores de moneda
                            if (col >= 5)
                                DataGridViewTools.PaintDecimal(_grd, row, col);
                        }
                    }
                }
            }
        }

        public void ClearInputs()
        {
            _selectedRecordPeriod = null;
            _dtpDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            _txtUsdValue.Text = "";
            _txtUsdValue.OperatorSymbol = "+";
            _txtSalaryArs.Text = "";
            _txtSalaryArs.OperatorSymbol = "+";
            _txtSalaryUsd.Text = "";
            _txtSalaryUsd.OperatorSymbol = "+";

            ButtonsLogic();
        }

        public void ButtonsLogic()
        {
            _tsbInsert.Enabled = SelectedRecordPeriod == null;
            _tsbUpdate.Enabled = SelectedRecordPeriod != null;
            _tsbDelete.Enabled = SelectedRecordPeriod != null;
        }

        private void ControlsSetup()
        {
            GrdHeaderSetup();
            GrdSetup();
            ClearInputs();
        }

        private void GrdHeaderSetup()
        {
            DataGridViewTools.DataGridViewSetup(_grdHeader);

            //Configuracion de columnas
            _grdHeader.Columns.Add(new DataGridViewColumn() //0 groupHeader
            {
                Name = "groupHeader",
                HeaderText = "",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = 30,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
            });
            _grdHeader.Columns.Add(new DataGridViewColumn() //0 Resumen
            {
                Name = "resume",
                HeaderText = "Resumen",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colPeriodWidth,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
            });
            _grdHeader.Columns.Add(new DataGridViewColumn() //1 Comparacion con USD
            {
                Name = "usdCompare",
                HeaderText = "Comparacion",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colUsdCompareWidth,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
            });
            _grdHeader.Columns.Add(new DataGridViewColumn() //2 Billetera
            {
                Name = "transactions",
                HeaderText = "Transacciones",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colWalletWidth,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
            });
        }

        private void GrdSetup()
        {
            int groupUsdCompare = 2;
            int groupWallet = 5;

            DataGridViewTools.DataGridViewSetup(_grd);

            //Configuracion de columnas
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
                Name = "date",
                HeaderText = "Periodo",
                Width = _colPeriodWidth,
                ReadOnly = true,
            }); //4 date
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
                Name = "usdCompareReference",
                HeaderText = "USD",
                Width = _colUsdCompareWidth / groupUsdCompare,
                ReadOnly = true,
            }); //5 usdCompareReference
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
                Name = "usdCompareValue",
                HeaderText = "Sueldo",
                Width = _colUsdCompareWidth / groupUsdCompare,
                ReadOnly = true,
            }); //6 usdCompareValue
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
                Name = "walletSalaryArsWidth",
                HeaderText = "Sueldo ARS",
                Width = _colWalletWidth / groupWallet,
                ReadOnly = true,
            }); //7 walletSalaryArsWidth
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
                Name = "walletSalaryUsdWidth",
                HeaderText = "Sueldo USD",
                Width = _colWalletWidth / groupWallet,
                ReadOnly = true,
            }); //8 walletSalaryUsdWidth
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
                Name = "walletAssetsWidth",
                HeaderText = "Activos",
                Width = _colWalletWidth / groupWallet,
                ReadOnly = true,
            }); //9 walletAssetsWidth
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
                Name = "walletPassives",
                HeaderText = "Pasivos",
                Width = _colWalletWidth / groupWallet,
                ReadOnly = true,
            }); //10 walletPassives
            _grd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
                Name = "walletBalance",
                HeaderText = "Balance",
                Width = _colWalletWidth / groupWallet,
                ReadOnly = true,
            }); //11 walletBalance
        }

        private void AssosiateEvents()
        {
            _tsbExit.Click += (sender, e) => ButtonExitClick?.Invoke(sender, e);
        }

        //events
        private void _tsbInsert_Click(object sender, EventArgs e)
        {
            ButtonInsertClick.Invoke(sender, e);
            ClearInputs();
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

        private void _grd_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            using (new CursorWait())
            {
                var value = (sender as DataGridView).Rows[e.RowIndex].Cells[0].Value.ToString();

                //Si la fecha no tiene el tamaño para "yyyy-MM"
                if (value.Length != 7)
                    return;

                string[] values = value.Split('-');
                int year = int.Parse(values[0]);
                int month = int.Parse(values[1]);

                SelectedRecordPeriod = new DateTime(year, month, 1);

                GrdDoubleClick?.Invoke(sender, e);
                ButtonsLogic();
            }
        }

        private void _grd_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                _grdHeader.HorizontalScrollingOffset = e.NewValue;
            }
        }

        private void _grd_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //Si la fila es la de los headers
            if (e.RowIndex < 0)
                return;

            Color cellBorder = Color.FromArgb(50, 50, 50);

            //Consulto si la fila es un separador
            var isSeparator = (bool)_grd.Rows[e.RowIndex].Cells["IsGroupHeader"].Value;

            //Pinto linea en la columna con indicadores de grupos
            if (e.ColumnIndex == 3)
            {
                // Dibuja el contenido predeterminado de la celda
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);
                // Pinto el borde derecho de la fecha
                DataGridViewTools.PaintCellBorder(e, cellBorder, DataGridViewBorder.RightBorder);
                e.Handled = true;
                return;
            }

            // Dibuja el contenido predeterminado de la celda
            e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

            if (isSeparator)
            {
                // Pinto el borde inferior
                DataGridViewTools.PaintCellBorder(e, cellBorder, DataGridViewBorder.BottomBorder);
            }
            else
            {
                // Pinto el borde derecho de la fecha
                if (e.ColumnIndex == 4 || e.ColumnIndex == 6 || e.ColumnIndex == 9)
                    DataGridViewTools.PaintCellBorder(e, cellBorder, DataGridViewBorder.RightBorder);
            }

            // Indica que hemos manejado el evento y no se requiere el dibujo predeterminado
            e.Handled = true;
        }

        public event EventHandler ButtonInsertClick;
        public event EventHandler ButtonUpdateClick;
        public event EventHandler ButtonDeleteClick;
        public event EventHandler ButtonExitClick;
        public event EventHandler GrdDoubleClick;
    }
}
