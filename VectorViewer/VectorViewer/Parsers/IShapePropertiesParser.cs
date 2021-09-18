using System.Windows;
using System.Windows.Media;

namespace VectorViewer.Parsers
{
    /// <summary>
    /// Represents a parser for shapes properties
    /// </summary>
    public interface IShapePropertiesParser
    {
        /// <summary>
        /// Parses  2D pointcoordinate string 
        /// </summary>
        /// <param name="coordinates">coordinates string</param>
        /// <returns>cartesian coordinate</returns>
        Point ParsePointCoordinate(string coordinate);

        /// <summary>
        /// Parses color string to Color object
        /// </summary>
        /// <param name="color">color string</param>
        /// <returns>color object</returns>
        Color ParseArgbColor(string color);

        /// <summary>
        /// Parses string number
        /// </summary>
        /// <param name="number">number string</param>
        /// <returns>parsed number as double</returns>
        double ParseNumber(string number);
    }
}
