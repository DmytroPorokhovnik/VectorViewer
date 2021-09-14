using Newtonsoft.Json;
using System;
using VectorViewer.Shapes.Interfaces;

namespace VectorViewer.Shapes
{
    /// <summary>
    /// Implements IPolygon as a triangle
    /// </summary>
    public class Triangle : IPolygon
    {
        [JsonProperty("a")]
        public string PointA { get; set; }

        [JsonProperty("b")]
        public string PointB { get; set; }

        [JsonProperty("c")]
        public string PointC { get; set; }

        [JsonProperty("filled")]
        public bool IsFilled { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }


        public void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
