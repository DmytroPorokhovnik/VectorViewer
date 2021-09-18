using Newtonsoft.Json;
using CanvasLine = System.Windows.Shapes.Line;
using System.Windows.Controls;
using VectorViewer.Parsers;
using VectorViewer.Shapes.Interfaces;
using System.Windows.Media;
using System.Windows;
using VectorViewer.Misc;

namespace VectorViewer.Shapes
{
    /// <summary>
    /// Implements IShape as a line
    /// </summary>
    public class Line : IShape
    {
        private readonly IShapePropertiesParser _shapePropertiesParser;

        public Color Color { get; }

        public Point PointA { get; }

        public Point PointB { get; }

        public Line(LineModel lineModel)
        {
            _shapePropertiesParser = new ShapePropertiesParser();
            PointA = _shapePropertiesParser.ParsePointCoordinate(lineModel.PointA);
            PointB = _shapePropertiesParser.ParsePointCoordinate(lineModel.PointB);
            Color = _shapePropertiesParser.ParseArgbColor(lineModel.Color);
        }

        public void Draw(Canvas canvas)
        {
            var canvasLine = new CanvasLine()
            {
                X1 = PointA.X,
                Y1 = PointA.Y,
                X2 = PointB.X,
                Y2 = PointB.Y,
                Stroke = new SolidColorBrush(Color),
                StrokeThickness = Constants.DefaultShapeThickness
            };

            canvas.Children.Add(canvasLine);
        }

        public void Scale(Canvas canvas)
        {
            throw new System.NotImplementedException();
        }
    }
}
