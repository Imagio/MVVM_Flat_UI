using System.Windows;
using System.Windows.Forms;
using Ru.Imagio.ViewModel;

namespace Ru.Imagio.View.Controls
{
    public partial class NotificationIcon : Control
    {
        private readonly Notificator _notificator;

        private readonly Window _mainWindow;

        public NotificationIcon(Notificator notificator, Window mainWindow)
        {
            _notificator = notificator;
            _mainWindow = mainWindow;
            _notificator.IconVisibilityChanged += NotificatorOnIconVisibilityChanged;
            _notificator.Notify += NotificatorOnNotify;

            InitializeComponent();

            closeMenuItem.Click += (sender, args) => notificator.TryShutdown();
            notifyIcon.MouseDoubleClick += (sender, args) => ShowMainWindow();
        }

        private void NotificatorOnNotify(object sender, Notificator.NotificationEventArgs notificationEventArgs)
        {
            notifyIcon.BalloonTipText = notificationEventArgs.Message;
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.ShowBalloonTip(1000);
        }

        private void NotificatorOnIconVisibilityChanged(object sender, Notificator.IconVisibilityEventArgs iconVisibilityEventArgs)
        {
            switch (iconVisibilityEventArgs.IconVisibility)
            {
                case Notificator.IconVisibility.Delete: DeleteIcon(); break;
                case Notificator.IconVisibility.Hide: HideIcon(); break;
                case Notificator.IconVisibility.Show: ShowIcon(); break;
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public void HideIcon()
        {
            notifyIcon.Visible = false;
        }

        public void ShowIcon()
        {
            notifyIcon.Visible = true;
        }

        public void DeleteIcon()
        {
            notifyIcon.Visible = false;
            if (notifyIcon.Container != null)
                notifyIcon.Container.Remove(this);
        }

        private void ShowMainWindow()
        {
            _notificator.IsWindowVisible = true;
            // Показать окно (и перенести его на передний план, если оно уже видимо) . 
            if (_mainWindow.WindowState == WindowState.Minimized)
                _mainWindow.WindowState = WindowState.Normal;
            _mainWindow.Show();
            _mainWindow.Activate();
        }
    }
}
