namespace MoneyAdministrator.Views.UserControls
{
    partial class Dashboard
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
            this._grd = new System.Windows.Forms.DataGridView();
            this._grdHeader = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this._lblDate = new System.Windows.Forms.Label();
            this._dtpDate = new System.Windows.Forms.DateTimePicker();
            this._txtSalaryArs = new MoneyAdministrator.CustomControls.MoneyTextBox();
            this._lblSalaryArs = new System.Windows.Forms.Label();
            this._txtUsdValue = new MoneyAdministrator.CustomControls.MoneyTextBox();
            this._lblUsdValue = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._tsbUpdate = new System.Windows.Forms.ToolStripButton();
            this._tsbClear = new System.Windows.Forms.ToolStripButton();
            this._tsbExit = new System.Windows.Forms.ToolStripButton();
            this._txtSalaryUsd = new MoneyAdministrator.CustomControls.MoneyTextBox();
            this._lblSalaryUsd = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._grd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._grdHeader)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _grd
            // 
            this._grd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grd.Location = new System.Drawing.Point(6, 33);
            this._grd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._grd.Name = "_grd";
            this._grd.RowTemplate.Height = 25;
            this._grd.Size = new System.Drawing.Size(910, 387);
            this._grd.TabIndex = 0;
            this._grd.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._grd_CellMouseDoubleClick);
            // 
            // _grdHeader
            // 
            this._grdHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._grdHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grdHeader.Location = new System.Drawing.Point(6, 7);
            this._grdHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._grdHeader.Name = "_grdHeader";
            this._grdHeader.RowTemplate.Height = 25;
            this._grdHeader.Size = new System.Drawing.Size(910, 29);
            this._grdHeader.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this._txtSalaryUsd);
            this.panel1.Controls.Add(this._lblSalaryUsd);
            this.panel1.Controls.Add(this._lblDate);
            this.panel1.Controls.Add(this._dtpDate);
            this.panel1.Controls.Add(this._txtSalaryArs);
            this.panel1.Controls.Add(this._lblSalaryArs);
            this.panel1.Controls.Add(this._txtUsdValue);
            this.panel1.Controls.Add(this._lblUsdValue);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(921, 61);
            this.panel1.TabIndex = 2;
            // 
            // _lblDate
            // 
            this._lblDate.AutoSize = true;
            this._lblDate.Location = new System.Drawing.Point(3, 3);
            this._lblDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblDate.Name = "_lblDate";
            this._lblDate.Size = new System.Drawing.Size(60, 20);
            this._lblDate.TabIndex = 35;
            this._lblDate.Text = "Periodo";
            // 
            // _dtpDate
            // 
            this._dtpDate.CustomFormat = " yyyy-MM";
            this._dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._dtpDate.Location = new System.Drawing.Point(6, 27);
            this._dtpDate.Margin = new System.Windows.Forms.Padding(4);
            this._dtpDate.Name = "_dtpDate";
            this._dtpDate.Size = new System.Drawing.Size(95, 27);
            this._dtpDate.TabIndex = 36;
            // 
            // _txtSalaryArs
            // 
            this._txtSalaryArs.Colored = true;
            this._txtSalaryArs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this._txtSalaryArs.Location = new System.Drawing.Point(272, 27);
            this._txtSalaryArs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._txtSalaryArs.Name = "_txtSalaryArs";
            this._txtSalaryArs.OperatorSymbol = "-";
            this._txtSalaryArs.Size = new System.Drawing.Size(150, 27);
            this._txtSalaryArs.TabIndex = 34;
            this._txtSalaryArs.Tag = "";
            this._txtSalaryArs.Text = "-0,00 $";
            // 
            // _lblSalaryArs
            // 
            this._lblSalaryArs.AutoSize = true;
            this._lblSalaryArs.Location = new System.Drawing.Point(269, 3);
            this._lblSalaryArs.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this._lblSalaryArs.Name = "_lblSalaryArs";
            this._lblSalaryArs.Size = new System.Drawing.Size(106, 20);
            this._lblSalaryArs.TabIndex = 33;
            this._lblSalaryArs.Text = "Sueldo en ARS";
            // 
            // _txtUsdValue
            // 
            this._txtUsdValue.Colored = true;
            this._txtUsdValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this._txtUsdValue.Location = new System.Drawing.Point(108, 27);
            this._txtUsdValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._txtUsdValue.Name = "_txtUsdValue";
            this._txtUsdValue.OperatorSymbol = "-";
            this._txtUsdValue.Size = new System.Drawing.Size(150, 27);
            this._txtUsdValue.TabIndex = 32;
            this._txtUsdValue.Tag = "";
            this._txtUsdValue.Text = "-0,00 $";
            // 
            // _lblUsdValue
            // 
            this._lblUsdValue.AutoSize = true;
            this._lblUsdValue.Location = new System.Drawing.Point(106, 3);
            this._lblUsdValue.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this._lblUsdValue.Name = "_lblUsdValue";
            this._lblUsdValue.Size = new System.Drawing.Size(127, 20);
            this._lblUsdValue.TabIndex = 31;
            this._lblUsdValue.Text = "Valor USD en ARS";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._grdHeader);
            this.panel2.Controls.Add(this._grd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 88);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(921, 427);
            this.panel2.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._tsbUpdate,
            this._tsbClear,
            this._tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStrip1.Size = new System.Drawing.Size(921, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _tsbUpdate
            // 
            this._tsbUpdate.Image = global::MoneyAdministrator.Properties.Resources.document_edit_shadow;
            this._tsbUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbUpdate.Name = "_tsbUpdate";
            this._tsbUpdate.Size = new System.Drawing.Size(95, 24);
            this._tsbUpdate.Text = "Actualizar";
            // 
            // _tsbClear
            // 
            this._tsbClear.Image = global::MoneyAdministrator.Properties.Resources.document_shadow;
            this._tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbClear.Name = "_tsbClear";
            this._tsbClear.Size = new System.Drawing.Size(79, 24);
            this._tsbClear.Text = "Limpiar";
            this._tsbClear.Click += new System.EventHandler(this._tsbClear_Click);
            // 
            // _tsbExit
            // 
            this._tsbExit.Image = global::MoneyAdministrator.Properties.Resources.exit_shadow;
            this._tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbExit.Name = "_tsbExit";
            this._tsbExit.Size = new System.Drawing.Size(58, 24);
            this._tsbExit.Text = "Salir";
            // 
            // _txtSalaryUsd
            // 
            this._txtSalaryUsd.Colored = true;
            this._txtSalaryUsd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this._txtSalaryUsd.Location = new System.Drawing.Point(428, 26);
            this._txtSalaryUsd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._txtSalaryUsd.Name = "_txtSalaryUsd";
            this._txtSalaryUsd.OperatorSymbol = "-";
            this._txtSalaryUsd.Size = new System.Drawing.Size(150, 27);
            this._txtSalaryUsd.TabIndex = 38;
            this._txtSalaryUsd.Tag = "";
            this._txtSalaryUsd.Text = "-0,00 $";
            // 
            // _lblSalaryUsd
            // 
            this._lblSalaryUsd.AutoSize = true;
            this._lblSalaryUsd.Location = new System.Drawing.Point(425, 2);
            this._lblSalaryUsd.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this._lblSalaryUsd.Name = "_lblSalaryUsd";
            this._lblSalaryUsd.Size = new System.Drawing.Size(108, 20);
            this._lblSalaryUsd.TabIndex = 37;
            this._lblSalaryUsd.Text = "Sueldo en USD";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Dashboard";
            this.Size = new System.Drawing.Size(921, 515);
            ((System.ComponentModel.ISupportInitialize)(this._grd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._grdHeader)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView _grd;
        private DataGridView _grdHeader;
        private Panel panel1;
        private Panel panel2;
        private ToolStrip toolStrip1;
        private ToolStripButton _tsbUpdate;
        private ToolStripButton _tsbClear;
        private ToolStripButton _tsbExit;
        private CustomControls.MoneyTextBox _txtUsdValue;
        private Label _lblUsdValue;
        private CustomControls.MoneyTextBox _txtSalaryArs;
        private Label _lblSalaryArs;
        private Label _lblDate;
        private DateTimePicker _dtpDate;
        private CustomControls.MoneyTextBox _txtSalaryUsd;
        private Label _lblSalaryUsd;
    }
}
