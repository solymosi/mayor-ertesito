using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaploNotifier
{
    [Serializable()]
    public class Settings
    {
        public string Domain = "";
        public string User = "";
        public string EncryptedPassword = "";
        public string BaseAddress { get { return "https://" + this.Domain + "/index.php"; } }
        public string Password
        {
            get { return Encoding.UTF8.GetString(Convert.FromBase64String(EncryptedPassword)); }
            set { EncryptedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(value)); }
        }
        public bool CheckAutomatically = true;
    }
}
