using System.Text.Json;
using Tetris.Engine.Core.Interfaces;
using Tetris.Engine.Interfaces;

namespace Tetris.Engine
{
    internal class ShapeGeneratorFromJSON : IShapeGenerator
    {
        private IShape? Shape;

        private Point[]? Points;

        private readonly Shapes LoadedShapes;

        public ShapeGeneratorFromJSON(string jsonfile)
        {
            LoadedShapes = JsonSerializer.Deserialize<Shapes>(jsonfile); //TODO: add handling exceptions
            GenerateNewShape();
        }

        public void GenerateNewShape()
        {
            Random random = new();
            ShapeData shape = LoadedShapes.ShapesTypes[random.Next(LoadedShapes.ShapesTypes.Length)];
            Points = shape.Points;
            Shape = (IShape?)Activator.CreateInstance(shape.AssemblyName, shape.ShapeType);
        }

        public Point[]? GetPoints() => Points;

        public IShape? GetShapeType() => Shape;
    }
}
