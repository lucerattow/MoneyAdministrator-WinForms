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
using MoneyAdministrator.DTOs;
using System.Globalization;

namespace MoneyAdministrator.Views.UserControls
{
    public partial class DashboardView : UserControl, IDashboardView
    {
        private DateTime? _selectedPeriod;

        public DateTime? SelectedPeriod
        { 
            get => _selectedPeriod;
            set
            {
                _selectedPeriod = value;
                if (value != null)
                    _dtpDate.Value = (DateTime)value;
                else
                    _dtpDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            } 
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
        private int _colUsdCompareWidth = 250;
        private int _colWalletWidth = 600;

        public DashboardView()
        {
            this.Visible = false;

            Dock = DockStyle.Fill;
            InitializeComponent();
            ControlsSetup();
            AssosiateEvents();

            //Muestro la ventana ya cargada
            this.Visible = true;
        }

        //methods
        public void GrdRefreshData(List<DashboardViewDto> dashboardDtos)
        {
            //Limpio la grilla y el yearPicker
            _grd.Rows.Clear();

            if (dashboardDtos.Count <= 0)
                return;

            var years = dashboardDtos.Select(x => x.Period.Year).Distinct().ToList();

            var row = 0;
            Color SeparatorBackColor = Color.FromArgb(153, 0, 255);
            foreach (var year in years)
            {
                var yearDashboarDtos = dashboardDtos.Where(x => x.Period.Year == year).ToList();

                if (yearDashboarDtos.Count == 0)
                    continue;

                DateTime separatorDate = new DateTime(yearDashboarDtos[0].Period.Year, yearDashboarDtos[0].Period.Month, 1);
                //Añado un separador
                row = _grd.Rows.Add(new object[]
                {
                    yearDashboarDtos[0].Period.Year,
                    "",
                    "",
                    "",
                    "Año completo",
                    "",
                    "",
                    "",
                });

                //Pinto el separador
                foreach (DataGridViewCell cell in _grd.Rows[row].Cells)
                {
                    cell.Style.BackColor = SeparatorBackColor;
                    cell.Style.ForeColor = Color.White;
                    cell.Style.SelectionBackColor = cell.Style.BackColor;
                    cell.Style.SelectionForeColor = cell.Style.ForeColor;
                }
                SeparatorBackColor = Color.FromArgb(100, 100, 100);

                //Añado los registros a la tabla
                foreach (var dashboardDto in yearDashboarDtos)
                {
                    row = _grd.Rows.Add(new object[]
                    {
                        dashboardDto.Period.ToString("yyyy-MM"),
                        dashboardDto.UsdValue.ToString("#,##0.00 U$D", CultureInfo.GetCultureInfo("es-ES")),
                        dashboardDto.UsdSalary.ToString("#,##0.00 AR$", CultureInfo.GetCultureInfo("es-ES")),
                        dashboardDto.SalaryArs.ToString("#,##0.00 AR$", CultureInfo.GetCultureInfo("es-ES")),
                        dashboardDto.SalaryUsd.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")),
                        dashboardDto.Assets.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")),
                        dashboardDto.Passives.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")),
                        dashboardDto.Balance.ToString("#,##0.00 $", CultureInfo.GetCultureInfo("es-ES")),
                    });

                    //Pinto el monto segun corresponda
                    for (int i = 1; i < _grd.Rows[row].Cells.Count; i++)
                    {
                        var strValue = string.Concat(_grd.Rows[row].Cells[i].Value.ToString().Where(x => char.IsDigit(x) || x == ',' || x == '-'));

                        if (decimal.TryParse(strValue, out decimal value))
                        { 
                            if (value > 0)
                                _grd.Rows[row].Cells[i].Style.ForeColor = Color.Green;
                            else if (value < 0)
                                _grd.Rows[row].Cells[i].Style.ForeColor = Color.FromArgb(150, 0, 0);
                            else
                                _grd.Rows[row].Cells[i].Style.ForeColor = Color.FromArgb(80, 80, 80);
                        }
                    }
                }
            }
        }

        public void ClearInputs()
        {
            _selectedPeriod = null;
            _dtpDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            _txtUsdValue.Text = "";
            _txtUsdValue.OperatorSymbol = "+";
            _txtSalaryArs.Text = "";
            _txtSalaryArs.OperatorSymbol = "+";

            ButtonsLogic();
        }

        public void ButtonsLogic()
        {
            _tsbUpdate.Enabled = SelectedPeriod != null;
        }

        private void ControlsSetup()
        {
            GrdHeaderSetup();
            GrdSetup();
            ClearInputs();
        }

        private void GrdHeaderSetup()
        {
            ControlConfig.DataGridViewSetup(_grdHeader);

            //Configuracion de columnas
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
                Name = "wallet",
                HeaderText = "Billetera",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colWalletWidth,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
            });
        }

