namespace QuestionBankClient
{
    partial class UC_TypeSelector
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
            this.lblTypeHeader = new System.Windows.Forms.Label();
            this.btnTF = new System.Windows.Forms.Button();
            this.btnMulti = new System.Windows.Forms.Button();
            this.btnShort = new System.Windows.Forms.Button();
            this.pnlleft = new System.Windows.Forms.Panel();
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.flpQuestionList = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlright = new System.Windows.Forms.Panel();
            this.lblSetHeader = new System.Windows.Forms.Label();
            this.pnlbottom = new System.Windows.Forms.Panel();
            this.btnnewquestion = new System.Windows.Forms.Button();
            this.pnlleft.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            this.pnlright.SuspendLayout();
            this.pnlbottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTypeHeader
            // 
            this.lblTypeHeader.AutoSize = true;
            this.lblTypeHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTypeHeader.Location = new System.Drawing.Point(16, 15);
            this.lblTypeHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTypeHeader.Name = "lblTypeHeader";
            this.lblTypeHeader.Size = new System.Drawing.Size(250, 45);
            this.lblTypeHeader.TabIndex = 0;
            this.lblTypeHeader.Text = "Kérdés Típúsok";
            // 
            // btnTF
            // 
            this.btnTF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTF.Location = new System.Drawing.Point(24, 504);
            this.btnTF.Margin = new System.Windows.Forms.Padding(4);
            this.btnTF.Name = "btnTF";
            this.btnTF.Size = new System.Drawing.Size(204, 85);
            this.btnTF.TabIndex = 10;
            this.btnTF.Text = "Igaz-Hamis";
            this.btnTF.UseVisualStyleBackColor = true;
            this.btnTF.Click += new System.EventHandler(this.btnTF_Click);
            // 
            // btnMulti
            // 
            this.btnMulti.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMulti.Location = new System.Drawing.Point(24, 323);
            this.btnMulti.Margin = new System.Windows.Forms.Padding(4);
            this.btnMulti.Name = "btnMulti";
            this.btnMulti.Size = new System.Drawing.Size(204, 85);
            this.btnMulti.TabIndex = 8;
            this.btnMulti.Text = "Feleletválasztós";
            this.btnMulti.UseVisualStyleBackColor = true;
            this.btnMulti.Click += new System.EventHandler(this.btnMulti_Click);
            // 
            // btnShort
            // 
            this.btnShort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShort.Location = new System.Drawing.Point(24, 142);
            this.btnShort.Margin = new System.Windows.Forms.Padding(4);
            this.btnShort.Name = "btnShort";
            this.btnShort.Size = new System.Drawing.Size(204, 85);
            this.btnShort.TabIndex = 6;
            this.btnShort.Text = "Rövid válasz";
            this.btnShort.UseVisualStyleBackColor = true;
            this.btnShort.Click += new System.EventHandler(this.btnShort_Click);
            // 
            // pnlleft
            // 
            this.pnlleft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlleft.Controls.Add(this.btnShort);
            this.pnlleft.Controls.Add(this.btnMulti);
            this.pnlleft.Controls.Add(this.btnTF);
            this.pnlleft.Controls.Add(this.lblTypeHeader);
            this.pnlleft.Location = new System.Drawing.Point(0, 0);
            this.pnlleft.Margin = new System.Windows.Forms.Padding(4);
            this.pnlleft.Name = "pnlleft";
            this.pnlleft.Size = new System.Drawing.Size(268, 938);
            this.pnlleft.TabIndex = 0;
            // 
            // pnlCenter
            // 
            this.pnlCenter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCenter.Controls.Add(this.flpQuestionList);
            this.pnlCenter.Location = new System.Drawing.Point(276, 0);
            this.pnlCenter.Margin = new System.Windows.Forms.Padding(4);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(439, 788);
            this.pnlCenter.TabIndex = 1;
            // 
            // flpQuestionList
            // 
            this.flpQuestionList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpQuestionList.AutoScroll = true;
            this.flpQuestionList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpQuestionList.Location = new System.Drawing.Point(11, 17);
            this.flpQuestionList.Name = "flpQuestionList";
            this.flpQuestionList.Size = new System.Drawing.Size(428, 736);
            this.flpQuestionList.TabIndex = 0;
            this.flpQuestionList.WrapContents = false;
            this.flpQuestionList.Paint += new System.Windows.Forms.PaintEventHandler(this.flpQuestionList_Paint);
            this.flpQuestionList.Resize += new System.EventHandler(this.flpQuestionList_Resize);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Location = new System.Drawing.Point(68, 25);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(148, 99);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Kérdés hozzáadása";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click_1);
            // 
            // pnlright
            // 
            this.pnlright.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlright.Controls.Add(this.lblSetHeader);
            this.pnlright.Location = new System.Drawing.Point(718, 0);
            this.pnlright.Margin = new System.Windows.Forms.Padding(4);
            this.pnlright.Name = "pnlright";
            this.pnlright.Size = new System.Drawing.Size(849, 937);
            this.pnlright.TabIndex = 2;
            this.pnlright.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlright_Paint);
            // 
            // lblSetHeader
            // 
            this.lblSetHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSetHeader.AutoSize = true;
            this.lblSetHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblSetHeader.Location = new System.Drawing.Point(2, 23);
            this.lblSetHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSetHeader.Name = "lblSetHeader";
            this.lblSetHeader.Size = new System.Drawing.Size(239, 31);
            this.lblSetHeader.TabIndex = 0;
            this.lblSetHeader.Text = "Kérdés beállítása";
            // 
            // pnlbottom
            // 
            this.pnlbottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlbottom.Controls.Add(this.btnnewquestion);
            this.pnlbottom.Controls.Add(this.btnAdd);
            this.pnlbottom.Location = new System.Drawing.Point(276, 795);
            this.pnlbottom.Name = "pnlbottom";
            this.pnlbottom.Size = new System.Drawing.Size(439, 142);
            this.pnlbottom.TabIndex = 3;
            // 
            // btnnewquestion
            // 
            this.btnnewquestion.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnnewquestion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnnewquestion.Location = new System.Drawing.Point(212, 25);
            this.btnnewquestion.Margin = new System.Windows.Forms.Padding(4);
            this.btnnewquestion.Name = "btnnewquestion";
            this.btnnewquestion.Size = new System.Drawing.Size(148, 99);
            this.btnnewquestion.TabIndex = 1;
            this.btnnewquestion.Text = "Új Kérdés hozzáadása";
            this.btnnewquestion.UseVisualStyleBackColor = true;
            this.btnnewquestion.Click += new System.EventHandler(this.btnnewquestion_Click);
            // 
            // UC_TypeSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlbottom);
            this.Controls.Add(this.pnlright);
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlleft);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UC_TypeSelector";
            this.Size = new System.Drawing.Size(1567, 938);
            this.Load += new System.EventHandler(this.UC_TypeSelector_Load_1);
            this.pnlleft.ResumeLayout(false);
            this.pnlleft.PerformLayout();
            this.pnlCenter.ResumeLayout(false);
            this.pnlright.ResumeLayout(false);
            this.pnlright.PerformLayout();
            this.pnlbottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTypeHeader;
        private System.Windows.Forms.Button btnTF;
        private System.Windows.Forms.Button btnMulti;
        private System.Windows.Forms.Button btnShort;
        private System.Windows.Forms.Panel pnlleft;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblSetHeader;
        public System.Windows.Forms.Panel pnlright;
        private System.Windows.Forms.Panel pnlbottom;
        private System.Windows.Forms.Button btnnewquestion;
        private System.Windows.Forms.FlowLayoutPanel flpQuestionList;
    }
}
