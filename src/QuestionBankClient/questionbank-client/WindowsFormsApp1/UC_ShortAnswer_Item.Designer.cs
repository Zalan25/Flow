namespace QuestionBankClient
{
    partial class UC_ShortAnswer_Item
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
            this.txtAltAnswer = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtAltAnswer
            // 
            this.txtAltAnswer.Location = new System.Drawing.Point(21, 19);
            this.txtAltAnswer.Name = "txtAltAnswer";
            this.txtAltAnswer.Size = new System.Drawing.Size(269, 31);
            this.txtAltAnswer.TabIndex = 0;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(357, 19);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(142, 36);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "törlés";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // UC_ShortAnswer_Item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.txtAltAnswer);
            this.Name = "UC_ShortAnswer_Item";
            this.Size = new System.Drawing.Size(546, 71);
            this.Load += new System.EventHandler(this.UC_ShortAnswer_Item_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAltAnswer;
        private System.Windows.Forms.Button btnRemove;
    }
}
