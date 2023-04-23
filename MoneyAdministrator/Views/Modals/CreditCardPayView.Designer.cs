namespace MoneyAdministrator.Views.Modals
{
    partial class CreditCardPayView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreditCardPayView));
            _pnlContainer = new Panel();
            panel2 = new Panel();
            _grd = new DataGridView();
            panel1 = new Panel();
            _lblDate = new Label();
            _dtpDate = new DateTimePicker();
            label1 = new Label();
            _txtCreditCard = new TextBox();
            _txtAmountPay = new CustomControls.MoneyTextBox();
            _lblEntityName = new Label();
            toolStrip1 = new ToolStrip();
            _tsbInsert = new ToolStripButton();
            _tsbUpdate = new ToolStripButton();
            _tsbDelete = new ToolStripButton();
            _tsbClear = new ToolStripButton();
            _pnlContainer.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_grd).BeginInit();
            panel1.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // _pnlContainer
            // 
            _pnlContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _pnlContainer.BackColor = Color.White;
            _pnlContainer.Controls.Add(panel2);
            _pnlContainer.Controls.Add(panel1);
            _pnlContainer.Controls.Add(toolStrip1);
            _pnlContainer.Location = new Point(7, 9);
            _pnlContainer.Margin = new Padding(3, 5, 3, 5);
            _pnlContainer.Name = "_pnlContainer";
            _pnlContainer.Size = new Size(1058, 316);
            _pnlContainer.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Controls.Add(_grd);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 88);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(1058, 228);
            panel2.TabIndex = 22;
            // 
            // _grd
            // 
            _grd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _grd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _grd.Location = new Point(5, 5);
            _grd.Margin = new Padding(3, 5, 3, 5);
            _grd.Name = "_grd";
            _grd.RowTemplate.Height = 25;
            _grd.Size = new Size(1048, 218);
            _grd.TabIndex = 20;
            _grd.CellMouseDoubleClick += _grd_CellMouseDoubleClick;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonFace;
            panel1.Controls.Add(_lblDate);
            panel1.Controls.Add(_dtpDate);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(_txtCreditCard);
            panel1.Controls.Add(_txtAmountPay);
            panel1.Controls.Add(_lblEntityName);
            panel1.Dock = DockStyle.Top;
            panel1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            panel1.Location = new Point(0, 27);
            panel1.Margin = new Padding(3, 5, 3, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(1058, 61);
            panel1.TabIndex = 21;
            // 
            // _lblDate
            // 
            _lblDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _lblDate.AutoSize = true;
            _lblDate.Location = new Point(780, 5);
            _lblDate.Margin = new Padding(4, 0, 4, 0);
            _lblDate.Name = "_lblDate";
            _lblDate.Size = new Size(107, 20);
            _lblDate.TabIndex = 27;
            _lblDate.Text = "Fecha de pago";
            // 
            // _dtpDate
            // 
            _dtpDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _dtpDate.CustomFormat = " yyyy-MM-dd";
            _dtpDate.Format = DateTimePickerFormat.Custom;
            _dtpDate.Location = new Point(783, 29);
            _dtpDate.Margin = new Padding(4);
            _dtpDate.Name = "_dtpDate";
            _dtpDate.Size = new Size(123, 27);
            _dtpDate.TabIndex = 28;
            _dtpDate.ValueChanged += _dtpDate_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1, 5);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(125, 20);
            label1.TabIndex = 26;
            label1.Text = "Tarjeta de credito";
            // 
            // _txtCreditCard
            // 
            _txtCreditCard.Location = new Point(5, 29);
            _txtCreditCard.Margin = new Padding(3, 4, 3, 4);
            _txtCreditCard.Name = "_txtCreditCard";
            _txtCreditCard.Size = new Size(355, 27);
            _txtCreditCard.TabIndex = 25;
            _txtCreditCard.KeyPress += _txtCreditCard_KeyPress;
            // 
            // _txtAmountPay
            // 
            _txtAmountPay.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtAmountPay.Colored = true;
            _txtAmountPay.ForeColor = Color.FromArgb(80, 80, 80);
            _txtAmountPay.Location = new Point(913, 29);
            _txtAmountPay.Margin = new Padding(3, 4, 3, 4);
            _txtAmountPay.Name = "_txtAmountPay";
            _txtAmountPay.OperatorSymbol = "-";
            _txtAmountPay.OperatorSymbolIsConstant = true;
            _txtAmountPay.Size = new Size(140, 27);
            _txtAmountPay.TabIndex = 24;
            _txtAmountPay.Text = "-0,00 $";
            _txtAmountPay.TextAlign = HorizontalAlignment.Right;
            _txtAmountPay.KeyDown += _txtAmountPay_KeyDown;
            _txtAmountPay.KeyPress += _txtAmountPay_KeyPress;
            // 
            // _lblEntityName
            // 
            _lblEntityName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _lblEntityName.AutoSize = true;
            _lblEntityName.Location = new Point(909, 5);
            _lblEntityName.Margin = new Padding(5, 0, 5, 0);
            _lblEntityName.Name = "_lblEntityName";
            _lblEntityName.Size = new Size(102, 20);
            _lblEntityName.TabIndex = 23;
            _lblEntityName.Text = "Saldo a pagar";
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = Color.White;
            toolStrip1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new ToolStripItem[] { _tsbInsert, _tsbUpdate, _tsbDelete, _tsbClear });
            toolStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(7, 0, 1, 0);
            toolStrip1.RenderMode = ToolStripRenderMode.System;
            toolStrip1.Size = new Size(1058, 27);
            toolStrip1.TabIndex = 17;
            toolStrip1.Text = "toolStrip1";
            // 
            // _tsbInsert
            // 
            _tsbInsert.Image = Properties.Resources.money_envelope_add_shadow;
            _tsbInsert.ImageAlign = ContentAlignment.MiddleLeft;
            _tsbInsert.ImageTransparentColor = Color.Magenta;
            _tsbInsert.Name = "_tsbInsert";
            _tsbInsert.Size = new Size(134, 24);
            _tsbInsert.Text = "Confirmar pago";
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
            // CreditCardPayView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(1071, 333);
            Controls.Add(_pnlContainer);
            Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "CreditCardPayView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CreditCardPayView";
            _pnlContainer.ResumeLayout(false);
            _pnlContainer.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_grd).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel _pnlContainer;
        private Panel panel2;
        private DataGridView _grd;
        private Panel panel1;
        private Label _lblEntityName;
        private ToolStrip toolStrip1;
        private ToolStripButton _tsbInsert;
        private ToolStripButton _tsbUpdate;
        private ToolStripButton _tsbDelete;
        private ToolStripButton _tsbClear;
        private Label label1;
        private TextBox textBox1;
        private CustomControls.MoneyTextBox _txtAmountPay;
        private TextBox _txtCreditCard;
        private Label _lblDate;
        private DateTimePicker _dtpDate;
    }
}