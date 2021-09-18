using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;
using VectorViewer.Geometry;
using VectorViewer.Shapes;
using Vector = VectorViewer.Geometry.Vector;

namespace VecrorViewerTests
{
    /// <summary>
    /// Test class for shape properties parser
    /// </summary>
    [TestClass]
    public class GeometryCalculationsTests
    {
        [DataTestMethod]
        [DataRow(4, 3, 3, -2, 5.099)]
        [DataRow(0, 0, 0, 0, 0)]
        [DataRow(10, 10, 256, 120.5678, 269.706)]
        public void CalculateDistance_ValidPoints_ReturnsDistance(double x1, double y1, double x2, double y2, double result)
        {
            var distance = GeometryCalculations.CalculateDistance(new Point(x1, y1), new Point(x2, y2));

            Assert.IsTrue(Math.Round(distance, 3) == result, "Wrong distance");
        }

        [DataTestMethod]
        [DataRow(0, 0, 10, 10, 5, 5, 20, 19.142, 19.142)]
        [DataRow(0, 0, 10, 10, 5, 5, 0, 5, 5)]
        public void FindPointOnVector_ValidData_ReturnsPointOnVector(double x1, double y1, double x2, double y2, double x3, double y3,
            double distance, double resultX, double resultY)
        {
            var point = GeometryCalculations.FindPointOnVector(new Vector(new Point(x1, y1), new Point(x2, y2)), new Point(x3, y3), distance);

            Assert.IsTrue(Math.Round(point.X, 3) == resultX && Math.Round(point.Y, 3) == resultY, "Wrong point");
        }
    }
}
