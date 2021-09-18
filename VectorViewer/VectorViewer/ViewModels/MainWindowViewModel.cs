using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VectorViewer.Commands;

namespace VectorViewer.ViewModels
{
    class MainWindowViewModel
    {
        public ICommand SelectFileCommand { get; set; }

        public MainWindowViewModel()
        {
            SelectFileCommand = new GetShapesFromFileCommand();
        }
    }
}
