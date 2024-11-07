namespace Tetris.Engine.Interfaces
{
    #region Delegates for events

    public delegate void GameTickDelegate(IGame game, float dtime);

    public delegate void GameRotateDelegate(IGame game, bool isRight);

    #endregion

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

        public void MoveShape(IShape shape, Point delta);

        public void RewriteCells(Point[] points, IShape? shape);

        #region Events

        public event GameTickDelegate TickEvent;

        public void StartTick(float dt);

        public event GameRotateDelegate RotateEvent;

        public void StartRotate(bool isRight);

        #endregion

        public void AddShape(IShape shape, Point[] Cells, bool isMainShape);
    }
}
