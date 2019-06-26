using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Frame
    {
        List<int[]> DividerHorizontal = new List<int[]>();
        List<int[]> DividerVertical = new List<int[]>();

        public int Height { get; private set; }

        public int Width { get; private set; }

        public int OffsetTop { get; private set; }

        public int OffsetLeft { get; private set; }

        public Frame(int[] frameDim)
        {
            Width = frameDim[0];
            Height = frameDim[1];
            OffsetLeft = frameDim[2];
            OffsetTop = frameDim[3];
        }

        public Frame(int width, int height)
        {
            Width = width;
            Height = height;
            OffsetLeft = 0;
            OffsetTop = 0;
        }

        public Frame(int width, int height, int offsetLeft, int offsetTop)
        {
            Width = width;
            Height = height;
            OffsetLeft = offsetLeft;
            OffsetTop = offsetTop;
        }

        public void Print()
        {
            Console.SetCursorPosition(OffsetLeft, OffsetTop);
            Console.Write("╔");
            for (int i = 0; i < Width - 2; i++)
            {
                Console.Write("═");
            }
            Console.Write("╗");
            for (int i = 0; i < Height - 3; i++)
            {
                Console.SetCursorPosition(OffsetLeft, OffsetTop + i + 1);
                Console.Write("║");
                Console.SetCursorPosition(OffsetLeft + Width - 1, OffsetTop + i + 1);
                Console.Write("║");
            }
            Console.SetCursorPosition(OffsetLeft, OffsetTop + Height - 2);
            Console.Write("╚");
            for (int i = 0; i < Width - 2; i++)
            {
                Console.Write("═");
            }
            Console.Write("╝");

            if (DividerHorizontal.Count > 0)
            {
                for (int i = 0; i < DividerHorizontal.Count; i++)
                {
                    Console.SetCursorPosition(OffsetLeft + DividerHorizontal[i][1], OffsetTop + DividerHorizontal[i][0]);
                    Console.Write("╠");
                    for (int j = 0; j < DividerHorizontal[i][2] - DividerHorizontal[i][1] - 2; j++)
                    {
                        Console.Write("═");
                    }
                    Console.Write("╣");
                }
            }

            if (DividerVertical.Count > 0)
            {
                for (int i = 0; i < DividerVertical.Count; i++)
                {
                    Console.SetCursorPosition(DividerVertical[i][0] + OffsetLeft, DividerVertical[i][1] + OffsetTop);
                    Console.Write("╦");
                    for (int j = 0; j < DividerVertical[i][2] - DividerVertical[i][1] - 1; j++)
                    {
                        Console.SetCursorPosition(DividerVertical[i][0] + OffsetLeft, DividerVertical[i][1] + OffsetTop + j + 1);
                        Console.Write("║");
                    }
                    Console.SetCursorPosition(DividerVertical[i][0] + OffsetLeft, DividerVertical[i][2] + OffsetTop);
                    Console.Write("╩");
                }
            }
        }

        public void AddHorizontalDivider(int row, int start, int stop)
        {
            int[] output = { row, start, stop };
            DividerHorizontal.Add(output);
        }

        public void AddVerticalDivider(int collumn, int start, int stop)
        {
            int[] output = { collumn, start, stop };
            DividerVertical.Add(output);
        }
    }
}
