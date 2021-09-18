using Newtonsoft.Json;

namespace VectorViewer.Shapes
{
    /// <summary>
    /// Triangle model
    /// </summary>
    public class TriangleModel
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
    }
}
