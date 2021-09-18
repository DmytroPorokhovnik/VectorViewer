using System.Windows.Media;
using Path = System.Windows.Shapes.Path;
using Point = System.Windows.Point;
using System.Windows.Controls;
using VectorViewer.Parsers;
using VectorViewer.Shapes.Interfaces;
using VectorViewer.Misc;
using VectorViewer.Geometry;
using System;

namespace VectorViewer.Shapes
{
    /// <summary>
    /// Implements IShape as a circle
    /// </summary>
    public class Circle : IShape
    {
        private readonly IShapePropertiesParser _shapePropertiesParser;

        public Point Center { get; }
        public double Radius { get; private set; }
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

        private Circle(Point center, double radius)
        {
            Center = center;
            Radius = radius;
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
            if (GeometryCalculations.IsCircleInsideBoundedCartesianSystem(this, canvas.ActualWidth, canvas.ActualHeight))
                return;
            var scaledCircle = GetScaledCircle(canvas);
            if (scaledCircle == null) throw new InvalidOperationException("Couldn't scale circle");
            Radius = scaledCircle.Radius;
        }

        private Circle GetScaledCircle(Canvas canvas)
        {   
            Circle scaledCircle = null;

            for (var i = 1 - Constants.ScaleStep; i > 0; i -= 0.05)
            {                       
                scaledCircle = new Circle(Center, Radius * i);
                if (!GeometryCalculations.IsCircleInsideBoundedCartesianSystem(scaledCircle, canvas.ActualWidth, canvas.ActualHeight)) continue;
                break;
            }

            return scaledCircle;
        }
    }
}
