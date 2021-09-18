﻿using System;
using System.Collections.Generic;
using VectorViewer.Models;
using VectorViewer.Misc;
using VectorViewer.Exceptions;
using System.Drawing;
using System.Globalization;

namespace VectorViewer.Parsers
{
    /// <summary>
    /// Basic shape properties class
    /// </summary>
    public class ShapePropertiesParser: IShapePropertiesParser
    {
        private NumberFormatInfo _numberFormatInfo;
        
        public ShapePropertiesParser()
        {
            _numberFormatInfo = new NumberFormatInfo()
            {
                NegativeSign = "-"
            };
        }

        public CartesianPoint ParsePointCoordinate(string coordinate)
        {
            if (string.IsNullOrEmpty(coordinate)) throw new ArgumentNullException(nameof(coordinate));
          
            var coordinateStringArray = coordinate.Trim().Split(Constants.CoordinateDelimeter);
            if (coordinateStringArray.Length != 2) throw new ParseShapePropertiesException("Wrong input string");
            var xResult = double.TryParse(coordinateStringArray[0].Replace(',', '.'), NumberStyles.Any, _numberFormatInfo, out var x);
            var yResult = double.TryParse(coordinateStringArray[1].Replace(',', '.'), NumberStyles.Any, _numberFormatInfo, out var y);
            if (!xResult || !yResult) throw new ParseShapePropertiesException();
            return new CartesianPoint(x, y);
        }

        public Color ParseArgbColor(string colorStr)
        {
            if (string.IsNullOrEmpty(colorStr)) throw new ArgumentNullException(nameof(colorStr));

            var colorStringArray = colorStr.Trim().Split(Constants.ColorStringDelimeter);
            if (colorStringArray.Length != 4) throw new ParseShapePropertiesException("Wrong input string");
            var alphaResult = int.TryParse(colorStringArray[0], out var alpha);
            var redResult = int.TryParse(colorStringArray[1], out var red);
            var greenResult = int.TryParse(colorStringArray[2], out var green);
            var blueResult = int.TryParse(colorStringArray[3], out var blue);

            if (!alphaResult || !redResult || !greenResult || !blueResult) throw new ParseShapePropertiesException();
            if (alpha < 0 || red < 0 || green < 0 || blue < 0) throw new ParseShapePropertiesException();
            return Color.FromArgb(alpha, red, green, blue);
        }

        public double ParseNumber(string numberStr)
        {
            if (string.IsNullOrEmpty(numberStr)) throw new ArgumentNullException(nameof(numberStr));

            var parseResult =  double.TryParse(numberStr.Replace(',', '.'), NumberStyles.Any, _numberFormatInfo, out var number);
            if (!parseResult) throw new ParseShapePropertiesException();
            return number;
        }
    }
}
