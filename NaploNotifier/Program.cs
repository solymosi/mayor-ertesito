using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NaploNotifier
{
    static class Program
    {
        static public Context Context;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Guardian.LaunchApplication();
        }
    }

    static class Guardian
    {
        public static void LaunchApplication()
        {
            try
            {
                Program.Context = new Context();
                Application.Run(Program.Context);
            }
            catch (Exception Exception)
            {
                Crashed(Exception);
            }
        }

        public static void Crashed(Exception Exception)
        {
            MessageBox.Show("Hoppá! A MaYoR értesítő lefagyott. Kérlek, CTRL+C-vel másold ki ezt a hibaüzenetet és küldd el nekem a tartalmát a http://solymosi.eu oldal alján található email címre. Köszönöm!\r\nA hiba részletei:\r\n" + Exception.Message + "\r\n" + Exception.StackTrace, Application.ProductName, 0, MessageBoxIcon.Error);
            Environment.Exit(0);
        }
    }
}
