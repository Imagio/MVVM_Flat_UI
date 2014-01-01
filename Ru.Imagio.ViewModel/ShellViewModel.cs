using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;
using Ru.Imagio.Model;
using Ru.Imagio.ViewModel.Notifications;

namespace Ru.Imagio.ViewModel
{
    public class ShellViewModel : ViewModelBase
    {
        #region SINGLETON

        private static ShellViewModel _instance;

        public static ShellViewModel Instance
        {
            get
            {
                return _instance ?? (_instance = new ShellViewModel());
            }
        }

        #endregion

        public bool IsSigned()
        {
            return UserID > 0;
        }

        private readonly DispatcherTimer _timer;

        private ShellViewModel()
        {
            UserID = 0;
            Notificator = new Notificator();
            _timer = new DispatcherTimer
            {
#if DEBUG
                Interval = new TimeSpan(0, 0, 3)
#else
                Interval = new TimeSpan(0, 10, 0)
#endif
            };
            _timer.Tick += TimerOnTick;
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            if (UserID == 0)
                return;

            var notification = new StringBuilder();

            using (var context = new DocsContext())
            {
                var account = context.GetAccount(UserID);
                if (account == null)
                    return;
                if (account.SendAccounts.Count == 0)
                    return;
                foreach (var document in account.SendAccounts.Select(sendAccount => sendAccount.Document))
                {
                    notification.AppendLine(String.Format("№{0}\t{1}\tот {2:d}", document.DocumentNumber, document.Name, document.Date));
                }
            }
            Notificator.ShowNotify(notification.ToString());
        }

        public int UserID { get; private set; }

        public Notificator Notificator
        {
            get; private set;
        }

        private ViewModelBase _activeWorkspace;
        public ViewModelBase ActiveWorkspace
        {
            get
            {
                if (UserID != 0) 
                    return _activeWorkspace ?? (_activeWorkspace = new WorkspaceShellViewModel());
                return GetSignViewModel();
            }
        }

        private ViewModelBase GetSignViewModel()
        {
            var signViewModel = new SignViewModel();
            signViewModel.Signed += OnSignViewModelSigned;
            return signViewModel;            
        }

        private void OnSignViewModelSigned(object sender, SignedEventArgs args)
        {
            UserID = args.UserId;
            OnPropertyChanged("ActiveWorkspace");
            _timer.Start();
        }

        public event EventHandler Shutdown;

        protected virtual void OnShutdown()
        {
            var handler = Shutdown;
            if (handler != null) 
                handler(this, EventArgs.Empty);
        }

        public void TryShutdown()
        {
            OnShutdown();
        }

        private ICommand _signOutCommand;

        public ICommand SignOutCommand
        {
            get
            {
                return _signOutCommand ?? (_signOutCommand = new DelegateCommand(o =>
                {
                    UserID = 0;
                    _timer.Stop();
                    OnPropertyChanged("ActiveWorkspace");
                }, o => IsSigned()));
            }
        }

        public ICollection<NotificationItem> Notifications
        {
            get { return NotificationAdapter.GetNotifications(); }
        }

        public void AddNotification(string message, NotificationType notificationType = NotificationType.Notice)
        {
            NotificationAdapter.AddNotification(message, notificationType);
        }
    }
}
