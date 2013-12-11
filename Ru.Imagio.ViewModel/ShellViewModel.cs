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
                Thread.Sleep(1000);
                return _handle ?? (_handle = new ShellViewModel());
            }
        }
    }
}
