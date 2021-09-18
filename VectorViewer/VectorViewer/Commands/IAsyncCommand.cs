using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VectorViewer.Commands
{
    /// <summary>
    /// Represent an async command
    /// </summary>
    interface IAsyncCommand: ICommand, INotifyPropertyChanged
    {
        bool CanExecute();
        bool IsBusy { get; }
        Task ExecuteAsync();
        /// <summary>
        /// Force to recheck command CanExecute state
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}
