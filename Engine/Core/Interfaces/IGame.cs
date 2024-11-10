namespace Tetris.Engine.Core.Interfaces
{
    public interface IGame
    {
        public List<IShape?> Shapes { get; }

        public Point Size { get; }

        public IShape?[,] GetGrid();

        public IShape? GetCellFromGrid(int x, int y);

        public Point[] GetCellsOfShape(IShape shape);

        public bool IsEmptySpace(Point point);

        public bool IsEmptySpace(Point point, IShape shape);

        public bool IsEmptySpace(Point[] point, IShape shape);

        public bool IsPointsInsideBorders(Point[] point);

        public void CreateNewMainShape(IShape shape);

        public void RewriteCells(Point[] points, IShape? shape);

        #region Events

        public event GameTickDelegate? TickEvent;

        public void StartTick(float dt);

        public event GameRotateMainShapeDelegate? RotateMainShapeEvent;

        public void RotateMainShape(bool isRight);

        public event GameMoveMainShapeDelegate? MoveMainShapeEvent;

        public void MoveMainShape(Point offset);

        #endregion

        public void AddShape(IShape shape, Point[] Cells, bool isMainShape);
    }
}