        private void GrdSetup()
        {
            int groupUsdCompare = 2;
            int groupWallet = 5;

            ControlConfig.DataGridViewSetup(_grd);

            //Configuracion de columnas
            _grd.Columns.Add(new DataGridViewColumn() //0 Periodo
            {
                Name = "date",
                HeaderText = "Periodo",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colPeriodWidth,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
            });
            _grd.Columns.Add(new DataGridViewColumn() //1 Coparar usd - USD
            {
                Name = "usdCompareReference",
                HeaderText = "USD",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colUsdCompareWidth / groupUsdCompare,

                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
            });
            _grd.Columns.Add(new DataGridViewColumn() //2 Coparar usd - Sueldo
            {
                Name = "usdCompareValue",
                HeaderText = "Sueldo",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colUsdCompareWidth / groupUsdCompare,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
            });
            _grd.Columns.Add(new DataGridViewColumn() //3 Billetera - Sueldo
            {
                Name = "walletSalaryArsWidth",
                HeaderText = "Sueldo ARS",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colWalletWidth / groupWallet,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
            });
            _grd.Columns.Add(new DataGridViewColumn() //3 Billetera - Sueldo
            {
                Name = "walletSalaryUsdWidth",
                HeaderText = "Sueldo USD",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colWalletWidth / groupWallet,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
            });
            _grd.Columns.Add(new DataGridViewColumn() //4 Billetera - Activos
            {
                Name = "walletAssetsWidth",
                HeaderText = "Activos",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colWalletWidth / groupWallet,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
            });
            _grd.Columns.Add(new DataGridViewColumn() //5 Billetera - Pasivos
            {
                Name = "walletPassives",
                HeaderText = "Pasivos",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colWalletWidth / groupWallet,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
            });
            _grd.Columns.Add(new DataGridViewColumn() //6 Billetera - Balance
            {
                Name = "walletBalance",
                HeaderText = "Balance",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colWalletWidth / groupWallet,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
            });
        }

        private void AssosiateEvents()
        {
            _tsbUpdate.Click += (sender, e) => ButtonUpdateClick?.Invoke(sender, e);
            _tsbExit.Click += (sender, e) => ButtonExitClick?.Invoke(sender, e);
        }

        //events
        private void _tsbClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void _grd_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var value = (sender as DataGridView).Rows[e.RowIndex].Cells[0].Value.ToString();

            //Si la fecha no tiene el tamaño para "yyyy-MM"
            if (value.Length != 7)
                return;

            string[] values = value.Split('-');
            int year = int.Parse(values[0]);
            int month = int.Parse(values[1]);

            SelectedPeriod = new DateTime(year, month, 1);

            GrdDoubleClick?.Invoke(sender, e);
            ButtonsLogic();
        }

        public event EventHandler ButtonUpdateClick;
        public event EventHandler ButtonExitClick;
        public event EventHandler GrdDoubleClick;
    }
}
