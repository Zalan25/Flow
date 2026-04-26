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
            this.lblBaseSettings = new System.Windows.Forms.Label();
            this.pnlSep1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuestionText = new System.Windows.Forms.TextBox();
            this.lblMandatory = new System.Windows.Forms.Label();
            this.chkMandatory = new System.Windows.Forms.CheckBox();
            this.pnlSep1_5 = new System.Windows.Forms.Panel();
            this.lblHint = new System.Windows.Forms.Label();
            this.txtHint = new System.Windows.Forms.TextBox();

            this.lblCheck = new System.Windows.Forms.Label();
            this.pnlSep2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.flpAnswers = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddAns = new System.Windows.Forms.Button();

            this.lblValidation = new System.Windows.Forms.Label();
            this.pnlSep3 = new System.Windows.Forms.Panel();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbTextType = new System.Windows.Forms.ComboBox();
            this.lblMaxChars = new System.Windows.Forms.Label();
            this.txtMaxChars = new System.Windows.Forms.TextBox();

            this.lblEval = new System.Windows.Forms.Label();
            this.pnlSep4 = new System.Windows.Forms.Panel();
            this.lblrate = new System.Windows.Forms.Label();
            this.txtPoints = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();

            this.panel1.SuspendLayout();
            this.SuspendLayout();

            // panel1
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.panel1.Controls.Add(this.lblBaseSettings);
            this.panel1.Controls.Add(this.pnlSep1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtQuestionText);
            this.panel1.Controls.Add(this.lblMandatory);
            this.panel1.Controls.Add(this.chkMandatory);
            this.panel1.Controls.Add(this.pnlSep1_5);
            this.panel1.Controls.Add(this.lblHint);
            this.panel1.Controls.Add(this.txtHint);
            this.panel1.Controls.Add(this.lblCheck);
            this.panel1.Controls.Add(this.pnlSep2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.flpAnswers);
            this.panel1.Controls.Add(this.btnAddAns);
            this.panel1.Controls.Add(this.lblValidation);
            this.panel1.Controls.Add(this.pnlSep3);
            this.panel1.Controls.Add(this.lblType);
            this.panel1.Controls.Add(this.cmbTextType);
            this.panel1.Controls.Add(this.lblMaxChars);
            this.panel1.Controls.Add(this.txtMaxChars);
            this.panel1.Controls.Add(this.lblEval);
            this.panel1.Controls.Add(this.pnlSep4);
            this.panel1.Controls.Add(this.lblrate);
            this.panel1.Controls.Add(this.txtPoints);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Name = "panel1";

            // --- ALAPADATOK ---
            this.lblBaseSettings.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblBaseSettings.Location = new System.Drawing.Point(20, 20);
            this.lblBaseSettings.Size = new System.Drawing.Size(200, 30);
            this.lblBaseSettings.Text = "Alapadatok:";

            this.pnlSep1.BackColor = System.Drawing.Color.DarkGray;
            this.pnlSep1.Location = new System.Drawing.Point(20, 55);
            this.pnlSep1.Size = new System.Drawing.Size(450, 2);

            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(40, 75);
            this.label1.Size = new System.Drawing.Size(200, 25);
            this.label1.Text = "Kérdés szövege:";

            this.txtQuestionText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtQuestionText.Location = new System.Drawing.Point(45, 105);
            this.txtQuestionText.Multiline = true;
            this.txtQuestionText.Size = new System.Drawing.Size(410, 50);

            this.lblMandatory.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblMandatory.Location = new System.Drawing.Point(40, 175);
            this.lblMandatory.Size = new System.Drawing.Size(200, 25);
            this.lblMandatory.Text = "Kötelező kérdés";

            this.chkMandatory.Location = new System.Drawing.Point(380, 180);
            this.chkMandatory.Size = new System.Drawing.Size(15, 14);

            this.pnlSep1_5.BackColor = System.Drawing.Color.LightGray;
            this.pnlSep1_5.Location = new System.Drawing.Point(45, 215);
            this.pnlSep1_5.Size = new System.Drawing.Size(410, 1);

            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHint.Location = new System.Drawing.Point(40, 230);
            this.lblHint.Size = new System.Drawing.Size(200, 25);
            this.lblHint.Text = "Súgó szöveg:";

            this.txtHint.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtHint.Location = new System.Drawing.Point(45, 260);
            this.txtHint.Size = new System.Drawing.Size(410, 35);

            // --- ELLENŐRZÉS ---
            this.lblCheck.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblCheck.Location = new System.Drawing.Point(20, 320);
            this.lblCheck.Size = new System.Drawing.Size(200, 30);
            this.lblCheck.Text = "Ellenőrzés";

            this.pnlSep2.BackColor = System.Drawing.Color.DarkGray;
            this.pnlSep2.Location = new System.Drawing.Point(20, 355);
            this.pnlSep2.Size = new System.Drawing.Size(450, 2);

            this.label2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(40, 375);
            this.label2.Size = new System.Drawing.Size(200, 25);
            this.label2.Text = "Helyes válasz:";

            this.flpAnswers.AutoScroll = true;
            this.flpAnswers.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpAnswers.Location = new System.Drawing.Point(45, 405);
            this.flpAnswers.Size = new System.Drawing.Size(410, 120);
            this.flpAnswers.WrapContents = false;

            this.btnAddAns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(80)))));
            this.btnAddAns.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAns.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddAns.ForeColor = System.Drawing.Color.White;
            this.btnAddAns.Location = new System.Drawing.Point(100, 535);
            this.btnAddAns.Size = new System.Drawing.Size(300, 40);
            this.btnAddAns.Text = "+ Másik válasz";
            this.btnAddAns.Click += new System.EventHandler(this.btnAddAns_Click_1);

            // --- VALIDÁCIÓ ---
            this.lblValidation.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblValidation.Location = new System.Drawing.Point(20, 600);
            this.lblValidation.Size = new System.Drawing.Size(200, 30);
            this.lblValidation.Text = "Validáció:";

            this.pnlSep3.BackColor = System.Drawing.Color.DarkGray;
            this.pnlSep3.Location = new System.Drawing.Point(20, 635);
            this.pnlSep3.Size = new System.Drawing.Size(450, 2);

            this.lblType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblType.Location = new System.Drawing.Point(40, 655);
            this.lblType.Size = new System.Drawing.Size(150, 25);
            this.lblType.Text = "Szöveg típusa:";

            this.cmbTextType.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbTextType.Items.AddRange(new object[] { "szöveg", "szám", "email" });
            this.cmbTextType.Location = new System.Drawing.Point(200, 650);
            this.cmbTextType.Size = new System.Drawing.Size(150, 29);
            this.cmbTextType.Text = "szöveg";

            this.lblMaxChars.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMaxChars.Location = new System.Drawing.Point(40, 695);
            this.lblMaxChars.Size = new System.Drawing.Size(150, 25);
            this.lblMaxChars.Text = "Max. karakter:";

            this.txtMaxChars.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtMaxChars.Location = new System.Drawing.Point(200, 690);
            this.txtMaxChars.Size = new System.Drawing.Size(100, 29);

            // --- ÉRTÉKELÉS ---
            this.lblEval.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblEval.Location = new System.Drawing.Point(20, 740);
            this.lblEval.Size = new System.Drawing.Size(200, 30);
            this.lblEval.Text = "Értékelés";

            this.pnlSep4.BackColor = System.Drawing.Color.DarkGray;
            this.pnlSep4.Location = new System.Drawing.Point(20, 775);
            this.pnlSep4.Size = new System.Drawing.Size(450, 2);

            this.lblrate.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblrate.Location = new System.Drawing.Point(40, 795);
            this.lblrate.Size = new System.Drawing.Size(150, 25);
            this.lblrate.Text = "Pontszám:";

            this.txtPoints.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtPoints.Location = new System.Drawing.Point(200, 790);
            this.txtPoints.Size = new System.Drawing.Size(100, 32);

            this.btnDelete.BackColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(80)))));
            this.btnDelete.Location = new System.Drawing.Point(45, 850);
            this.btnDelete.Size = new System.Drawing.Size(150, 45);
            this.btnDelete.Text = "Törlés";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // UC_Shortans_settings
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UC_Shortans_settings";
            this.Size = new System.Drawing.Size(500, 950);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblBaseSettings;
        private System.Windows.Forms.Panel pnlSep1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQuestionText;
        private System.Windows.Forms.Label lblMandatory;
        private System.Windows.Forms.CheckBox chkMandatory;
        private System.Windows.Forms.Panel pnlSep1_5;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.TextBox txtHint;
        private System.Windows.Forms.Label lblCheck;
        private System.Windows.Forms.Panel pnlSep2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flpAnswers;
        private System.Windows.Forms.Button btnAddAns;
        private System.Windows.Forms.Label lblValidation;
        private System.Windows.Forms.Panel pnlSep3;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbTextType;
        private System.Windows.Forms.Label lblMaxChars;
        private System.Windows.Forms.TextBox txtMaxChars;
        private System.Windows.Forms.Label lblEval;
        private System.Windows.Forms.Panel pnlSep4;
        private System.Windows.Forms.Label lblrate;
        private System.Windows.Forms.TextBox txtPoints;
        private System.Windows.Forms.Button btnDelete;
    }
}