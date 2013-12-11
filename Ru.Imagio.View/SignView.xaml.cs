using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ru.Imagio.ViewModel;

namespace Ru.Imagio.View
{
    /// <summary>
    /// Логика взаимодействия для SignView.xaml
    /// </summary>
    public partial class SignView : UserControl
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
