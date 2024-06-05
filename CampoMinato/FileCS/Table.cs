using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CampoMinato.FileCS
{
    static class Table
    {
        public static Piece[,] field = new Piece[10, 8];

        public static void Start()
        {

            bool[,] aux = new bool[10, 8];
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for(int j = 0; j < field.GetLength(1); j++)
                {
                    aux[i, j] = false;
                }
            }
            int mine = 10;
            while (mine > 0)
            {
                int x = new Random().Next(0, 10);
                int y = new Random().Next(0, 8);
                if (!aux[x, y])
                {
                    aux[x, y] = true;
                    mine--;
                }
            }
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = new Piece(i, j, aux[i, j]);
                    field[i, j].Click += Pressed;
                }
            }
        }

        public static void Pressed(object sender, EventArgs e)
        {
            (sender as Piece).Playing();
        }

        public static void Draw()
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j].Draw(Program.window);
                }
            }
        }
    }
}
