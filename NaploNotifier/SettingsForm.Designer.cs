namespace NaploNotifier
{
    partial class SettingsForm
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
            this.TitleLabel = new System.Windows.Forms.Label();
            this.Domain = new System.Windows.Forms.TextBox();
            this.DomainLabel = new System.Windows.Forms.Label();
            this.DomainFixedPart1 = new System.Windows.Forms.Label();
            this.DomainFixedPart2 = new System.Windows.Forms.Label();
            this.UserLabel = new System.Windows.Forms.Label();
            this.User = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.Save = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.AutoStart = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TitleLabel.Location = new System.Drawing.Point(12, 12);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(316, 13);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "A jegyeid lekéréséhez szükség van az alábbi adatokra:";
            // 
            // Domain
            // 
            this.Domain.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Domain.Location = new System.Drawing.Point(81, 62);
            this.Domain.Name = "Domain";
            this.Domain.Size = new System.Drawing.Size(223, 20);
            this.Domain.TabIndex = 1;
            // 
            // DomainLabel
            // 
            this.DomainLabel.AutoSize = true;
            this.DomainLabel.Location = new System.Drawing.Point(12, 41);
            this.DomainLabel.Name = "DomainLabel";
            this.DomainLabel.Size = new System.Drawing.Size(105, 13);
            this.DomainLabel.TabIndex = 2;
            this.DomainLabel.Text = "A napló webes címe:";
            // 
            // DomainFixedPart1
            // 
            this.DomainFixedPart1.AutoSize = true;
            this.DomainFixedPart1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DomainFixedPart1.Location = new System.Drawing.Point(12, 65);
            this.DomainFixedPart1.Name = "DomainFixedPart1";
            this.DomainFixedPart1.Size = new System.Drawing.Size(63, 14);
            this.DomainFixedPart1.TabIndex = 3;
            this.DomainFixedPart1.Text = "https://";
            // 
            // DomainFixedPart2
            // 
            this.DomainFixedPart2.AutoSize = true;
            this.DomainFixedPart2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DomainFixedPart2.Location = new System.Drawing.Point(310, 65);
            this.DomainFixedPart2.Name = "DomainFixedPart2";
            this.DomainFixedPart2.Size = new System.Drawing.Size(77, 14);
            this.DomainFixedPart2.TabIndex = 4;
            this.DomainFixedPart2.Text = "/index.php";
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Location = new System.Drawing.Point(53, 104);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(85, 13);
            this.UserLabel.TabIndex = 6;
            this.UserLabel.Text = "Felhasználónév:";
            // 
            // User
            // 
            this.User.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.User.Location = new System.Drawing.Point(144, 101);
            this.User.Name = "User";
            this.User.Size = new System.Drawing.Size(201, 20);
            this.User.TabIndex = 5;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(98, 135);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(40, 13);
            this.PasswordLabel.TabIndex = 7;
            this.PasswordLabel.Text = "Jelszó:";
            // 
            // Password
            // 
            this.Password.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Password.Location = new System.Drawing.Point(144, 132);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(201, 20);
            this.Password.TabIndex = 8;
            this.Password.UseSystemPasswordChar = true;
            // 
            // Save
            // 
            this.Save.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Save.Location = new System.Drawing.Point(104, 199);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(92, 25);
            this.Save.TabIndex = 9;
            this.Save.Text = "Mentés";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Cancel.Location = new System.Drawing.Point(202, 199);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(92, 25);
            this.Cancel.TabIndex = 10;
            this.Cancel.Text = "Mégse";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // AutoStart
            // 
            this.AutoStart.AutoSize = true;
            this.AutoStart.Location = new System.Drawing.Point(15, 164);
            this.AutoStart.Name = "AutoStart";
            this.AutoStart.Size = new System.Drawing.Size(229, 17);
            this.AutoStart.TabIndex = 11;
            this.AutoStart.Text = "Automatikus indítás, amikor bejelentkezem";
            this.AutoStart.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(399, 236);
            this.Controls.Add(this.AutoStart);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.User);
            this.Controls.Add(this.DomainFixedPart2);
            this.Controls.Add(this.DomainFixedPart1);
            this.Controls.Add(this.DomainLabel);
            this.Controls.Add(this.Domain);
            this.Controls.Add(this.TitleLabel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MaYoR értesítő beállítások";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label DomainLabel;
        private System.Windows.Forms.Label DomainFixedPart1;
        private System.Windows.Forms.Label DomainFixedPart2;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Cancel;
        public System.Windows.Forms.TextBox Domain;
        public System.Windows.Forms.TextBox User;
        public System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.CheckBox AutoStart;
    }
}