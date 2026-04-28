namespace QuestionBankClient
{
    partial class UC_QuizList
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.flpQuizzes = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(80)))));
            this.lblTitle.Location = new System.Drawing.Point(80, 58);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(772, 72);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Korábban mentett kérdőíveid";
            // 
            // flpQuizzes
            // 
            this.flpQuizzes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpQuizzes.AutoScroll = true;
            this.flpQuizzes.BackColor = System.Drawing.Color.White;
            this.flpQuizzes.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpQuizzes.Location = new System.Drawing.Point(90, 173);
            this.flpQuizzes.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.flpQuizzes.Name = "flpQuizzes";
            this.flpQuizzes.Size = new System.Drawing.Size(1700, 1058);
            this.flpQuizzes.TabIndex = 1;
            this.flpQuizzes.WrapContents = false;
            this.flpQuizzes.Paint += new System.Windows.Forms.PaintEventHandler(this.flpQuizzes_Paint);
            // 
            // UC_QuizList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.flpQuizzes);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "UC_QuizList";
            this.Size = new System.Drawing.Size(1900, 1308);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flpQuizzes;
    }
}