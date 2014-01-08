using System;
using System.Windows;
using Ru.Imagio.View;
using Ru.Imagio.View.Controls;
using Ru.Imagio.ViewModel;

namespace Ru.Imagio.Main
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                ShutdownMode = ShutdownMode.OnExplicitShutdown;
                var shellViewModel = ShellViewModel.Instance;
                shellViewModel.Shutdown += ShellViewModelOnShutdown;
                var shellWindow = new ShellWindow {DataContext = shellViewModel};
                var notificationIcon = new NotificationIcon(shellViewModel.Notificator, shellWindow);
                notificationIcon.Show();
                shellViewModel.Notificator.IsWindowVisible = true;
                shellWindow.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Unhandled error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static void ShellViewModelOnShutdown(object sender, EventArgs eventArgs)
        {
            var shellViewModel = sender as ShellViewModel;
            if (shellViewModel == null)
                return;
            var userAnswer = 
                MessageBox.Show(
                    "Вы действительно хотите закрыть программу?", 
                    "Внимание", 
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question, 
                    MessageBoxResult.No);
            if (userAnswer != MessageBoxResult.Yes) return;
            shellViewModel.Notificator.DeleteIcon();
            Current.Shutdown();
        }
    }
}
