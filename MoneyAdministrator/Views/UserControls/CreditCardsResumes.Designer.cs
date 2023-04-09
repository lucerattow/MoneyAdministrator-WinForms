using System.Windows.Forms;

namespace MoneyAdministrator.Views
{
    partial class CreditCardResumesView
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
            this._toolStripButton = new System.Windows.Forms.ToolStrip();
            this._tsbClear = new System.Windows.Forms.ToolStripButton();
            this._tsbExit = new System.Windows.Forms.ToolStripButton();
            this._pnlContent = new System.Windows.Forms.Panel();
            this._grd = new System.Windows.Forms.DataGridView();
            this._pnlInputs = new System.Windows.Forms.Panel();
            this._lblCreditCardName = new System.Windows.Forms.Label();
            this._lblPeriod = new System.Windows.Forms.Label();
            this._btnEntitySearch = new System.Windows.Forms.Button();
            this._txtEntityName = new System.Windows.Forms.TextBox();
            this._dtpPeriod = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this._toolStripButton.SuspendLayout();
            this._pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grd)).BeginInit();
            this._pnlInputs.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolStripButton
            // 
            this._toolStripButton.AllowMerge = false;
            this._toolStripButton.BackColor = System.Drawing.Color.White;
            this._toolStripButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._toolStripButton.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStripButton.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._tsbClear,
            this._tsbExit});
            this._toolStripButton.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this._toolStripButton.Location = new System.Drawing.Point(0, 0);
            this._toolStripButton.Name = "_toolStripButton";
            this._toolStripButton.Padding = new System.Windows.Forms.Padding(6, 0, 1, 0);
            this._toolStripButton.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._toolStripButton.Size = new System.Drawing.Size(800, 27);
            this._toolStripButton.TabIndex = 16;
            this._toolStripButton.Text = "toolStrip1";
            // 
            // _tsbClear
            // 
            this._tsbClear.Image = global::MoneyAdministrator.Properties.Resources.document_shadow;
            this._tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbClear.Name = "_tsbClear";
            this._tsbClear.Size = new System.Drawing.Size(79, 24);
            this._tsbClear.Text = "Limpiar";
            // 
            // _tsbExit
            // 
            this._tsbExit.Image = global::MoneyAdministrator.Properties.Resources.exit_shadow;
            this._tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbExit.Name = "_tsbExit";
            this._tsbExit.Size = new System.Drawing.Size(58, 24);
            this._tsbExit.Text = "Salir";
            // 
            // _pnlContent
            // 
            this._pnlContent.Controls.Add(this._grd);
            this._pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pnlContent.Location = new System.Drawing.Point(200, 90);
            this._pnlContent.Name = "_pnlContent";
            this._pnlContent.Size = new System.Drawing.Size(600, 310);
            this._pnlContent.TabIndex = 27;
            // 
            // _grd
            // 
            this._grd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grd.Location = new System.Drawing.Point(5, 5);
            this._grd.Margin = new System.Windows.Forms.Padding(4);
            this._grd.Name = "_grd";
            this._grd.Size = new System.Drawing.Size(590, 300);
            this._grd.TabIndex = 12;
            // 
            // _pnlInputs
            // 
            this._pnlInputs.BackColor = System.Drawing.SystemColors.ButtonFace;
            this._pnlInputs.Controls.Add(this._lblCreditCardName);
            this._pnlInputs.Controls.Add(this._lblPeriod);
            this._pnlInputs.Controls.Add(this._btnEntitySearch);
            this._pnlInputs.Controls.Add(this._txtEntityName);
            this._pnlInputs.Controls.Add(this._dtpPeriod);
            this._pnlInputs.Dock = System.Windows.Forms.DockStyle.Top;
            this._pnlInputs.Location = new System.Drawing.Point(0, 27);
            this._pnlInputs.Margin = new System.Windows.Forms.Padding(4);
            this._pnlInputs.Name = "_pnlInputs";
            this._pnlInputs.Size = new System.Drawing.Size(800, 63);
            this._pnlInputs.TabIndex = 26;
            // 
            // _lblCreditCardName
            // 
            this._lblCreditCardName.AutoSize = true;
            this._lblCreditCardName.Location = new System.Drawing.Point(105, 5);
            this._lblCreditCardName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblCreditCardName.Name = "_lblCreditCardName";
            this._lblCreditCardName.Size = new System.Drawing.Size(125, 20);
            this._lblCreditCardName.TabIndex = 0;
            this._lblCreditCardName.Text = "Tarjeta de credito";
            // 
            // _lblPeriod
            // 
            this._lblPeriod.AutoSize = true;
            this._lblPeriod.Location = new System.Drawing.Point(2, 5);
            this._lblPeriod.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblPeriod.Name = "_lblPeriod";
            this._lblPeriod.Size = new System.Drawing.Size(47, 20);
            this._lblPeriod.TabIndex = 13;
            this._lblPeriod.Text = "Fecha";
            // 
            // _btnEntitySearch
            // 
            this._btnEntitySearch.Image = global::MoneyAdministrator.Properties.Resources.window_view;
            this._btnEntitySearch.Location = new System.Drawing.Point(309, 28);
            this._btnEntitySearch.Margin = new System.Windows.Forms.Padding(4);
            this._btnEntitySearch.Name = "_btnEntitySearch";
            this._btnEntitySearch.Size = new System.Drawing.Size(29, 29);
            this._btnEntitySearch.TabIndex = 22;
            this._btnEntitySearch.UseVisualStyleBackColor = true;
            // 
            // _txtEntityName
            // 
            this._txtEntityName.Location = new System.Drawing.Point(109, 29);
            this._txtEntityName.Margin = new System.Windows.Forms.Padding(4);
            this._txtEntityName.Name = "_txtEntityName";
            this._txtEntityName.Size = new System.Drawing.Size(200, 27);
            this._txtEntityName.TabIndex = 1;
            // 
            // _dtpPeriod
            // 
            this._dtpPeriod.CustomFormat = " yyyy-MM";
            this._dtpPeriod.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._dtpPeriod.Location = new System.Drawing.Point(5, 29);
            this._dtpPeriod.Margin = new System.Windows.Forms.Padding(4);
            this._dtpPeriod.Name = "_dtpPeriod";
            this._dtpPeriod.Size = new System.Drawing.Size(96, 27);
            this._dtpPeriod.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 90);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 310);
            this.panel1.TabIndex = 13;
            // 
            // CreditCardResumesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._pnlContent);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._pnlInputs);
            this.Controls.Add(this._toolStripButton);
            this.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MinimumSize = new System.Drawing.Size(700, 300);
            this.Name = "CreditCardResumesView";
            this.Size = new System.Drawing.Size(800, 400);
            this._toolStripButton.ResumeLayout(false);
            this._toolStripButton.PerformLayout();
            this._pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grd)).EndInit();
            this._pnlInputs.ResumeLayout(false);
            this._pnlInputs.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _toolStripButton;
        private System.Windows.Forms.ToolStripButton _tsbClear;
        private System.Windows.Forms.Label _lblCreditCardName;
        private System.Windows.Forms.DataGridView _grd;
        private System.Windows.Forms.DateTimePicker _dtpPeriod;
        private System.Windows.Forms.TextBox _txtEntityName;
        private System.Windows.Forms.Label _lblPeriod;
        private Button _btnEntitySearch;
        private Panel _pnlInputs;
        private ToolStripButton _tsbExit;
        private Panel _pnlContent;
        private Panel panel1;
    }
}
