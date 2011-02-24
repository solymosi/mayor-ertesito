using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Media;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NaploNotifier
{
    static class Tools
    {
        public static string SettingsDirectory
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MayorNotify"; }
        }

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

        public static void Serialize<T>(T Object, string FileName)
        {
            CreateSettingsDirectory();

            using (Stream Stream = File.Open(SettingsDirectory + Path.DirectorySeparatorChar.ToString() + FileName, FileMode.Create))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                Formatter.Serialize(Stream, Object);
            }
        }

        public static T Deserialize<T>(string FileName)
        {
            CreateSettingsDirectory();

            using (Stream Stream = File.Open(SettingsDirectory + Path.DirectorySeparatorChar.ToString() + FileName, FileMode.Open))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                return (T)Formatter.Deserialize(Stream);
            }
        }

        public static void CreateSettingsDirectory()
        {
            if (!Directory.Exists(SettingsDirectory))
            {
                Directory.CreateDirectory(SettingsDirectory);
            }
        }
    }
}
