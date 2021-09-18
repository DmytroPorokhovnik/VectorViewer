using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using VectorViewer.Misc;
using VectorViewer.Shapes.Interfaces;

namespace VectorViewer.ShapeReaders
{
    /// <summary>
    /// Represents a shape reader
    /// </summary>
    class ShapeReader
    {
        private IShapeFileReader _shapeFileReader;

        public ShapeReader(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));
            _shapeFileReader = GetShapeFileReader(filePath);
        }

        public void SetShapeFileReader(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));
            _shapeFileReader = GetShapeFileReader(filePath);
        }

        public Task<IEnumerable<IShape>> GetShapesFromFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));
            return _shapeFileReader.GetShapes(filePath);
        }

        private IShapeFileReader GetShapeFileReader(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));

            IShapeFileReader shapeFileReader;
            switch (Path.GetExtension(filePath))
            {
                case Constants.JsonExtension:
                    shapeFileReader = new JsonShapeReader();
                    break;
                default:
                    throw new InvalidOperationException("Such file extension is not supported;");
            }

            return shapeFileReader;
        }
    }
}
