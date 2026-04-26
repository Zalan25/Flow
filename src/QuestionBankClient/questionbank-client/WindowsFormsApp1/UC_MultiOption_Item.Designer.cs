namespace QuestionBankClient
{
    partial class UC_MultiOption_Item
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
            this.txtOption = new System.Windows.Forms.TextBox();
            this.rbCorrect = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // txtOption
            // 
            this.txtOption.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtOption.Location = new System.Drawing.Point(0, 0);
            this.txtOption.Name = "txtOption";
            this.txtOption.Size = new System.Drawing.Size(983, 31);
            this.txtOption.TabIndex = 1;
            // 
            // rbCorrect
            // 
            this.rbCorrect.AutoSize = true;
            this.rbCorrect.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbCorrect.Location = new System.Drawing.Point(0, 31);
            this.rbCorrect.Name = "rbCorrect";
            this.rbCorrect.Size = new System.Drawing.Size(983, 29);
            this.rbCorrect.TabIndex = 2;
            this.rbCorrect.TabStop = true;
            this.rbCorrect.Text = "radioButton1";
            this.rbCorrect.UseVisualStyleBackColor = true;
            // 
            // UC_MultiOption_Item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbCorrect);
            this.Controls.Add(this.txtOption);
            this.Name = "UC_MultiOption_Item";
            this.Size = new System.Drawing.Size(983, 226);
            this.Load += new System.EventHandler(this.UC_MultiOption_Item_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOption;
        private System.Windows.Forms.RadioButton rbCorrect;
    }
}
