using System;
using System.Linq;
using System.Windows.Input;
using Ru.Imagio.Model;
using Ru.Imagio.ViewModel.Crypto;
using Ru.Imagio.ViewModel.Notifications;

namespace Ru.Imagio.ViewModel
{
    public class SignViewModel : ViewModelBase
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
            if (!IsRememberMe) return;
            UserName = PasswordEncoder.Decode(signData.UserName);
            Password = PasswordEncoder.Decode(signData.Password);
        }

        private ICommand _signCommand;

        public ICommand SignCommand
        {
            get
            {
                return _signCommand ?? (_signCommand = new AsyncDelegateCommand<int>(o =>
                {
                    var password = Password ?? "";
                    var userName = UserName ?? "";
                    var hashPassword = PasswordHash.CalcPasswordHash(password);

                    int userId;

                    using (var context = new DocsContext())
                    {
                        var account = context.Accounts.FirstOrDefault(acc => acc.IsActive &&
                                                                             acc.UserName == userName &&
                                                                             acc.Password == hashPassword);
                        if (account == null)
                            return 0;
                        userId = account.Id;
                    }

                    var signData = new CryptoSign
                    {
                        RememberMe = IsRememberMe,
                    };

                    if (IsRememberMe)
                    {
                        signData.UserName = PasswordEncoder.Encode(userName);
                        signData.Password = PasswordEncoder.Encode(password);
                    }

                    CryptoSignStore.Save(signData);

                    return userId;
                }, completed: result =>
                {
                    if (result == 0)
                        ShellViewModel.Instance.AddNotification("Неправильное имя пользователя или пароль", NotificationType.Error);
                    else
                        OnSigned(result);
                }, exception: ShellViewModel.Instance.Exception));
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
