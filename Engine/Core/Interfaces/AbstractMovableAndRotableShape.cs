using System.Diagnostics;

namespace Tetris.Engine.Core.Interfaces
{
    internal abstract class AbstractMovableAndRotableShape : IShape
    {
        public event ShapeCantFallHandler? CantFallEvent;

        public void OnTickHandler(IGame game, float dtime)
        {
            MoveHandler(game, new(0, 1));
        }

        public void OnRotateHandler(IGame game, bool isRight)
        {
            Point[] points = game.GetCellsOfShape(this);

            for (int i = 0; i < points.Length; i++)
            {
                Point centerPoint = points[i];

                Point[] newPoints = new Point[points.Length];

                for (int j = 0; j < newPoints.Length; j++)
                {

                    if (i == j)
                    {
                        newPoints[j].X = centerPoint.X;
                        newPoints[j].Y = centerPoint.Y;
                    }
                    else if (isRight)
                    {
                        newPoints[j].X = centerPoint.X - (points[j].Y - centerPoint.Y);
                        newPoints[j].Y = centerPoint.Y + (points[j].X - centerPoint.X);
                    }
                    else
                    {
                        newPoints[j].X = centerPoint.X + (points[j].Y - centerPoint.Y);
                        newPoints[j].Y = centerPoint.Y - (points[j].X - centerPoint.X);
                    }
                }

                if (!game.IsPointsInsideBorders(newPoints) || !game.IsEmptySpace(newPoints, this))
                {
                    continue;
                }

                game.RewriteCells(points, null);
                game.RewriteCells(newPoints, this);
                return;
            }
        }

        public void MoveHandler(IGame game, Point offset)
        {
            Point[] currentCells = game.GetCellsOfShape(this);
            Point[] newPoints = new Point[currentCells.Length];

            for (Point distance = new(offset.X, offset.Y); distance.X != 0 || distance.Y != 0; distance.Offset(-Math.Sign(distance.X), -Math.Sign(distance.Y)))
            {
                for (int i = 0; i < currentCells.Length; i++)
                {
                    newPoints[i].X = currentCells[i].X + distance.X;
                    newPoints[i].Y = currentCells[i].Y + distance.Y;
                }

                if (!game.IsPointsInsideBorders(newPoints) || !game.IsEmptySpace(newPoints, this))
                {
                    continue;
                }

                game.RewriteCells(currentCells, null);
                game.RewriteCells(newPoints, this);
                return;
            }

            if (offset.Y != 0) CantFallEvent?.Invoke(this);
        }
    }
}
