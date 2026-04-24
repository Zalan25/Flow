namespace QuestionBankClient
{
    partial class UC_QuestionCard
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
            this.pnlquestioncard = new System.Windows.Forms.Panel();
            this.lblText = new System.Windows.Forms.Label();
            this.lblNumber = new System.Windows.Forms.Label();
            this.pnlquestioncard.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlquestioncard
            // 
            this.pnlquestioncard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlquestioncard.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlquestioncard.Controls.Add(this.lblText);
            this.pnlquestioncard.Controls.Add(this.lblNumber);
            this.pnlquestioncard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlquestioncard.Location = new System.Drawing.Point(3, 0);
            this.pnlquestioncard.Name = "pnlquestioncard";
            this.pnlquestioncard.Padding = new System.Windows.Forms.Padding(10);
            this.pnlquestioncard.Size = new System.Drawing.Size(795, 394);
            this.pnlquestioncard.TabIndex = 0;
            this.pnlquestioncard.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlquestioncard_Paint);
            // 
            // lblText
            // 
            this.lblText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(213, 212);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(71, 25);
            this.lblText.TabIndex = 1;
            this.lblText.Text = "Leírás";
            // 
            // lblNumber
            // 
            this.lblNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(85, 80);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(45, 25);
            this.lblNumber.TabIndex = 0;
            this.lblNumber.Text = "cim";
            // 
            // UC_QuestionCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlquestioncard);
            this.Name = "UC_QuestionCard";
            this.Size = new System.Drawing.Size(795, 394);
            this.pnlquestioncard.ResumeLayout(false);
            this.pnlquestioncard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlquestioncard;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Label lblNumber;
    }
}
