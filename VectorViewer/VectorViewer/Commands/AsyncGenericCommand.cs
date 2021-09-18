using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VectorViewer.Exceptions;
using VectorViewer.Misc;

namespace VectorViewer.Commands
{
    /// <summary>
    /// Base generic class for asynchronous commands with parameter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AsyncGenericCommand<T> : NotifyPropertyChangedBase, IAsyncGenericCommand<T>
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            private set => RaiseAndSetIfChanged(ref _isBusy, value);
        }

        private event EventHandler CanExecuteChanged = (sender, e) => { };
        event EventHandler ICommand.CanExecuteChanged
        {
            add => CanExecuteChanged += value;
            remove => CanExecuteChanged -= value;
        }

        protected AsyncGenericCommand()
        {
            CommandManager.RequerySuggested += (sender, args) => RaiseCanExecuteChanged();
        }

        protected abstract bool CanExecute(T parameter);
        protected abstract Task AsyncAction(T parameter);
        protected abstract void OnException(Exception exception);

        bool IAsyncGenericCommand<T>.CanExecute(T parameter)
        {
            return !IsBusy && CanExecute(parameter);
        }

        async Task IAsyncGenericCommand<T>.ExecuteAsync(T parameter)
        {
            try
            {
                await ExecuteAsyncInternal(parameter);
            }
            catch (Exception e)
            {
                OnException(e);
            }
        }

        void ICommand.Execute(object parameter)
        {
            if (parameter != null && !(parameter is T))
            {
                var argumentException = new ArgumentException($"{nameof(parameter)} should be of {typeof(T)} type");                
                throw argumentException;
            }
            ExecuteAsyncInternal((T)parameter).RunSafe(OnException);
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (parameter != null && !(parameter is T))
            {
                var argumentException = new ArgumentException($"{nameof(parameter)} should be of {typeof(T)} type");               
                throw argumentException;
            }

            return ((IAsyncGenericCommand<T>)this).CanExecute((T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged.Invoke(this, new EventArgs());
        }

        private async Task ExecuteAsyncInternal(T parameter)
        {
            try
            {
                IsBusy = true;
                RaiseCanExecuteChanged();
                await AsyncAction(parameter);
            }
            finally
            {
                IsBusy = false;
                RaiseCanExecuteChanged();
            }
        }
    }
}
