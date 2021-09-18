using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VectorViewer.ViewModels;

namespace VectorViewer.Commands
{
    /// <summary>
    /// Draw shapes command
    /// </summary>
    class DrawShapesCommand : AsyncGenericCommand<Canvas>
    {
        private readonly MainWindowViewModel _viewModel;

        public DrawShapesCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));          
        }

        protected override async Task AsyncAction(Canvas canvas)
        {
            canvas.Children.Clear();
        }

        protected override bool CanExecute(Canvas canvas)
        {
            return _viewModel.Shapes?.Count() > 0;
        }

        protected override void OnException(Exception exception)
        {
            MessageBox.Show(exception.Message);
            //Here should be logging
        }
    }
}
