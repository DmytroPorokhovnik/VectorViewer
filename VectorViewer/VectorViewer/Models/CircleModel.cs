using Newtonsoft.Json;

namespace VectorViewer.Shapes
{
    /// <summary>
    /// Circle model
    /// </summary>
    public class CircleModel 
    {
        [JsonProperty("center")]
        public string Center { get; set; }

        [JsonProperty("radius")]
        public string Radius { get; set; }

        [JsonProperty("filled")]
        public bool IsFilled { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }      
    }
}
