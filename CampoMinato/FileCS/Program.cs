using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using CampoMinato.FileCS;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace CampoMinato
{
    class Program
    {
        public static RenderWindow window;
        public const int proportion = 100;
        static void Main(string[] args)
        {
            window = new RenderWindow(new VideoMode((proportion + 5) * 10, (proportion + 5) * 8), "Campo Minato");
            window.SetVerticalSyncEnabled(true);
            window.Closed += (sender, args) => Close();
            Logger.Grade = 600;
            Table.Start();
            Draw();
        }

        public static void Draw()
        {
            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear();

                Table.Draw();

                window.Display();
            }

        }

        public static void Close()
        {
            window.Close();
        }
    }
}
