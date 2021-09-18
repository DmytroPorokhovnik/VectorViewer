using System;
using System.Collections.Generic;
using System.Text;

namespace VectorViewer.Models
{
    /// <summary>
    /// Represents a point in Cartesian system
    /// </summary>
    public class CartesianPoint
    {
        public double X { get; }
        public double Y { get; }

        public CartesianPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
