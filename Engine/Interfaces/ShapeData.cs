using Tetris.Engine.Core.Interfaces;

namespace Tetris.Engine.Interfaces
{
    public class ShapeData
    {
        public Point[] Points { get; set; }

        public string ShapeType { get; set; }

        public string AssemblyName { get; set; }

        public ShapeData(Point[] points, IShape shape)
        {
            Points = points;
            ShapeType = shape.GetType().Name;
            AssemblyName = shape.GetType().Assembly.FullName;
        }
    }
}
