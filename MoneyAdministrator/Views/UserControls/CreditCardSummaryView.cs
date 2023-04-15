﻿using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.DTOs.Enums;
using MoneyAdministrator.Interfaces;
using MoneyAdministrator.Models;
using MoneyAdministrator.Utilities;
using MoneyAdministrator.Utilities.Disposable;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MoneyAdministrator.Views
{
    public partial class CreditCardResumesView : UserControl, ICreditCardSummaryView
    {
        //fields
        private bool _importSupport = false;
        private bool _importedSummary = false;
        private int _selectedSummaryId = 0;
        private List<CreditCardSummaryDetailDto> _CCSummaryDetailDtos;

        //grd columns width
        private const int _colWidthDate = 90;
        private const int _colWidthInstall = 60;
        private const int _colWidthAmountArs = 120;
        private const int _colWidthAmountUsd = 120;
        private const int _colWidthTotal = _colWidthDate + _colWidthInstall + _colWidthAmountArs + _colWidthAmountUsd;

        //properties
        public CreditCard CreditCard
        {
            get => (CreditCard)_txtCreditCard.Tag;
            set
            {
                _txtCreditCard.Tag = value;
                _txtCreditCard.Text = $"{value.CreditCardBank.Name} {value.CreditCardBrand.Name} *{value.LastFourNumbers}";
                _importSupport = value.CreditCardBank.ImportSupport;
            }
        }
        public List<CreditCardSummaryDetailDto> CCSummaryDetailDtos
        {
            get => _CCSummaryDetailDtos;
            set
            {
                _CCSummaryDetailDtos = value;
                GrdRefreshData();
            }
        }
        public int SelectedSummaryId
        {
            get => _selectedSummaryId;
            set => _selectedSummaryId = value;
        }

        public DateTime Period
        {
            get => _dtpDatePeriod.Value;
            set => _dtpDatePeriod.Value = value;
        }
        public DateTime Date
        {
            get => _dtpDate.Value;
            set => _dtpDate.Value = value;
        }
        public DateTime Expiration
        {
            get => _dtpDateExpiration.Value;
            set => _dtpDateExpiration.Value = value;
        }
        public DateTime NextDate
        {
            get => _dtpDateNext.Value;
            set => _dtpDateNext.Value = value;
        }
        public DateTime NextExpiration
        {
            get => _dtpDateNextExpiration.Value;
            set => _dtpDateNextExpiration.Value = value;
        }
        public decimal TotalArs
        {
            get
            {
                var numbers = new string(_txtTotalArs.Text.Where(char.IsDigit).ToArray());
                var value = decimal.Parse(numbers) / 100;

                var oper = _txtTotalArs.OperatorSymbol;
                if (oper == "-" && value > 0 || oper == "+" && value < 0)
                    value *= -1;

                return value;
            }
            set
            {
                if (value >= 0)
                    _txtTotalArs.OperatorSymbol = "+";
                else
                    _txtTotalArs.OperatorSymbol = "-";

                _txtTotalArs.Text = value.ToString("N2");
            }
        }
        public decimal TotalUsd
        {
            get
            {
                var numbers = new string(_txtTotalUsd.Text.Where(char.IsDigit).ToArray());
                var value = decimal.Parse(numbers) / 100;

                var oper = _txtTotalUsd.OperatorSymbol;
                if (oper == "-" && value > 0 || oper == "+" && value < 0)
                    value *= -1;

                return value;
            }
            set
            {
                if (value >= 0)
                    _txtTotalUsd.OperatorSymbol = "+";
                else
                    _txtTotalUsd.OperatorSymbol = "-";

                _txtTotalUsd.Text = value.ToString("N2");
            }
        }
        public decimal minimumPayment
        {
            get
            {
                var numbers = new string(_txtMinimumPayment.Text.Where(char.IsDigit).ToArray());
                var value = decimal.Parse(numbers) / 100;

                var oper = _txtMinimumPayment.OperatorSymbol;
                if (oper == "-" && value > 0 || oper == "+" && value < 0)
                    value *= -1;

                return value;
            }
            set
            {
                if (value >= 0)
                    _txtMinimumPayment.OperatorSymbol = "+";
                else
                    _txtMinimumPayment.OperatorSymbol = "-";

                _txtMinimumPayment.Text = value.ToString("N2");
            }
        }

        public bool ImportedSummary
        {
            get => _importedSummary;
            set => _importedSummary = value;
        }

        public CreditCardResumesView()
        {
            this.Visible = false;

            using (new CursorWait())
            {
                Dock = DockStyle.Fill;
                InitializeComponent();

                AssosiateEvents();
                ControlsSetup();
            }

            //Muestro la ventana ya cargada
            this.Visible = true;
        }

        //methods
        public void TvRefreshData(List<TreeViewSummaryListDto> datasource)
        {
            _tvSummaryList.Nodes.Clear();

            if (datasource.Count == 0)
                return;

            datasource = datasource.OrderByDescending(x => x.Period).ToList();

            DateTime date = DateTime.MaxValue;
            var yearNode = new TreeNode();

            for (int i = 0; i < datasource.Count; i++)
            {
                if (date.Year > datasource[i].Period.Year)
                {
                    //Antes de crear un nodo anual nuevo, expando el anterior
                    yearNode.Expand();

                    date = datasource[i].Period;
                    yearNode = new TreeNode
                    {
                        Text = $"{date.Year}",
                    };
                    _tvSummaryList.Nodes.Add(yearNode);
                }

                var node = new TreeNode();
                node.Text = $"Periodo: {datasource[i].Period.ToString("yyyy-MM")}";
                node.Tag = datasource[i].Id;
                yearNode.Nodes.Add(node);
            }

            //Expando el ultimo año añadido
            yearNode.Expand();
        }

        private void GrdRefreshData()
        {
            var datasource = _CCSummaryDetailDtos;

            using (new CursorWait())
            using (new DataGridViewHide(_grd))
            {
                //Limpio la grilla y el yearPicker
                _grd.Rows.Clear();

                if (datasource.Count <= 0)
                    return;

                GrdRefreshDataDetailed(datasource, CreditCardSummaryDetailType.Summary);
                GrdRefreshDataDetailed(datasource, CreditCardSummaryDetailType.Details);
                GrdRefreshDataDetailed(datasource, CreditCardSummaryDetailType.Installments);
                GrdRefreshDataDetailed(datasource, CreditCardSummaryDetailType.AutomaticDebits);
                GrdRefreshDataDetailed(datasource, CreditCardSummaryDetailType.TaxesAndMaintenance);
            }
        }

        private void GrdRefreshDataDetailed(List<CreditCardSummaryDetailDto> datasource, CreditCardSummaryDetailType type)
        {
            //Guardo los colores de los separadores
            Color separatorBackColor = Color.FromArgb(116, 27, 71);
            Color separatorForeColor = Color.White;
            //Variable para guardar los id de las nuevas filas
            var row = 0;
            datasource = datasource.Where(x => x.Type == type).ToList();

            if (datasource.Count == 0)
                return;

            string separatorText = "";
            switch (type)
            {
                case CreditCardSummaryDetailType.Summary:
                    separatorText = "Resumen";
                    break;
                case CreditCardSummaryDetailType.Details:
                    separatorText = "Consumos";
                    break;
                case CreditCardSummaryDetailType.Installments:
                    separatorText = "Consumos en Cuotas";
                    break;
                case CreditCardSummaryDetailType.AutomaticDebits:
                    separatorText = "Debitos Automaticos";
                    break;
                case CreditCardSummaryDetailType.TaxesAndMaintenance:
                    separatorText = "Mantenimiento e Impuestos";
                    break;
            }

            //Añado un separador
            row = _grd.Rows.Add(new object[]
            {
                "",
                separatorText,
                "",
                "",
                "",
                "",
            });

            //Pinto el separador
            PaintDgvCells.PaintSeparator(_grd, row, separatorBackColor, separatorForeColor);

            foreach (var ccSummaryDetail in datasource)
            {
                var date = ccSummaryDetail.Date.ToString("yyyy-MM-dd");
                row = _grd.Rows.Add(new object[]
                {
                    date != "0001-01-01" ? date : "",
                    ccSummaryDetail.Description,
                    ccSummaryDetail.Installments,
                    ccSummaryDetail.AmountArs,
                    ccSummaryDetail.AmountUsd,
                });

                //Pinto el monto segun corresponda
                PaintDgvCells.PaintDecimal(_grd, row, "amountArs", true);
                PaintDgvCells.PaintDecimal(_grd, row, "amountUsd", true);
            }
        }

        public void ButtonsLogic()
        {
            bool creditCardLoaded = CreditCard != null;
            bool? _importSupport = CreditCard?.CreditCardBank.ImportSupport;
            bool importSupport = _importSupport ?? false;

            _tsbImport.Enabled = creditCardLoaded && importSupport;
            _tsbInsert.Enabled = creditCardLoaded && _importedSummary;
            _tsbDelete.Enabled = creditCardLoaded && !_importedSummary;
        }

        private void ClearInputs()
        {
            ClearSummaryInputs();

            _importSupport = false;
            _tvSummaryList.Nodes.Clear();

            _txtCreditCard.Tag = null;
            _txtCreditCard.Text = "";

            ButtonsLogic();
        }

        private void ClearSummaryInputs()
        {
            _grd.Rows.Clear();
            _dtpDatePeriod.Value = DateTime.Now;
            _dtpDate.Value = DateTime.Now;
            _dtpDateExpiration.Value = DateTime.Now;
            _dtpDateNext.Value = DateTime.Now;
            _dtpDateNextExpiration.Value = DateTime.Now;

            _txtTotalArs.Text = "0";
            _txtTotalUsd.Text = "0";
            _txtMinimumPayment.Text = "0";

            _selectedSummaryId = 0;
            _importedSummary = false;

            ButtonsLogic();
        }

        private void AssosiateEvents()
        {
            _tsbExit.Click += (sender, e) => ButtonExitClick?.Invoke(sender, e);
        }

        private void ControlsSetup()
        {
            _txtCreditCard.Tag = null;
            _txtCreditCard.Text = "";

            _dtpDatePeriod.CustomFormat = ConfigurationManager.AppSettings["DateFormatPeriod"];
            _dtpDate.CustomFormat = ConfigurationManager.AppSettings["DateFormat"];
            _dtpDateExpiration.CustomFormat = ConfigurationManager.AppSettings["DateFormat"];
            _dtpDateNext.CustomFormat = ConfigurationManager.AppSettings["DateFormat"];
            _dtpDateNextExpiration.CustomFormat = ConfigurationManager.AppSettings["DateFormat"];

            _dtpDatePeriod.Value = DateTime.Now;
            _dtpDate.Value = DateTime.Now;
            _dtpDateExpiration.Value = DateTime.Now;
            _dtpDateNext.Value = DateTime.Now;
            _dtpDateNextExpiration.Value = DateTime.Now;

            _txtTotalArs.Text = "0";
            _txtTotalUsd.Text = "0";
            _txtMinimumPayment.Text = "0";

            _dtpDate.Enabled = false;
            _dtpDateExpiration.Enabled = false;
            _dtpDateNext.Enabled = false;
            _dtpDateNextExpiration.Enabled = false;

            _txtTotalArs.Enabled = false;
            _txtTotalUsd.Enabled = false;
            _txtMinimumPayment.Enabled = false;

            _tvSummaryList.ImageList = imagesTreeView;

            GrdSetup();
            ButtonsLogic();
        }

        private void GrdSetup()
        {
            ControlConfig.DataGridViewSetup(_grd);

            //Configuracion de columnas
            _grd.Columns.Add(new DataGridViewColumn() //0 date
            {
                Name = "date",
                HeaderText = "Fecha",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colWidthDate,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
            });
            _grd.Columns.Add(new DataGridViewColumn() //1 date
            {
                Name = "description",
                HeaderText = "Descripcion",
                CellTemplate = new DataGridViewTextBoxCell(),
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft },
            });
            _grd.Columns.Add(new DataGridViewColumn() //2 date
            {
                Name = "installments",
                HeaderText = "Cuotas",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colWidthInstall,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter },
            });
            _grd.Columns.Add(new DataGridViewColumn() //3 date
            {
                Name = "amountArs",
                HeaderText = "ARS",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colWidthAmountArs,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
            });
            _grd.Columns.Add(new DataGridViewColumn() //4 date
            {
                Name = "amountUsd",
                HeaderText = "USD",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = _colWidthAmountUsd,
                DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
            });
        }

        //events
        private void _tsbImport_Click(object sender, EventArgs e)
        {
            ButtonImportClick?.Invoke(sender, e);
            ButtonsLogic();
        }

        private void _tsbInsert_Click(object sender, EventArgs e)
        {
            ButtonInsertClick.Invoke(sender, e);
            ImportedSummary = false;
            ButtonsLogic();
        }

        private void _tsbDelete_Click(object sender, EventArgs e)
        {
            ButtonDeleteClick.Invoke(sender, e);
            ImportedSummary = false;
            ClearSummaryInputs();
        }

        private void _tsbClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void _btnCreditCardSearch_Click(object sender, EventArgs e)
        {
            ButtonSearchCreditCardClick.Invoke(sender, e);
            ButtonsLogic();
        }

        private void CreditCardResumesView_Resize(object sender, EventArgs e)
        {
            _grd.Columns["description"].Width = _grd.Width - _colWidthTotal - 19;
        }

        private void _tvSummaryList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag == null)
                return;

            _selectedSummaryId = (int)e.Node.Tag;
            SummaryListNodeClick.Invoke(sender, e);
            ButtonsLogic();
        }

        private void _dtpDatePeriod_ValueChanged(object sender, EventArgs e)
        {
            var date = _dtpDatePeriod.Value;
            _dtpDatePeriod.Value = new DateTime(date.Year, date.Month, 1);
        }

        public event EventHandler ButtonImportClick;
        public event EventHandler ButtonInsertClick;
        public event EventHandler ButtonDeleteClick;
        public event EventHandler ButtonExitClick;
        public event EventHandler ButtonSearchCreditCardClick;
        public event EventHandler SummaryListNodeClick;
    }
}
