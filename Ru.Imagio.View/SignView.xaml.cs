using System.Windows;
using Ru.Imagio.ViewModel;

namespace Ru.Imagio.View
{
    /// <summary>
    /// Логика взаимодействия для SignView.xaml
    /// </summary>
    public partial class SignView
    {
        public SignView()
        {
            InitializeComponent();
        }

        private SignViewModel _context;

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_context == null)
                _context = DataContext as SignViewModel;
            if (_context != null) 
                _context.Password = PasswordBox.Password;
        }
    }
}
