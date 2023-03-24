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
            this.BtnPrevius = new System.Windows.Forms.Button();
            this.BtnNext = new System.Windows.Forms.Button();
            this.BtnYearPicker = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnPrevius
            // 
            this.BtnPrevius.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnPrevius.Location = new System.Drawing.Point(0, 0);
            this.BtnPrevius.Name = "BtnPrevius";
            this.BtnPrevius.Size = new System.Drawing.Size(23, 23);
            this.BtnPrevius.TabIndex = 0;
            this.BtnPrevius.UseVisualStyleBackColor = true;
            this.BtnPrevius.Click += new System.EventHandler(this.BtnPrevius_Click);
            // 
            // BtnNext
            // 
            this.BtnNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnNext.Location = new System.Drawing.Point(179, 0);
            this.BtnNext.Name = "BtnNext";
            this.BtnNext.Size = new System.Drawing.Size(23, 23);
            this.BtnNext.TabIndex = 1;
            this.BtnNext.UseVisualStyleBackColor = true;
            this.BtnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // BtnYearPicker
            // 
            this.BtnYearPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnYearPicker.Location = new System.Drawing.Point(23, 0);
            this.BtnYearPicker.Name = "BtnYearPicker";
            this.BtnYearPicker.Size = new System.Drawing.Size(156, 23);
            this.BtnYearPicker.TabIndex = 2;
            this.BtnYearPicker.Text = "2022";
            this.BtnYearPicker.UseVisualStyleBackColor = true;
            this.BtnYearPicker.TextChanged += new System.EventHandler(this.BtnYearPicker_TextChanged);
            this.BtnYearPicker.Click += new System.EventHandler(this.BtnYearPicker_Click);
            // 
            // YearPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnYearPicker);
            this.Controls.Add(this.BtnNext);
            this.Controls.Add(this.BtnPrevius);
            this.Name = "YearPicker";
            this.Size = new System.Drawing.Size(202, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private Button BtnPrevius;
        private Button BtnNext;
        private Button BtnYearPicker;
    }
}
