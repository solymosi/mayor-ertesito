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

        ToolStripMenuItem OpenNaplo;
        ToolStripMenuItem RecentChanges;
        ToolStripMenuItem CheckNow;
        ToolStripMenuItem AutoCheck;
        ToolStripMenuItem Settings;
        ToolStripMenuItem Exit;

        public void InstallMenu()
        {
            Icon = new NotifyIcon();
            Menu = new ContextMenuStrip();

            OpenNaplo = new ToolStripMenuItem("Napló megnyitása", null, new EventHandler(OpenNaplo_Click));
            OpenNaplo.Font = new Font(OpenNaplo.Font, FontStyle.Bold);
            RecentChanges = new ToolStripMenuItem("Legutóbbi változások", null, new EventHandler(RecentChanges_Click));
            CheckNow = new ToolStripMenuItem("Ellenőrzés most", null, new EventHandler(CheckNow_Click));
            AutoCheck = new ToolStripMenuItem("Automatikus ellenőrzés", null, new EventHandler(AutoCheck_Click));
            AutoCheck.Checked = Mayor.Settings.CheckAutomatically;
            Settings = new ToolStripMenuItem("Beállítások...", null, new EventHandler(Settings_Click));
            Exit = new ToolStripMenuItem("Kilépés                                ", null, new EventHandler(Exit_Click));

            Menu.RenderMode = ToolStripRenderMode.System;

            Menu.Items.Add(OpenNaplo);
            Menu.Items.Add(new ToolStripSeparator());
            Menu.Items.Add(RecentChanges);
            Menu.Items.Add(CheckNow);
            Menu.Items.Add(AutoCheck);
            Menu.Items.Add(new ToolStripSeparator());
            Menu.Items.Add(Exit);

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
                Mayor.Settings.CheckAutomatically = !Mayor.Settings.CheckAutomatically;
                RegularCheckEnabled.Checked = Mayor.Settings.CheckAutomatically;
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
