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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreditCardResumesView));
            _toolStripButton = new ToolStrip();
            _tsbImport = new ToolStripButton();
            _tsbInsert = new ToolStripButton();
            _tsbDelete = new ToolStripButton();
            _tsbClear = new ToolStripButton();
            _tsbExit = new ToolStripButton();
            _pnlContent = new Panel();
            panel3 = new Panel();
            _grd = new DataGridView();
            panel2 = new Panel();
            _txtMinimumPayment = new CustomControls.MoneyTextBox();
            label7 = new Label();
            _txtTotalArs = new CustomControls.MoneyTextBox();
            label6 = new Label();
            _txtTotalUsd = new CustomControls.MoneyTextBox();
            _lblAmount = new Label();
            label4 = new Label();
            _dtpDateNextExpiration = new DateTimePicker();
            label5 = new Label();
            _dtpDateNext = new DateTimePicker();
            label3 = new Label();
            _dtpDateExpiration = new DateTimePicker();
            label2 = new Label();
            _dtpDate = new DateTimePicker();
            _lblDatePeriod = new Label();
            _dtpDatePeriod = new DateTimePicker();
            _lblCreditCardName = new Label();
            _btnCreditCardSearch = new Button();
            _txtCreditCard = new TextBox();
            panel1 = new Panel();
            label1 = new Label();
            _tvSummaryList = new TreeView();
            imagesTreeView = new ImageList(components);
            _toolStripButton.SuspendLayout();
            _pnlContent.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_grd).BeginInit();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // _toolStripButton
            // 
            _toolStripButton.AllowMerge = false;
            _toolStripButton.BackColor = Color.White;
            _toolStripButton.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            _toolStripButton.GripStyle = ToolStripGripStyle.Hidden;
            _toolStripButton.Items.AddRange(new ToolStripItem[] { _tsbImport, _tsbInsert, _tsbDelete, _tsbClear, _tsbExit });
            _toolStripButton.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            _toolStripButton.Location = new Point(0, 0);
            _toolStripButton.Name = "_toolStripButton";
            _toolStripButton.Padding = new Padding(6, 0, 1, 0);
            _toolStripButton.RenderMode = ToolStripRenderMode.System;
            _toolStripButton.Size = new Size(1000, 27);
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
            _tsbImport.Click += _tsbImport_Click;
            // 
            // _tsbInsert
            // 
            _tsbInsert.Image = Properties.Resources.save_as_shadow;
            _tsbInsert.ImageAlign = ContentAlignment.MiddleLeft;
            _tsbInsert.ImageTransparentColor = Color.Magenta;
            _tsbInsert.Name = "_tsbInsert";
            _tsbInsert.Size = new Size(82, 24);
            _tsbInsert.Text = "Guardar";
            _tsbInsert.Click += _tsbInsert_Click;
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
            // _pnlContent
            // 
            _pnlContent.Controls.Add(panel3);
            _pnlContent.Controls.Add(panel2);
            _pnlContent.Dock = DockStyle.Fill;
            _pnlContent.Location = new Point(284, 27);
            _pnlContent.Name = "_pnlContent";
            _pnlContent.Size = new Size(716, 273);
            _pnlContent.TabIndex = 27;
            // 
            // panel3
            // 
            panel3.Controls.Add(_grd);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 122);
            panel3.Name = "panel3";
            panel3.Size = new Size(716, 151);
            panel3.TabIndex = 14;
            // 
            // _grd
            // 
            _grd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _grd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _grd.Location = new Point(5, 5);
            _grd.Margin = new Padding(4);
            _grd.Name = "_grd";
            _grd.Size = new Size(707, 141);
            _grd.TabIndex = 12;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ButtonFace;
            panel2.Controls.Add(_txtMinimumPayment);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(_txtTotalArs);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(_txtTotalUsd);
            panel2.Controls.Add(_lblAmount);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(_dtpDateNextExpiration);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(_dtpDateNext);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(_dtpDateExpiration);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(_dtpDate);
            panel2.Controls.Add(_lblDatePeriod);
            panel2.Controls.Add(_dtpDatePeriod);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(716, 122);
            panel2.TabIndex = 13;
            // 
            // _txtMinimumPayment
            // 
            _txtMinimumPayment.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtMinimumPayment.Colored = true;
            _txtMinimumPayment.ForeColor = Color.FromArgb(80, 80, 80);
            _txtMinimumPayment.Location = new Point(441, 84);
            _txtMinimumPayment.Name = "_txtMinimumPayment";
            _txtMinimumPayment.OperatorSymbol = "-";
            _txtMinimumPayment.Size = new Size(132, 27);
            _txtMinimumPayment.TabIndex = 36;
            _txtMinimumPayment.Tag = "";
            _txtMinimumPayment.Text = "-0,00 $";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new Point(438, 60);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(97, 20);
            label7.TabIndex = 35;
            label7.Text = "Pago minimo";
            // 
            // _txtTotalArs
            // 
            _txtTotalArs.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtTotalArs.Colored = true;
            _txtTotalArs.ForeColor = Color.FromArgb(80, 80, 80);
            _txtTotalArs.Location = new Point(441, 29);
            _txtTotalArs.Name = "_txtTotalArs";
            _txtTotalArs.OperatorSymbol = "-";
            _txtTotalArs.Size = new Size(132, 27);
            _txtTotalArs.TabIndex = 34;
            _txtTotalArs.Tag = "";
            _txtTotalArs.Text = "-0,00 $";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new Point(438, 5);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(73, 20);
            label6.TabIndex = 33;
            label6.Text = "Total ARS";
            // 
            // _txtTotalUsd
            // 
            _txtTotalUsd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtTotalUsd.Colored = true;
            _txtTotalUsd.ForeColor = Color.FromArgb(80, 80, 80);
            _txtTotalUsd.Location = new Point(579, 29);
            _txtTotalUsd.Name = "_txtTotalUsd";
            _txtTotalUsd.OperatorSymbol = "-";
            _txtTotalUsd.Size = new Size(132, 27);
            _txtTotalUsd.TabIndex = 32;
            _txtTotalUsd.Tag = "";
            _txtTotalUsd.Text = "-0,00 $";
            // 
            // _lblAmount
            // 
            _lblAmount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _lblAmount.AutoSize = true;
            _lblAmount.Location = new Point(576, 5);
            _lblAmount.Margin = new Padding(4, 0, 4, 0);
            _lblAmount.Name = "_lblAmount";
            _lblAmount.Size = new Size(75, 20);
            _lblAmount.TabIndex = 31;
            _lblAmount.Text = "Total USD";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(274, 60);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(150, 20);
            label4.TabIndex = 24;
            label4.Text = "Proximo Vencimiento";
            // 
            // _dtpDateNextExpiration
            // 
            _dtpDateNextExpiration.CustomFormat = " yyyy-MM-dd";
            _dtpDateNextExpiration.Format = DateTimePickerFormat.Custom;
            _dtpDateNextExpiration.Location = new Point(277, 84);
            _dtpDateNextExpiration.Margin = new Padding(4);
            _dtpDateNextExpiration.Name = "_dtpDateNextExpiration";
            _dtpDateNextExpiration.Size = new Size(140, 27);
            _dtpDateNextExpiration.TabIndex = 25;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(126, 60);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(119, 20);
            label5.TabIndex = 22;
            label5.Text = "Priximo resumen";
            // 
            // _dtpDateNext
            // 
            _dtpDateNext.CustomFormat = " yyyy-MM-dd";
            _dtpDateNext.Format = DateTimePickerFormat.Custom;
            _dtpDateNext.Location = new Point(129, 84);
            _dtpDateNext.Margin = new Padding(4);
            _dtpDateNext.Name = "_dtpDateNext";
            _dtpDateNext.Size = new Size(140, 27);
            _dtpDateNext.TabIndex = 23;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(274, 5);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(91, 20);
            label3.TabIndex = 20;
            label3.Text = "Vencimiento";
            // 
            // _dtpDateExpiration
            // 
            _dtpDateExpiration.CustomFormat = " yyyy-MM-dd";
            _dtpDateExpiration.Format = DateTimePickerFormat.Custom;
            _dtpDateExpiration.Location = new Point(277, 29);
            _dtpDateExpiration.Margin = new Padding(4);
            _dtpDateExpiration.Name = "_dtpDateExpiration";
            _dtpDateExpiration.Size = new Size(140, 27);
            _dtpDateExpiration.TabIndex = 21;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(126, 5);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(107, 20);
            label2.TabIndex = 18;
            label2.Text = "Fecha resumen";
            // 
            // _dtpDate
            // 
            _dtpDate.CustomFormat = " yyyy-MM-dd";
            _dtpDate.Format = DateTimePickerFormat.Custom;
            _dtpDate.Location = new Point(129, 29);
            _dtpDate.Margin = new Padding(4);
            _dtpDate.Name = "_dtpDate";
            _dtpDate.Size = new Size(140, 27);
            _dtpDate.TabIndex = 19;
            // 
            // _lblDatePeriod
            // 
            _lblDatePeriod.AutoSize = true;
            _lblDatePeriod.Location = new Point(2, 5);
            _lblDatePeriod.Margin = new Padding(4, 0, 4, 0);
            _lblDatePeriod.Name = "_lblDatePeriod";
            _lblDatePeriod.Size = new Size(60, 20);
            _lblDatePeriod.TabIndex = 16;
            _lblDatePeriod.Text = "Periodo";
            // 
            // _dtpDatePeriod
            // 
            _dtpDatePeriod.CustomFormat = " yyyy-MM";
            _dtpDatePeriod.Format = DateTimePickerFormat.Custom;
            _dtpDatePeriod.Location = new Point(5, 29);
            _dtpDatePeriod.Margin = new Padding(4);
            _dtpDatePeriod.Name = "_dtpDatePeriod";
            _dtpDatePeriod.Size = new Size(116, 27);
            _dtpDatePeriod.TabIndex = 17;
            _dtpDatePeriod.ValueChanged += _dtpDatePeriod_ValueChanged;
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
            // _btnCreditCardSearch
            // 
            _btnCreditCardSearch.Image = Properties.Resources.creditcards1;
            _btnCreditCardSearch.Location = new Point(250, 28);
            _btnCreditCardSearch.Margin = new Padding(4);
            _btnCreditCardSearch.Name = "_btnCreditCardSearch";
            _btnCreditCardSearch.Size = new Size(29, 29);
            _btnCreditCardSearch.TabIndex = 22;
            _btnCreditCardSearch.UseVisualStyleBackColor = true;
            _btnCreditCardSearch.Click += _btnCreditCardSearch_Click;
            // 
            // _txtCreditCard
            // 
            _txtCreditCard.Location = new Point(5, 29);
            _txtCreditCard.Margin = new Padding(4);
            _txtCreditCard.Name = "_txtCreditCard";
            _txtCreditCard.Size = new Size(245, 27);
            _txtCreditCard.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonFace;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(_lblCreditCardName);
            panel1.Controls.Add(_tvSummaryList);
            panel1.Controls.Add(_btnCreditCardSearch);
            panel1.Controls.Add(_txtCreditCard);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(284, 273);
            panel1.TabIndex = 13;
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
            // _tvSummaryList
            // 
            _tvSummaryList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _tvSummaryList.Location = new Point(5, 83);
            _tvSummaryList.Name = "_tvSummaryList";
            _tvSummaryList.Size = new Size(274, 185);
            _tvSummaryList.TabIndex = 0;
            _tvSummaryList.NodeMouseClick += _tvSummaryList_NodeMouseClick;
            // 
            // imagesTreeView
            // 
            imagesTreeView.ColorDepth = ColorDepth.Depth8Bit;
            imagesTreeView.ImageStream = (ImageListStreamer)resources.GetObject("imagesTreeView.ImageStream");
            imagesTreeView.TransparentColor = Color.Transparent;
            imagesTreeView.Images.SetKeyName(0, "document_attachment_shadow.png");
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
            MinimumSize = new Size(1000, 300);
            Name = "CreditCardResumesView";
            Size = new Size(1000, 300);
            Resize += CreditCardResumesView_Resize;
            _toolStripButton.ResumeLayout(false);
            _toolStripButton.PerformLayout();
            _pnlContent.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_grd).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolStrip _toolStripButton;
        private System.Windows.Forms.ToolStripButton _tsbClear;
        private System.Windows.Forms.Label _lblCreditCardName;
        private System.Windows.Forms.DataGridView _grd;
        private System.Windows.Forms.TextBox _txtCreditCard;
        private Button _btnCreditCardSearch;
        private ToolStripButton _tsbExit;
        private Panel _pnlContent;
        private Panel panel1;
        private TreeView _tvSummaryList;
        private ToolStripButton _tsbImport;
        private Panel panel3;
        private Panel panel2;
        private Label label1;
        private Label _lblDatePeriod;
        private DateTimePicker _dtpDatePeriod;
        private Label label4;
        private DateTimePicker _dtpDateNextExpiration;
        private Label label5;
        private DateTimePicker _dtpDateNext;
        private Label label3;
        private DateTimePicker _dtpDateExpiration;
        private Label label2;
        private DateTimePicker _dtpDate;
        private CustomControls.MoneyTextBox _txtMinimumPayment;
        private Label label7;
        private CustomControls.MoneyTextBox _txtTotalArs;
        private Label label6;
        private CustomControls.MoneyTextBox _txtTotalUsd;
        private Label _lblAmount;
        private ToolStripButton _tsbInsert;
        private ToolStripButton _tsbDelete;
        private ImageList imagesTreeView;
    }
}
