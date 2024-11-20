using System.Text.Json;
using System.IO;
using Tetris.Engine.Interfaces;
using Tetris.Engine.Core;

namespace Tetris
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*Shapes shapes = new(
                new Shape[]
                {
                    new Shape(
                        new Point[]
                        {
                            new Point(0, 0),
                            new Point(0, 1),
                            new Point(1, 0)
                        },
                        new StandardShape()
                        ),

                    new Shape(
                        new Point[]
                        {
                            new Point(0, 0),
                            new Point(0, 1),
                            new Point(0, 2)
                        },
                        new StandardShape()
                        )
                }
                );

            Directory.CreateDirectory("data");
            using (StreamWriter writer = new("data/shapes.json", false))
            {
                writer.Write(JsonSerializer.Serialize(shapes, new JsonSerializerOptions() { WriteIndented = true}));
            }*/

            ApplicationConfiguration.Initialize();
            Application.Run(new GameForm());
        }
    }
}