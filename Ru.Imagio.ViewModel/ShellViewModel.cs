using System;
using System.Net.Sockets;
using System.Windows.Threading;

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

        private ShellViewModel()
        {
            UserID = 0;
            Notificator = new Notificator();
            var timer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 10)};
            timer.Tick += TimerOnTick;
            timer.Start();
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            Notificator.ShowNotify("Foo\nBar");
        }

        public int UserID { get; private set; }

        public Notificator Notificator
        {
            get; private set;
        }

        public ViewModelBase ActiveWorkspace
        {
            get
            {
                if (UserID == 0)
                {
                    var signViewModel = new SignViewModel();
                    signViewModel.Signed += (sender, args) =>
                    {
                        UserID = args.UserId;
                    };
                    return signViewModel;
                }
                return null;
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
