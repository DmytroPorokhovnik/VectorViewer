using CanvasLine = System.Windows.Shapes.Line;
using System;
using System.Windows.Media;
using GeometryCalculations = VectorViewer.Geometry.GeometryCalculations;
using Point = System.Windows.Point;
using System.Windows.Controls;
using VectorViewer.Parsers;
using VectorViewer.Shapes.Interfaces;
using VectorViewer.Misc;
using VectorViewer.Geometry;
namespace VectorViewer.Shapes
{
    /// <summary>
    /// Implements IShape as a line
    /// </summary>
    public class Line : IShape
    {
        private readonly IShapePropertiesParser _shapePropertiesParser;

        public Color Color { get; }

        public Point PointA { get; private set; }

        public Point PointB { get; private set; }

        public Line(LineModel lineModel)
        {
            _shapePropertiesParser = new ShapePropertiesParser();
            PointA = _shapePropertiesParser.ParsePointCoordinate(lineModel.PointA);
            PointB = _shapePropertiesParser.ParsePointCoordinate(lineModel.PointB);
            Color = _shapePropertiesParser.ParseArgbColor(lineModel.Color);
        }

        private Line(Point a, Point b)
        {
            PointA = a;
            PointB = b;            
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
            if (GeometryCalculations.IsLineInsideBoundedCartesianSystem(this, canvas.ActualWidth, canvas.ActualHeight))
                return;

            var scaledLine = GetScaledLine(canvas);
            if (scaledLine == null) throw new InvalidOperationException("Couldn't scale line");
            PointA = scaledLine.PointA;
            PointB = scaledLine.PointB;
        }

        private Line GetScaledLine(Canvas canvas)
        {
            var zeroPoint = new Point(0, 0);
            var aDistance = GeometryCalculations.CalculateDistance(zeroPoint, PointA);
            var bDistance = GeometryCalculations.CalculateDistance(zeroPoint, PointB);          
            var aVector = new Vector(zeroPoint, PointA);
            var bVector = new Vector(zeroPoint, PointB);         
            Line scaledLine = null;

            for (var i = 1 - Constants.ScaleStep; i > 0; i -= 0.05)
            {
                var a = GeometryCalculations.FindPointOnVector(aVector, zeroPoint, aDistance * i);
                var b = GeometryCalculations.FindPointOnVector(bVector, zeroPoint, bDistance * i);             
                scaledLine = new Line(a, b);
                if (!GeometryCalculations.IsLineInsideBoundedCartesianSystem(scaledLine, canvas.ActualWidth, canvas.ActualHeight)) continue;
                break;
            }

            return scaledLine;
        }
    }
}
