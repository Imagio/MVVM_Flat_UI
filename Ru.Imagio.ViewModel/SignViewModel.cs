using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace Ru.Imagio.ViewModel
{
    public class SignViewModel : ViewModelBase
    {
        private string _login;
        private string _password;
        private ICommand _signCOmmand;

        public SignViewModel()
        {
            
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
                return _signCOmmand ?? (_signCOmmand = new DelegateCommand<object>(o =>
                {
                    Thread.Sleep(2000);
                    return null;
                }));
            }
        }
    }
}
