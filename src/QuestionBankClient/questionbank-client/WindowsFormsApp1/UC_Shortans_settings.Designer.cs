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
            
            // Labels
            this.label4 = new System.Windows.Forms.Label(); // Nyelv
            this.label5 = new System.Windows.Forms.Label(); // Szint
            this.label6 = new System.Windows.Forms.Label(); // Skill
            this.lblrate = new System.Windows.Forms.Label(); // Pontszám
            this.label1 = new System.Windows.Forms.Label(); // Kérdés szövege
            this.label2 = new System.Windows.Forms.Label(); // Helyes válaszok
            
            // Inputs
            this.cmbQuestionLanguage = new System.Windows.Forms.ComboBox();
            this.cmbQuestionLevel = new System.Windows.Forms.ComboBox();
            this.cmbSkill = new System.Windows.Forms.ComboBox();
            this.txtPoints = new System.Windows.Forms.TextBox();
            this.txtQuestionText = new System.Windows.Forms.TextBox();
            this.flpAnswers = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddAns = new System.Windows.Forms.Button();
            
            // Buttons
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            
            // Separators
            this.pnlSep1 = new System.Windows.Forms.Panel();
            this.pnlSep2 = new System.Windows.Forms.Panel();

            this.panel1.SuspendLayout();
            this.SuspendLayout();

            // --- Panel 1 (Konténer) ---
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(751, 975);
            this.panel1.TabIndex = 0;

            // --- 1. SOR: NYELV ÉS SZINT ---
            this.label5.AutoSize = true; this.label5.Location = new System.Drawing.Point(30, 30); this.label5.Text = "Nyelv:";
            this.cmbQuestionLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuestionLanguage.Location = new System.Drawing.Point(30, 60); this.cmbQuestionLanguage.Size = new System.Drawing.Size(300, 33);
            
            this.label4.AutoSize = true; this.label4.Location = new System.Drawing.Point(380, 30); this.label4.Text = "Szint:";
            this.cmbQuestionLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuestionLevel.Location = new System.Drawing.Point(380, 60); this.cmbQuestionLevel.Size = new System.Drawing.Size(300, 33);

            // --- 2. SOR: KÉSZSÉG ÉS PONTSZÁM ---
            this.label6.AutoSize = true; this.label6.Location = new System.Drawing.Point(30, 120); this.label6.Text = "Készség:";
            this.cmbSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSkill.Location = new System.Drawing.Point(30, 150); this.cmbSkill.Size = new System.Drawing.Size(300, 33);

            this.lblrate.AutoSize = true; this.lblrate.Location = new System.Drawing.Point(380, 120); this.lblrate.Text = "Pontszám:";
            this.txtPoints.Location = new System.Drawing.Point(380, 150); this.txtPoints.Size = new System.Drawing.Size(120, 31);

            // --- 3. SOR: KÉRDÉS SZÖVEGE ---
            this.label1.AutoSize = true; this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(30, 220); this.label1.Text = "Kérdés szövege:";
            this.txtQuestionText.Location = new System.Drawing.Point(30, 250); this.txtQuestionText.Multiline = true;
            this.txtQuestionText.Size = new System.Drawing.Size(650, 100);

            // --- 4. SOR: VÁLASZOK ---
            this.label2.AutoSize = true; this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(30, 380); this.label2.Text = "Helyes válaszok:";
            
            this.btnAddAns.Location = new System.Drawing.Point(530, 375); this.btnAddAns.Size = new System.Drawing.Size(150, 40); this.btnAddAns.Text = "+ Másik válasz";
            this.btnAddAns.Click += new System.EventHandler(this.btnAddAns_Click_1);

            this.flpAnswers.AutoScroll = true; this.flpAnswers.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpAnswers.Location = new System.Drawing.Point(30, 430); this.flpAnswers.Size = new System.Drawing.Size(650, 350);

            // --- ALSÓ GOMBOK ---
            this.btnDelete.Location = new System.Drawing.Point(37, 850); this.btnDelete.Size = new System.Drawing.Size(152, 87);
            this.btnSave.Location = new System.Drawing.Point(503, 850); this.btnSave.Size = new System.Drawing.Size(152, 87);

            // --- ÖSSZESZERELÉS ---
            this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.cmbSkill, this.label6, this.label5, this.label4, this.cmbQuestionLevel, 
                this.cmbQuestionLanguage, this.label1, this.txtQuestionText, 
                this.label2, this.flpAnswers, this.btnAddAns, this.lblrate, 
                this.txtPoints, this.btnDelete, this.btnSave
            });
            
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
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbQuestionLevel;
        private System.Windows.Forms.ComboBox cmbQuestionLanguage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbSkill;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlSep1;
        private System.Windows.Forms.Panel pnlSep2;
        private System.Windows.Forms.Panel pnlSep4; // Ezeket a változókat megtartottam, ha később még használnád
        private System.Windows.Forms.Label lblBaseSettings;
        private System.Windows.Forms.Label lblCheck;
        private System.Windows.Forms.Label lblEval;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
    }
}