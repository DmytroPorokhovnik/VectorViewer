using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VectorViewer.Commands
{
    /// <summary>
    /// Generic interface for asynchronous command
    /// </summary>  
    public interface IAsyncGenericCommand<in T>: ICommand, INotifyPropertyChanged
    {
        bool IsBusy { get; }
        bool CanExecute(T parameter);
        Task ExecuteAsync(T parameter);

        /// <summary>
        /// Force to recheck command CanExecute state
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}
