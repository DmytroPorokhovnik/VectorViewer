using System;
using System.Windows.Media;
using Polygon = System.Windows.Shapes.Polygon;
using GeometryCalculations = VectorViewer.Geometry.GeometryCalculations;
using Point = System.Windows.Point;
using System.Windows.Controls;
using VectorViewer.Parsers;
using VectorViewer.Shapes.Interfaces;
using System.Collections.Generic;
using VectorViewer.Misc;
using VectorViewer.Geometry;

namespace VectorViewer.Shapes
{
    /// <summary>
    /// Implements IPolygon as a triangle
    /// </summary>
    public class Triangle : IPolygon
    {
        private IShapePropertiesParser _shapePropertiesParser;       

        public Color Color { get; }
        public Point PointA { get; private set; }
        public Point PointB { get; private set; }
        public Point PointC { get; private set; }        
        public bool IsFilled { get; private set; }

        public Triangle (TriangleModel triangleModel)
        {
            _shapePropertiesParser = new ShapePropertiesParser();
            PointA = _shapePropertiesParser.ParsePointCoordinate(triangleModel.PointA);
            PointB = _shapePropertiesParser.ParsePointCoordinate(triangleModel.PointB);
            PointC = _shapePropertiesParser.ParsePointCoordinate(triangleModel.PointC);
            Color = _shapePropertiesParser.ParseArgbColor(triangleModel.Color);
            IsFilled = triangleModel.IsFilled;
        }

        private Triangle(Point a, Point b, Point c)
        {
            PointA = a;
            PointB = b;
            PointC = c;
        }


        public void Draw(Canvas canvas)
        {        
            var points = new List<Point>() { PointA, PointB, PointC };

            var triangle = new Polygon()
            {
                Stroke = new SolidColorBrush(Color),
                Points = new PointCollection(points),
                StrokeThickness = Constants.DefaultShapeThickness
            };

            if (IsFilled)
            {
                triangle.Fill = new SolidColorBrush(Color);
            }

            canvas.Children.Add(triangle);
        }

        public void Scale(Canvas canvas)
        {
            if (GeometryCalculations.IsTriangleInsideBoundedCartesianSystem(this, canvas.ActualWidth, canvas.ActualHeight))
                return;

            var scaledTriangle = GetScaledTriangle(canvas);
            if (scaledTriangle == null) throw new InvalidOperationException("Couldn't scale triangle");
            PointA = scaledTriangle.PointA;
            PointB = scaledTriangle.PointB;
            PointC = scaledTriangle.PointC;
        }

        private Triangle GetScaledTriangle(Canvas canvas)
        {           
            var zeroPoint = new Point(0, 0);
            var aDistance = GeometryCalculations.CalculateDistance(zeroPoint, PointA);
            var bDistance = GeometryCalculations.CalculateDistance(zeroPoint, PointB);
            var cDistance = GeometryCalculations.CalculateDistance(zeroPoint, PointC);           
            var aVector = new Vector(zeroPoint, PointA);
            var bVector = new Vector(zeroPoint, PointB);
            var cVector = new Vector(zeroPoint, PointC);
            Triangle scaledTriangle = null;

            for (var i = 1 - Constants.ScaleStep; i > 0; i -= 0.05)
            {
                var a = GeometryCalculations.FindPointOnVector(aVector, zeroPoint, aDistance * i);
                var b = GeometryCalculations.FindPointOnVector(bVector, zeroPoint, bDistance * i);
                var c = GeometryCalculations.FindPointOnVector(cVector, zeroPoint, cDistance * i);
                scaledTriangle = new Triangle(a, b, c);
                if (!GeometryCalculations.IsTriangleInsideBoundedCartesianSystem(scaledTriangle, canvas.ActualWidth, canvas.ActualHeight)) continue;
                break;
            }          

            return scaledTriangle;
        }
    }
}
