using System.Diagnostics;

namespace Tetris.Engine.Core.Interfaces
{
    internal abstract class AbstractMovableAndRotableShape : IShape
    {
        public event ShapeCantFallHandler? CantFallEvent;

        protected void VerticalMove(IGame game, int distance)
        {
            if (distance == 0) return;
            Point[] currentPoints = game.GetCellsOfShape(this);
            Point[] newPoints = new Point[currentPoints.Length];

            for (int dy = Math.Sign(distance); Math.Abs(dy) <= Math.Abs(distance); dy += Math.Sign(distance))
            {
                for (int i = 0; i < currentPoints.Length; i++)
                {
                    newPoints[i].X = currentPoints[i].X;
                    newPoints[i].Y = currentPoints[i].Y + dy;
                }

                if (!game.IsPointsInsideBorders(newPoints) || !game.IsEmptySpace(newPoints, this))
                {
                    for (int i = 0; i < newPoints.Length; i++)
                    {
                        newPoints[i].Y -= Math.Sign(distance);
                    }

                    if (distance != 0 && dy == 1) CantFallEvent?.Invoke(this);
                    break;
                }
            }
            game.RewriteCells(currentPoints, null);
            game.RewriteCells(newPoints, this);

            
        }

        protected void SideMove(IGame game, int distance)
        {
            if (distance == 0) return;
            Point[] currentCells = game.GetCellsOfShape(this);
            Point[] newPoints = new Point[currentCells.Length];

            for (int dx = Math.Sign(distance); Math.Abs(dx) <= Math.Abs(distance); dx += Math.Sign(distance))
            {
                for (int i = 0; i < currentCells.Length; i++)
                {
                    newPoints[i].X = currentCells[i].X + dx;
                    newPoints[i].Y = currentCells[i].Y;
                }

                if (!game.IsPointsInsideBorders(newPoints) || !game.IsEmptySpace(newPoints, this))
                {
                    for (int i = 0; i < newPoints.Length; i++)
                    {
                        newPoints[i].X -= Math.Sign(distance);
                    }

                    break;
                }
            }

            game.RewriteCells(currentCells, null);
            game.RewriteCells(newPoints, this);
        }

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
            SideMove(game, offset.X);
            VerticalMove(game, offset.Y);
        }
    }
}
