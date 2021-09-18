using System;
using System.Windows;
using VectorViewer.Shapes;

namespace VectorViewer.Geometry
{
    /// <summary>
    /// Provides geometry calculations
    /// </summary>
    public static class GeometryCalculations
    {

        /// <summary>
        /// Calculates distance beetween two points
        /// </summary>
        public static double CalculateDistance(Point x, Point y)
        {
            var sumOfquadraticCoord = Math.Pow(x.X - y.X, 2) + Math.Pow(x.Y - y.Y, 2);
            return Math.Pow(sumOfquadraticCoord, 0.5);
        }

        /// <summary>
        /// Calculates if triangle fits into s cartesian system with some bounds (rectangle)
        /// </summary>
        public static bool IsTriangleInsideBoundedCartesianSystem(Triangle triangle, double width, double height)
        {
            if (triangle.PointA.IsPointInsideCartesianBoundedSystem(width, height) && triangle.PointB.IsPointInsideCartesianBoundedSystem(width, height)
                && triangle.PointC.IsPointInsideCartesianBoundedSystem(width, height)) return true;
            return false;
        }

        /// <summary>
        /// Calculates if line fits into s cartesian system with some bounds (rectangle)
        /// </summary>
        public static bool IsLineInsideBoundedCartesianSystem(Line line, double width, double height)
        {
            if (line.PointA.IsPointInsideCartesianBoundedSystem(width, height) && line.PointB.IsPointInsideCartesianBoundedSystem(width, height))
                return true;
            return false;
        }

        /// <summary>
        /// Calculates if circle fits into s cartesian system with some bounds (rectangle)
        /// </summary>
        public static bool IsCircleInsideBoundedCartesianSystem(Circle circle, double width, double height)
        {
            if (Math.Abs(circle.Center.X) + circle.Radius < width / 2 && Math.Abs(circle.Center.Y) + circle.Radius < height / 2)
                return true;
            return false;
        }

        /// <summary>
        /// Calculates if point fits into s cartesian system with some bounds (rectangle)
        /// </summary>
        public static bool IsPointInsideCartesianBoundedSystem(this Point point, double width, double height)
        {
            return Math.Abs(point.X) < width / 2 && Math.Abs(point.Y) < height / 2;
        }

        public static Point FindPointOnVector(Vector vector, Point startPoint, double distance)
        {
            var x = startPoint.X + vector.UnitVector.X * distance;
            var y = startPoint.Y + vector.UnitVector.Y * distance;
            return new Point(x, y);
        }
    }
}
