namespace QuestionBankClient
{
    partial class UC_Shortans_settings
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cmbSkill = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbQuestionLevel = new System.Windows.Forms.ComboBox();
            this.cmbQuestionLanguage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuestionText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.flpAnswers = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddAns = new System.Windows.Forms.Button();
            this.lblrate = new System.Windows.Forms.Label();
            this.txtPoints = new System.Windows.Forms.TextBox();
            this.lblBaseSettings = new System.Windows.Forms.Label();
            this.lblCheck = new System.Windows.Forms.Label();
            this.lblEval = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.cmbSkill);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbQuestionLevel);
            this.panel1.Controls.Add(this.cmbQuestionLanguage);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtQuestionText);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.flpAnswers);
            this.panel1.Controls.Add(this.btnAddAns);
            this.panel1.Controls.Add(this.lblrate);
            this.panel1.Controls.Add(this.txtPoints);
            this.panel1.Controls.Add(this.lblBaseSettings);
            this.panel1.Controls.Add(this.lblCheck);
            this.panel1.Controls.Add(this.lblEval);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(751, 975);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint_1);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.OliveDrab;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(503, 850);
            this.btnSave.Margin = new System.Windows.Forms.Padding(6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(152, 87);
            this.btnSave.TabIndex = 30;
            this.btnSave.Text = "Mentés";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.DarkRed;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Location = new System.Drawing.Point(37, 850);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(152, 87);
            this.btnDelete.TabIndex = 31;
            this.btnDelete.Text = "Törlés";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click_1);
            // 
            // cmbSkill
            // 
            this.cmbSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSkill.Location = new System.Drawing.Point(30, 150);
            this.cmbSkill.Name = "cmbSkill";
            this.cmbSkill.Size = new System.Drawing.Size(300, 33);
            this.cmbSkill.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 25);
            this.label6.TabIndex = 1;
            this.label6.Text = "Készség:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 25);
            this.label5.TabIndex = 2;
            this.label5.Text = "Nyelv:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(380, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Szint:";
            // 
            // cmbQuestionLevel
            // 
            this.cmbQuestionLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuestionLevel.Location = new System.Drawing.Point(380, 60);
            this.cmbQuestionLevel.Name = "cmbQuestionLevel";
            this.cmbQuestionLevel.Size = new System.Drawing.Size(300, 33);
            this.cmbQuestionLevel.TabIndex = 4;
            // 
            // cmbQuestionLanguage
            // 
            this.cmbQuestionLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuestionLanguage.Location = new System.Drawing.Point(30, 60);
            this.cmbQuestionLanguage.Name = "cmbQuestionLanguage";
            this.cmbQuestionLanguage.Size = new System.Drawing.Size(300, 33);
            this.cmbQuestionLanguage.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(30, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Kérdés szövege:";
            // 
            // txtQuestionText
            // 
            this.txtQuestionText.Location = new System.Drawing.Point(30, 250);
            this.txtQuestionText.Multiline = true;
            this.txtQuestionText.Name = "txtQuestionText";
            this.txtQuestionText.Size = new System.Drawing.Size(650, 100);
            this.txtQuestionText.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(30, 380);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Helyes válaszok:";
            // 
            // flpAnswers
            // 
            this.flpAnswers.AutoScroll = true;
            this.flpAnswers.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpAnswers.Location = new System.Drawing.Point(30, 430);
            this.flpAnswers.Name = "flpAnswers";
            this.flpAnswers.Size = new System.Drawing.Size(650, 350);
            this.flpAnswers.TabIndex = 9;
            this.flpAnswers.WrapContents = false;
            // 
            // btnAddAns
            // 
            this.btnAddAns.Location = new System.Drawing.Point(530, 375);
            this.btnAddAns.Name = "btnAddAns";
            this.btnAddAns.Size = new System.Drawing.Size(150, 40);
            this.btnAddAns.TabIndex = 10;
            this.btnAddAns.Text = "+ Másik válasz";
            this.btnAddAns.Click += new System.EventHandler(this.btnAddAns_Click_1);
            // 
            // lblrate
            // 
            this.lblrate.AutoSize = true;
            this.lblrate.Location = new System.Drawing.Point(380, 120);
            this.lblrate.Name = "lblrate";
            this.lblrate.Size = new System.Drawing.Size(113, 25);
            this.lblrate.TabIndex = 11;
            this.lblrate.Text = "Pontszám:";
            // 
            // txtPoints
            // 
            this.txtPoints.Location = new System.Drawing.Point(380, 150);
            this.txtPoints.Name = "txtPoints";
            this.txtPoints.Size = new System.Drawing.Size(120, 31);
            this.txtPoints.TabIndex = 12;
            // 
            // lblBaseSettings
            // 
            this.lblBaseSettings.Location = new System.Drawing.Point(0, 0);
            this.lblBaseSettings.Name = "lblBaseSettings";
            this.lblBaseSettings.Size = new System.Drawing.Size(100, 23);
            this.lblBaseSettings.TabIndex = 36;
            this.lblBaseSettings.Visible = false;
            // 
            // lblCheck
            // 
            this.lblCheck.Location = new System.Drawing.Point(0, 0);
            this.lblCheck.Name = "lblCheck";
            this.lblCheck.Size = new System.Drawing.Size(100, 23);
            this.lblCheck.TabIndex = 37;
            this.lblCheck.Visible = false;
            // 
            // lblEval
            // 
            this.lblEval.Location = new System.Drawing.Point(0, 0);
            this.lblEval.Name = "lblEval";
            this.lblEval.Size = new System.Drawing.Size(100, 23);
            this.lblEval.TabIndex = 38;
            this.lblEval.Visible = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 39;
            this.label3.Visible = false;
            // 
            // UC_Shortans_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UC_Shortans_settings";
            this.Size = new System.Drawing.Size(751, 975);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQuestionText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flpAnswers;
        private System.Windows.Forms.Button btnAddAns;
        private System.Windows.Forms.Label lblrate;
        private System.Windows.Forms.TextBox txtPoints;
        private System.Windows.Forms.ComboBox cmbQuestionLevel;
        private System.Windows.Forms.ComboBox cmbQuestionLanguage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbSkill;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblBaseSettings;
        private System.Windows.Forms.Label lblCheck;
        private System.Windows.Forms.Label lblEval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
    }
}