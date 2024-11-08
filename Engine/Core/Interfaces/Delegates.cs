using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Engine.Core.Interfaces;

namespace Tetris.Engine.Core.Interfaces
{
    #region Delegates for events

    public delegate void GameTickDelegate(IGame game, float dtime);

    public delegate void GameRotateDelegate(IGame game, bool isRight);

    #endregion
}
