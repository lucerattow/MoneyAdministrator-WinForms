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
            _toolStripButton = new ToolStrip();
            _tsbImport = new ToolStripButton();
            _tsbClear = new ToolStripButton();
            _tsbExit = new ToolStripButton();
            _pnlContent = new Panel();
            _grd = new DataGridView();
            _lblCreditCardName = new Label();
            _btnEntitySearch = new Button();
            _txtEntityName = new TextBox();
            panel1 = new Panel();
            treeView1 = new TreeView();
            panel2 = new Panel();
            panel3 = new Panel();
            label1 = new Label();
            _toolStripButton.SuspendLayout();
            _pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_grd).BeginInit();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // _toolStripButton
            // 
            _toolStripButton.AllowMerge = false;
            _toolStripButton.BackColor = Color.White;
            _toolStripButton.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            _toolStripButton.GripStyle = ToolStripGripStyle.Hidden;
            _toolStripButton.Items.AddRange(new ToolStripItem[] { _tsbImport, _tsbClear, _tsbExit });
            _toolStripButton.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            _toolStripButton.Location = new Point(0, 0);
            _toolStripButton.Name = "_toolStripButton";
            _toolStripButton.Padding = new Padding(6, 0, 1, 0);
            _toolStripButton.RenderMode = ToolStripRenderMode.System;
            _toolStripButton.Size = new Size(772, 27);
            _toolStripButton.TabIndex = 16;
            _toolStripButton.Text = "toolStrip1";
            // 
            // _tsbImport
            // 
            _tsbImport.Image = Properties.Resources.document_into_shadow;
            _tsbImport.ImageTransparentColor = Color.Magenta;
            _tsbImport.Name = "_tsbImport";
            _tsbImport.Size = new Size(147, 24);
            _tsbImport.Text = "Importar resumen";
            // 
            // _tsbClear
            // 
            _tsbClear.Image = Properties.Resources.document_shadow;
            _tsbClear.ImageTransparentColor = Color.Magenta;
            _tsbClear.Name = "_tsbClear";
            _tsbClear.Size = new Size(79, 24);
            _tsbClear.Text = "Limpiar";
            // 
            // _tsbExit
            // 
            _tsbExit.Image = Properties.Resources.exit_shadow;
            _tsbExit.ImageTransparentColor = Color.Magenta;
            _tsbExit.Name = "_tsbExit";
            _tsbExit.Size = new Size(58, 24);
            _tsbExit.Text = "Salir";
            // 
            // _pnlContent
            // 
            _pnlContent.Controls.Add(panel3);
            _pnlContent.Controls.Add(panel2);
            _pnlContent.Dock = DockStyle.Fill;
            _pnlContent.Location = new Point(239, 27);
            _pnlContent.Name = "_pnlContent";
            _pnlContent.Size = new Size(533, 282);
            _pnlContent.TabIndex = 27;
            // 
            // _grd
            // 
            _grd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _grd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _grd.Location = new Point(5, 5);
            _grd.Margin = new Padding(4);
            _grd.Name = "_grd";
            _grd.Size = new Size(523, 192);
            _grd.TabIndex = 12;
            // 
            // _lblCreditCardName
            // 
            _lblCreditCardName.AutoSize = true;
            _lblCreditCardName.Location = new Point(2, 5);
            _lblCreditCardName.Margin = new Padding(4, 0, 4, 0);
            _lblCreditCardName.Name = "_lblCreditCardName";
            _lblCreditCardName.Size = new Size(125, 20);
            _lblCreditCardName.TabIndex = 0;
            _lblCreditCardName.Text = "Tarjeta de credito";
            // 
            // _btnEntitySearch
            // 
            _btnEntitySearch.Image = Properties.Resources.creditcards1;
            _btnEntitySearch.Location = new Point(205, 28);
            _btnEntitySearch.Margin = new Padding(4);
            _btnEntitySearch.Name = "_btnEntitySearch";
            _btnEntitySearch.Size = new Size(29, 29);
            _btnEntitySearch.TabIndex = 22;
            _btnEntitySearch.UseVisualStyleBackColor = true;
            // 
            // _txtEntityName
            // 
            _txtEntityName.Location = new Point(5, 29);
            _txtEntityName.Margin = new Padding(4);
            _txtEntityName.Name = "_txtEntityName";
            _txtEntityName.Size = new Size(200, 27);
            _txtEntityName.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BackColor = Color.WhiteSmoke;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(_lblCreditCardName);
            panel1.Controls.Add(treeView1);
            panel1.Controls.Add(_btnEntitySearch);
            panel1.Controls.Add(_txtEntityName);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(239, 282);
            panel1.TabIndex = 13;
            // 
            // treeView1
            // 
            treeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            treeView1.Location = new Point(5, 83);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(229, 194);
            treeView1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ButtonFace;
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(533, 80);
            panel2.TabIndex = 13;
            // 
            // panel3
            // 
            panel3.Controls.Add(_grd);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 80);
            panel3.Name = "panel3";
            panel3.Size = new Size(533, 202);
            panel3.TabIndex = 14;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2, 60);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(164, 20);
            label1.TabIndex = 23;
            label1.Text = "Resumenes importados";
            // 
            // CreditCardResumesView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(_pnlContent);
            Controls.Add(panel1);
            Controls.Add(_toolStripButton);
            Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(4, 6, 4, 6);
            MinimumSize = new Size(700, 300);
            Name = "CreditCardResumesView";
            Size = new Size(772, 309);
            _toolStripButton.ResumeLayout(false);
            _toolStripButton.PerformLayout();
            _pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_grd).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolStrip _toolStripButton;
        private System.Windows.Forms.ToolStripButton _tsbClear;
        private System.Windows.Forms.Label _lblCreditCardName;
        private System.Windows.Forms.DataGridView _grd;
        private System.Windows.Forms.TextBox _txtEntityName;
        private Button _btnEntitySearch;
        private ToolStripButton _tsbExit;
        private Panel _pnlContent;
        private Panel panel1;
        private TreeView treeView1;
        private ToolStripButton _tsbImport;
        private Panel panel3;
        private Panel panel2;
        private Label label1;
    }
}
