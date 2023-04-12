namespace MoneyAdministrator.Views.Modals
{
    partial class CreditCardView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreditCardView));
            _pnlContainer = new Panel();
            panel2 = new Panel();
            _grd = new DataGridView();
            panel1 = new Panel();
            _lblCreditCardType = new Label();
            _cbCreditCardType = new ComboBox();
            _lblEntityName = new Label();
            _txtLastFourNumbers = new TextBox();
            _lblLastFourNumbers = new Label();
            toolStrip1 = new ToolStrip();
            _tsbSelect = new ToolStripButton();
            _tsbInsert = new ToolStripButton();
            _tsbUpdate = new ToolStripButton();
            _tsbDelete = new ToolStripButton();
            _tsbClear = new ToolStripButton();
            _cbBank = new ComboBox();
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
            _pnlContainer.Location = new Point(6, 7);
            _pnlContainer.Margin = new Padding(3, 4, 3, 4);
            _pnlContainer.Name = "_pnlContainer";
            _pnlContainer.Size = new Size(563, 363);
            _pnlContainer.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(_grd);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 88);
            panel2.Name = "panel2";
            panel2.Size = new Size(563, 275);
            panel2.TabIndex = 22;
            // 
            // _grd
            // 
            _grd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _grd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _grd.Location = new Point(5, 5);
            _grd.Margin = new Padding(3, 4, 3, 4);
            _grd.Name = "_grd";
            _grd.RowTemplate.Height = 25;
            _grd.Size = new Size(553, 265);
            _grd.TabIndex = 20;
            _grd.CellMouseDoubleClick += _grd_CellMouseDoubleClick;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonFace;
            panel1.Controls.Add(_cbBank);
            panel1.Controls.Add(_lblCreditCardType);
            panel1.Controls.Add(_cbCreditCardType);
            panel1.Controls.Add(_lblEntityName);
            panel1.Controls.Add(_txtLastFourNumbers);
            panel1.Controls.Add(_lblLastFourNumbers);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 27);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(563, 61);
            panel1.TabIndex = 21;
            // 
            // _lblCreditCardType
            // 
            _lblCreditCardType.AutoSize = true;
            _lblCreditCardType.Location = new Point(237, 5);
            _lblCreditCardType.Margin = new Padding(4, 0, 4, 0);
            _lblCreditCardType.Name = "_lblCreditCardType";
            _lblCreditCardType.Size = new Size(60, 20);
            _lblCreditCardType.TabIndex = 27;
            _lblCreditCardType.Text = "Entidad";
            // 
            // _cbCreditCardType
            // 
            _cbCreditCardType.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbCreditCardType.FormattingEnabled = true;
            _cbCreditCardType.Location = new Point(241, 29);
            _cbCreditCardType.Name = "_cbCreditCardType";
            _cbCreditCardType.Size = new Size(181, 28);
            _cbCreditCardType.TabIndex = 26;
            // 
            // _lblEntityName
            // 
            _lblEntityName.AutoSize = true;
            _lblEntityName.Location = new Point(1, 5);
            _lblEntityName.Margin = new Padding(4, 0, 4, 0);
            _lblEntityName.Name = "_lblEntityName";
            _lblEntityName.Size = new Size(99, 20);
            _lblEntityName.TabIndex = 23;
            _lblEntityName.Text = "Banco emisor";
            // 
            // _txtLastFourNumbers
            // 
            _txtLastFourNumbers.Location = new Point(428, 29);
            _txtLastFourNumbers.Margin = new Padding(3, 4, 3, 4);
            _txtLastFourNumbers.Name = "_txtLastFourNumbers";
            _txtLastFourNumbers.Size = new Size(130, 27);
            _txtLastFourNumbers.TabIndex = 19;
            // 
            // _lblLastFourNumbers
            // 
            _lblLastFourNumbers.AutoSize = true;
            _lblLastFourNumbers.Location = new Point(424, 5);
            _lblLastFourNumbers.Name = "_lblLastFourNumbers";
            _lblLastFourNumbers.Size = new Size(133, 20);
            _lblLastFourNumbers.TabIndex = 18;
            _lblLastFourNumbers.Text = "Ultimos 4 numeros";
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = Color.White;
            toolStrip1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new ToolStripItem[] { _tsbSelect, _tsbInsert, _tsbUpdate, _tsbDelete, _tsbClear });
            toolStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(6, 0, 1, 0);
            toolStrip1.RenderMode = ToolStripRenderMode.System;
            toolStrip1.Size = new Size(563, 27);
            toolStrip1.TabIndex = 17;
            toolStrip1.Text = "toolStrip1";
            // 
            // _tsbSelect
            // 
            _tsbSelect.Image = Properties.Resources.document_check_shadow;
            _tsbSelect.ImageTransparentColor = Color.Magenta;
            _tsbSelect.Name = "_tsbSelect";
            _tsbSelect.Size = new Size(105, 24);
            _tsbSelect.Text = "Seleccionar";
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
            // _cbBank
            // 
            _cbBank.FormattingEnabled = true;
            _cbBank.Location = new Point(5, 29);
            _cbBank.Name = "_cbBank";
            _cbBank.Size = new Size(230, 28);
            _cbBank.TabIndex = 28;
            // 
            // CreditCardView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(574, 376);
            Controls.Add(_pnlContainer);
            Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(590, 265);
            Name = "CreditCardView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CreditCardView";
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
        private ToolStrip toolStrip1;
        private ToolStripButton _tsbClear;
        private DataGridView _grd;
        private TextBox _txtLastFourNumbers;
        private Label _lblLastFourNumbers;
        private ToolStripButton _tsbSelect;
        private Panel panel1;
        private Label _lblEntityName;
        private ComboBox _cbCreditCardType;
        private Label _lblCreditCardType;
        private Panel panel2;
        private ToolStripButton _tsbInsert;
        private ToolStripButton _tsbUpdate;
        private ToolStripButton _tsbDelete;
        private ComboBox _cbBank;
    }
}