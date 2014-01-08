using System;

namespace Ru.Imagio.ViewModel
{
    public class Notificator
    {
        public enum IconVisibility
        {
            Show,
            Hide,
            Delete
        }

        public class IconVisibilityEventArgs : EventArgs
        {
            public IconVisibility IconVisibility { get; private set; }

            public IconVisibilityEventArgs(IconVisibility iconVisibility)
            {
                IconVisibility = iconVisibility;
            }
        }

        public class NotificationEventArgs : EventArgs
        {
            public string Message { get; private set; }
            public NotificationEventArgs(string message)
            {
                Message = message;
            } 
        }

        public event EventHandler<IconVisibilityEventArgs> IconVisibilityChanged;
        public event EventHandler<NotificationEventArgs> Notify;

        protected virtual void OnNotify(NotificationEventArgs e)
        {
            var handler = Notify;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnIconVisibilityChanged(IconVisibility iconVisibility)
        {
            var handler = IconVisibilityChanged;
            if (handler != null) handler(this, new IconVisibilityEventArgs(iconVisibility));
        }

        public bool IsWindowVisible { get; set; }

        public void TryShutdown()
        {
            ShellViewModel.Instance.TryShutdown();
        }

        public void DeleteIcon()
        {
            OnIconVisibilityChanged(IconVisibility.Delete);
        }

        public void HideIcon()
        {
            OnIconVisibilityChanged(IconVisibility.Hide);
        }

        public void ShowIcon()
        {
            OnIconVisibilityChanged(IconVisibility.Show);
        }

        public void ShowNotify(string message)
        {
            OnNotify(new NotificationEventArgs(message));
        }
    }
}
