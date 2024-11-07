namespace Tetris.Engine.Interfaces
{
    public interface IShape
    {
        public void OnTickHandler(IGame game, float dtime);

        public void OnRotateHandler(IGame game, bool isRight);
    }
}
