using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VectorViewer.Misc;
using VectorViewer.ShapeReaders;

namespace VectorViewer.Commands
{
    class GetShapesFromFileCommand : AsyncCommand
    {
        private ShapeReader _shapeReader;
        private string[] _supportedExtensions = new string[] {Constants.JsonExtension };
        private string _filesFilter;
        private const string FilterDelimeter = "|";

        public GetShapesFromFileCommand()
        {
            _filesFilter = GetStringFilter();
        }

        protected override async Task AsyncAction()
        {
            var openFileDialog = new OpenFileDialog()
            {
                Multiselect = false,
                Filter = _filesFilter
            };
            var result = openFileDialog.ShowDialog() ?? false;
            if(!result) return;

            _shapeReader = new ShapeReader(openFileDialog.FileName);
            var shapes = await _shapeReader.GetShapesFromFile(openFileDialog.FileName);
        }

        protected override bool CanExecute()
        {
            return true;
        }

        protected override void OnException(Exception exception)
        {
            MessageBox.Show(exception.Message);
            //Here should be logging
        }

        private string GetStringFilter()
        {
            var result = new StringBuilder();            
            foreach (var extension in _supportedExtensions)
            {
                var extensionUpper = extension.Substring(extension.IndexOf(".", StringComparison.CurrentCultureIgnoreCase) + 1).ToUpper();
                result.Append(extensionUpper);
            }

            result.Append(FilterDelimeter);
            foreach (var extension in _supportedExtensions)
            {
                result.Append($"*{extension};");
            }
            return result.ToString();

        }
    }
}
