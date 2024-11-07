using Tetris.Engine.Interfaces;

namespace Tetris.Engine.Core
{
    internal class Game : IGame
    {
        public List<IShape?> Shapes { get; }

        protected IShape? MainShape { get; set; }

        private IShape?[,] Grid { get; set; }

        public Point Size { get; }

        #region Events

        public event GameTickDelegate? TickEvent;

        public void StartTick(float dt)
        {
            TickEvent?.Invoke(this, dt);
        }

        public event GameRotateDelegate? RotateEvent;

        public void StartRotate(bool isRught)
        {
            RotateEvent?.Invoke(this, isRught);
        }

        #endregion

        public Game(int height, int width)
        {
            Shapes = new List<IShape?>();
            Grid = new IShape?[width, height];
            Size = new(width, height);
        }

        public IShape?[,] GetGrid() => Grid;

        public IShape? GetCellFromGrid(int x, int y) => Grid[x, y];

        public Point[] GetCellsOfShape(IShape shape)
        {
            List<Point> cells = new();
            for(int x = 0; x < Size.X; x++)
            {
                for(int y = 0; y < Size.Y; y++)
                {
                    if (Grid[x, y] ==  shape) cells.Add(new Point(x, y));
                }
            }
            return cells.ToArray();
        }

        public bool IsEmptySpace(Point point) => Grid[point.X, point.Y] == null;

        public bool IsEmptySpace(Point point, IShape shape) => Grid[point.X, point.Y] == null || Grid[point.X, point.Y] == shape;

        public bool IsEmptySpace(Point[] points, IShape shape)
        {
            foreach(Point point in points)
            {
                if(!IsEmptySpace(point, shape)) return false;
            }
            return true;
        }

        public bool IsPointsInsideBorders(Point[] points)
        {
            foreach(Point point in points)
            {
                if (!Enumerable.Range(0, Size.X).Contains(point.X) || !Enumerable.Range(0, Size.Y).Contains(point.Y))
                {
                    return false;
                }
            }

            return true;
        }

        public void MoveShape(IShape shape, Point delta)
        {
            Point[] points = GetCellsOfShape(shape);

            foreach(Point point in points)
            {
                Grid[point.X, point.Y] = null;
            }

            foreach(Point point in points)
            {
                Grid[point.X + delta.X, point.Y + delta.Y] = shape;
            }
        }

        public void RewriteCells(Point[] points, IShape? shape)
        {
            foreach (Point point in points)
            {
                Grid[point.X, point.Y] = shape;
            }
        }

        public void AddShape(IShape shape, Point[] cells, bool isMainShape)
        {
            TickEvent += shape.OnTickHandler;

            if (isMainShape)
            {
                MainShape = shape;
                RotateEvent += shape.OnRotateHandler;
            }

            Shapes.Add(shape);
            foreach (Point point in cells)
            {
                Grid[point.X, point.Y] = shape;
            }
        }
    }
}
