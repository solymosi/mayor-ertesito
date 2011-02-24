using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NaploNotifier
{
    public partial class Context
    {
        public NotificationForm Notification;

        void ShowNotification(List<Change> Changes)
        {
            MainSynchronizationContext.Post(new SendOrPostCallback(delegate
            {
                if (Changes.Count == 0) { return; }
                if (Notification != null && Notification.Visible) { Notification.Close(); }
                Notification = new NotificationForm();
                Notification.Changes = Changes;
                Notification.Show();
            }), new object());
        }
    }
}
