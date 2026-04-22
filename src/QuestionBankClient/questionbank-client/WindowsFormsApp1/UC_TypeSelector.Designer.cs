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
            this.btnDrop = new System.Windows.Forms.Button();
            this.btnMulti = new System.Windows.Forms.Button();
            this.btnLong = new System.Windows.Forms.Button();
            this.btnShort = new System.Windows.Forms.Button();
            this.pnlleft = new System.Windows.Forms.Panel();
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlright = new System.Windows.Forms.Panel();
            this.lblSetHeader = new System.Windows.Forms.Label();
            this.pnlleft.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            this.pnlright.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTypeHeader
            // 
            this.lblTypeHeader.AutoSize = true;
            this.lblTypeHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTypeHeader.Location = new System.Drawing.Point(16, 16);
            this.lblTypeHeader.Name = "lblTypeHeader";
            this.lblTypeHeader.Size = new System.Drawing.Size(250, 45);
            this.lblTypeHeader.TabIndex = 0;
            this.lblTypeHeader.Text = "Kérdés Típúsok";
            // 
            // btnTF
            // 
            this.btnTF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTF.Location = new System.Drawing.Point(24, 503);
            this.btnTF.Name = "btnTF";
            this.btnTF.Size = new System.Drawing.Size(205, 84);
            this.btnTF.TabIndex = 10;
            this.btnTF.Text = "Igaz-Hamis";
            this.btnTF.UseVisualStyleBackColor = true;
            // 
            // btnDrop
            // 
            this.btnDrop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDrop.Location = new System.Drawing.Point(24, 413);
            this.btnDrop.Name = "btnDrop";
            this.btnDrop.Size = new System.Drawing.Size(205, 84);
            this.btnDrop.TabIndex = 9;
            this.btnDrop.Text = "Legördülő lista";
            this.btnDrop.UseVisualStyleBackColor = true;
            // 
            // btnMulti
            // 
            this.btnMulti.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMulti.Location = new System.Drawing.Point(24, 323);
            this.btnMulti.Name = "btnMulti";
            this.btnMulti.Size = new System.Drawing.Size(205, 84);
            this.btnMulti.TabIndex = 8;
            this.btnMulti.Text = "Feleletválasztós";
            this.btnMulti.UseVisualStyleBackColor = true;
            // 
            // btnLong
            // 
            this.btnLong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLong.Location = new System.Drawing.Point(24, 233);
            this.btnLong.Name = "btnLong";
            this.btnLong.Size = new System.Drawing.Size(205, 84);
            this.btnLong.TabIndex = 7;
            this.btnLong.Text = "Hosszú válasz";
            this.btnLong.UseVisualStyleBackColor = true;
            // 
            // btnShort
            // 
            this.btnShort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShort.Location = new System.Drawing.Point(24, 143);
            this.btnShort.Name = "btnShort";
            this.btnShort.Size = new System.Drawing.Size(205, 84);
            this.btnShort.TabIndex = 6;
            this.btnShort.Text = "Rövid válasz";
            this.btnShort.UseVisualStyleBackColor = true;
            // 
            // pnlleft
            // 
            this.pnlleft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlleft.Controls.Add(this.btnShort);
            this.pnlleft.Controls.Add(this.btnLong);
            this.pnlleft.Controls.Add(this.btnMulti);
            this.pnlleft.Controls.Add(this.btnDrop);
            this.pnlleft.Controls.Add(this.btnTF);
            this.pnlleft.Controls.Add(this.lblTypeHeader);
            this.pnlleft.Location = new System.Drawing.Point(0, 0);
            this.pnlleft.Name = "pnlleft";
            this.pnlleft.Size = new System.Drawing.Size(269, 893);
            this.pnlleft.TabIndex = 0;
            // 
            // pnlCenter
            // 
            this.pnlCenter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCenter.Controls.Add(this.btnAdd);
            this.pnlCenter.Location = new System.Drawing.Point(275, 0);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(613, 893);
            this.pnlCenter.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Location = new System.Drawing.Point(66, 146);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(472, 133);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Kérdés hozzáadása";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // pnlright
            // 
            this.pnlright.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlright.Controls.Add(this.lblSetHeader);
            this.pnlright.Location = new System.Drawing.Point(894, 0);
            this.pnlright.Name = "pnlright";
            this.pnlright.Size = new System.Drawing.Size(303, 892);
            this.pnlright.TabIndex = 2;
            // 
            // lblSetHeader
            // 
            this.lblSetHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSetHeader.AutoSize = true;
            this.lblSetHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblSetHeader.Location = new System.Drawing.Point(35, 30);
            this.lblSetHeader.Name = "lblSetHeader";
            this.lblSetHeader.Size = new System.Drawing.Size(248, 31);
            this.lblSetHeader.TabIndex = 0;
            this.lblSetHeader.Text = "Kérdés beállítása.";
            // 
            // UC_TypeSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlright);
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlleft);
            this.Name = "UC_TypeSelector";
            this.Size = new System.Drawing.Size(1198, 893);
            this.pnlleft.ResumeLayout(false);
            this.pnlleft.PerformLayout();
            this.pnlCenter.ResumeLayout(false);
            this.pnlright.ResumeLayout(false);
            this.pnlright.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTypeHeader;
        private System.Windows.Forms.Button btnTF;
        private System.Windows.Forms.Button btnDrop;
        private System.Windows.Forms.Button btnMulti;
        private System.Windows.Forms.Button btnLong;
        private System.Windows.Forms.Button btnShort;
        private System.Windows.Forms.Panel pnlleft;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.Panel pnlright;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblSetHeader;
    }
}
