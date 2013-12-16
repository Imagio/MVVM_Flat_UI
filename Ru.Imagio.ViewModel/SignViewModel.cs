using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Ru.Imagio.ViewModel
{
    public class SignViewModel: ViewModelBase
    {
        public string UserName { private get; set; }
        public string Password { private get; set; }

        public event EventHandler<SignedEventArgs> Signed;

        protected virtual void OnSigned(int userId)
        {
            var handler = Signed;
            if (handler != null) 
                handler(this, new SignedEventArgs(userId));
        }

        public SignViewModel()
        {
            UserName = String.Empty;
            Password = String.Empty;
        }

        private ICommand _signCommand;

        public ICommand SignCommand
        {
            get
            {
                return _signCommand ?? (_signCommand = new DelegateCommand(o => OnSigned(1)));
            }
        }
    }

    public class SignedEventArgs : EventArgs
    {
        public int UserId { get; private set; }

        public SignedEventArgs(int userId)
        {
            UserId = userId;
        }
    }
}
