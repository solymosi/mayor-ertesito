using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NaploNotifier
{
    public partial class Context
    {
        WindowsFormsSynchronizationContext UpdateSynchronizationContext;
        Thread UpdateThread;

        public bool Updating { get { return this.UpdateThread != null && this.UpdateThread.ThreadState == ThreadState.Running; } }

        public void RunUpdate() { RunUpdate(false); }
        public void RunUpdate(bool InitialUpdate)
        {
            if (this.UpdateThread != null && this.UpdateThread.ThreadState == ThreadState.Running)
            {
                throw new InvalidOperationException("A frissítés már folyamatban van.");
            }
            UpdateThread = new Thread(new ParameterizedThreadStart(RunUpdateThread));
            UpdateThread.IsBackground = true;
            UpdateThread.Priority = ThreadPriority.Lowest;
            UpdateThread.Start((object)InitialUpdate);
        }

        public void RunFirstUpdate()
        {
            RunUpdate(true);
        }

        void RunUpdateThread(object Parameter)
        {
            UpdateSynchronizationContext = new WindowsFormsSynchronizationContext();
            Mayor.RunUpdate((bool)Parameter);
        }
    }
}
