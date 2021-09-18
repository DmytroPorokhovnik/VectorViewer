using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using VectorViewer.Exceptions;
using VectorViewer.Misc;
using VectorViewer.Shapes;
using VectorViewer.Shapes.Interfaces;

namespace VectorViewer.ShapeReaders
{
    public class JsonShapeReader : IShapeFileReader
    {      
        private const string TypeMarker = "type";
        private const string CircleTypeMarker = "circle";
        private const string TriangleTypeMarker = "triangle";
        private const string LineTypeMarker = "line";       

        /// <param name="filePath">File path from which shapes should be read</param>
        /// <returns>collection of shapes</returns>
        /// <exception cref="InvalidFileException">If file was invalid (dosn't exist, wrong extension, empty).</exception>
        /// /// <exception cref="Newtonsoft.Json.JsonReaderException">If file was invalid (dosn't exist, wrong extension, empty).</exception>      
        public async Task<IEnumerable<IShape>> GetShapes(string filePath)
        {
            if (!IsFileValid(filePath)) throw new InvalidFileException();
            using (var file = File.OpenText(filePath))
            {
                var parsedJson = await JArray.LoadAsync(new JsonTextReader(file), CancellationToken.None);
                var result = new List<IShape>();

                foreach (JObject jsonShapeObj in parsedJson.Children<JObject>())
                {
                    result.Add(GetShapeFromJsonObject(jsonShapeObj));
                }
                return result;
            }
        }

        /// <exception cref="InvalidShapeTypeException">If file was invalid (dosn't exist, wrong extension, empty).</exception>
        private IShape GetShapeFromJsonObject(JObject jObject)
        {
            var type = jObject.Value<string>(TypeMarker);
            IShape result = type switch
            {
                CircleTypeMarker => new Circle(jObject.ToObject<CircleModel>()),
                LineTypeMarker => new Line(jObject.ToObject<LineModel>()),
                TriangleTypeMarker => new Triangle(jObject.ToObject<TriangleModel>()),
                _ => throw new InvalidShapeTypeException(),
            };
            return result;
        }

        private bool IsFileValid(string filePath)
        {
            try
            {
                var fileInfo = new FileInfo(filePath);
                if (!fileInfo.Exists) return false;
                if (fileInfo.Extension != Constants.JsonExtension) return false;
                if (fileInfo.Length == 0) return false;
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
