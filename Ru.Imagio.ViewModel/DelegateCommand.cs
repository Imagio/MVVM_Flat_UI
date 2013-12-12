using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Ru.Imagio.ViewModel.Annotations;

namespace Ru.Imagio.ViewModel
{
    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute = null, Action<Exception> exception = null)
            : base(
                o =>
                {
                    execute(o);
                    return null;
                }, canExecute, exception: exception)
        {
            
        }
    }

    public class DelegateCommand<TResult> : ICommand, INotifyPropertyChanged
    {
        private Task<TResult> _task;
        private readonly Predicate<object> _canExecute;
        private readonly Func<object, TResult> _execute;
        private readonly Action<TResult> _completed;
        private readonly Action<Exception> _exception;

        public DelegateCommand(Func<object, TResult> execute, Predicate<object> canExecute = null, Action<TResult> completed = null, Action<Exception> exception = null)
        {
            _execute = execute;
            _canExecute = canExecute ?? (o => true);
            _completed = completed;
            _exception = exception;
        }

        public void Execute(object parameter)
        {
            if (_task != null && _task.Status == TaskStatus.Running)
                return;

            _task = new Task<TResult>(o =>
            {
                OnPropertyChanged("IsExecuting");
                CommandManager.InvalidateRequerySuggested();
                return _execute(o);
            }, parameter);

            _task.ContinueWith(t => OnPropertyChanged("IsExecuting"));

            _task.ContinueWith(t =>
            {
                if (_completed != null)
                {
                    _completed(t.Result);
                }
                CommandManager.InvalidateRequerySuggested();
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            _task.ContinueWith(t =>
            {
                if (_execute != null)
                {
                    _exception(t.Exception);
                }
            }, TaskContinuationOptions.OnlyOnFaulted);

            _task.Start();
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool IsExecuting
        {
            get { return _task != null && _task.Status == TaskStatus.Running; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
