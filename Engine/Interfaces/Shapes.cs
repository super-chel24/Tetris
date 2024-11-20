namespace Tetris.Engine.Interfaces
{
    internal class Shapes
    {
        public ShapeData[] ShapesTypes { get; set; }

        public Shapes(ShapeData[] shapesTypes)
        {
            ShapesTypes = shapesTypes;
        }
    }
}
