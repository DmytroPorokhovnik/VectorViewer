using System;
using System.Collections.Generic;
using VectorViewer.Shapes.Interfaces;
using System.Windows.Input;
using VectorViewer.Commands;
using System.Windows.Shapes;
using VectorViewer.Misc;

namespace VectorViewer.ViewModels
{
    class MainWindowViewModel: NotifyPropertyChangedBase
    {
        private string _selectedFileName;

        public IEnumerable<IShape> Shapes { get; set; }
        public ICommand SelectFileCommand { get; set; }
        public ICommand DrawShapes { get; set; }

        public string SelectedFileName
        {
            get => _selectedFileName;
            set => RaiseAndSetIfChanged(ref _selectedFileName, value);
        }    

        public MainWindowViewModel()
        {
            SelectFileCommand = new GetShapesFromFileCommand(this);
            DrawShapes = new DrawShapesCommand(this);
        }
    }
}
