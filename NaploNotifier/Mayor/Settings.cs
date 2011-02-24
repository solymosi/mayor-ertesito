using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace NaploNotifier
{
    [Serializable()]
    public class Settings
    {
        public string ServerDomain = "";
        public string User = "";
        public string EncryptedPassword = "";
        public string ServerAddress { get { return "https://" + this.ServerDomain + "/index.php"; } }
        public string Password
        {
            get { return Encoding.UTF8.GetString(Convert.FromBase64String(EncryptedPassword)); }
            set { EncryptedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(value)); }
        }
        public bool AutoUpdate = true;
        public bool AutoStart
        {
            get { return (Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "Mayor", null) != null); }
            set
            {
                if (value) { Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "Mayor", Application.ExecutablePath); }
                else
                {
                    try { Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true).DeleteValue("Mayor"); }
                    catch { }
                }
            }
        }
    }
}
