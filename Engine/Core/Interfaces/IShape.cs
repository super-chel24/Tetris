namespace Tetris.Engine.Core.Interfaces
{
    public interface IShape
    {
        public event ShapeCantFallHandler? CantFallEvent;

        public void OnTickHandler(IGame game, float dtime);

        public void MoveHandler(IGame game, Point offset);

        public void OnRotateHandler(IGame game, bool isRight);
    }
}
