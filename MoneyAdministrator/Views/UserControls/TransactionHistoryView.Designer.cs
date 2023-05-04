using MoneyAdministrator.CustomControls;

namespace MoneyAdministrator.Views.UserControls
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
            _toolStripButton = new ToolStrip();
            _tsbNewPay = new ToolStripButton();
            _tsbInsert = new ToolStripButton();
            _tsbUpdate = new ToolStripButton();
            _tsbDelete = new ToolStripButton();
            _tsbClear = new ToolStripButton();
            _tsbExit = new ToolStripButton();
            panel1 = new Panel();
            groupBox2 = new GroupBox();
            _ckbInstallments = new CheckBox();
            _txtInstallmentCurrent = new TextBox();
            _txtInstallments = new TextBox();
            _lnlInstallmentSeparator = new Label();
            groupBox1 = new GroupBox();
            _ckbService = new CheckBox();
            _cbFrequency = new ComboBox();
            _txtAmount = new CustomControls.MoneyTextBox();
            _lblMoney = new Label();
            _lblOrigin = new Label();
            _lblDescription = new Label();
            _cbCurrency = new ComboBox();
            _txtDescription = new TextBox();
            _lblDate = new Label();
            _btnEntitySearch = new Button();
            _txtEntityName = new TextBox();
            _lblAmount = new Label();
            _dtpDate = new DateTimePicker();
            panel2 = new Panel();
            _cettogrd = new CettoDataGridView();
            _toolStripButton.SuspendLayout();
            panel1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_cettogrd).BeginInit();
            SuspendLayout();
            // 
            // _toolStripButton
            // 
            _toolStripButton.AllowMerge = false;
            _toolStripButton.BackColor = Color.White;
            _toolStripButton.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            _toolStripButton.GripStyle = ToolStripGripStyle.Hidden;
            _toolStripButton.Items.AddRange(new ToolStripItem[] { _tsbNewPay, _tsbInsert, _tsbUpdate, _tsbDelete, _tsbClear, _tsbExit });
            _toolStripButton.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            _toolStripButton.Location = new Point(0, 0);
            _toolStripButton.Name = "_toolStripButton";
            _toolStripButton.Padding = new Padding(7, 0, 1, 0);
            _toolStripButton.RenderMode = ToolStripRenderMode.System;
            _toolStripButton.Size = new Size(861, 27);
            _toolStripButton.TabIndex = 17;
            _toolStripButton.Text = "toolStrip1";
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
            _tsbExit.Click += _tsbExit_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(_txtAmount);
            panel1.Controls.Add(_lblMoney);
            panel1.Controls.Add(_lblOrigin);
            panel1.Controls.Add(_lblDescription);
            panel1.Controls.Add(_cbCurrency);
            panel1.Controls.Add(_txtDescription);
            panel1.Controls.Add(_lblDate);
            panel1.Controls.Add(_btnEntitySearch);
            panel1.Controls.Add(_txtEntityName);
            panel1.Controls.Add(_lblAmount);
            panel1.Controls.Add(_dtpDate);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(861, 116);
            panel1.TabIndex = 18;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox2.Controls.Add(_ckbInstallments);
            groupBox2.Controls.Add(_txtInstallmentCurrent);
            groupBox2.Controls.Add(_txtInstallments);
            groupBox2.Controls.Add(_lnlInstallmentSeparator);
            groupBox2.Location = new Point(670, 1);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(81, 66);
            groupBox2.TabIndex = 49;
            groupBox2.TabStop = false;
            // 
            // _ckbInstallments
            // 
            _ckbInstallments.AutoSize = true;
            _ckbInstallments.Location = new Point(5, 0);
            _ckbInstallments.Name = "_ckbInstallments";
            _ckbInstallments.Size = new Size(73, 24);
            _ckbInstallments.TabIndex = 0;
            _ckbInstallments.Text = "Cuotas";
            _ckbInstallments.UseVisualStyleBackColor = true;
            _ckbInstallments.CheckedChanged += _ckbInstallments_CheckedChanged;
            // 
            // _txtInstallmentCurrent
            // 
            _txtInstallmentCurrent.BackColor = Color.FromArgb(230, 230, 230);
            _txtInstallmentCurrent.Location = new Point(5, 28);
            _txtInstallmentCurrent.Margin = new Padding(4);
            _txtInstallmentCurrent.Name = "_txtInstallmentCurrent";
            _txtInstallmentCurrent.Size = new Size(28, 27);
            _txtInstallmentCurrent.TabIndex = 42;
            // 
            // _txtInstallments
            // 
            _txtInstallments.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtInstallments.Location = new Point(48, 28);
            _txtInstallments.Margin = new Padding(4);
            _txtInstallments.Name = "_txtInstallments";
            _txtInstallments.Size = new Size(28, 27);
            _txtInstallments.TabIndex = 37;
            // 
            // _lnlInstallmentSeparator
            // 
            _lnlInstallmentSeparator.Anchor = AnchorStyles.Top;
            _lnlInstallmentSeparator.AutoSize = true;
            _lnlInstallmentSeparator.Location = new Point(33, 30);
            _lnlInstallmentSeparator.Margin = new Padding(4, 0, 4, 0);
            _lnlInstallmentSeparator.Name = "_lnlInstallmentSeparator";
            _lnlInstallmentSeparator.Size = new Size(15, 20);
            _lnlInstallmentSeparator.TabIndex = 46;
            _lnlInstallmentSeparator.Text = "/";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox1.Controls.Add(_ckbService);
            groupBox1.Controls.Add(_cbFrequency);
            groupBox1.Location = new Point(756, 1);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(100, 66);
            groupBox1.TabIndex = 48;
            groupBox1.TabStop = false;
            // 
            // _ckbService
            // 
            _ckbService.AutoSize = true;
            _ckbService.Location = new Point(5, 0);
            _ckbService.Margin = new Padding(4);
            _ckbService.Name = "_ckbService";
            _ckbService.Size = new Size(80, 24);
            _ckbService.TabIndex = 43;
            _ckbService.Text = "Servicio";
            _ckbService.UseVisualStyleBackColor = true;
            _ckbService.CheckedChanged += _ckbService_CheckedChanged;
            // 
            // _cbFrequency
            // 
            _cbFrequency.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbFrequency.FormattingEnabled = true;
            _cbFrequency.Location = new Point(5, 28);
            _cbFrequency.Margin = new Padding(4);
            _cbFrequency.Name = "_cbFrequency";
            _cbFrequency.Size = new Size(90, 28);
            _cbFrequency.TabIndex = 45;
            // 
            // _txtAmount
            // 
            _txtAmount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtAmount.Colored = true;
            _txtAmount.ForeColor = Color.FromArgb(80, 80, 80);
            _txtAmount.Location = new Point(464, 28);
            _txtAmount.Name = "_txtAmount";
            _txtAmount.OperatorSymbol = "-";
            _txtAmount.OperatorSymbolIsConstant = false;
            _txtAmount.Size = new Size(132, 27);
            _txtAmount.TabIndex = 47;
            _txtAmount.Tag = "";
            _txtAmount.Text = "-0,00 $";
            _txtAmount.TextAlign = HorizontalAlignment.Right;
            // 
            // _lblMoney
            // 
            _lblMoney.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _lblMoney.AutoSize = true;
            _lblMoney.Location = new Point(599, 4);
            _lblMoney.Margin = new Padding(4, 0, 4, 0);
            _lblMoney.Name = "_lblMoney";
            _lblMoney.Size = new Size(64, 20);
            _lblMoney.TabIndex = 44;
            _lblMoney.Text = "Moneda";
            // 
            // _lblOrigin
            // 
            _lblOrigin.AutoSize = true;
            _lblOrigin.Location = new Point(132, 4);
            _lblOrigin.Margin = new Padding(4, 0, 4, 0);
            _lblOrigin.Name = "_lblOrigin";
            _lblOrigin.Size = new Size(60, 20);
            _lblOrigin.TabIndex = 31;
            _lblOrigin.Text = "Entidad";
            // 
            // _lblDescription
            // 
            _lblDescription.AutoSize = true;
            _lblDescription.Location = new Point(1, 59);
            _lblDescription.Margin = new Padding(4, 0, 4, 0);
            _lblDescription.Name = "_lblDescription";
            _lblDescription.Size = new Size(87, 20);
            _lblDescription.TabIndex = 33;
            _lblDescription.Text = "Descripcion";
            // 
            // _cbCurrency
            // 
            _cbCurrency.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cbCurrency.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbCurrency.FormattingEnabled = true;
            _cbCurrency.Location = new Point(603, 28);
            _cbCurrency.Margin = new Padding(4);
            _cbCurrency.Name = "_cbCurrency";
            _cbCurrency.Size = new Size(60, 28);
            _cbCurrency.TabIndex = 41;
            // 
            // _txtDescription
            // 
            _txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _txtDescription.Location = new Point(5, 83);
            _txtDescription.Margin = new Padding(4);
            _txtDescription.Name = "_txtDescription";
            _txtDescription.Size = new Size(851, 27);
            _txtDescription.TabIndex = 34;
            // 
            // _lblDate
            // 
            _lblDate.AutoSize = true;
            _lblDate.Location = new Point(2, 4);
            _lblDate.Margin = new Padding(4, 0, 4, 0);
            _lblDate.Name = "_lblDate";
            _lblDate.Size = new Size(47, 20);
            _lblDate.TabIndex = 38;
            _lblDate.Text = "Fecha";
            // 
            // _btnEntitySearch
            // 
            _btnEntitySearch.Image = Properties.Resources.window_view;
            _btnEntitySearch.Location = new Point(336, 27);
            _btnEntitySearch.Margin = new Padding(4);
            _btnEntitySearch.Name = "_btnEntitySearch";
            _btnEntitySearch.Size = new Size(29, 29);
            _btnEntitySearch.TabIndex = 40;
            _btnEntitySearch.UseVisualStyleBackColor = true;
            _btnEntitySearch.Click += _btnEntitySearch_Click;
            // 
            // _txtEntityName
            // 
            _txtEntityName.Location = new Point(136, 28);
            _txtEntityName.Margin = new Padding(4);
            _txtEntityName.Name = "_txtEntityName";
            _txtEntityName.Size = new Size(200, 27);
            _txtEntityName.TabIndex = 32;
            // 
            // _lblAmount
            // 
            _lblAmount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _lblAmount.AutoSize = true;
            _lblAmount.Location = new Point(460, 4);
            _lblAmount.Margin = new Padding(4, 0, 4, 0);
            _lblAmount.Name = "_lblAmount";
            _lblAmount.Size = new Size(53, 20);
            _lblAmount.TabIndex = 36;
            _lblAmount.Text = "Monto";
            // 
            // _dtpDate
            // 
            _dtpDate.CustomFormat = " yyyy-MM-dd";
            _dtpDate.Format = DateTimePickerFormat.Custom;
            _dtpDate.Location = new Point(5, 28);
            _dtpDate.Margin = new Padding(4);
            _dtpDate.Name = "_dtpDate";
            _dtpDate.ShowUpDown = true;
            _dtpDate.Size = new Size(123, 27);
            _dtpDate.TabIndex = 39;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(_cettogrd);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 143);
            panel2.Name = "panel2";
            panel2.Size = new Size(861, 277);
            panel2.TabIndex = 19;
            // 
            // _cettogrd
            // 
            _cettogrd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _cettogrd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _cettogrd.Location = new Point(5, 5);
            _cettogrd.Margin = new Padding(4);
            _cettogrd.Name = "_cettogrd";
            _cettogrd.Size = new Size(851, 267);
            _cettogrd.TabIndex = 13;
            _cettogrd.CellMouseClick += _grd_CellMouseClick;
            _cettogrd.CellMouseDoubleClick += _grd_CellMouseDoubleClick;
            _cettogrd.CellPainting += _grd_CellPainting;
            _cettogrd.CellValueChanged += _grd_CellValueChanged;
            _cettogrd.Resize += _grd_Resize;
            // 
            // TransactionHistoryView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(_toolStripButton);
            Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(3, 4, 3, 4);
            Name = "TransactionHistoryView";
            Size = new Size(861, 420);
            _toolStripButton.ResumeLayout(false);
            _toolStripButton.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_cettogrd).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip _toolStripButton;
        private ToolStripButton _tsbInsert;
        private ToolStripButton _tsbNewPay;
        private ToolStripButton _tsbUpdate;
        private ToolStripButton _tsbDelete;
        private ToolStripButton _tsbClear;
        private ToolStripButton _tsbExit;
        private Panel panel1;
        private CustomControls.MoneyTextBox _txtAmount;
        private ComboBox _cbFrequency;
        private Label _lblMoney;
        private Label _lblOrigin;
        private Label _lblDescription;
        private TextBox _txtInstallmentCurrent;
        private ComboBox _cbCurrency;
        private TextBox _txtDescription;
        private Label _lblDate;
        private Button _btnEntitySearch;
        private TextBox _txtEntityName;
        private TextBox _txtInstallments;
        private Label _lblAmount;
        private DateTimePicker _dtpDate;
        private Label _lnlInstallmentSeparator;
        private CheckBox _ckbService;
        private Panel panel2;
        private GroupBox groupBox2;
        private CheckBox _ckbInstallments;
        private GroupBox groupBox1;
        private CettoDataGridView _cettogrd;
    }
}
