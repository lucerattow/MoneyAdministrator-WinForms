namespace MoneyAdministrator.Views.Modals
{
    partial class ResultPickerView
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
            this._pnlContainer = new System.Windows.Forms.Panel();
            this._grd = new System.Windows.Forms.DataGridView();
            this._txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._tsbSearch = new System.Windows.Forms.ToolStripButton();
            this._tsbInsert = new System.Windows.Forms.ToolStripButton();
            this._tsbUpdate = new System.Windows.Forms.ToolStripButton();
            this._tsbDelete = new System.Windows.Forms.ToolStripButton();
            this._tsbClear = new System.Windows.Forms.ToolStripButton();
            this._tsbSelect = new System.Windows.Forms.ToolStripButton();
            this._pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grd)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _pnlContainer
            // 
            this._pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pnlContainer.BackColor = System.Drawing.Color.White;
            this._pnlContainer.Controls.Add(this._grd);
            this._pnlContainer.Controls.Add(this._txtName);
            this._pnlContainer.Controls.Add(this.label1);
            this._pnlContainer.Controls.Add(this.toolStrip1);
            this._pnlContainer.Location = new System.Drawing.Point(5, 5);
            this._pnlContainer.Name = "_pnlContainer";
            this._pnlContainer.Size = new System.Drawing.Size(549, 319);
            this._pnlContainer.TabIndex = 0;
            // 
            // _grd
            // 
            this._grd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grd.Location = new System.Drawing.Point(7, 61);
            this._grd.Name = "_grd";
            this._grd.RowTemplate.Height = 25;
            this._grd.Size = new System.Drawing.Size(535, 251);
            this._grd.TabIndex = 20;
            // 
            // _txtName
            // 
            this._txtName.Location = new System.Drawing.Point(67, 32);
            this._txtName.Name = "_txtName";
            this._txtName.Size = new System.Drawing.Size(166, 23);
            this._txtName.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "Nombre";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._tsbSelect,
            this._tsbSearch,
            this._tsbInsert,
            this._tsbUpdate,
            this._tsbDelete,
            this._tsbClear});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(549, 25);
            this.toolStrip1.TabIndex = 17;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _tsbSearch
            // 
            this._tsbSearch.Image = global::MoneyAdministrator.Properties.Resources.document_view_shadow;
            this._tsbSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbSearch.Name = "_tsbSearch";
            this._tsbSearch.Size = new System.Drawing.Size(62, 22);
            this._tsbSearch.Text = "Buscar";
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
            // _tsbSelect
            // 
            this._tsbSelect.Image = global::MoneyAdministrator.Properties.Resources.document_check_shadow;
            this._tsbSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbSelect.Name = "_tsbSelect";
            this._tsbSelect.Size = new System.Drawing.Size(87, 22);
            this._tsbSelect.Text = "Seleccionar";
            // 
            // ResultPickerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(559, 329);
            this.Controls.Add(this._pnlContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResultPickerView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ResultPickerView";
            this._pnlContainer.ResumeLayout(false);
            this._pnlContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grd)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel _pnlContainer;
        private ToolStrip toolStrip1;
        private ToolStripButton _tsbSearch;
        private ToolStripButton _tsbInsert;
        private ToolStripButton _tsbUpdate;
        private ToolStripButton _tsbDelete;
        private ToolStripButton _tsbClear;
        private DataGridView _grd;
        private TextBox _txtName;
        private Label label1;
        private ToolStripButton _tsbSelect;
    }
}