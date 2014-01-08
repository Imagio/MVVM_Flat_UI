using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Threading;

namespace Ru.Imagio.ViewModel.Notifications
{
    public class NotificationItem
    {
        public event EventHandler Close;

        private static readonly Dictionary<NotificationType, TimeSpan> DefaultTimeSpans = new Dictionary
            <NotificationType, TimeSpan>
        {
            {NotificationType.Error,  new TimeSpan(0, 0, 0, 5)},
            {NotificationType.Notice,  new TimeSpan(0, 0, 0, 10)},
            {NotificationType.Success,  new TimeSpan(0, 0, 0, 5)},
            {NotificationType.Warning,  new TimeSpan(0, 0, 0, 5)},
        };

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
            var timer = new DispatcherTimer { Interval = span ?? DefaultTimeSpans[NotificationType] };
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
