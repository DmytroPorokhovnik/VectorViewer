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
    /// Base class for asynchronous commands without parameter
    /// </summary>
    public abstract class AsyncCommand : NotifyPropertyChangedBase, IAsyncCommand
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

        protected AsyncCommand()
        {
            CommandManager.RequerySuggested += (sender, args) => RaiseCanExecuteChanged();
        }

        protected abstract bool CanExecute();
        protected abstract Task AsyncAction();
        protected abstract void OnException(Exception exception);

        bool IAsyncCommand.CanExecute()
        {
            return !IsBusy && CanExecute();
        }

        async Task IAsyncCommand.ExecuteAsync()
        {
            try
            {
                await ExecuteAsyncInternal();
            }
            catch (Exception e)
            {
                OnException(e);
            }
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsyncInternal().RunSafe(OnException);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return ((IAsyncCommand)this).CanExecute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged.Invoke(this, new EventArgs());
        }

        private async Task ExecuteAsyncInternal()
        {
            try
            {
                IsBusy = true;
                RaiseCanExecuteChanged();
                await AsyncAction();
            }
            finally
            {
                IsBusy = false;
                RaiseCanExecuteChanged();
            }
        }
    }
}
