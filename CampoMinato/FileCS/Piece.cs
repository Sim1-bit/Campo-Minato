using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace CampoMinato.FileCS
{
    class Piece : Button
    {
        private static Font font = new Font(@"..\\..\\..\\File\FileTTF\PressStart2P-Regular.ttf");

        public Vector2i index = new Vector2i();
        private bool played;

        private bool isMine;
        public bool IsMine
        {
            get => IsMine;
            set => isMine = true;
        }

        public int NearMine
        {
            get
            {
                int numMine = 0;
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        try
                        {
                            if ((index.X + i != index.X || index.Y + j != index.Y) && Table.field[index.X + i, index.Y + j].isMine)
                                numMine++;
                        }
                        catch (IndexOutOfRangeException)
                        {

                        }

                    }
                }
                return numMine;
            }
        }
        public Piece(int x, int y, bool isMine) : base(new Vector2f(x * Program.proportion + 5 * x, y * Program.proportion + 5 * y), " ", Program.proportion, new Color(100, 100, 100), Color.Black, font)
        {
            index = new Vector2i(x, y);
            this.isMine = isMine;
            shape.OutlineColor = new Color(25, 25, 25);
            shape.OutlineThickness = 2.5f;

            new Thread(() => this.Update(Program.window)).Start();
        }

        public void Show(int numMine)
        {
            if (numMine == 0)
            {
                Message = "";
                PlayingOthers();
            }
            else
                Message = numMine.ToString();
            shape.FillColor = Color.White;

            if (isMine)
                Message = "+";
        }

        private void PlayingOthers()
        {
            if(Message == "" && played)
            {
                CreateThread(index.X - 1, index.Y);
                CreateThread(index.X + 1, index.Y);
                CreateThread(index.X, index.Y - 1);
                CreateThread(index.X, index.Y + 1);
            }
        }
        private void CreateThread(int x, int y)
        {
            try
            {
                Piece aux = Table.field[x, y];
                new Thread(() => aux.Playing()).Start();
            }
            catch (Exception)
            {

            }
        }
        public void Playing()
        {
            if (played)
                return;
            played = true;
            
            Show(NearMine);    
        }

        public override void Update(RenderWindow window)
        {
            while(Program.window.IsOpen)
                base.Update(window);
        }

        public override void Draw(RenderWindow window)
        {
            DrawAspect(window);
        }
    }
}
