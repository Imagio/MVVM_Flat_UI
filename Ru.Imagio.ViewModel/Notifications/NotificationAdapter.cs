using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace Ru.Imagio.ViewModel.Notifications
{
    internal static class NotificationAdapter
    {
        private static readonly Collection<NotificationItem> Notifications = new ObservableCollection<NotificationItem>();

        private static Dispatcher dispatcher;

        static NotificationAdapter()
        {
            dispatcher = Dispatcher.CurrentDispatcher;
        }

        public static void AddNotification(string message, NotificationType notificationType = NotificationType.Notice,
            TimeSpan? timeSpan = null)
        {
            AddNotification(new NotificationItem(notificationType, message, timeSpan));
        }

        public static void AddNotification(NotificationItem notification)
        {

                notification.Close += NotificationOnClose;
                Notifications.Insert(0, notification);
        }

        private static void NotificationOnClose(object sender, EventArgs eventArgs)
        {
            Notifications.Remove(sender as NotificationItem);
        }

        public static ICollection<NotificationItem> GetNotifications()
        {
            return Notifications;
        }
    }
}
