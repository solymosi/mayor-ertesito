using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using Microsoft.Win32;

namespace NaploNotifier
{
    public partial class Context : ApplicationContext
    {
        System.Timers.Timer CheckTimer = new System.Timers.Timer(120000);

        WindowsFormsSynchronizationContext SC;

        public Context()
        {
            SC = new WindowsFormsSynchronizationContext();

            try { Mayor.LoadSettings(); }
            catch
            {
                try { this.EditSettings(); }
                catch { Environment.Exit(0); }
            }


            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                try { Mayor.LoadChanges(); }
                catch { Mayor.UpdateOsztalyozo(true); }
                try { Mayor.LoadOsztalyozo(); }
                catch { Mayor.UpdateOsztalyozo(true); }
                CheckTimer_Tick(null, null);
            }));

            Mayor.OsztalyozoUpdated += new Mayor.OsztalyozoUpdateDelegate(Mayor_OsztalyozoUpdated);
            CheckTimer.Elapsed += new System.Timers.ElapsedEventHandler(CheckTimer_Tick);
            CheckTimer.Start();
        }

        void EditSettings()
        {
            SettingsForm SF = new SettingsForm();
            SF.Domain.Text = Mayor.Settings.Domain;
            SF.User.Text = Mayor.Settings.User;

            if (SF.ShowDialog() == DialogResult.OK)
            {
                Mayor.Settings.Domain = SF.Domain.Text;
                Mayor.Settings.User = SF.User.Text;
                Mayor.Settings.Password = SF.Pass.Text;
                Mayor.SaveSettings();
            }
            else { throw new OperationCanceledException(); }
        }

        void CheckTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Mayor.Settings.CheckAutomatically)
            {
                Mayor.UpdateOsztalyozo();
            }
        }

        void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CheckNow.Text = (Mayor.OsztalyozoUpdating ? "Ellenőrzés folyamatban..." : "Ellenőrzés most");
            CheckNow.Enabled = !Mayor.OsztalyozoUpdating;

            LastTen.Text = (Mayor.Changes.Count == 0 ? "Nincs változás" : "Legutóbbi változások");
            LastTen.Enabled = (Mayor.Changes.Count > 0);
        }

        void Context_ThreadExit(object sender, EventArgs e)
        {
            Icon.Visible = false;
        }

        void OpenWebsite(object sender, EventArgs e)
        {
            Process.Start(Mayor.Session.CreateURL("naplo", "osztalyozo", "diak", "", "private", "classic"));
        }

        void ShowRecentChanges(object sender, EventArgs e)
        {
            Mayor.SortChanges();
            List<NoteChange> Recent = Mayor.Changes.GetRange(0, Math.Min(5, Mayor.Changes.Count));
            ShowNotifier(Recent);
        }

        public NotifierForm NF;

        void Mayor_OsztalyozoUpdated(List<NoteChange> Changes)
        {
            if (Changes.Count > 0) { PlayNotificationSound(); }
            ShowNotifier(Changes);
            Mayor.SaveChanges();
        }

        void ShowNotifier(List<NoteChange> Changes)
        {
            SC.Send(new SendOrPostCallback(delegate
            {
                if (Changes.Count == 0) { return; }
                if (NF != null && NF.Visible)
                {
                    NF.Close();
                }
                NF = new NotifierForm();
                NF.Changes = Changes;
                NF.Show();
            }), new object());
        }

        void PlayNotificationSound()
        {
            try
            {
                string Path = (string)Registry.GetValue(@"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\MailBeep\.Default", null, "");
                new System.Media.SoundPlayer(Path).Play();
            }
            catch { }
        }
    }
}