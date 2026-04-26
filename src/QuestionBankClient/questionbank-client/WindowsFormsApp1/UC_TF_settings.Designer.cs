namespace QuestionBankClient
{
    partial class UC_TF_settings
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSetHeader = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuestionText = new System.Windows.Forms.TextBox();
            this.lblMandatory = new System.Windows.Forms.Label();
            this.chkMandatory = new System.Windows.Forms.CheckBox();
            this.pnlSeparator = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.rbTrue = new System.Windows.Forms.RadioButton();
            this.rbFalse = new System.Windows.Forms.RadioButton();
            this.btnAddOption = new System.Windows.Forms.Button();
            this.lblrate = new System.Windows.Forms.Label();
            this.txtPoints = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.panel1.Controls.Add(this.lblSetHeader);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtQuestionText);
            this.panel1.Controls.Add(this.lblMandatory);
            this.panel1.Controls.Add(this.chkMandatory);
            this.panel1.Controls.Add(this.pnlSeparator);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.rbTrue);
            this.panel1.Controls.Add(this.rbFalse);
            this.panel1.Controls.Add(this.btnAddOption);
            this.panel1.Controls.Add(this.lblrate);
            this.panel1.Controls.Add(this.txtPoints);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 800);
            this.panel1.TabIndex = 0;
            // 
            // lblSetHeader
            // 
            this.lblSetHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblSetHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(80)))));
            this.lblSetHeader.Location = new System.Drawing.Point(0, 20);
            this.lblSetHeader.Name = "lblSetHeader";
            this.lblSetHeader.Size = new System.Drawing.Size(500, 40);
            this.lblSetHeader.TabIndex = 0;
            this.lblSetHeader.Text = "Kérdés beállítása";
            this.lblSetHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(40, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kérdés szövege:";
            // 
            // txtQuestionText
            // 
            this.txtQuestionText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtQuestionText.Location = new System.Drawing.Point(45, 135);
            this.txtQuestionText.Multiline = true;
            this.txtQuestionText.Name = "txtQuestionText";
            this.txtQuestionText.Size = new System.Drawing.Size(410, 50);
            this.txtQuestionText.TabIndex = 2;
            // 
            // lblMandatory
            // 
            this.lblMandatory.AutoSize = true;
            this.lblMandatory.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblMandatory.Location = new System.Drawing.Point(40, 210);
            this.lblMandatory.Name = "lblMandatory";
            this.lblMandatory.Size = new System.Drawing.Size(160, 25);
            this.lblMandatory.TabIndex = 3;
            this.lblMandatory.Text = "Kötelező kérdés";
            // 
            // chkMandatory
            // 
            this.chkMandatory.AutoSize = true;
            this.chkMandatory.Location = new System.Drawing.Point(380, 217);
            this.chkMandatory.Name = "chkMandatory";
            this.chkMandatory.Size = new System.Drawing.Size(15, 14);
            this.chkMandatory.TabIndex = 4;
            this.chkMandatory.UseVisualStyleBackColor = true;
            // 
            // pnlSeparator
            // 
            this.pnlSeparator.BackColor = System.Drawing.Color.LightGray;
            this.pnlSeparator.Location = new System.Drawing.Point(45, 260);
            this.pnlSeparator.Name = "pnlSeparator";
            this.pnlSeparator.Size = new System.Drawing.Size(410, 1);
            this.pnlSeparator.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(40, 280);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Helyes válasz:";
            // 
            // rbTrue
            // 
            this.rbTrue.BackColor = System.Drawing.Color.White;
            this.rbTrue.Checked = true;
            this.rbTrue.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.rbTrue.Location = new System.Drawing.Point(45, 320);
            this.rbTrue.Name = "rbTrue";
            this.rbTrue.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.rbTrue.Size = new System.Drawing.Size(410, 45);
            this.rbTrue.TabIndex = 7;
            this.rbTrue.TabStop = true;
            this.rbTrue.Text = "Igaz";
            this.rbTrue.UseVisualStyleBackColor = false;
            // 
            // rbFalse
            // 
            this.rbFalse.BackColor = System.Drawing.Color.White;
            this.rbFalse.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.rbFalse.Location = new System.Drawing.Point(45, 375);
            this.rbFalse.Name = "rbFalse";
            this.rbFalse.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.rbFalse.Size = new System.Drawing.Size(410, 45);
            this.rbFalse.TabIndex = 8;
            this.rbFalse.Text = "Hamis";
            this.rbFalse.UseVisualStyleBackColor = false;
            // 
            // btnAddOption
            // 
            this.btnAddOption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(80)))));
            this.btnAddOption.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddOption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddOption.ForeColor = System.Drawing.Color.White;
            this.btnAddOption.Location = new System.Drawing.Point(100, 440);
            this.btnAddOption.Name = "btnAddOption";
            this.btnAddOption.Size = new System.Drawing.Size(300, 45);
            this.btnAddOption.TabIndex = 9;
            this.btnAddOption.Text = "+ Opció hozzáadása";
            this.btnAddOption.UseVisualStyleBackColor = false;
            // 
            // lblrate
            // 
            this.lblrate.AutoSize = true;
            this.lblrate.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblrate.Location = new System.Drawing.Point(40, 530);
            this.lblrate.Name = "lblrate";
            this.lblrate.Size = new System.Drawing.Size(104, 25);
            this.lblrate.TabIndex = 10;
            this.lblrate.Text = "Pontszám:";
            // 
            // txtPoints
            // 
            this.txtPoints.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtPoints.Location = new System.Drawing.Point(170, 527);
            this.txtPoints.Name = "txtPoints";
            this.txtPoints.Size = new System.Drawing.Size(80, 32);
            this.txtPoints.TabIndex = 11;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(80)))));
            this.btnDelete.Location = new System.Drawing.Point(45, 620);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(150, 45);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Törlés";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // UC_TF_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UC_TF_settings";
            this.Size = new System.Drawing.Size(500, 800);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSetHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQuestionText;
        private System.Windows.Forms.Label lblMandatory;
        private System.Windows.Forms.CheckBox chkMandatory;
        private System.Windows.Forms.Panel pnlSeparator;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbTrue;
        private System.Windows.Forms.RadioButton rbFalse;
        private System.Windows.Forms.Button btnAddOption;
        private System.Windows.Forms.Label lblrate;
        private System.Windows.Forms.TextBox txtPoints;
        private System.Windows.Forms.Button btnDelete;
    }
}