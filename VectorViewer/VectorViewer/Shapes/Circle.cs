using System.Windows.Media;
using Path = System.Windows.Shapes.Path;
using System.Windows.Controls;
using VectorViewer.Parsers;
using VectorViewer.Shapes.Interfaces;
using System.Windows;
using VectorViewer.Misc;

namespace VectorViewer.Shapes
{
    /// <summary>
    /// Implements IShape as a circle
    /// </summary>
    public class Circle : IShape
    {
        private readonly IShapePropertiesParser _shapePropertiesParser;

        public Point Center { get; }
        public double Radius { get; }
        public bool IsFilled { get; }
        public Color Color { get; }

        public Circle(CircleModel circleModel)
        {
            _shapePropertiesParser = new ShapePropertiesParser();
            Center = _shapePropertiesParser.ParsePointCoordinate(circleModel.Center);
            Radius = _shapePropertiesParser.ParseNumber(circleModel.Radius);
            Color = _shapePropertiesParser.ParseArgbColor(circleModel.Color);
            IsFilled = circleModel.IsFilled;
        }

        public void Draw(Canvas canvas)
        {
            var circle = new Path()
            {
                Stroke = new SolidColorBrush(Color),
                StrokeThickness = Constants.DefaultShapeThickness
            };

            if (IsFilled)
            {
                circle.Fill = new SolidColorBrush(Color);
            }

            circle.Data = new EllipseGeometry()
            {
                Center = new System.Windows.Point(Center.X, Center.Y),
                RadiusX = Radius,
                RadiusY = Radius,
            };

            canvas.Children.Add(circle);
        }

        public void Scale(Canvas canvas)
        {
            throw new System.NotImplementedException();
        }
    }
}
