using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Engine.Core.Interfaces
{
    internal interface IShapeGenerator
    {
        public void GenerateNewShape();
        
        public IShape? GetShapeType();

        public Point[]? GetPoints();
    }
}
