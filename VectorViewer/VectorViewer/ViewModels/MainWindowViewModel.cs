using System;
using System.Collections.Generic;
using VectorViewer.Shapes.Interfaces;
using System.Windows.Input;
using VectorViewer.Commands;

namespace VectorViewer.ViewModels
{
    class MainWindowViewModel
    {
        public IEnumerable<IShape> Shapes { get; set; }
        public ICommand SelectFileCommand { get; set; }
        public ICommand DrawShapes { get; set; }

        public MainWindowViewModel()
        {
            SelectFileCommand = new GetShapesFromFileCommand(this);
            DrawShapes = new DrawShapesCommand(this);
        }
    }
}
