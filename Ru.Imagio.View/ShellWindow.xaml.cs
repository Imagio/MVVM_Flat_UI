using System.ComponentModel;
using Ru.Imagio.ViewModel;

namespace Ru.Imagio.View
{
    /// <summary>
    /// Логика взаимодействия для ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow
    {
        public ShellWindow()
        {
            InitializeComponent();
        }

        private ShellViewModel _context;

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_context == null)
                _context = DataContext as ShellViewModel;
            if (_context != null) 
                _context.Notificator.IsWindowVisible = false;
            Hide();
            e.Cancel = true;
            base.OnClosing(e);
        }
    }
}
