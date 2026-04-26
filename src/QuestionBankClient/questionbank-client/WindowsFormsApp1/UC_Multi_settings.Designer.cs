namespace QuestionBankClient
{
    partial class UC_Multi_settings
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuestionText = new System.Windows.Forms.TextBox();
            this.lblMandatory = new System.Windows.Forms.Label();
            this.chkMandatory = new System.Windows.Forms.CheckBox();
            this.pnlSeparator = new System.Windows.Forms.Panel();
            this.lblOptions = new System.Windows.Forms.Label();
            this.flpOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddOption = new System.Windows.Forms.Button();
            this.lblHint = new System.Windows.Forms.Label();
            this.txtHint = new System.Windows.Forms.TextBox();
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
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtQuestionText);
            this.panel1.Controls.Add(this.lblMandatory);
            this.panel1.Controls.Add(this.chkMandatory);
            this.panel1.Controls.Add(this.pnlSeparator);
            this.panel1.Controls.Add(this.lblOptions);
            this.panel1.Controls.Add(this.flpOptions);
            this.panel1.Controls.Add(this.btnAddOption);
            this.panel1.Controls.Add(this.lblHint);
            this.panel1.Controls.Add(this.txtHint);
            this.panel1.Controls.Add(this.lblrate);
            this.panel1.Controls.Add(this.txtPoints);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Name = "panel1";

            // 
            // label1 (Kérdés szövege)
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(40, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 25);
            this.label1.Text = "Kérdés szövege:";

            // 
            // txtQuestionText
            // 
            this.txtQuestionText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtQuestionText.Location = new System.Drawing.Point(45, 55);
            this.txtQuestionText.Multiline = true;
            this.txtQuestionText.Name = "txtQuestionText";
            this.txtQuestionText.Size = new System.Drawing.Size(410, 50);

            // 
            // lblMandatory
            // 
            this.lblMandatory.AutoSize = true;
            this.lblMandatory.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblMandatory.Location = new System.Drawing.Point(40, 130);
            this.lblMandatory.Name = "lblMandatory";
            this.lblMandatory.Size = new System.Drawing.Size(160, 25);
            this.lblMandatory.Text = "Kötelező kérdés";

            // 
            // chkMandatory
            // 
            this.chkMandatory.AutoSize = true;
            this.chkMandatory.Location = new System.Drawing.Point(380, 137);
            this.chkMandatory.Name = "chkMandatory";
            this.chkMandatory.Size = new System.Drawing.Size(15, 14);

            // 
            // pnlSeparator
            // 
            this.pnlSeparator.BackColor = System.Drawing.Color.LightGray;
            this.pnlSeparator.Location = new System.Drawing.Point(45, 175);
            this.pnlSeparator.Name = "pnlSeparator";
            this.pnlSeparator.Size = new System.Drawing.Size(410, 1);

            // 
            // lblOptions
            // 
            this.lblOptions.AutoSize = true;
            this.lblOptions.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblOptions.Location = new System.Drawing.Point(40, 195);
            this.lblOptions.Name = "lblOptions";
            this.lblOptions.Size = new System.Drawing.Size(320, 25);
            this.lblOptions.Text = "Opciók- megfelelőek kiválasztása";

            // 
            // flpOptions
            // 
            this.flpOptions.AutoScroll = true;
            this.flpOptions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpOptions.Location = new System.Drawing.Point(45, 230);
            this.flpOptions.Name = "flpOptions";
            this.flpOptions.Size = new System.Drawing.Size(410, 150);
            this.flpOptions.WrapContents = false;

            // 
            // btnAddOption
            // 
            this.btnAddOption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(80)))));
            this.btnAddOption.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddOption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddOption.ForeColor = System.Drawing.Color.White;
            this.btnAddOption.Location = new System.Drawing.Point(100, 395);
            this.btnAddOption.Name = "btnAddOption";
            this.btnAddOption.Size = new System.Drawing.Size(300, 40);
            this.btnAddOption.Text = "+ Opció hozzáadása";
            this.btnAddOption.UseVisualStyleBackColor = false;
            this.btnAddOption.Click += new System.EventHandler(this.btnAddOption_Click);

            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHint.Location = new System.Drawing.Point(40, 460);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(126, 25);
            this.lblHint.Text = "Súgó szöveg:";

            // 
            // txtHint
            // 
            this.txtHint.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtHint.Location = new System.Drawing.Point(45, 490);
            this.txtHint.Name = "txtHint";
            this.txtHint.Size = new System.Drawing.Size(410, 29);

            // 
            // lblrate
            // 
            this.lblrate.AutoSize = true;
            this.lblrate.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblrate.Location = new System.Drawing.Point(40, 545);
            this.lblrate.Name = "lblrate";
            this.lblrate.Size = new System.Drawing.Size(104, 25);
            this.lblrate.Text = "Pontszám:";

            // 
            // txtPoints
            // 
            this.txtPoints.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtPoints.Location = new System.Drawing.Point(170, 542);
            this.txtPoints.Name = "txtPoints";
            this.txtPoints.Size = new System.Drawing.Size(80, 32);

            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(80)))));
            this.btnDelete.Location = new System.Drawing.Point(45, 610);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(150, 45);
            this.btnDelete.Text = "Törlés";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // 
            // UC_Multi_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UC_Multi_settings";
            this.Size = new System.Drawing.Size(500, 800);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQuestionText;
        private System.Windows.Forms.Label lblMandatory;
        private System.Windows.Forms.CheckBox chkMandatory;
        private System.Windows.Forms.Panel pnlSeparator;
        private System.Windows.Forms.Label lblOptions;
        private System.Windows.Forms.FlowLayoutPanel flpOptions;
        private System.Windows.Forms.Button btnAddOption;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.TextBox txtHint;
        private System.Windows.Forms.Label lblrate;
        private System.Windows.Forms.TextBox txtPoints;
        private System.Windows.Forms.Button btnDelete;
    }
}