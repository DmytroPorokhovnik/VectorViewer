using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using VectorViewer.Exceptions;
using VectorViewer.ShapeReaders;
using VectorViewer.Shapes;

namespace VecrorViewerTests
{
    [TestClass]
    public class JsonShapeReaderTests
    {
        private const string AllShapesFileName = "allShapes.json";
        private const string EmptyFileName = "emptyFile.txt";
        private const string InvalidJsonFileName = "invalid.json";
        private const string InvlidExtFileName = "invalidExt.txt";
        private const string MissingFileName = "miss.json";
        private const string NoShapeFileName = "noShape.json";
        private const string TestFilesRelativePath = @"TestData\jsonFiles";
        private readonly string _workingDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        private readonly IShapeFileReader _shapeReader = new JsonShapeReader();

        [TestMethod]
        public async Task GetShapes_ValidJson_ReturnsAllShapes()
        {
            var shapes = await _shapeReader.GetShapes(Path.Combine(_workingDirectory, TestFilesRelativePath, AllShapesFileName));
            Assert.IsTrue(shapes.Count() == 3, "Wrong shapes number");
            var circles = shapes.Where(shape => shape is Circle).ToList();
            var lines = shapes.Where(shape => shape is Line).ToList();
            var triangles = shapes.Where(shape => shape is Triangle).ToList();
            Assert.IsTrue(circles.Count > 0, "There is no circles");
            Assert.IsTrue(lines.Count > 0, "There is no lines");
            Assert.IsTrue(triangles.Count > 0, "There is no triangles");
            Assert.IsTrue(lines.Count > 0, "There is no lines");
            Assert.IsTrue(triangles.Count > 0, "There is no triangles");
        }

        [TestMethod]
        public async Task GetShapes_EmptyFile_ThrowsInvalidFileException()
        {
            try
            {
                var shapes = await _shapeReader.GetShapes(Path.Combine(_workingDirectory, TestFilesRelativePath, EmptyFileName));
            }
            catch (InvalidFileException)
            {
                return;
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            Assert.Fail("Exception was not thrown");
        }

        [TestMethod]
        public async Task GetShapes_InvalidJson_ThrowsJsonReaderException()
        {
            try
            {
                var shapes = await _shapeReader.GetShapes(Path.Combine(_workingDirectory, TestFilesRelativePath, InvalidJsonFileName));
            }
            catch (JsonReaderException)
            {
                return;
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            Assert.Fail("Exception was not thrown");
        }

        [TestMethod]
        public async Task GetShapes_WrongExtension_ThrowsInvalidFileException()
        {
            try
            {
                var shapes = await _shapeReader.GetShapes(Path.Combine(_workingDirectory, TestFilesRelativePath, InvlidExtFileName));
            }
            catch (InvalidFileException)
            {
                return;
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            Assert.Fail("Exception was not thrown");
        }

        [TestMethod]
        public async Task GetShapes_MissingFile_ThrowsInvalidFileException()
        {
            try
            {
                var shapes = await _shapeReader.GetShapes(Path.Combine(_workingDirectory, TestFilesRelativePath, MissingFileName));
            }
            catch (InvalidFileException)
            {
                return;
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            Assert.Fail("Exception was not thrown");
        }

        [TestMethod]
        public async Task GetShapes_NoShape_ReturnEmptyCollection()
        {
            var shapes = await _shapeReader.GetShapes(Path.Combine(_workingDirectory, TestFilesRelativePath, NoShapeFileName));

            Assert.IsTrue(shapes.Count() == 0, "The shape collection isn't empty");
        }
    }
}
