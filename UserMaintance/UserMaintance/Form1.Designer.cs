namespace UserMaintance
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listBox1 = new ListBox();
            lblLastName = new Label();
            lblFirstName = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            btn = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(41, 112);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(518, 820);
            listBox1.TabIndex = 0;
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(770, 136);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(115, 48);
            lblLastName.TabIndex = 1;
            lblLastName.Text = "label1";
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.Location = new Point(770, 309);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(115, 48);
            lblFirstName.TabIndex = 2;
            lblFirstName.Text = "label2";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(981, 136);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(431, 55);
            textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(981, 309);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(431, 55);
            textBox2.TabIndex = 4;
            // 
            // btn
            // 
            btn.Location = new Point(770, 438);
            btn.Name = "btn";
            btn.Size = new Size(642, 69);
            btn.TabIndex = 5;
            btn.Text = "button1";
            btn.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(20F, 48F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1455, 1040);
            Controls.Add(btn);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(lblFirstName);
            Controls.Add(lblLastName);
            Controls.Add(listBox1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private Label lblLastName;
        private Label lblFirstName;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button btn;
    }
}
