using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Ru.Imagio.ViewModel.Annotations;

namespace Ru.Imagio.ViewModel
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        private readonly Action<Exception> _exception;

        public DelegateCommand(Action<object> execute = null, Predicate<object> canExecute = null, Action<Exception> exception = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            _exception = exception;
        }

        public void Execute(object parameter)
        {
            if (_execute == null)
                return;

            try
            {
                _execute(parameter);
            }
            catch (Exception exception)
            {
                if (_exception != null)
                {
                    _exception(exception);
                }
                else
                {
                    throw;
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    public class AsyncDelegateCommand : AsyncDelegateCommand<object>
    {
        public AsyncDelegateCommand(Action<object> execute = null, Predicate<object> canExecute = null, Action<Exception> exception = null)
            : base(o =>
            {
                if (execute != null)
                    execute(o);
                return null;
            }, canExecute, exception: exception)
        {
            
        }
    }

    public class AsyncDelegateCommand<TResult> : ICommand, INotifyPropertyChanged
    {
        private Task<TResult> _task;
        private readonly Predicate<object> _canExecute;
        private readonly Func<object, TResult> _execute;
        private readonly Action<TResult> _completed;
        private readonly Action<Exception> _exception;
        private readonly TaskScheduler _context;

        public AsyncDelegateCommand(Func<object, TResult> execute, Predicate<object> canExecute = null, Action<TResult> completed = null, Action<Exception> exception = null)
        {
            _execute = execute;
            _canExecute = canExecute ?? (o => true);
            _completed = completed;
            _exception = exception;

            _context = TaskScheduler.FromCurrentSynchronizationContext();
        }

        public void Execute(object parameter)
        {
           if (_task != null && _task.Status == TaskStatus.Running)
               return;

            _task = new Task<TResult>(() =>
            {
                OnPropertyChanged("IsExecuting");
                return _execute(parameter);
            });

            if (_completed != null)
                _task.ContinueWith(t => _completed(t.Result), CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, _context);

            if (_exception != null)
                _task.ContinueWith(t => _exception(t.Exception), CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, _context);

            _task.ContinueWith(t => OnPropertyChanged("IsExecuting"));

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
