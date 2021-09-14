using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using VectorViewer.Shapes.Interfaces;

namespace VectorViewer.Shapes
{
    /// <summary>
    /// Implements IShape as a line
    /// </summary>
    public class Line : IShape
    {
        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("a")]
        public string PointA { get; set; }

        [JsonProperty("b")]
        public string PointB { get; set; }

        public void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
