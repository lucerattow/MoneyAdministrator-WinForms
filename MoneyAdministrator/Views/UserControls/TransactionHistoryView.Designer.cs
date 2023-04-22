using System.Windows.Forms;

namespace MoneyAdministrator.Views
{
    partial class TransactionHistoryView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransactionHistoryView));
            _toolStripButton = new ToolStrip();
            _tsbInsert = new ToolStripButton();
            _tsbNewPay = new ToolStripButton();
            _tsbUpdate = new ToolStripButton();
            _tsbDelete = new ToolStripButton();
            _tsbClear = new ToolStripButton();
            _tsbExit = new ToolStripButton();
            _pnlContent = new Panel();
            _grd = new DataGridView();
            _ypYearPage = new CustomControls.YearPicker();
            _pnlInputs = new Panel();
            _txtAmount = new CustomControls.MoneyTextBox();
            _cbFrequency = new ComboBox();
            _lblMoney = new Label();
            _lblOrigin = new Label();
            _lblDescription = new Label();
            _txtInstallmentCurrent = new TextBox();
            _cbCurrency = new ComboBox();
            _txtDescription = new TextBox();
            _lblDate = new Label();
            _btnEntitySearch = new Button();
            _txtEntityName = new TextBox();
            _txtInstallments = new TextBox();
            _lblAmount = new Label();
            _dtpDate = new DateTimePicker();
            _lblInstallments = new Label();
            _lnlInstallmentSeparator = new Label();
            _ckbService = new CheckBox();
            _toolStripButton.SuspendLayout();
            _pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_grd).BeginInit();
            _pnlInputs.SuspendLayout();
            SuspendLayout();
            // 
            // _toolStripButton
            // 
            _toolStripButton.AllowMerge = false;
            _toolStripButton.BackColor = Color.White;
            _toolStripButton.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            _toolStripButton.GripStyle = ToolStripGripStyle.Hidden;
            _toolStripButton.Items.AddRange(new ToolStripItem[] { _tsbInsert, _tsbNewPay, _tsbUpdate, _tsbDelete, _tsbClear, _tsbExit });
            _toolStripButton.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            _toolStripButton.Location = new Point(0, 0);
            _toolStripButton.Name = "_toolStripButton";
            _toolStripButton.Padding = new Padding(6, 0, 1, 0);
            _toolStripButton.RenderMode = ToolStripRenderMode.System;
            _toolStripButton.Size = new Size(800, 27);
            _toolStripButton.TabIndex = 16;
            _toolStripButton.Text = "toolStrip1";
            // 
            // _tsbInsert
            // 
            _tsbInsert.Image = Properties.Resources.document_add_shadow;
            _tsbInsert.ImageAlign = ContentAlignment.MiddleLeft;
            _tsbInsert.ImageTransparentColor = Color.Magenta;
            _tsbInsert.Name = "_tsbInsert";
            _tsbInsert.Size = new Size(64, 24);
            _tsbInsert.Text = "Crear";
            _tsbInsert.Click += _tsbInsert_Click;
            // 
            // _tsbNewPay
            // 
            _tsbNewPay.Image = Properties.Resources.money_envelope_add_shadow;
            _tsbNewPay.ImageTransparentColor = Color.Magenta;
            _tsbNewPay.Name = "_tsbNewPay";
            _tsbNewPay.Size = new Size(113, 24);
            _tsbNewPay.Text = "Pagar tarjeta";
            _tsbNewPay.Click += _tsbNewPay_Click;
            // 
            // _tsbUpdate
            // 
            _tsbUpdate.Image = Properties.Resources.document_edit_shadow;
            _tsbUpdate.ImageTransparentColor = Color.Magenta;
            _tsbUpdate.Name = "_tsbUpdate";
            _tsbUpdate.Size = new Size(93, 24);
            _tsbUpdate.Text = "Modificar";
            _tsbUpdate.Click += _tsbUpdate_Click;
            // 
            // _tsbDelete
            // 
            _tsbDelete.Image = Properties.Resources.document_error_shadow;
            _tsbDelete.ImageTransparentColor = Color.Magenta;
            _tsbDelete.Name = "_tsbDelete";
            _tsbDelete.Size = new Size(83, 24);
            _tsbDelete.Text = "Eliminar";
            _tsbDelete.Click += _tsbDelete_Click;
            // 
            // _tsbClear
            // 
            _tsbClear.Image = Properties.Resources.document_shadow;
            _tsbClear.ImageTransparentColor = Color.Magenta;
            _tsbClear.Name = "_tsbClear";
            _tsbClear.Size = new Size(79, 24);
            _tsbClear.Text = "Limpiar";
            _tsbClear.Click += _tsbClear_Click;
            // 
            // _tsbExit
            // 
            _tsbExit.Image = Properties.Resources.exit_shadow;
            _tsbExit.ImageTransparentColor = Color.Magenta;
            _tsbExit.Name = "_tsbExit";
            _tsbExit.Size = new Size(58, 24);
            _tsbExit.Text = "Salir";
            // 
            // _pnlContent
            // 
            _pnlContent.Controls.Add(_grd);
            _pnlContent.Controls.Add(_ypYearPage);
            _pnlContent.Dock = DockStyle.Fill;
            _pnlContent.Location = new Point(0, 143);
            _pnlContent.Name = "_pnlContent";
            _pnlContent.Size = new Size(800, 257);
            _pnlContent.TabIndex = 27;
            // 
            // _grd
            // 
            _grd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _grd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _grd.Location = new Point(5, 5);
            _grd.Margin = new Padding(4);
            _grd.Name = "_grd";
            _grd.Size = new Size(790, 214);
            _grd.TabIndex = 12;
            _grd.CellMouseDoubleClick += _grd_CellMouseDoubleClick;
            // 
            // _ypYearPage
            // 
            _ypYearPage.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _ypYearPage.AvailableYears = (List<int>)resources.GetObject("_ypYearPage.AvailableYears");
            _ypYearPage.BackColor = Color.Transparent;
            _ypYearPage.ButtonNextImage = Properties.Resources.arrow_right_green;
            _ypYearPage.ButtonPreviousImage = Properties.Resources.arrow_left_green;
            _ypYearPage.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            _ypYearPage.ForeColor = SystemColors.ControlText;
            _ypYearPage.Location = new Point(4, 222);
            _ypYearPage.Margin = new Padding(4);
            _ypYearPage.MaximumSize = new Size(11428, 30);
            _ypYearPage.MinimumSize = new Size(0, 30);
            _ypYearPage.Name = "_ypYearPage";
            _ypYearPage.Size = new Size(792, 30);
            _ypYearPage.TabIndex = 21;
            _ypYearPage.Value = 2023;
            // 
            // _pnlInputs
            // 
            _pnlInputs.BackColor = SystemColors.ButtonFace;
            _pnlInputs.Controls.Add(_txtAmount);
            _pnlInputs.Controls.Add(_cbFrequency);
            _pnlInputs.Controls.Add(_lblMoney);
            _pnlInputs.Controls.Add(_lblOrigin);
            _pnlInputs.Controls.Add(_lblDescription);
            _pnlInputs.Controls.Add(_txtInstallmentCurrent);
            _pnlInputs.Controls.Add(_cbCurrency);
            _pnlInputs.Controls.Add(_txtDescription);
            _pnlInputs.Controls.Add(_lblDate);
            _pnlInputs.Controls.Add(_btnEntitySearch);
            _pnlInputs.Controls.Add(_txtEntityName);
            _pnlInputs.Controls.Add(_txtInstallments);
            _pnlInputs.Controls.Add(_lblAmount);
            _pnlInputs.Controls.Add(_dtpDate);
            _pnlInputs.Controls.Add(_lblInstallments);
            _pnlInputs.Controls.Add(_lnlInstallmentSeparator);
            _pnlInputs.Controls.Add(_ckbService);
            _pnlInputs.Dock = DockStyle.Top;
            _pnlInputs.Location = new Point(0, 27);
            _pnlInputs.Margin = new Padding(4);
            _pnlInputs.Name = "_pnlInputs";
            _pnlInputs.Size = new Size(800, 116);
            _pnlInputs.TabIndex = 26;
            // 
            // _txtAmount
            // 
            _txtAmount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtAmount.Colored = true;
            _txtAmount.ForeColor = Color.FromArgb(80, 80, 80);
            _txtAmount.Location = new Point(403, 29);
            _txtAmount.Name = "_txtAmount";
            _txtAmount.OperatorSymbol = "-";
            _txtAmount.Size = new Size(132, 27);
            _txtAmount.TabIndex = 30;
            _txtAmount.Tag = "";
            _txtAmount.Text = "-0,00 $";
            _txtAmount.TextAlign = HorizontalAlignment.Right;
            // 
            // _cbFrequency
            // 
            _cbFrequency.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cbFrequency.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbFrequency.FormattingEnabled = true;
            _cbFrequency.Location = new Point(692, 29);
            _cbFrequency.Margin = new Padding(4);
            _cbFrequency.Name = "_cbFrequency";
            _cbFrequency.Size = new Size(103, 28);
            _cbFrequency.TabIndex = 27;
            // 
            // _lblMoney
            // 
            _lblMoney.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _lblMoney.AutoSize = true;
            _lblMoney.Location = new Point(532, 5);
            _lblMoney.Margin = new Padding(4, 0, 4, 0);
            _lblMoney.Name = "_lblMoney";
            _lblMoney.Size = new Size(64, 20);
            _lblMoney.TabIndex = 26;
            _lblMoney.Text = "Moneda";
            // 
            // _lblOrigin
            // 
            _lblOrigin.AutoSize = true;
            _lblOrigin.Location = new Point(132, 5);
            _lblOrigin.Margin = new Padding(4, 0, 4, 0);
            _lblOrigin.Name = "_lblOrigin";
            _lblOrigin.Size = new Size(60, 20);
            _lblOrigin.TabIndex = 0;
            _lblOrigin.Text = "Entidad";
            // 
            // _lblDescription
            // 
            _lblDescription.AutoSize = true;
            _lblDescription.Location = new Point(1, 60);
            _lblDescription.Margin = new Padding(4, 0, 4, 0);
            _lblDescription.Name = "_lblDescription";
            _lblDescription.Size = new Size(87, 20);
            _lblDescription.TabIndex = 2;
            _lblDescription.Text = "Descripcion";
            // 
            // _txtInstallmentCurrent
            // 
            _txtInstallmentCurrent.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtInstallmentCurrent.BackColor = Color.FromArgb(230, 230, 230);
            _txtInstallmentCurrent.Location = new Point(612, 29);
            _txtInstallmentCurrent.Margin = new Padding(4);
            _txtInstallmentCurrent.Name = "_txtInstallmentCurrent";
            _txtInstallmentCurrent.Size = new Size(28, 27);
            _txtInstallmentCurrent.TabIndex = 24;
            _txtInstallmentCurrent.KeyDown += _txtInstallmentCurrent_KeyDown;
            // 
            // _cbCurrency
            // 
            _cbCurrency.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cbCurrency.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbCurrency.FormattingEnabled = true;
            _cbCurrency.Location = new Point(536, 29);
            _cbCurrency.Margin = new Padding(4);
            _cbCurrency.Name = "_cbCurrency";
            _cbCurrency.Size = new Size(60, 28);
            _cbCurrency.TabIndex = 23;
            // 
            // _txtDescription
            // 
            _txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _txtDescription.Location = new Point(5, 84);
            _txtDescription.Margin = new Padding(4);
            _txtDescription.Name = "_txtDescription";
            _txtDescription.Size = new Size(790, 27);
            _txtDescription.TabIndex = 3;
            // 
            // _lblDate
            // 
            _lblDate.AutoSize = true;
            _lblDate.Location = new Point(2, 5);
            _lblDate.Margin = new Padding(4, 0, 4, 0);
            _lblDate.Name = "_lblDate";
            _lblDate.Size = new Size(47, 20);
            _lblDate.TabIndex = 13;
            _lblDate.Text = "Fecha";
            // 
            // _btnEntitySearch
            // 
            _btnEntitySearch.Image = Properties.Resources.window_view;
            _btnEntitySearch.Location = new Point(336, 28);
            _btnEntitySearch.Margin = new Padding(4);
            _btnEntitySearch.Name = "_btnEntitySearch";
            _btnEntitySearch.Size = new Size(29, 29);
            _btnEntitySearch.TabIndex = 22;
            _btnEntitySearch.UseVisualStyleBackColor = true;
            // 
            // _txtEntityName
            // 
            _txtEntityName.Location = new Point(136, 29);
            _txtEntityName.Margin = new Padding(4);
            _txtEntityName.Name = "_txtEntityName";
            _txtEntityName.Size = new Size(200, 27);
            _txtEntityName.TabIndex = 1;
            // 
            // _txtInstallments
            // 
            _txtInstallments.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtInstallments.Location = new Point(648, 29);
            _txtInstallments.Margin = new Padding(4);
            _txtInstallments.Name = "_txtInstallments";
            _txtInstallments.Size = new Size(28, 27);
            _txtInstallments.TabIndex = 11;
            _txtInstallments.KeyPress += _txtInstallments_KeyPress;
            // 
            // _lblAmount
            // 
            _lblAmount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _lblAmount.AutoSize = true;
            _lblAmount.Location = new Point(400, 5);
            _lblAmount.Margin = new Padding(4, 0, 4, 0);
            _lblAmount.Name = "_lblAmount";
            _lblAmount.Size = new Size(53, 20);
            _lblAmount.TabIndex = 6;
            _lblAmount.Text = "Monto";
            // 
            // _dtpDate
            // 
            _dtpDate.CustomFormat = " yyyy-MM-dd";
            _dtpDate.Format = DateTimePickerFormat.Custom;
            _dtpDate.Location = new Point(5, 29);
            _dtpDate.Margin = new Padding(4);
            _dtpDate.Name = "_dtpDate";
            _dtpDate.Size = new Size(123, 27);
            _dtpDate.TabIndex = 14;
            // 
            // _lblInstallments
            // 
            _lblInstallments.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _lblInstallments.AutoSize = true;
            _lblInstallments.Location = new Point(608, 5);
            _lblInstallments.Margin = new Padding(4, 0, 4, 0);
            _lblInstallments.Name = "_lblInstallments";
            _lblInstallments.Size = new Size(54, 20);
            _lblInstallments.TabIndex = 4;
            _lblInstallments.Text = "Cuotas";
            // 
            // _lnlInstallmentSeparator
            // 
            _lnlInstallmentSeparator.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _lnlInstallmentSeparator.AutoSize = true;
            _lnlInstallmentSeparator.Location = new Point(637, 30);
            _lnlInstallmentSeparator.Margin = new Padding(4, 0, 4, 0);
            _lnlInstallmentSeparator.Name = "_lnlInstallmentSeparator";
            _lnlInstallmentSeparator.Size = new Size(15, 20);
            _lnlInstallmentSeparator.TabIndex = 29;
            _lnlInstallmentSeparator.Text = "/";
            // 
            // _ckbService
            // 
            _ckbService.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _ckbService.AutoSize = true;
            _ckbService.Location = new Point(692, 5);
            _ckbService.Margin = new Padding(4);
            _ckbService.Name = "_ckbService";
            _ckbService.Size = new Size(80, 24);
            _ckbService.TabIndex = 25;
            _ckbService.Text = "Servicio";
            _ckbService.UseVisualStyleBackColor = true;
            _ckbService.CheckedChanged += _ckbService_CheckedChanged;
            // 
            // TransactionHistoryView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(_pnlContent);
            Controls.Add(_pnlInputs);
            Controls.Add(_toolStripButton);
            Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(4, 6, 4, 6);
            MinimumSize = new Size(700, 300);
            Name = "TransactionHistoryView";
            Size = new Size(800, 400);
            Resize += TransactionHistoryView_Resize;
            _toolStripButton.ResumeLayout(false);
            _toolStripButton.PerformLayout();
            _pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_grd).EndInit();
            _pnlInputs.ResumeLayout(false);
            _pnlInputs.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolStrip _toolStripButton;
        private System.Windows.Forms.ToolStripButton _tsbInsert;
        private System.Windows.Forms.ToolStripButton _tsbUpdate;
        private System.Windows.Forms.ToolStripButton _tsbDelete;
        private System.Windows.Forms.ToolStripButton _tsbClear;
        private System.Windows.Forms.Label _lblOrigin;
        private System.Windows.Forms.DataGridView _grd;
        private System.Windows.Forms.Label _lblInstallments;
        private System.Windows.Forms.DateTimePicker _dtpDate;
        private System.Windows.Forms.Label _lblAmount;
        private System.Windows.Forms.TextBox _txtInstallments;
        private System.Windows.Forms.TextBox _txtEntityName;
        private System.Windows.Forms.Label _lblDate;
        private System.Windows.Forms.TextBox _txtDescription;
        private System.Windows.Forms.Label _lblDescription;
        private MoneyAdministrator.CustomControls.YearPicker _ypYearPage;
        private Button _btnEntitySearch;
        private ComboBox _cbCurrency;
        private TextBox _txtInstallmentCurrent;
        private Panel _pnlInputs;
        private CheckBox _ckbService;
        private Label _lblMoney;
        private ComboBox _cbFrequency;
        private Label _lnlInstallmentSeparator;
        private ToolStripButton _tsbExit;
        private Panel _pnlContent;
        private CustomControls.MoneyTextBox _txtAmount;
        private ToolStripButton _tsbNewPay;
    }
}
