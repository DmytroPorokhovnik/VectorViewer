using System.Collections.Generic;
using System.Threading.Tasks;
using VectorViewer.Shapes.Interfaces;

namespace VectorViewer.ShapeReaders
{
    /// <summary>
    /// Represents a reader for shapes
    /// </summary>
    public interface IShapeFileReader
    {
        Task<IEnumerable<IShape>> GetShapes(string filePath);
    }
}
