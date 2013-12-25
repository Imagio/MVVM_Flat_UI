using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Threading;
using Ru.Imagio.Model;

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


        private readonly DispatcherTimer _timer;

        private ShellViewModel()
        {
            UserID = 0;
            Notificator = new Notificator();
            _timer = new DispatcherTimer
            {
#if DEBUG
                Interval = new TimeSpan(0, 0, 10)
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
                foreach (var sendAccount in account.SendAccounts)
                {
                    notification.AppendLine(String.Format("{0}; дата: {1}", sendAccount.DocumentId, sendAccount.SendDate));
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
                var signViewModel = new SignViewModel();
                signViewModel.Signed += (sender, args) =>
                {
                    UserID = args.UserId;
                    OnPropertyChanged("ActiveWorkspace");
                    _timer.Start();
                };
                return signViewModel;
            }
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
    }
}
