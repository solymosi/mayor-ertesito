namespace NaploNotifier
{
    partial class NotificationForm
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
            this.components = new System.ComponentModel.Container();
            this.Title = new System.Windows.Forms.Label();
            this.Date = new System.Windows.Forms.Label();
            this.OldNote = new System.Windows.Forms.Label();
            this.OldName = new System.Windows.Forms.Label();
            this.NewName = new System.Windows.Forms.Label();
            this.NewNote = new System.Windows.Forms.Label();
            this.DisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Title.Location = new System.Drawing.Point(12, 9);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(249, 26);
            this.Title.TabIndex = 0;
            this.Title.Text = "Title";
            // 
            // Date
            // 
            this.Date.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Date.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Date.Location = new System.Drawing.Point(267, 9);
            this.Date.Name = "Date";
            this.Date.Size = new System.Drawing.Size(79, 13);
            this.Date.TabIndex = 1;
            this.Date.Text = "Date";
            this.Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OldNote
            // 
            this.OldNote.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.OldNote.Location = new System.Drawing.Point(12, 40);
            this.OldNote.Name = "OldNote";
            this.OldNote.Size = new System.Drawing.Size(65, 33);
            this.OldNote.TabIndex = 2;
            this.OldNote.Text = "?";
            this.OldNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OldName
            // 
            this.OldName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OldName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.OldName.Location = new System.Drawing.Point(83, 40);
            this.OldName.Name = "OldName";
            this.OldName.Size = new System.Drawing.Size(263, 33);
            this.OldName.TabIndex = 3;
            this.OldName.Text = "Old note";
            this.OldName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NewName
            // 
            this.NewName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.NewName.Location = new System.Drawing.Point(83, 73);
            this.NewName.Name = "NewName";
            this.NewName.Size = new System.Drawing.Size(263, 33);
            this.NewName.TabIndex = 5;
            this.NewName.Text = "New note";
            this.NewName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NewNote
            // 
            this.NewNote.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.NewNote.Location = new System.Drawing.Point(12, 73);
            this.NewNote.Name = "NewNote";
            this.NewNote.Size = new System.Drawing.Size(65, 33);
            this.NewNote.TabIndex = 4;
            this.NewNote.Text = "?";
            this.NewNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DisplayTimer
            // 
            this.DisplayTimer.Enabled = true;
            this.DisplayTimer.Interval = 5000;
            this.DisplayTimer.Tick += new System.EventHandler(this.DisplayTimer_Tick);
            // 
            // NotifierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(358, 115);
            this.ControlBox = false;
            this.Controls.Add(this.NewName);
            this.Controls.Add(this.NewNote);
            this.Controls.Add(this.OldName);
            this.Controls.Add(this.OldNote);
            this.Controls.Add(this.Date);
            this.Controls.Add(this.Title);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NotifierForm";
            this.Opacity = 0.8;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MaYoR értesítő";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.NotifierForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NotifierForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label Date;
        private System.Windows.Forms.Label OldNote;
        private System.Windows.Forms.Label OldName;
        private System.Windows.Forms.Label NewName;
        private System.Windows.Forms.Label NewNote;
        private System.Windows.Forms.Timer DisplayTimer;
    }
}