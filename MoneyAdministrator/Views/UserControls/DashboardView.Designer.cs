using MoneyAdministrator.CustomControls;

namespace MoneyAdministrator.Views.UserControls
{
    partial class DashboardView
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
            _grd = new CettoDataGridView();
            _grdHeader = new DataGridView();
            panel1 = new Panel();
            _txtSalaryUsd = new MoneyTextBox();
            _lblSalaryUsd = new Label();
            _lblDate = new Label();
            _dtpDate = new DateTimePicker();
            _txtSalaryArs = new MoneyTextBox();
            _lblSalaryArs = new Label();
            _txtUsdValue = new MoneyTextBox();
            _lblUsdValue = new Label();
            panel2 = new Panel();
            toolStrip1 = new ToolStrip();
            _tsbInsert = new ToolStripButton();
            _tsbUpdate = new ToolStripButton();
            _tsbDelete = new ToolStripButton();
            _tsbClear = new ToolStripButton();
            _tsbExit = new ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)_grd).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_grdHeader).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // _grd
            // 
            _grd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _grd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _grd.ExpandColumnHeight = 30;
            _grd.Location = new Point(5, 31);
            _grd.Margin = new Padding(3, 4, 3, 4);
            _grd.Name = "_grd";
            _grd.RowTemplate.Height = 25;
            _grd.Size = new Size(911, 392);
            _grd.TabIndex = 0;
            _grd.CellMouseDoubleClick += _grd_CellMouseDoubleClick;
            _grd.CellPainting += _grd_CellPainting;
            _grd.Scroll += _grd_Scroll;
            // 
            // _grdHeader
            // 
            _grdHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _grdHeader.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _grdHeader.Location = new Point(5, 5);
            _grdHeader.Margin = new Padding(3, 4, 3, 4);
            _grdHeader.Name = "_grdHeader";
            _grdHeader.RowTemplate.Height = 25;
            _grdHeader.Size = new Size(911, 29);
            _grdHeader.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonFace;
            panel1.Controls.Add(_txtSalaryUsd);
            panel1.Controls.Add(_lblSalaryUsd);
            panel1.Controls.Add(_lblDate);
            panel1.Controls.Add(_dtpDate);
            panel1.Controls.Add(_txtSalaryArs);
            panel1.Controls.Add(_lblSalaryArs);
            panel1.Controls.Add(_txtUsdValue);
            panel1.Controls.Add(_lblUsdValue);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 27);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(921, 61);
            panel1.TabIndex = 2;
            // 
            // _txtSalaryUsd
            // 
            _txtSalaryUsd.Colored = true;
            _txtSalaryUsd.ForeColor = Color.FromArgb(80, 80, 80);
            _txtSalaryUsd.Location = new Point(428, 27);
            _txtSalaryUsd.Margin = new Padding(3, 4, 3, 4);
            _txtSalaryUsd.Name = "_txtSalaryUsd";
            _txtSalaryUsd.OperatorSymbol = "-";
            _txtSalaryUsd.OperatorSymbolIsConstant = false;
            _txtSalaryUsd.Size = new Size(150, 27);
            _txtSalaryUsd.TabIndex = 38;
            _txtSalaryUsd.Tag = "";
            _txtSalaryUsd.Text = "-0,00 $";
            _txtSalaryUsd.TextAlign = HorizontalAlignment.Right;
            // 
            // _lblSalaryUsd
            // 
            _lblSalaryUsd.AutoSize = true;
            _lblSalaryUsd.Location = new Point(425, 3);
            _lblSalaryUsd.Margin = new Padding(5, 0, 5, 0);
            _lblSalaryUsd.Name = "_lblSalaryUsd";
            _lblSalaryUsd.Size = new Size(108, 20);
            _lblSalaryUsd.TabIndex = 37;
            _lblSalaryUsd.Text = "Sueldo en USD";
            // 
            // _lblDate
            // 
            _lblDate.AutoSize = true;
            _lblDate.Location = new Point(3, 3);
            _lblDate.Margin = new Padding(4, 0, 4, 0);
            _lblDate.Name = "_lblDate";
            _lblDate.Size = new Size(60, 20);
            _lblDate.TabIndex = 35;
            _lblDate.Text = "Periodo";
            // 
            // _dtpDate
            // 
            _dtpDate.CustomFormat = " yyyy-MM";
            _dtpDate.Format = DateTimePickerFormat.Custom;
            _dtpDate.Location = new Point(6, 27);
            _dtpDate.Margin = new Padding(4);
            _dtpDate.Name = "_dtpDate";
            _dtpDate.Size = new Size(95, 27);
            _dtpDate.TabIndex = 36;
            // 
            // _txtSalaryArs
            // 
            _txtSalaryArs.Colored = true;
            _txtSalaryArs.ForeColor = Color.FromArgb(80, 80, 80);
            _txtSalaryArs.Location = new Point(272, 27);
            _txtSalaryArs.Margin = new Padding(3, 4, 3, 4);
            _txtSalaryArs.Name = "_txtSalaryArs";
            _txtSalaryArs.OperatorSymbol = "-";
            _txtSalaryArs.OperatorSymbolIsConstant = false;
            _txtSalaryArs.Size = new Size(150, 27);
            _txtSalaryArs.TabIndex = 34;
            _txtSalaryArs.Tag = "";
            _txtSalaryArs.Text = "-0,00 $";
            _txtSalaryArs.TextAlign = HorizontalAlignment.Right;
            // 
            // _lblSalaryArs
            // 
            _lblSalaryArs.AutoSize = true;
            _lblSalaryArs.Location = new Point(269, 3);
            _lblSalaryArs.Margin = new Padding(5, 0, 5, 0);
            _lblSalaryArs.Name = "_lblSalaryArs";
            _lblSalaryArs.Size = new Size(106, 20);
            _lblSalaryArs.TabIndex = 33;
            _lblSalaryArs.Text = "Sueldo en ARS";
            // 
            // _txtUsdValue
            // 
            _txtUsdValue.Colored = true;
            _txtUsdValue.ForeColor = Color.FromArgb(80, 80, 80);
            _txtUsdValue.Location = new Point(108, 27);
            _txtUsdValue.Margin = new Padding(3, 4, 3, 4);
            _txtUsdValue.Name = "_txtUsdValue";
            _txtUsdValue.OperatorSymbol = "-";
            _txtUsdValue.OperatorSymbolIsConstant = false;
            _txtUsdValue.Size = new Size(150, 27);
            _txtUsdValue.TabIndex = 32;
            _txtUsdValue.Tag = "";
            _txtUsdValue.Text = "-0,00 $";
            _txtUsdValue.TextAlign = HorizontalAlignment.Right;
            // 
            // _lblUsdValue
            // 
            _lblUsdValue.AutoSize = true;
            _lblUsdValue.Location = new Point(106, 3);
            _lblUsdValue.Margin = new Padding(5, 0, 5, 0);
            _lblUsdValue.Name = "_lblUsdValue";
            _lblUsdValue.Size = new Size(127, 20);
            _lblUsdValue.TabIndex = 31;
            _lblUsdValue.Text = "Valor USD en ARS";
            // 
            // panel2
            // 
            panel2.Controls.Add(_grdHeader);
            panel2.Controls.Add(_grd);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 88);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(921, 427);
            panel2.TabIndex = 3;
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = Color.White;
            toolStrip1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new ToolStripItem[] { _tsbInsert, _tsbUpdate, _tsbDelete, _tsbClear, _tsbExit });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(3, 0, 0, 0);
            toolStrip1.Size = new Size(921, 27);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
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
            // 
            // DashboardView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(3, 4, 3, 4);
            Name = "DashboardView";
            Size = new Size(921, 515);
            ((System.ComponentModel.ISupportInitialize)_grd).EndInit();
            ((System.ComponentModel.ISupportInitialize)_grdHeader).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CettoDataGridView _grd;
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
        private ToolStripButton _tsbInsert;
        private ToolStripButton _tsbDelete;
    }
}
