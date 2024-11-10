using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Engine.Core.Interfaces;

namespace Tetris.Engine.Core.Interfaces
{
    #region Delegates for IGame events

    public delegate void GameTickDelegate(IGame game, float dtime);

    public delegate void GameRotateMainShapeDelegate(IGame game, bool isRight);

    public delegate void GameMoveMainShapeDelegate(IGame game, Point offset);

    #endregion

    #region Delegates for IShape events

    public delegate void ShapeCantFallHandler(IShape sender);

    #endregion
}
