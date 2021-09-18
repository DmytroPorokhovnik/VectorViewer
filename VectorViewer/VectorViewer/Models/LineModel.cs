using Newtonsoft.Json;

namespace VectorViewer.Shapes
{
    /// <summary>
    /// Line model
    /// </summary>
    public class LineModel
    {
        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("a")]
        public string PointA { get; set; }

        [JsonProperty("b")]
        public string PointB { get; set; }
    }
}
