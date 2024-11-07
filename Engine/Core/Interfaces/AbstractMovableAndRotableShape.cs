using System.Diagnostics;
using Tetris.Engine.Interfaces;

namespace Tetris.Engine.Core.Interfaces
{
    internal abstract class AbstractMovableAndRotableShape : IShape
    {

        protected void VerticalMove(IGame game, int distance)
        {
            Point[] currentCells = game.GetCellsOfShape(this);
            Point[] newPoints = new Point[currentCells.Length];

            for (int dy = distance; dy != 0; dy -= Math.Sign(distance))
            {
                for (int i = 0; i < currentCells.Length; i++)
                {
                    newPoints[i].X = currentCells[i].X;
                    newPoints[i].Y = currentCells[i].Y + dy;
                }

                if (!game.IsPointsInsideBorders(newPoints) || !game.IsEmptySpace(newPoints, this))
                {
                    continue;
                }

                game.RewriteCells(currentCells, null);
                game.RewriteCells(newPoints, this);
                return;
            }

            
        }

        protected void SideMove(IGame game, int distance)
        {
            Point[] currentCells = game.GetCellsOfShape(this);

            for (int dx = Math.Sign(distance); Math.Abs(dx) <= Math.Abs(distance); dx += Math.Sign(distance))
            {
                foreach (Point cell in currentCells)
                {
                    if (cell.X + dx >= game.Size.X || cell.X + dx < 0)
                    {
                        return;
                    }
                    else if (!game.IsEmptySpace(new Point(cell.X + dx, cell.Y), this))
                    {
                        return;
                    }
                }
            }

            game.MoveShape(this, new Point(distance, 0));
        }

        public void OnTickHandler(IGame game, float dtime)
        {
            VerticalMove(game, 4);
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
                foreach (Point point in newPoints)
                {
                    Debug.WriteLine("Rotate to: " +  point.X + " " + point.Y);
                }
                game.RewriteCells(newPoints, this);
                return;
            }
        }
    }
}
