using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Ru.Imagio.ViewModel.Crypto;

namespace Ru.Imagio.ViewModel
{
    public class SignViewModel: ViewModelBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsRememberMe { get; set; }

        public event EventHandler<SignedEventArgs> Signed;

        protected virtual void OnSigned(int userId)
        {
            var handler = Signed;
            if (handler != null) 
                handler(this, new SignedEventArgs(userId));
        }

        public SignViewModel()
        {
            var signData = CryptoSignStore.Load();
            IsRememberMe = signData.RememberMe;
            if (IsRememberMe)
            {
                UserName = PasswordEncoder.Decode(signData.UserName);
                Password = PasswordEncoder.Decode(signData.Password);
            }
        }

        private ICommand _signCommand;

        public ICommand SignCommand
        {
            get
            {
                return _signCommand ?? (_signCommand = new DelegateCommand(o =>
                {
                    var password = Password;
                    var userName = UserName;
                    var signData = new CryptoSign()
                    {
                        RememberMe = IsRememberMe,
                    };

                    if (IsRememberMe)
                    {
                        signData.UserName = PasswordEncoder.Encode(userName);
                        signData.Password = PasswordEncoder.Encode(password);
                    }

                    CryptoSignStore.Save(signData);
                    OnSigned(1);
                } ));
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
