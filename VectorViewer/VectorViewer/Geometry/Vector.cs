using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace VectorViewer.Geometry
{
    /// <summary>
    /// Represents a 2d vector
    /// </summary>
    public struct Vector
    {
        public readonly double X;
        public readonly double Y;

        public double Length
        {
            get
            {
                double aSq = Math.Pow(X, 2);
                double bSq = Math.Pow(Y, 2);
                return Math.Sqrt(aSq + bSq);
            }
        }

        public Vector UnitVector
        {
            get
            {
                if (Length == 0) return new Vector(0, 0);
                return this / Length;
            }
        }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector(Point p): this(p.X, p.Y)
        {           
        }

        public Vector (Point a, Point b)
        {
            X = b.X - a.X;
            Y = b.Y - a.Y;
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(b.X - a.X, b.Y - a.Y);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator *(Vector a, double d)
        {
            return new Vector(a.X * d, a.Y * d);
        }

        public static Vector operator /(Vector a, double d)
        {
            return new Vector(a.X / d, a.Y / d);
        }
    }
}
