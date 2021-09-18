using System;
using System.Windows.Media;
using Polygon = System.Windows.Shapes.Polygon;
using System.Windows.Controls;
using Newtonsoft.Json;
using VectorViewer.Parsers;
using VectorViewer.Shapes.Interfaces;
using System.Collections.Generic;
using System.Windows;
using VectorViewer.Misc;

namespace VectorViewer.Shapes
{
    /// <summary>
    /// Implements IPolygon as a triangle
    /// </summary>
    public class Triangle : IPolygon
    {
        private IShapePropertiesParser _shapePropertiesParser;       

        public Color Color { get; }
        public Point PointA { get; }
        public Point PointB { get; }
        public Point PointC { get; }        
        public bool IsFilled { get; set; }

        public Triangle (TriangleModel triangleModel)
        {
            _shapePropertiesParser = new ShapePropertiesParser();
            PointA = _shapePropertiesParser.ParsePointCoordinate(triangleModel.PointA);
            PointB = _shapePropertiesParser.ParsePointCoordinate(triangleModel.PointB);
            PointC = _shapePropertiesParser.ParsePointCoordinate(triangleModel.PointC);
            Color = _shapePropertiesParser.ParseArgbColor(triangleModel.Color);
            IsFilled = triangleModel.IsFilled;
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

            canvas.Children.Add(triangle);
        }

        public void Scale(Canvas canvas)
        {
            throw new NotImplementedException();
        }
    }
}
