namespace MoneyAdministrator.Views.Modals
{
    partial class EntityView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntityView));
            this._pnlContainer = new System.Windows.Forms.Panel();
            this._grd = new System.Windows.Forms.DataGridView();
            this._txtName = new System.Windows.Forms.TextBox();
            this._lblName = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._tsbSelect = new System.Windows.Forms.ToolStripButton();
            this._tsbClear = new System.Windows.Forms.ToolStripButton();
            this._cbEntityType = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this._lblEntityType = new System.Windows.Forms.Label();
            this._tsbInsert = new System.Windows.Forms.ToolStripButton();
            this._tsbUpdate = new System.Windows.Forms.ToolStripButton();
            this._tsbDelete = new System.Windows.Forms.ToolStripButton();
            this._pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grd)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this._pnlContainer.Size = new System.Drawing.Size(562, 363);
            this._pnlContainer.TabIndex = 0;
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
            this._grd.Size = new System.Drawing.Size(552, 265);
            this._grd.TabIndex = 20;
            this._grd.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._grd_CellMouseDoubleClick);
            // 
            // _txtName
            // 
            this._txtName.Location = new System.Drawing.Point(5, 29);
            this._txtName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._txtName.Name = "_txtName";
            this._txtName.Size = new System.Drawing.Size(189, 27);
            this._txtName.TabIndex = 19;
            // 
            // _lblName
            // 
            this._lblName.AutoSize = true;
            this._lblName.Location = new System.Drawing.Point(1, 5);
            this._lblName.Name = "_lblName";
            this._lblName.Size = new System.Drawing.Size(64, 20);
            this._lblName.TabIndex = 18;
            this._lblName.Text = "Nombre";
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
            this.toolStrip1.Size = new System.Drawing.Size(562, 27);
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
            // _tsbClear
            // 
            this._tsbClear.Image = global::MoneyAdministrator.Properties.Resources.document_shadow;
            this._tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbClear.Name = "_tsbClear";
            this._tsbClear.Size = new System.Drawing.Size(79, 24);
            this._tsbClear.Text = "Limpiar";
            // 
            // _cbEntityType
            // 
            this._cbEntityType.FormattingEnabled = true;
            this._cbEntityType.Location = new System.Drawing.Point(200, 29);
            this._cbEntityType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._cbEntityType.Name = "_cbEntityType";
            this._cbEntityType.Size = new System.Drawing.Size(205, 28);
            this._cbEntityType.TabIndex = 27;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this._lblEntityType);
            this.panel1.Controls.Add(this._txtName);
            this.panel1.Controls.Add(this._cbEntityType);
            this.panel1.Controls.Add(this._lblName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(562, 61);
            this.panel1.TabIndex = 28;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._grd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 88);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(562, 275);
            this.panel2.TabIndex = 29;
            // 
            // _lblEntityType
            // 
            this._lblEntityType.AutoSize = true;
            this._lblEntityType.Location = new System.Drawing.Point(196, 5);
            this._lblEntityType.Name = "_lblEntityType";
            this._lblEntityType.Size = new System.Drawing.Size(115, 20);
            this._lblEntityType.TabIndex = 28;
            this._lblEntityType.Text = "Tipo de entidad";
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
            // EntityView
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
            this.Name = "EntityView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EntityView";
            this._pnlContainer.ResumeLayout(false);
            this._pnlContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grd)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel _pnlContainer;
        private ToolStrip toolStrip1;
        private ToolStripButton _tsbClear;
        private DataGridView _grd;
        private TextBox _txtName;
        private Label _lblName;
        private ToolStripButton _tsbSelect;
        private Panel panel2;
        private Panel panel1;
        private ComboBox _cbEntityType;
        private Label _lblEntityType;
        private ToolStripButton _tsbInsert;
        private ToolStripButton _tsbUpdate;
        private ToolStripButton _tsbDelete;
    }
}