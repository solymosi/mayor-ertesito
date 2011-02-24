using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace NaploNotifier
{
    public partial class Context : ApplicationContext
    {
        public const int UpdateFrequency = 120;

        System.Timers.Timer UpdateTimer;
        WindowsFormsSynchronizationContext MainSynchronizationContext;

        public Context()
        {
            MainSynchronizationContext = new WindowsFormsSynchronizationContext();
            ThreadExit += new EventHandler(Context_ThreadExit);

            LoadSettings();
            LoadData();

            Mayor.Updated += new Mayor.UpdateDelegate(UpdateCallback);

            InstallMenu();
            RunUpdate();

            UpdateTimer = new System.Timers.Timer(UpdateFrequency * 1000);
            UpdateTimer.Elapsed += new System.Timers.ElapsedEventHandler(CheckTimer_Tick);
            UpdateTimer.Start();
        }

        void OpenNaplo_Click(object sender, EventArgs e)
        {
            Process.Start(Mayor.Session.CreateURL("naplo", "osztalyozo", "diak", "", "private", "classic", false));
        }

        void RecentChanges_Click(object sender, EventArgs e)
        {
            ShowNotification(Mayor.RecentChanges);
        }

        void CheckNow_Click(object sender, EventArgs e)
        {
            RunUpdate();
        }

        void AutoCheck_Click(object sender, EventArgs e)
        {
            Mayor.Settings.AutoUpdate = !Mayor.Settings.AutoUpdate;
            mAutoCheck.Checked = Mayor.Settings.AutoUpdate;
            SaveSettings();
        }

        void Settings_Click(object sender, EventArgs e)
        {
            try
            {
                EditSettings();
                RunUpdate();
            }
            catch { }
        }

        void Exit_Click(object sender, EventArgs e)
        {
            ExitThread();
        }

        void CheckTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Mayor.Settings.AutoUpdate)
            {
                RunUpdate();
            }
        }

        void Context_ThreadExit(object sender, EventArgs e)
        {
            RemoveMenu();
        }

        void UpdateCallback(List<Change> Changes)
        {
            if (Changes.Count > 0) { Tools.PlayNotificationSound(); }
            ShowNotification(Changes);
            Mayor.SaveChanges();
        }

    }
}