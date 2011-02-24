using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NaploNotifier
{
    public partial class Context
    {
        void LoadSettings()
        {
            try
            {
                Mayor.LoadSettings();
                if (!Mayor.Settings.Present) { throw new Exception(); }
            }
            catch
            {
                try { EditSettings(); }
                catch { Program.Exit(); }
            }
        }

        void SaveSettings()
        {
            try { Mayor.SaveSettings(); }
            catch (Exception e)
            {
                Tools.ErrorMessage("Nem sikerült elmenteni a beállításokat:\r\n" + e.Message);
                Program.Exit();
            }
        }

        void EditSettings()
        {
            SettingsForm Form = new SettingsForm();
            Form.Domain.Text = Mayor.Settings.ServerDomain;
            Form.User.Text = Mayor.Settings.User;
            Form.AutoStart.Checked = Mayor.Settings.Present ? Mayor.Settings.AutoStart : true;

            if (Form.ShowDialog() == DialogResult.OK)
            {
                Mayor.Settings.ServerDomain = Form.Domain.Text;
                Mayor.Settings.User = Form.User.Text;
                if (Mayor.Settings.Password == "" || Form.Password.Text != "")
                {
                    Mayor.Settings.Password = Form.Password.Text;
                }
                Mayor.Settings.AutoStart = Form.AutoStart.Checked;
                SaveSettings();
            }
            else { throw new OperationCanceledException(); }
        }
    }
}
