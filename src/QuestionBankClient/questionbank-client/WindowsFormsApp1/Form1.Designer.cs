namespace QuestionBankClient
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlmain = new System.Windows.Forms.Panel();
            this.pnlbetamain = new System.Windows.Forms.Panel();
            this.pnlchoose = new System.Windows.Forms.Panel();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnexisting = new System.Windows.Forms.Button();
            this.lbledit = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnedit = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnFinalSave = new System.Windows.Forms.Button();
            this.lblMainTitle = new System.Windows.Forms.Label();
            this.pnlmain.SuspendLayout();
            this.pnlchoose.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlmain
            // 
            this.pnlmain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlmain.Controls.Add(this.pnlbetamain);
            this.pnlmain.Controls.Add(this.pnlchoose);
            this.pnlmain.Location = new System.Drawing.Point(2, 313);
            this.pnlmain.Margin = new System.Windows.Forms.Padding(6);
            this.pnlmain.Name = "pnlmain";
            this.pnlmain.Size = new System.Drawing.Size(1288, 798);
            this.pnlmain.TabIndex = 0;
            this.pnlmain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlmain_Paint);
            // 
            // pnlbetamain
            // 
            this.pnlbetamain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlbetamain.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pnlbetamain.Location = new System.Drawing.Point(460, 0);
            this.pnlbetamain.Margin = new System.Windows.Forms.Padding(6);
            this.pnlbetamain.Name = "pnlbetamain";
            this.pnlbetamain.Size = new System.Drawing.Size(828, 798);
            this.pnlbetamain.TabIndex = 1;
            // 
            // pnlchoose
            // 
            this.pnlchoose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlchoose.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlchoose.Controls.Add(this.btnnew);
            this.pnlchoose.Controls.Add(this.btnexisting);
            this.pnlchoose.Controls.Add(this.lbledit);
            this.pnlchoose.Location = new System.Drawing.Point(2, 0);
            this.pnlchoose.Margin = new System.Windows.Forms.Padding(6);
            this.pnlchoose.Name = "pnlchoose";
            this.pnlchoose.Size = new System.Drawing.Size(446, 796);
            this.pnlchoose.TabIndex = 0;
            // 
            // btnnew
            // 
            this.btnnew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnnew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(84, 542);
            this.btnnew.Margin = new System.Windows.Forms.Padding(4);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(246, 154);
            this.btnnew.TabIndex = 3;
            this.btnnew.Text = "Új Kérdőív";
            this.btnnew.UseVisualStyleBackColor = false;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // btnexisting
            // 
            this.btnexisting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnexisting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnexisting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnexisting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnexisting.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnexisting.ForeColor = System.Drawing.Color.White;
            this.btnexisting.Location = new System.Drawing.Point(68, 326);
            this.btnexisting.Margin = new System.Windows.Forms.Padding(4);
            this.btnexisting.Name = "btnexisting";
            this.btnexisting.Size = new System.Drawing.Size(294, 128);
            this.btnexisting.TabIndex = 4;
            this.btnexisting.Text = "Kérdőíveim";
            this.btnexisting.UseVisualStyleBackColor = false;
            this.btnexisting.Click += new System.EventHandler(this.btnexisting_Click);
            // 
            // lbledit
            // 
            this.lbledit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbledit.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbledit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbledit.Location = new System.Drawing.Point(36, 19);
            this.lbledit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbledit.Name = "lbledit";
            this.lbledit.Size = new System.Drawing.Size(374, 158);
            this.lbledit.TabIndex = 3;
            this.lbledit.Text = "Kérdőív Szerkesztő";
            this.lbledit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHeader.AutoScroll = true;
            this.pnlHeader.Controls.Add(this.btnedit);
            this.pnlHeader.Controls.Add(this.btnBack);
            this.pnlHeader.Controls.Add(this.btnFinalSave);
            this.pnlHeader.Controls.Add(this.lblMainTitle);
            this.pnlHeader.Location = new System.Drawing.Point(2, 2);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1288, 302);
            this.pnlHeader.TabIndex = 3;
            // 
            // btnedit
            // 
            this.btnedit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnedit.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnedit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnedit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnedit.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnedit.ForeColor = System.Drawing.Color.White;
            this.btnedit.Location = new System.Drawing.Point(950, 163);
            this.btnedit.Margin = new System.Windows.Forms.Padding(4);
            this.btnedit.Name = "btnedit";
            this.btnedit.Size = new System.Drawing.Size(296, 104);
            this.btnedit.TabIndex = 2;
            this.btnedit.Text = "Szerkesztés";
            this.btnedit.UseVisualStyleBackColor = false;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(24, 13);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(116, 56);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "Vissza";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Visible = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click_1);
            // 
            // btnFinalSave
            // 
            this.btnFinalSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinalSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnFinalSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFinalSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalSave.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalSave.ForeColor = System.Drawing.Color.White;
            this.btnFinalSave.Location = new System.Drawing.Point(1000, 19);
            this.btnFinalSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnFinalSave.Name = "btnFinalSave";
            this.btnFinalSave.Size = new System.Drawing.Size(246, 104);
            this.btnFinalSave.TabIndex = 1;
            this.btnFinalSave.Text = "Mentés";
            this.btnFinalSave.UseVisualStyleBackColor = false;
            this.btnFinalSave.Click += new System.EventHandler(this.btnFinalSave_Click);
            // 
            // lblMainTitle
            // 
            this.lblMainTitle.AutoSize = true;
            this.lblMainTitle.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblMainTitle.Location = new System.Drawing.Point(14, 144);
            this.lblMainTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMainTitle.Name = "lblMainTitle";
            this.lblMainTitle.Size = new System.Drawing.Size(548, 59);
            this.lblMainTitle.TabIndex = 0;
            this.lblMainTitle.Text = "Saját kérdőív összeállítása";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1288, 1112);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlmain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(890, 871);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.pnlmain.ResumeLayout(false);
            this.pnlchoose.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlHeader;
        public System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnFinalSave;
        private System.Windows.Forms.Label lblMainTitle;
        private System.Windows.Forms.Button btnedit;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnexisting;
        private System.Windows.Forms.Label lbledit;
        public System.Windows.Forms.Panel pnlmain;
        public System.Windows.Forms.Panel pnlbetamain;
        public System.Windows.Forms.Panel pnlchoose;
    }
}