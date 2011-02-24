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

        public bool Updating { get { return this.UpdateThread != null && (this.UpdateThread.ThreadState == ThreadState.Running || this.UpdateThread.ThreadState == ThreadState.Background); } }

        public void RunUpdate() { RunUpdate(false); }
        public void RunUpdate(bool InitialUpdate)
        {
            if (this.Updating) { return; }
            UpdateThread = new Thread(new ParameterizedThreadStart(RunUpdateThread));
            UpdateThread.Name = "Update Thread";
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
            try
            {
                Mayor.RunUpdate((bool)Parameter);
            }
            catch (LoginFailedException)
            {
                Tools.ErrorMessage("Nem sikerült ellenőrizni a jegyedidet, mert a megadott felhasználónév vagy jelszó hibás. Adj meg egy érvényes felhasználónév/jelszó párost a beállítások menüben majd próbáld újra!");
            }
            catch
            {
                Tools.ErrorMessage("Nem sikerült ellenőrizni a jegyedidet. Győződj meg arról, hogy van aktív internetkapcsolat és megfelelő a Beállítások menüpontban megadott webes cím.");
            }
        }
    }
}
