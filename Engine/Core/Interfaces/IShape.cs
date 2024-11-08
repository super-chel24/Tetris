namespace Tetris.Engine.Core.Interfaces
{
    public interface IShape
    {
        public void OnTickHandler(IGame game, float dtime);

        public void OnRotateHandler(IGame game, bool isRight);
    }
}
