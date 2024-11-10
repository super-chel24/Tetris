using System.Diagnostics;
using Tetris.Engine.Core.Interfaces;

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
            if (Shapes.Count == 0) CreateNewMainShape(null);

            TickEvent?.Invoke(this, dt);
        }

        public event GameRotateMainShapeDelegate? RotateMainShapeEvent;

        public void RotateMainShape(bool isRught)
        {
            RotateMainShapeEvent?.Invoke(this, isRught);
        }

        public event GameMoveMainShapeDelegate? MoveMainShapeEvent;

        public void MoveMainShape(Point offset)
        {
            MoveMainShapeEvent?.Invoke(this, offset);
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

        public void CreateNewMainShape(IShape? previousShape)
        {
            Point[] points =
            {
                new Point(5, 0),
                new Point(5, 1),
                new Point(4, 0)
            };
            AddShape(new Shape(), points, true); //Real random shape generator will be in future
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
                if(MainShape != null) MainShape.CantFallEvent -= CreateNewMainShape;
                MainShape = shape;
                RotateMainShapeEvent = shape.OnRotateHandler;
                MoveMainShapeEvent = shape.MoveHandler;
                shape.CantFallEvent += CreateNewMainShape;
            }

            Shapes.Add(shape);
            foreach (Point point in cells)
            {
                Grid[point.X, point.Y] = shape;
            }
        }
    }
}
