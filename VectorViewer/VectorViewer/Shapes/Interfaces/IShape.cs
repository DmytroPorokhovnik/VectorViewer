using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace VectorViewer.Shapes.Interfaces   
{
    /// <summary>
    /// Represents a common shape
    /// </summary>
    public interface IShape
    {
        void Draw(Canvas canvas);

        void Scale(Canvas canvas);
    }
}
