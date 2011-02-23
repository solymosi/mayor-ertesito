using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NaploNotifier
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            List<string> Errors = new List<string>();
            if (Domain.Text.Trim() == "") { Errors.Add("Nem adtad meg a napló webes címét."); }
            if (User.Text.Trim() == "") { Errors.Add("Nem adtad meg a felhasználónevedet."); }
            if (Password.Text.Trim() == "") { Errors.Add("Nem adtad meg a jelszavadat."); }
            if (Errors.Count > 0)
            {
                MessageBox.Show("Az alábbi hibák léptek fel a beállítások mentése során:\r\n\r\n" + string.Join("\r\n", Errors.ToArray()), Application.ProductName, 0, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
