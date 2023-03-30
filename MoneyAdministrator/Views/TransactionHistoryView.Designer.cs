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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._tsbInsert = new System.Windows.Forms.ToolStripButton();
            this._tsbUpdate = new System.Windows.Forms.ToolStripButton();
            this._tsbDelete = new System.Windows.Forms.ToolStripButton();
            this._tsbClear = new System.Windows.Forms.ToolStripButton();
            this._pnlContent = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this._lblFrequency = new System.Windows.Forms.Label();
            this._ckbService = new System.Windows.Forms.CheckBox();
            this._cbFrequency = new System.Windows.Forms.ComboBox();
            this._lblMoney = new System.Windows.Forms.Label();
            this._lblOrigin = new System.Windows.Forms.Label();
            this._lblDescription = new System.Windows.Forms.Label();
            this._txtInstallmentCurrent = new System.Windows.Forms.TextBox();
            this._cbCurrency = new System.Windows.Forms.ComboBox();
            this._txtDescription = new System.Windows.Forms.TextBox();
            this._lblDate = new System.Windows.Forms.Label();
            this._btnEntitySearch = new System.Windows.Forms.Button();
            this._txtEntityName = new System.Windows.Forms.TextBox();
            this._txtInstallments = new System.Windows.Forms.TextBox();
            this._txtAmount = new System.Windows.Forms.TextBox();
            this._lblAmount = new System.Windows.Forms.Label();
            this._dtpDate = new System.Windows.Forms.DateTimePicker();
            this._lblInstallments = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._ypYearPage = new MoneyAdministrator.CustomControls.YearPicker();
            this._grd = new System.Windows.Forms.DataGridView();
            this._tsbExit = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this._pnlContent.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grd)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._tsbInsert,
            this._tsbUpdate,
            this._tsbDelete,
            this._tsbClear,
            this._tsbExit});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(850, 25);
            this.toolStrip1.TabIndex = 16;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _tsbInsert
            // 
            this._tsbInsert.Image = global::MoneyAdministrator.Properties.Resources.document_add_shadow;
            this._tsbInsert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbInsert.Name = "_tsbInsert";
            this._tsbInsert.Size = new System.Drawing.Size(55, 22);
            this._tsbInsert.Text = "Crear";
            // 
            // _tsbUpdate
            // 
            this._tsbUpdate.Image = global::MoneyAdministrator.Properties.Resources.document_edit_shadow;
            this._tsbUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbUpdate.Name = "_tsbUpdate";
            this._tsbUpdate.Size = new System.Drawing.Size(78, 22);
            this._tsbUpdate.Text = "Modificar";
            // 
            // _tsbDelete
            // 
            this._tsbDelete.Image = global::MoneyAdministrator.Properties.Resources.document_error_shadow;
            this._tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbDelete.Name = "_tsbDelete";
            this._tsbDelete.Size = new System.Drawing.Size(70, 22);
            this._tsbDelete.Text = "Eliminar";
            // 
            // _tsbClear
            // 
            this._tsbClear.Image = global::MoneyAdministrator.Properties.Resources.document_shadow;
            this._tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbClear.Name = "_tsbClear";
            this._tsbClear.Size = new System.Drawing.Size(67, 22);
            this._tsbClear.Text = "Limpiar";
            // 
            // _pnlContent
            // 
            this._pnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pnlContent.BackColor = System.Drawing.Color.White;
            this._pnlContent.Controls.Add(this.panel1);
            this._pnlContent.Controls.Add(this._ypYearPage);
            this._pnlContent.Controls.Add(this.toolStrip1);
            this._pnlContent.Controls.Add(this._grd);
            this._pnlContent.Location = new System.Drawing.Point(5, 5);
            this._pnlContent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._pnlContent.Name = "_pnlContent";
            this._pnlContent.Size = new System.Drawing.Size(850, 410);
            this._pnlContent.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this._lblFrequency);
            this.panel1.Controls.Add(this._ckbService);
            this.panel1.Controls.Add(this._cbFrequency);
            this.panel1.Controls.Add(this._lblMoney);
            this.panel1.Controls.Add(this._lblOrigin);
            this.panel1.Controls.Add(this._lblDescription);
            this.panel1.Controls.Add(this._txtInstallmentCurrent);
            this.panel1.Controls.Add(this._cbCurrency);
            this.panel1.Controls.Add(this._txtDescription);
            this.panel1.Controls.Add(this._lblDate);
            this.panel1.Controls.Add(this._btnEntitySearch);
            this.panel1.Controls.Add(this._txtEntityName);
            this.panel1.Controls.Add(this._txtInstallments);
            this.panel1.Controls.Add(this._txtAmount);
            this.panel1.Controls.Add(this._lblAmount);
            this.panel1.Controls.Add(this._dtpDate);
            this.panel1.Controls.Add(this._lblInstallments);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(850, 144);
            this.panel1.TabIndex = 26;
            // 
            // _lblFrequency
            // 
            this._lblFrequency.AutoSize = true;
            this._lblFrequency.Location = new System.Drawing.Point(443, 6);
            this._lblFrequency.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblFrequency.Name = "_lblFrequency";
            this._lblFrequency.Size = new System.Drawing.Size(64, 15);
            this._lblFrequency.TabIndex = 28;
            this._lblFrequency.Text = "Frecuencia";
            // 
            // _ckbService
            // 
            this._ckbService.AutoSize = true;
            this._ckbService.Location = new System.Drawing.Point(371, 26);
            this._ckbService.Name = "_ckbService";
            this._ckbService.Size = new System.Drawing.Size(67, 19);
            this._ckbService.TabIndex = 25;
            this._ckbService.Text = "Servicio";
            this._ckbService.UseVisualStyleBackColor = true;
            // 
            // _cbFrequency
            // 
            this._cbFrequency.FormattingEnabled = true;
            this._cbFrequency.Items.AddRange(new object[] {
            "1 Mes.",
            "3 Meses.",
            "6 Meses.",
            "12 Meses."});
            this._cbFrequency.Location = new System.Drawing.Point(444, 24);
            this._cbFrequency.Name = "_cbFrequency";
            this._cbFrequency.Size = new System.Drawing.Size(87, 23);
            this._cbFrequency.TabIndex = 27;
            // 
            // _lblMoney
            // 
            this._lblMoney.AutoSize = true;
            this._lblMoney.Location = new System.Drawing.Point(130, 93);
            this._lblMoney.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblMoney.Name = "_lblMoney";
            this._lblMoney.Size = new System.Drawing.Size(51, 15);
            this._lblMoney.TabIndex = 26;
            this._lblMoney.Text = "Moneda";
            // 
            // _lblOrigin
            // 
            this._lblOrigin.AutoSize = true;
            this._lblOrigin.Location = new System.Drawing.Point(124, 6);
            this._lblOrigin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblOrigin.Name = "_lblOrigin";
            this._lblOrigin.Size = new System.Drawing.Size(47, 15);
            this._lblOrigin.TabIndex = 0;
            this._lblOrigin.Text = "Entidad";
            // 
            // _lblDescription
            // 
            this._lblDescription.AutoSize = true;
            this._lblDescription.Location = new System.Drawing.Point(8, 49);
            this._lblDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblDescription.Name = "_lblDescription";
            this._lblDescription.Size = new System.Drawing.Size(69, 15);
            this._lblDescription.TabIndex = 2;
            this._lblDescription.Text = "Descripcion";
            // 
            // _txtInstallmentCurrent
            // 
            this._txtInstallmentCurrent.Enabled = false;
            this._txtInstallmentCurrent.Location = new System.Drawing.Point(200, 111);
            this._txtInstallmentCurrent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._txtInstallmentCurrent.Name = "_txtInstallmentCurrent";
            this._txtInstallmentCurrent.Size = new System.Drawing.Size(25, 23);
            this._txtInstallmentCurrent.TabIndex = 24;
            // 
            // _cbCurrency
            // 
            this._cbCurrency.FormattingEnabled = true;
            this._cbCurrency.Location = new System.Drawing.Point(132, 111);
            this._cbCurrency.Name = "_cbCurrency";
            this._cbCurrency.Size = new System.Drawing.Size(53, 23);
            this._cbCurrency.TabIndex = 23;
            // 
            // _txtDescription
            // 
            this._txtDescription.Location = new System.Drawing.Point(10, 67);
            this._txtDescription.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._txtDescription.Name = "_txtDescription";
            this._txtDescription.Size = new System.Drawing.Size(521, 23);
            this._txtDescription.TabIndex = 3;
            // 
            // _lblDate
            // 
            this._lblDate.AutoSize = true;
            this._lblDate.Location = new System.Drawing.Point(8, 6);
            this._lblDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblDate.Name = "_lblDate";
            this._lblDate.Size = new System.Drawing.Size(38, 15);
            this._lblDate.TabIndex = 13;
            this._lblDate.Text = "Fecha";
            // 
            // _btnEntitySearch
            // 
            this._btnEntitySearch.Image = global::MoneyAdministrator.Properties.Resources.window_view;
            this._btnEntitySearch.Location = new System.Drawing.Point(301, 23);
            this._btnEntitySearch.Name = "_btnEntitySearch";
            this._btnEntitySearch.Size = new System.Drawing.Size(24, 25);
            this._btnEntitySearch.TabIndex = 22;
            this._btnEntitySearch.UseVisualStyleBackColor = true;
            // 
            // _txtEntityName
            // 
            this._txtEntityName.Location = new System.Drawing.Point(126, 24);
            this._txtEntityName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._txtEntityName.Name = "_txtEntityName";
            this._txtEntityName.Size = new System.Drawing.Size(175, 23);
            this._txtEntityName.TabIndex = 1;
            // 
            // _txtInstallments
            // 
            this._txtInstallments.Location = new System.Drawing.Point(234, 111);
            this._txtInstallments.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._txtInstallments.Name = "_txtInstallments";
            this._txtInstallments.Size = new System.Drawing.Size(25, 23);
            this._txtInstallments.TabIndex = 11;
            // 
            // _txtAmount
            // 
            this._txtAmount.Location = new System.Drawing.Point(10, 111);
            this._txtAmount.Name = "_txtAmount";
            this._txtAmount.Size = new System.Drawing.Size(116, 23);
            this._txtAmount.TabIndex = 18;
            // 
            // _lblAmount
            // 
            this._lblAmount.AutoSize = true;
            this._lblAmount.Location = new System.Drawing.Point(8, 93);
            this._lblAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblAmount.Name = "_lblAmount";
            this._lblAmount.Size = new System.Drawing.Size(43, 15);
            this._lblAmount.TabIndex = 6;
            this._lblAmount.Text = "Monto";
            // 
            // _dtpDate
            // 
            this._dtpDate.CustomFormat = " yyyy-MM-yy";
            this._dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._dtpDate.Location = new System.Drawing.Point(10, 24);
            this._dtpDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._dtpDate.Name = "_dtpDate";
            this._dtpDate.Size = new System.Drawing.Size(108, 23);
            this._dtpDate.TabIndex = 14;
            // 
            // _lblInstallments
            // 
            this._lblInstallments.AutoSize = true;
            this._lblInstallments.Location = new System.Drawing.Point(198, 93);
            this._lblInstallments.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblInstallments.Name = "_lblInstallments";
            this._lblInstallments.Size = new System.Drawing.Size(44, 15);
            this._lblInstallments.TabIndex = 4;
            this._lblInstallments.Text = "Cuotas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(224, 115);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 15);
            this.label2.TabIndex = 29;
            this.label2.Text = "/";
            // 
            // _ypYearPage
            // 
            this._ypYearPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ypYearPage.AvailableYears = ((System.Collections.Generic.List<int>)(resources.GetObject("_ypYearPage.AvailableYears")));
            this._ypYearPage.BackColor = System.Drawing.Color.Transparent;
            this._ypYearPage.ButtonNextImage = global::MoneyAdministrator.Properties.Resources.arrow_right_green;
            this._ypYearPage.ButtonPreviousImage = global::MoneyAdministrator.Properties.Resources.arrow_left_green;
            this._ypYearPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._ypYearPage.ForeColor = System.Drawing.SystemColors.ControlText;
            this._ypYearPage.Location = new System.Drawing.Point(10, 377);
            this._ypYearPage.MaximumSize = new System.Drawing.Size(9999, 23);
            this._ypYearPage.MinimumSize = new System.Drawing.Size(0, 23);
            this._ypYearPage.Name = "_ypYearPage";
            this._ypYearPage.Size = new System.Drawing.Size(830, 23);
            this._ypYearPage.TabIndex = 21;
            this._ypYearPage.Value = 2023;
            // 
            // _grd
            // 
            this._grd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grd.Location = new System.Drawing.Point(10, 179);
            this._grd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._grd.Name = "_grd";
            this._grd.Size = new System.Drawing.Size(830, 192);
            this._grd.TabIndex = 12;
            // 
            // _tsbExit
            // 
            this._tsbExit.Image = global::MoneyAdministrator.Properties.Resources.exit_shadow;
            this._tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbExit.Name = "_tsbExit";
            this._tsbExit.Size = new System.Drawing.Size(49, 22);
            this._tsbExit.Text = "Salir";
            // 
            // TransactionHistoryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this._pnlContent);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TransactionHistoryView";
            this.Size = new System.Drawing.Size(860, 420);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._pnlContent.ResumeLayout(false);
            this._pnlContent.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _tsbInsert;
        private System.Windows.Forms.ToolStripButton _tsbUpdate;
        private System.Windows.Forms.ToolStripButton _tsbDelete;
        private System.Windows.Forms.ToolStripButton _tsbClear;
        private System.Windows.Forms.Panel _pnlContent;
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
        private TextBox _txtAmount;
        private MoneyAdministrator.CustomControls.YearPicker _ypYearPage;
        private Button _btnEntitySearch;
        private ComboBox _cbCurrency;
        private TextBox _txtInstallmentCurrent;
        private Panel panel1;
        private CheckBox _ckbService;
        private Label _lblMoney;
        private Label _lblFrequency;
        private ComboBox _cbFrequency;
        private Label label2;
        private ToolStripButton _tsbExit;
    }
}
