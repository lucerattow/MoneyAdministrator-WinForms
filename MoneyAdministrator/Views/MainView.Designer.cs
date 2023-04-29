namespace MyMoneyAdmin
{
    partial class MainView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            toolStrip = new ToolStrip();
            _tsddFile = new ToolStripDropDownButton();
            _tsbFileNew = new ToolStripMenuItem();
            _tsbFileOpen = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            _tsbFileClose = new ToolStripMenuItem();
            _pnlAside = new Panel();
            _btnCreditCards = new Button();
            _btnDashboard = new Button();
            _btnTransactions = new Button();
            _pnlContainer = new Panel();
            _pnlBackground = new Panel();
            toolStrip.SuspendLayout();
            _pnlAside.SuspendLayout();
            _pnlBackground.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.BackColor = Color.White;
            toolStrip.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip.Items.AddRange(new ToolStripItem[] { _tsddFile });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Padding = new Padding(5, 0, 1, 0);
            toolStrip.RenderMode = ToolStripRenderMode.System;
            toolStrip.Size = new Size(1051, 27);
            toolStrip.TabIndex = 0;
            toolStrip.Text = "toolStrip1";
            // 
            // _tsddFile
            // 
            _tsddFile.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _tsddFile.DropDownItems.AddRange(new ToolStripItem[] { _tsbFileNew, _tsbFileOpen, toolStripSeparator1, _tsbFileClose });
            _tsddFile.Image = MoneyAdministrator.Properties.Resources.documents;
            _tsddFile.ImageTransparentColor = Color.Magenta;
            _tsddFile.Name = "_tsddFile";
            _tsddFile.Size = new Size(72, 24);
            _tsddFile.Text = "Archivo";
            // 
            // _tsbFileNew
            // 
            _tsbFileNew.Image = MoneyAdministrator.Properties.Resources.document_plain_new_shadow;
            _tsbFileNew.Name = "_tsbFileNew";
            _tsbFileNew.Size = new Size(182, 24);
            _tsbFileNew.Text = "Nuevo archivo...";
            // 
            // _tsbFileOpen
            // 
            _tsbFileOpen.Image = MoneyAdministrator.Properties.Resources.folder_document_shadow;
            _tsbFileOpen.Name = "_tsbFileOpen";
            _tsbFileOpen.Size = new Size(182, 24);
            _tsbFileOpen.Text = "Abrir archivo...";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(179, 6);
            // 
            // _tsbFileClose
            // 
            _tsbFileClose.Image = MoneyAdministrator.Properties.Resources.document_forbidden_shadow;
            _tsbFileClose.Name = "_tsbFileClose";
            _tsbFileClose.Size = new Size(182, 24);
            _tsbFileClose.Text = "Cerrar archivo";
            // 
            // _pnlAside
            // 
            _pnlAside.BackColor = Color.White;
            _pnlAside.Controls.Add(_btnCreditCards);
            _pnlAside.Controls.Add(_btnDashboard);
            _pnlAside.Controls.Add(_btnTransactions);
            _pnlAside.Dock = DockStyle.Left;
            _pnlAside.Location = new Point(0, 27);
            _pnlAside.Name = "_pnlAside";
            _pnlAside.Size = new Size(200, 486);
            _pnlAside.TabIndex = 1;
            // 
            // _btnCreditCards
            // 
            _btnCreditCards.FlatAppearance.BorderSize = 0;
            _btnCreditCards.FlatStyle = FlatStyle.Flat;
            _btnCreditCards.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            _btnCreditCards.Location = new Point(0, 80);
            _btnCreditCards.Name = "_btnCreditCards";
            _btnCreditCards.Size = new Size(200, 40);
            _btnCreditCards.TabIndex = 2;
            _btnCreditCards.Text = "Tarjetas de crédito";
            _btnCreditCards.UseVisualStyleBackColor = true;
            _btnCreditCards.Click += _btnCreditCards_Click;
            // 
            // _btnDashboard
            // 
            _btnDashboard.BackColor = Color.White;
            _btnDashboard.FlatAppearance.BorderSize = 0;
            _btnDashboard.FlatStyle = FlatStyle.Flat;
            _btnDashboard.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            _btnDashboard.Location = new Point(0, 0);
            _btnDashboard.Name = "_btnDashboard";
            _btnDashboard.Size = new Size(200, 40);
            _btnDashboard.TabIndex = 1;
            _btnDashboard.Text = "Panel de resumen";
            _btnDashboard.UseVisualStyleBackColor = false;
            _btnDashboard.Click += _btnDashboard_Click;
            // 
            // _btnTransactions
            // 
            _btnTransactions.FlatAppearance.BorderSize = 0;
            _btnTransactions.FlatStyle = FlatStyle.Flat;
            _btnTransactions.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            _btnTransactions.Location = new Point(0, 40);
            _btnTransactions.Name = "_btnTransactions";
            _btnTransactions.Size = new Size(200, 40);
            _btnTransactions.TabIndex = 0;
            _btnTransactions.Text = "Transacciones Mensuales";
            _btnTransactions.UseVisualStyleBackColor = true;
            _btnTransactions.Click += _btnTransactions_Click;
            // 
            // _pnlContainer
            // 
            _pnlContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _pnlContainer.BackColor = Color.White;
            _pnlContainer.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            _pnlContainer.Location = new Point(1, 1);
            _pnlContainer.Name = "_pnlContainer";
            _pnlContainer.Size = new Size(850, 485);
            _pnlContainer.TabIndex = 2;
            // 
            // _pnlBackground
            // 
            _pnlBackground.BackColor = SystemColors.ControlDark;
            _pnlBackground.Controls.Add(_pnlContainer);
            _pnlBackground.Dock = DockStyle.Fill;
            _pnlBackground.Location = new Point(200, 27);
            _pnlBackground.Name = "_pnlBackground";
            _pnlBackground.Size = new Size(851, 486);
            _pnlBackground.TabIndex = 3;
            // 
            // MainView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1051, 513);
            Controls.Add(_pnlBackground);
            Controls.Add(_pnlAside);
            Controls.Add(toolStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MyMoneyAdmin";
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            _pnlAside.ResumeLayout(false);
            _pnlBackground.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip;
        private ToolStripDropDownButton _tsddFile;
        private ToolStripMenuItem _tsbFileNew;
        private ToolStripMenuItem _tsbFileOpen;
        private ToolStripMenuItem _tsbFileClose;
        private ToolStripSeparator toolStripSeparator1;
        private Panel _pnlAside;
        private Button _btnTransactions;
        private Panel _pnlContainer;
        private Button _btnCreditCards;
        private Button _btnDashboard;
        private Panel _pnlBackground;
    }
}