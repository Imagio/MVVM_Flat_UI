using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Ru.Imagio.ViewModel
{
    public class ShellViewModel : ViewModelBase
    {
        private static ShellViewModel _handle;

        public static ShellViewModel Handle
        {
            get
            {
                return _handle ?? (_handle = new ShellViewModel());
            }
        }

        public ShellViewModel()
        {
            UserID = 0;
        }

        public int UserID { get; private set; }

        public ViewModelBase ActiveShell
        {
            get
            {
                if (UserID == 0)
                {
                    var signViewModel = new SignViewModel();
                    return signViewModel;
                }
                return null;
            }
        }
    }
}
