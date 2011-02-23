using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Media;

namespace NaploNotifier
{
    public partial class Context
    {
        public static void ErrorMessage(string Message)
        {
            MessageBox.Show(Message, Application.ProductName, 0, MessageBoxIcon.Error);
        }

        public static void PlayNotificationSound()
        {
            try
            {
                string Path = (string)Registry.GetValue(@"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\MailBeep\.Default", null, "");
                new SoundPlayer(Path).Play();
            }
            catch { }
        }
    }
}
