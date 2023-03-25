namespace MoneyAdministrator.CustomControls
{
    partial class YearPicker
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
            this._btnPrevius = new System.Windows.Forms.Button();
            this._btnNext = new System.Windows.Forms.Button();
            this._btnYearPicker = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _btnPrevius
            // 
            this._btnPrevius.Dock = System.Windows.Forms.DockStyle.Left;
            this._btnPrevius.Location = new System.Drawing.Point(0, 0);
            this._btnPrevius.Name = "_btnPrevius";
            this._btnPrevius.Size = new System.Drawing.Size(23, 23);
            this._btnPrevius.TabIndex = 0;
            this._btnPrevius.UseVisualStyleBackColor = true;
            this._btnPrevius.Click += new System.EventHandler(this.BtnPrevius_Click);
            // 
            // _btnNext
            // 
            this._btnNext.Dock = System.Windows.Forms.DockStyle.Right;
            this._btnNext.Location = new System.Drawing.Point(179, 0);
            this._btnNext.Name = "_btnNext";
            this._btnNext.Size = new System.Drawing.Size(23, 23);
            this._btnNext.TabIndex = 1;
            this._btnNext.UseVisualStyleBackColor = true;
            this._btnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // _btnYearPicker
            // 
            this._btnYearPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btnYearPicker.Location = new System.Drawing.Point(23, 0);
            this._btnYearPicker.Name = "_btnYearPicker";
            this._btnYearPicker.Size = new System.Drawing.Size(156, 23);
            this._btnYearPicker.TabIndex = 2;
            this._btnYearPicker.Text = "2022";
            this._btnYearPicker.UseVisualStyleBackColor = true;
            this._btnYearPicker.TextChanged += new System.EventHandler(this.BtnYearPicker_TextChanged);
            this._btnYearPicker.Click += new System.EventHandler(this.BtnYearPicker_Click);
            // 
            // YearPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._btnYearPicker);
            this.Controls.Add(this._btnNext);
            this.Controls.Add(this._btnPrevius);
            this.Name = "YearPicker";
            this.Size = new System.Drawing.Size(202, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private Button _btnPrevius;
        private Button _btnNext;
        private Button _btnYearPicker;
    }
}
