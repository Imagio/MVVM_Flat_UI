using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ru.Imagio.ViewModel
{
    public class DelegateCommand : ICommand
    {
        private readonly Func<object, Task> _execute;
        private readonly Predicate<object> _canExecute;

        public DelegateCommand(Func<object, Task> execute = null, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            IsExecuting = false;
        }

        public void Execute(object parameter)
        {
            IsExecuting = true;
            var task = _execute(parameter);
            task.ContinueWith(t =>
            {
                IsExecuting = false;
                CommandManager.InvalidateRequerySuggested();
            });
        }

        public bool CanExecute(object parameter)
        {
            return !IsExecuting && (_canExecute == null || _canExecute(parameter));
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool IsExecuting { get; private set; }
    }
}
