using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace NaploNotifier
{
    public partial class Context
    {
        NotifyIcon Icon;

        ContextMenuStrip Menu;

        ToolStripMenuItem mOpenNaplo;
        ToolStripMenuItem mRecentChanges;
        ToolStripMenuItem mCheckNow;
        ToolStripMenuItem mAutoCheck;
        ToolStripMenuItem mSettings;
        ToolStripMenuItem mExit;

        public void InstallMenu()
        {
            Icon = new NotifyIcon();
            Menu = new ContextMenuStrip();

            mOpenNaplo = new ToolStripMenuItem("Napló megnyitása", null, new EventHandler(OpenNaplo_Click));
            mOpenNaplo.Font = new Font(mOpenNaplo.Font, FontStyle.Bold);
            mRecentChanges = new ToolStripMenuItem("Legutóbbi változások", null, new EventHandler(RecentChanges_Click));
            mCheckNow = new ToolStripMenuItem("Ellenőrzés most", null, new EventHandler(CheckNow_Click));
            mAutoCheck = new ToolStripMenuItem("Automatikus ellenőrzés", null, new EventHandler(AutoCheck_Click));
            mAutoCheck.Checked = Mayor.Settings.AutoUpdate;
            mSettings = new ToolStripMenuItem("Beállítások...", null, new EventHandler(Settings_Click));
            mExit = new ToolStripMenuItem("Kilépés                                ", null, new EventHandler(Exit_Click));

            Menu.RenderMode = ToolStripRenderMode.System;

            Menu.Items.Add(mOpenNaplo);
            Menu.Items.Add(new ToolStripSeparator());
            Menu.Items.Add(mRecentChanges);
            Menu.Items.Add(mCheckNow);
            Menu.Items.Add(mAutoCheck);
            Menu.Items.Add(new ToolStripSeparator());
            Menu.Items.Add(mExit);

            Icon.Icon = Properties.Resources.MayorIcon;
            Icon.DoubleClick += new EventHandler(OpenNaplo_Click);
            Icon.ContextMenuStrip = Menu;
            SetIconStatus("");

            this.CheckNow = new ToolStripMenuItem("", null, new EventHandler(delegate { ThreadPool.QueueUserWorkItem(new WaitCallback(delegate { Mayor.UpdateOsztalyozo(); })); }));
            this.LastTen = new ToolStripMenuItem("", null, ShowRecentChanges);
            this.ThreadExit += new EventHandler(Context_ThreadExit);
            ToolStripMenuItem RegularCheckEnabled = new ToolStripMenuItem("Automatikus ellenőrzés");
            RegularCheckEnabled.Click += new EventHandler(delegate
            {
                Mayor.Settings.AutoUpdate = !Mayor.Settings.AutoUpdate;
                RegularCheckEnabled.Checked = Mayor.Settings.AutoUpdate;
                Mayor.SaveSettings();
            });

            Icon.ContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(ContextMenuStrip_Opening);

            Icon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Beállítások...", null, new EventHandler(delegate
            {
                try
                {
                    this.EditSettings();
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                    {
                        CheckTimer_Tick(null, null);
                    }));
                }
                catch { }
            })));
            Icon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Kilépés                                ", null, new EventHandler(delegate
            {
                this.ExitThread();
            })));
            Icon.Visible = true;
        }

        void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mCheckNow.Text = (Updating ? "Ellenőrzés folyamatban..." : "Ellenőrzés most");
            mCheckNow.Enabled = !Updating;

            mRecentChanges.Text = (Mayor.Changes.Count == 0 ? "Nincs változás" : "Legutóbbi változások");
            mRecentChanges.Enabled = Mayor.Changes.Count > 0;
        }

        public void RemoveMenu()
        {
            Icon.Visible = false;
        }

        public void SetIconStatus(string Status)
        {
            Icon.Text = "MaYoR értesítő" + (Status == "" ? "" : " - " + Status);
        }
    }
}
