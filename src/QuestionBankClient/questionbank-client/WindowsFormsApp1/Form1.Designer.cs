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
            this.pnlchoose = new System.Windows.Forms.Panel();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnexisting = new System.Windows.Forms.Button();
            this.lbledit = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnFinalSave = new System.Windows.Forms.Button();
            this.lblMainTitle = new System.Windows.Forms.Label();
            this.pnlmain = new System.Windows.Forms.Panel();
            this.pnlbetamain = new System.Windows.Forms.Panel();
            this.pnlchoose.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlmain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlchoose
            // 
            this.pnlchoose.BackColor = System.Drawing.Color.White;
            this.pnlchoose.Controls.Add(this.btnnew);
            this.pnlchoose.Controls.Add(this.btnexisting);
            this.pnlchoose.Controls.Add(this.lbledit);
            this.pnlchoose.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlchoose.Location = new System.Drawing.Point(0, 140);
            this.pnlchoose.Margin = new System.Windows.Forms.Padding(0);
            this.pnlchoose.Name = "pnlchoose";
            this.pnlchoose.Size = new System.Drawing.Size(396, 972);
            this.pnlchoose.TabIndex = 0;
            // 
            // btnnew
            // 
            this.btnnew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnnew.BackColor = System.Drawing.Color.White;
            this.btnnew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnnew.FlatAppearance.BorderSize = 0;
            this.btnnew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.btnnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(43)))), ((int)(((byte)(71)))));
            this.btnnew.Location = new System.Drawing.Point(12, 300);
            this.btnnew.Margin = new System.Windows.Forms.Padding(4);
            this.btnnew.Name = "btnnew";
            this.btnnew.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnnew.Size = new System.Drawing.Size(372, 70);
            this.btnnew.TabIndex = 3;
            this.btnnew.Text = "➕  Új Kérdőív";
            this.btnnew.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnnew.UseVisualStyleBackColor = false;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // btnexisting
            // 
            this.btnexisting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnexisting.BackColor = System.Drawing.Color.White;
            this.btnexisting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnexisting.FlatAppearance.BorderSize = 0;
            this.btnexisting.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.btnexisting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnexisting.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnexisting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(43)))), ((int)(((byte)(71)))));
            this.btnexisting.Location = new System.Drawing.Point(12, 220);
            this.btnexisting.Margin = new System.Windows.Forms.Padding(4);
            this.btnexisting.Name = "btnexisting";
            this.btnexisting.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnexisting.Size = new System.Drawing.Size(372, 70);
            this.btnexisting.TabIndex = 4;
            this.btnexisting.Text = "📁  Kérdőíveim";
            this.btnexisting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnexisting.UseVisualStyleBackColor = false;
            this.btnexisting.Click += new System.EventHandler(this.btnexisting_Click);
            // 
            // lbledit
            // 
            this.lbledit.AutoSize = true;
            this.lbledit.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbledit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(43)))), ((int)(((byte)(71)))));
            this.lbledit.Location = new System.Drawing.Point(24, 40);
            this.lbledit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbledit.Name = "lbledit";
            this.lbledit.Size = new System.Drawing.Size(349, 172);
            this.lbledit.TabIndex = 3;
            this.lbledit.Text = "Kérdőív\r\nszerkesztő";
            this.lbledit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(246)))));
            this.pnlHeader.Controls.Add(this.btnBack);
            this.pnlHeader.Controls.Add(this.btnFinalSave);
            this.pnlHeader.Controls.Add(this.lblMainTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1288, 140);
            this.pnlHeader.TabIndex = 3;
            this.pnlHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlHeader_Paint);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(235)))));
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(43)))), ((int)(((byte)(71)))));
            this.btnBack.Location = new System.Drawing.Point(20, 45);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(90, 50);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "◀";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Visible = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click_1);
            // 
            // btnFinalSave
            // 
            this.btnFinalSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinalSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(43)))), ((int)(((byte)(71)))));
            this.btnFinalSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFinalSave.FlatAppearance.BorderSize = 0;
            this.btnFinalSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalSave.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalSave.ForeColor = System.Drawing.Color.White;
            this.btnFinalSave.Location = new System.Drawing.Point(1008, 40);
            this.btnFinalSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnFinalSave.Name = "btnFinalSave";
            this.btnFinalSave.Size = new System.Drawing.Size(240, 60);
            this.btnFinalSave.TabIndex = 1;
            this.btnFinalSave.Text = "Mentés";
            this.btnFinalSave.UseVisualStyleBackColor = false;
            this.btnFinalSave.Click += new System.EventHandler(this.btnFinalSave_Click);
            // 
            // lblMainTitle
            // 
            this.lblMainTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMainTitle.AutoSize = true;
            this.lblMainTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(43)))), ((int)(((byte)(71)))));
            this.lblMainTitle.Location = new System.Drawing.Point(169, 29);
            this.lblMainTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMainTitle.Name = "lblMainTitle";
            this.lblMainTitle.Size = new System.Drawing.Size(381, 86);
            this.lblMainTitle.TabIndex = 0;
            this.lblMainTitle.Text = "Kérdőíveim";
            this.lblMainTitle.Click += new System.EventHandler(this.lblMainTitle_Click);
            // 
            // pnlmain
            // 
            this.pnlmain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlmain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(246)))));
            this.pnlmain.Controls.Add(this.pnlbetamain);
            this.pnlmain.Location = new System.Drawing.Point(396, 140);
            this.pnlmain.Margin = new System.Windows.Forms.Padding(0);
            this.pnlmain.Name = "pnlmain";
            this.pnlmain.Size = new System.Drawing.Size(892, 972);
            this.pnlmain.TabIndex = 0;
            this.pnlmain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlmain_Paint);
            // 
            // pnlbetamain
            // 
            this.pnlbetamain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlbetamain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(246)))));
            this.pnlbetamain.Location = new System.Drawing.Point(0, 0);
            this.pnlbetamain.Margin = new System.Windows.Forms.Padding(0);
            this.pnlbetamain.Name = "pnlbetamain";
            this.pnlbetamain.Size = new System.Drawing.Size(892, 972);
            this.pnlbetamain.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(1288, 1112);
            this.Controls.Add(this.pnlchoose);
            this.Controls.Add(this.pnlmain);
            this.Controls.Add(this.pnlHeader);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1000, 800);
            this.Name = "Form1";
            this.Text = "Kérdőív Kezelő";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.pnlchoose.ResumeLayout(false);
            this.pnlchoose.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlmain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlHeader;
        public System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnFinalSave;
        private System.Windows.Forms.Label lblMainTitle;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnexisting;
        private System.Windows.Forms.Label lbledit;
        public System.Windows.Forms.Panel pnlmain;
        public System.Windows.Forms.Panel pnlbetamain;
        public System.Windows.Forms.Panel pnlchoose;
    }
}