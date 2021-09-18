using System;
using System.Collections.Generic;
using System.Text;

namespace VectorViewer.Exceptions
{
    /// <summary>
    /// Exception for shape propertoes parser
    /// </summary>
    public class ParseShapePropertiesException: Exception
    {
        public ParseShapePropertiesException(): base()
        {

        }

        public ParseShapePropertiesException(string message): base(message)
        {
           
        }
    }
}
