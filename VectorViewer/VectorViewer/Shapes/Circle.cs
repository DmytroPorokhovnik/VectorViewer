using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using VectorViewer.Shapes.Interfaces;

namespace VectorViewer.Shapes
{
    /// <summary>
    /// Implements IShape as a circle
    /// </summary>
    public class Circle : IShape
    {
        [JsonProperty("center")]
        public string Center { get; set; }

        [JsonProperty("radius")]
        public string Radius { get; set; }

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
