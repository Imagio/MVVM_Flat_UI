using System;
using System.Threading;
using System.Windows.Input;

namespace Ru.Imagio.ViewModel
{
    public class SignViewModel : ViewModelBase
    {
        private string _login;
        private string _password;
        private ICommand _signCommand;

        public class SignEventArgs : EventArgs
        {
            public int UserId { get; private set; }

            public SignEventArgs(int userId)
            {
                UserId = userId;
            }
        }

        public event EventHandler<SignEventArgs> SignComplete;

        protected virtual void OnSignComplete(int userId)
        {
            var handler = SignComplete;
            if (handler != null) handler(this, new SignEventArgs(userId));
        }

        public string Login
        {
            set
            {
                _login = value;
            }
        }

        public string Password
        {
            set
            {
                _password = value;
            }
        }

        public ICommand SignCommand
        {
            get
            {
                return _signCommand ?? (
                    _signCommand = new DelegateCommand<int>(Sign, CanSign, OnSignComplete));
            }
        }

        private bool CanSign(object o)
        {
#if DEBUG
            return true;
#else
            return 
                !String.IsNullOrEmpty(_login) && 
                !String.IsNullOrEmpty(_password);
#endif
        }

        private int Sign(object o)
        {
            var login = _login;
            var password = _password;
            Thread.Sleep(2000);
            return 1;
        }
    }
}
