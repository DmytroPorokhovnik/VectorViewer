using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Media;
using VectorViewer.Exceptions;
using VectorViewer.Parsers;

namespace VecrorViewerTests
{
    /// <summary>
    /// Test class for shape properties parser
    /// </summary>
    [TestClass]
    public class ShapePropertiesParserTests
    {
        private readonly IShapePropertiesParser _parser = new ShapePropertiesParser();

        [DataTestMethod]
        [DataRow("-1,5; 3,4", -1.5, 3.4)]
        [DataRow("-1.5; 3.4", -1.5, 3.4)]
        [DataRow("-15; -254", -15, -254)]
        [DataRow("110450; 654105", 110450, 654105)]
        [DataRow(" 110450 ;  654105  ", 110450, 654105)]
        public void ParsePointCoordinate_ValidPoints_ReturnsParsedPoints(string pointCoordinate, double x, double y)
        {
            var point = _parser.ParsePointCoordinate(pointCoordinate);

            Assert.IsTrue(point.X == x && point.Y == y, "Wrong parsed point coorinate");         
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void ParsePointCoordinate_EmptyString_ThrowsAgrumentNullException(string pointCoordinate)
        {
            try
            {
                var point = _parser.ParsePointCoordinate(pointCoordinate);
            }
            catch (ArgumentNullException)
            {
                return;
            }
            Assert.Fail("No ArgumentNullException was thrown");
        }

        [DataTestMethod]
        [DataRow("-1,5;")]
        [DataRow("-1,5; 3,4; 5; 6; 7; 5;")]
        public void ParsePointCoordinate_InvalidPointDimension_ThrowsParseShapePropertiesException(string pointCoordinate)
        {
            try
            {
                var point = _parser.ParsePointCoordinate(pointCoordinate);
            }
            catch (ParseShapePropertiesException)
            {
                return;
            }
            Assert.Fail("No ParseShapePropertiesException was thrown");
        }

        [DataTestMethod]
        [DataRow("127; 255; 255; 255", 127, 255, 255, 255)]
        [DataRow("127; 255; 0; 0", 127, 255, 0, 0)]
        [DataRow("215; 255; 255; 0", 215, 255, 255, 0)]
        public void ParseArgbColor_ValidColor_ReturnsParsedColor(string colorArgb, int alpha, int red, int green, int blue)
        {
            var parsedColor = _parser.ParseArgbColor(colorArgb);

            Assert.IsTrue(Color.FromArgb((byte)alpha, (byte)red, (byte)green, (byte)blue) == parsedColor, "Wrong parsed color");
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void ParseArgbColor_EmptyString_ThrowsAgrumentNullException(string colorStr)
        {
            try
            {
                var color = _parser.ParseArgbColor(colorStr);
            }
            catch (ArgumentNullException)
            {
                return;
            }
            Assert.Fail("No ArgumentNullException was thrown");
        }

        [DataTestMethod]
        [DataRow("127; 255; 255;")]
        [DataRow("-127; 255; 255; 255")]
        [DataRow("127.5; 255; 255; 255")]
        public void ParseArgbColor_InvalidColorStr_ThrowsParseShapePropertiesException(string colorStr)
        {
            try
            {
                var color = _parser.ParseArgbColor(colorStr);
            }
            catch (ParseShapePropertiesException)
            {
                return;
            }
            Assert.Fail("No ParseShapePropertiesException was thrown");
        }

        [DataTestMethod]
        [DataRow("-1,5", -1.5)]
        [DataRow("1.5", 1.5)]
        [DataRow("110450", 110450)]
        [DataRow("-110450", -110450)]
        public void ParseNumber_ValidNumber_ReturnsParsedNumber(string numberStr, double number)
        {
            var parsedNumber = _parser.ParseNumber(numberStr);

            Assert.IsTrue(parsedNumber == number, "Wrong parsed point coorinate");
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void ParseNumber_EmptyString_ThrowsAgrumentNullException(string number)
        {
            try
            {
                var parsedNumber = _parser.ParseNumber(number);
            }
            catch (ArgumentNullException)
            {
                return;
            }
            Assert.Fail("No ArgumentNullException was thrown");
        }

        [DataTestMethod]
        [DataRow("-1,5;")]
        [DataRow("hgyfuk")]
        public void ParseNumber_InvalidNumber_ThrowsParseShapePropertiesException(string number)
        {
            try
            {
                var parsedNumber = _parser.ParseNumber(number);
            }
            catch (ParseShapePropertiesException)
            {
                return;
            }
            Assert.Fail("No ParseShapePropertiesException was thrown");
        }

    }
}
