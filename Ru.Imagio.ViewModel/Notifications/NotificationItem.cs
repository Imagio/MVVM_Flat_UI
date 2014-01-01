using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;

namespace Ru.Imagio.ViewModel.Notifications
{
    public class NotificationItem
    {
        public event EventHandler Close;

        private static readonly TimeSpan DefaultSpan = new TimeSpan(0, 0, 0, 10);

        protected virtual void OnClose()
        {
            var handler = Close;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public NotificationItem(NotificationType notificationType, string message, TimeSpan? span = null)
        {
            Message = message;
            NotificationType = notificationType;
            var timer = new DispatcherTimer { Interval = span ?? DefaultSpan };
            timer.Tick += (sender, args) => OnClose();
            timer.Start();
        }

        public string Message { get; private set; }

        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new DelegateCommand(o => OnClose()));
            }
        }

        public NotificationType NotificationType { get; private set; }
    }
}
