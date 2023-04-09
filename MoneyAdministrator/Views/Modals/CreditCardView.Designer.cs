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
            this._pnlContainer = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this._grd = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this._lblCreditCardType = new System.Windows.Forms.Label();
            this._cbCreditCardType = new System.Windows.Forms.ComboBox();
            this._lblEntityName = new System.Windows.Forms.Label();
            this._btnEntitySearch = new System.Windows.Forms.Button();
            this._txtEntityName = new System.Windows.Forms.TextBox();
            this._txtLastFourNumbers = new System.Windows.Forms.TextBox();
            this._lblLastFourNumbers = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._tsbSelect = new System.Windows.Forms.ToolStripButton();
            this._tsbInsert = new System.Windows.Forms.ToolStripButton();
            this._tsbUpdate = new System.Windows.Forms.ToolStripButton();
            this._tsbDelete = new System.Windows.Forms.ToolStripButton();
            this._tsbClear = new System.Windows.Forms.ToolStripButton();
            this._pnlContainer.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grd)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _pnlContainer
            // 
            this._pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pnlContainer.BackColor = System.Drawing.Color.White;
            this._pnlContainer.Controls.Add(this.panel2);
            this._pnlContainer.Controls.Add(this.panel1);
            this._pnlContainer.Controls.Add(this.toolStrip1);
            this._pnlContainer.Location = new System.Drawing.Point(6, 7);
            this._pnlContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._pnlContainer.Name = "_pnlContainer";
            this._pnlContainer.Size = new System.Drawing.Size(563, 363);
            this._pnlContainer.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._grd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 88);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(563, 275);
            this.panel2.TabIndex = 22;
            // 
            // _grd
            // 
            this._grd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grd.Location = new System.Drawing.Point(5, 5);
            this._grd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._grd.Name = "_grd";
            this._grd.RowTemplate.Height = 25;
            this._grd.Size = new System.Drawing.Size(553, 265);
            this._grd.TabIndex = 20;
            this._grd.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._grd_CellMouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this._lblCreditCardType);
            this.panel1.Controls.Add(this._cbCreditCardType);
            this.panel1.Controls.Add(this._lblEntityName);
            this.panel1.Controls.Add(this._btnEntitySearch);
            this.panel1.Controls.Add(this._txtEntityName);
            this.panel1.Controls.Add(this._txtLastFourNumbers);
            this.panel1.Controls.Add(this._lblLastFourNumbers);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(563, 61);
            this.panel1.TabIndex = 21;
            // 
            // _lblCreditCardType
            // 
            this._lblCreditCardType.AutoSize = true;
            this._lblCreditCardType.Location = new System.Drawing.Point(237, 5);
            this._lblCreditCardType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblCreditCardType.Name = "_lblCreditCardType";
            this._lblCreditCardType.Size = new System.Drawing.Size(60, 20);
            this._lblCreditCardType.TabIndex = 27;
            this._lblCreditCardType.Text = "Entidad";
            // 
            // _cbCreditCardType
            // 
            this._cbCreditCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbCreditCardType.FormattingEnabled = true;
            this._cbCreditCardType.Location = new System.Drawing.Point(241, 29);
            this._cbCreditCardType.Name = "_cbCreditCardType";
            this._cbCreditCardType.Size = new System.Drawing.Size(181, 28);
            this._cbCreditCardType.TabIndex = 26;
            // 
            // _lblEntityName
            // 
            this._lblEntityName.AutoSize = true;
            this._lblEntityName.Location = new System.Drawing.Point(1, 5);
            this._lblEntityName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblEntityName.Name = "_lblEntityName";
            this._lblEntityName.Size = new System.Drawing.Size(60, 20);
            this._lblEntityName.TabIndex = 23;
            this._lblEntityName.Text = "Entidad";
            // 
            // _btnEntitySearch
            // 
            this._btnEntitySearch.Image = global::MoneyAdministrator.Properties.Resources.window_view;
            this._btnEntitySearch.Location = new System.Drawing.Point(205, 28);
            this._btnEntitySearch.Margin = new System.Windows.Forms.Padding(4);
            this._btnEntitySearch.Name = "_btnEntitySearch";
            this._btnEntitySearch.Size = new System.Drawing.Size(29, 29);
            this._btnEntitySearch.TabIndex = 25;
            this._btnEntitySearch.UseVisualStyleBackColor = true;
            // 
            // _txtEntityName
            // 
            this._txtEntityName.Location = new System.Drawing.Point(5, 29);
            this._txtEntityName.Margin = new System.Windows.Forms.Padding(4);
            this._txtEntityName.Name = "_txtEntityName";
            this._txtEntityName.Size = new System.Drawing.Size(200, 27);
            this._txtEntityName.TabIndex = 24;
            // 
            // _txtLastFourNumbers
            // 
            this._txtLastFourNumbers.Location = new System.Drawing.Point(428, 29);
            this._txtLastFourNumbers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._txtLastFourNumbers.Name = "_txtLastFourNumbers";
            this._txtLastFourNumbers.Size = new System.Drawing.Size(130, 27);
            this._txtLastFourNumbers.TabIndex = 19;
            // 
            // _lblLastFourNumbers
            // 
            this._lblLastFourNumbers.AutoSize = true;
            this._lblLastFourNumbers.Location = new System.Drawing.Point(424, 5);
            this._lblLastFourNumbers.Name = "_lblLastFourNumbers";
            this._lblLastFourNumbers.Size = new System.Drawing.Size(133, 20);
            this._lblLastFourNumbers.TabIndex = 18;
            this._lblLastFourNumbers.Text = "Ultimos 4 numeros";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._tsbSelect,
            this._tsbInsert,
            this._tsbUpdate,
            this._tsbDelete,
            this._tsbClear});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(6, 0, 1, 0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(563, 27);
            this.toolStrip1.TabIndex = 17;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _tsbSelect
            // 
            this._tsbSelect.Image = global::MoneyAdministrator.Properties.Resources.document_check_shadow;
            this._tsbSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbSelect.Name = "_tsbSelect";
            this._tsbSelect.Size = new System.Drawing.Size(105, 24);
            this._tsbSelect.Text = "Seleccionar";
            // 
            // _tsbInsert
            // 
            this._tsbInsert.Image = global::MoneyAdministrator.Properties.Resources.document_add_shadow;
            this._tsbInsert.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._tsbInsert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbInsert.Name = "_tsbInsert";
            this._tsbInsert.Size = new System.Drawing.Size(64, 24);
            this._tsbInsert.Text = "Crear";
            this._tsbInsert.Click += new System.EventHandler(this._tsbInsert_Click);
            // 
            // _tsbUpdate
            // 
            this._tsbUpdate.Image = global::MoneyAdministrator.Properties.Resources.document_edit_shadow;
            this._tsbUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbUpdate.Name = "_tsbUpdate";
            this._tsbUpdate.Size = new System.Drawing.Size(93, 24);
            this._tsbUpdate.Text = "Modificar";
            this._tsbUpdate.Click += new System.EventHandler(this._tsbUpdate_Click);
            // 
            // _tsbDelete
            // 
            this._tsbDelete.Image = global::MoneyAdministrator.Properties.Resources.document_error_shadow;
            this._tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbDelete.Name = "_tsbDelete";
            this._tsbDelete.Size = new System.Drawing.Size(83, 24);
            this._tsbDelete.Text = "Eliminar";
            this._tsbDelete.Click += new System.EventHandler(this._tsbDelete_Click);
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
            // CreditCardView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(574, 376);
            this.Controls.Add(this._pnlContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(590, 265);
            this.Name = "CreditCardView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CreditCardView";
            this._pnlContainer.ResumeLayout(false);
            this._pnlContainer.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grd)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

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
        private Button _btnEntitySearch;
        private TextBox _txtEntityName;
        private ComboBox _cbCreditCardType;
        private Label _lblCreditCardType;
        private Panel panel2;
        private ToolStripButton _tsbInsert;
        private ToolStripButton _tsbUpdate;
        private ToolStripButton _tsbDelete;
    }
}