using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Tetris.Engine.Interfaces;
using Tetris.Engine.Core;
using Tetris.Engine.Core.Interfaces;
using System.Diagnostics;

namespace Tetris
{
    public partial class GameForm : Form
    {
        public System.Windows.Forms.Timer TickTimer { get; set; }

        public System.Windows.Forms.Timer FrameTimer { get; set; }

        public IGame Game { get; set; }

        public IShape shape = new Shape();

        public Point GridSize { get; set; }

        public GameForm()
        {
            InitializeComponent();
            TickTimer = new System.Windows.Forms.Timer();
            TickTimer.Tick += OnGameTickHandler;
            TickTimer.Interval = 1000;
            FrameTimer = new System.Windows.Forms.Timer();
            FrameTimer.Tick += OnFrameTickHandler;
            FrameTimer.Interval = 100;

            GridSize = new Point(10, 10);
            Game = new Game(GridSize.X, GridSize.Y);

            #region Debug code (delete in future)

            Point[] points =
            {
                new Point(0, 8)
            };
            Game.AddShape(new Shape(), points, false);

            points = new Point[]
            {
                new Point(5, 3),
                new Point(5, 4)
            };
            Game.AddShape(shape, points, true);

            #endregion

            #region Initialize window (because constructor dont like editing InitilizeComponent

            GameGridOutPut.Size = new(GridSize.X * CellSizeDisplay, GridSize.Y * CellSizeDisplay);

            #endregion

            TickTimer.Start();
            FrameTimer.Start();
        }

        private void OnFrameTickHandler(object? sender, EventArgs e)
        {
            Refresh();
        }

        private void OnGameTickHandler(object? sender, EventArgs e)
        {
            foreach (Point point in Game.GetCellsOfShape(shape))
            {
                Debug.WriteLine(point.X + " " + point.Y);
            }

            Game.StartTick(TickTimer.Interval);
        }

        private void GameGridPaintHandler(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            IShape?[,] shapes = Game.GetGrid();

            g.Clear(Color.White);

            for (int y = 0; y < Game.Size.Y; y++)
            {
                for (int x = 0; x < Game.Size.X; x++)
                {
                    if (shapes[x, y] != null)
                    {
                        g.FillRectangle(Brushes.Black, x * CellSizeDisplay, y * CellSizeDisplay, CellSizeDisplay, CellSizeDisplay);
                    }
                }
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.E:
                    Game.StartRotate(true);
                    break;
                case Keys.Q:
                    Game.StartRotate(false); 
                    break;
            }
        }
    }
}
