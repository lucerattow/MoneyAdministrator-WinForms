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
            this._cbCurrency = new System.Windows.Forms.ComboBox();
            this._btnEntitySearch = new System.Windows.Forms.Button();
            this._ypYearPage = new MoneyAdministrator.CustomControls.YearPicker();
            this._tbValue = new System.Windows.Forms.TextBox();
            this._lbOrigin = new System.Windows.Forms.Label();
            this._grd = new System.Windows.Forms.DataGridView();
            this._lbInstallments = new System.Windows.Forms.Label();
            this._dtpDate = new System.Windows.Forms.DateTimePicker();
            this._lbValue = new System.Windows.Forms.Label();
            this._tbInstallments = new System.Windows.Forms.TextBox();
            this._tbEntity = new System.Windows.Forms.TextBox();
            this._lbDate = new System.Windows.Forms.Label();
            this._tbDescription = new System.Windows.Forms.TextBox();
            this._lbDescription = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this._pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grd)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._tsbInsert,
            this._tsbUpdate,
            this._tsbDelete,
            this._tsbClear});
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
            this._tsbInsert.Image = global::MoneyAdministrator.Properties.Resources.document_add;
            this._tsbInsert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbInsert.Name = "_tsbInsert";
            this._tsbInsert.Size = new System.Drawing.Size(55, 22);
            this._tsbInsert.Text = "Crear";
            // 
            // _tsbUpdate
            // 
            this._tsbUpdate.Image = global::MoneyAdministrator.Properties.Resources.document_edit;
            this._tsbUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbUpdate.Name = "_tsbUpdate";
            this._tsbUpdate.Size = new System.Drawing.Size(78, 22);
            this._tsbUpdate.Text = "Modificar";
            // 
            // _tsbDelete
            // 
            this._tsbDelete.Image = global::MoneyAdministrator.Properties.Resources.document_error;
            this._tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbDelete.Name = "_tsbDelete";
            this._tsbDelete.Size = new System.Drawing.Size(70, 22);
            this._tsbDelete.Text = "Eliminar";
            // 
            // _tsbClear
            // 
            this._tsbClear.Image = global::MoneyAdministrator.Properties.Resources.document;
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
            this._pnlContent.Controls.Add(this._cbCurrency);
            this._pnlContent.Controls.Add(this._btnEntitySearch);
            this._pnlContent.Controls.Add(this._ypYearPage);
            this._pnlContent.Controls.Add(this._tbValue);
            this._pnlContent.Controls.Add(this._lbOrigin);
            this._pnlContent.Controls.Add(this.toolStrip1);
            this._pnlContent.Controls.Add(this._grd);
            this._pnlContent.Controls.Add(this._lbInstallments);
            this._pnlContent.Controls.Add(this._dtpDate);
            this._pnlContent.Controls.Add(this._lbValue);
            this._pnlContent.Controls.Add(this._tbInstallments);
            this._pnlContent.Controls.Add(this._tbEntity);
            this._pnlContent.Controls.Add(this._lbDate);
            this._pnlContent.Controls.Add(this._tbDescription);
            this._pnlContent.Controls.Add(this._lbDescription);
            this._pnlContent.Location = new System.Drawing.Point(5, 5);
            this._pnlContent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._pnlContent.Name = "_pnlContent";
            this._pnlContent.Size = new System.Drawing.Size(850, 410);
            this._pnlContent.TabIndex = 17;
            this._pnlContent.Visible = false;
            // 
            // _cbCurrency
            // 
            this._cbCurrency.FormattingEnabled = true;
            this._cbCurrency.Location = new System.Drawing.Point(217, 97);
            this._cbCurrency.Name = "_cbCurrency";
            this._cbCurrency.Size = new System.Drawing.Size(53, 23);
            this._cbCurrency.TabIndex = 23;
            // 
            // _btnEntitySearch
            // 
            this._btnEntitySearch.Image = global::MoneyAdministrator.Properties.Resources.window_view;
            this._btnEntitySearch.Location = new System.Drawing.Point(301, 36);
            this._btnEntitySearch.Name = "_btnEntitySearch";
            this._btnEntitySearch.Size = new System.Drawing.Size(24, 25);
            this._btnEntitySearch.TabIndex = 22;
            this._btnEntitySearch.UseVisualStyleBackColor = true;
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
            // _tbValue
            // 
            this._tbValue.Location = new System.Drawing.Point(95, 97);
            this._tbValue.Name = "_tbValue";
            this._tbValue.Size = new System.Drawing.Size(116, 23);
            this._tbValue.TabIndex = 18;
            // 
            // _lbOrigin
            // 
            this._lbOrigin.AutoSize = true;
            this._lbOrigin.Location = new System.Drawing.Point(11, 41);
            this._lbOrigin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lbOrigin.Name = "_lbOrigin";
            this._lbOrigin.Size = new System.Drawing.Size(47, 15);
            this._lbOrigin.TabIndex = 0;
            this._lbOrigin.Text = "Entidad";
            // 
            // _grd
            // 
            this._grd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grd.Location = new System.Drawing.Point(10, 127);
            this._grd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._grd.Name = "_grd";
            this._grd.Size = new System.Drawing.Size(830, 244);
            this._grd.TabIndex = 12;
            // 
            // _lbInstallments
            // 
            this._lbInstallments.AutoSize = true;
            this._lbInstallments.Location = new System.Drawing.Point(421, 101);
            this._lbInstallments.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lbInstallments.Name = "_lbInstallments";
            this._lbInstallments.Size = new System.Drawing.Size(44, 15);
            this._lbInstallments.TabIndex = 4;
            this._lbInstallments.Text = "Cuotas";
            // 
            // _dtpDate
            // 
            this._dtpDate.CustomFormat = "dd/MM/yyyy";
            this._dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._dtpDate.Location = new System.Drawing.Point(388, 37);
            this._dtpDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._dtpDate.Name = "_dtpDate";
            this._dtpDate.Size = new System.Drawing.Size(127, 23);
            this._dtpDate.TabIndex = 14;
            // 
            // _lbValue
            // 
            this._lbValue.AutoSize = true;
            this._lbValue.Location = new System.Drawing.Point(11, 101);
            this._lbValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lbValue.Name = "_lbValue";
            this._lbValue.Size = new System.Drawing.Size(33, 15);
            this._lbValue.TabIndex = 6;
            this._lbValue.Text = "Valor";
            // 
            // _tbInstallments
            // 
            this._tbInstallments.Location = new System.Drawing.Point(473, 97);
            this._tbInstallments.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._tbInstallments.Name = "_tbInstallments";
            this._tbInstallments.Size = new System.Drawing.Size(42, 23);
            this._tbInstallments.TabIndex = 11;
            // 
            // _tbEntity
            // 
            this._tbEntity.Location = new System.Drawing.Point(95, 37);
            this._tbEntity.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._tbEntity.Name = "_tbEntity";
            this._tbEntity.Size = new System.Drawing.Size(206, 23);
            this._tbEntity.TabIndex = 1;
            // 
            // _lbDate
            // 
            this._lbDate.AutoSize = true;
            this._lbDate.Location = new System.Drawing.Point(339, 41);
            this._lbDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lbDate.Name = "_lbDate";
            this._lbDate.Size = new System.Drawing.Size(38, 15);
            this._lbDate.TabIndex = 13;
            this._lbDate.Text = "Fecha";
            // 
            // _tbDescription
            // 
            this._tbDescription.Location = new System.Drawing.Point(95, 67);
            this._tbDescription.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._tbDescription.Name = "_tbDescription";
            this._tbDescription.Size = new System.Drawing.Size(420, 23);
            this._tbDescription.TabIndex = 3;
            // 
            // _lbDescription
            // 
            this._lbDescription.AutoSize = true;
            this._lbDescription.Location = new System.Drawing.Point(11, 71);
            this._lbDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lbDescription.Name = "_lbDescription";
            this._lbDescription.Size = new System.Drawing.Size(69, 15);
            this._lbDescription.TabIndex = 2;
            this._lbDescription.Text = "Descripcion";
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
        private System.Windows.Forms.Label _lbOrigin;
        private System.Windows.Forms.DataGridView _grd;
        private System.Windows.Forms.Label _lbInstallments;
        private System.Windows.Forms.DateTimePicker _dtpDate;
        private System.Windows.Forms.Label _lbValue;
        private System.Windows.Forms.TextBox _tbInstallments;
        private System.Windows.Forms.TextBox _tbEntity;
        private System.Windows.Forms.Label _lbDate;
        private System.Windows.Forms.TextBox _tbDescription;
        private System.Windows.Forms.Label _lbDescription;
        private TextBox _tbValue;
        private MoneyAdministrator.CustomControls.YearPicker _ypYearPage;
        private Button _btnEntitySearch;
        private ComboBox _cbCurrency;
    }
}
