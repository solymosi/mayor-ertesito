using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NaploNotifier
{
    public partial class Context
    {
        void LoadSettings()
        {
            try { Mayor.LoadSettings(); }
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
                ErrorMessage("Nem sikerült elmenteni a beállításokat:\r\n" + e.Message);
                Program.Exit();
            }
        }

        void EditSettings()
        {
            SettingsForm Form = new SettingsForm();
            Form.Domain.Text = Mayor.Settings.Domain;
            Form.User.Text = Mayor.Settings.User;

            if (Form.ShowDialog() == DialogResult.OK)
            {
                Mayor.Settings.Domain = Form.Domain.Text;
                Mayor.Settings.User = Form.User.Text;
                Mayor.Settings.Password = Form.Password.Text;
                SaveSettings();
            }
            else { throw new OperationCanceledException(); }
        }
    }
}
