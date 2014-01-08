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

        private SignViewModel _viewModel;

        private void SignView_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _viewModel = e.NewValue as SignViewModel;
            if (_viewModel == null || !_viewModel.IsRememberMe) return;
            UserName.Text = _viewModel.UserName;
            Password.Password = _viewModel.Password;
        }

        private void Password_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
                _viewModel.Password = Password.Password;
        }

    }
}
