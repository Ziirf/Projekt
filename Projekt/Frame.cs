using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Frame
    {
        List<int> DividerVertical = new List<int>();
        List<int[]> DividerHorizontal = new List<int[]>();

        private int height;
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        private int width;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private int offsetTop;
        public int OffsetTop
        {
            get { return offsetTop; }
            set { offsetTop = value; }
        }

        private int offsetLeft;
        public int OffsetLeft
        {
            get { return offsetLeft; }
            set { offsetLeft = value; }
        }

        public Frame(int width, int height)
        {
            Width = width;
            Height = height;
            OffsetLeft = 0;
            OffsetTop = 0;
        }

        public Frame(int width,int height, int offsetLeft, int offsetTop)
        {
            Width = width;
            Height = height;
            OffsetLeft = offsetLeft;
            OffsetTop = offsetTop;
        }

        public void Print()
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.Write("╔");
            for (int i = 0; i < width - 2; i++)
            {
                Console.Write("═");
            }
            Console.Write("╗");
            for (int i = 0; i < height - 3; i++)
            {
                Console.SetCursorPosition(offsetLeft, offsetTop + i + 1);
                Console.Write("║");
                Console.SetCursorPosition(offsetLeft + width - 1, offsetTop + i + 1);
                Console.Write("║");
            }
            Console.SetCursorPosition(offsetLeft, offsetTop + height - 2);
            Console.Write("╚");
            for (int i = 0; i < width - 2; i++)
            {
                Console.Write("═");
            }
            Console.Write("╝");

            if (DividerVertical.Count > 0)
            {
                for (int i = 0; i < DividerVertical.Count; i++)
                {
                    Console.SetCursorPosition(offsetLeft, OffsetTop + DividerVertical[i]);
                    Console.Write("╠");
                    for (int j = 0; j < width - 2; j++)
                    {
                        Console.Write("═");
                    }
                    Console.Write("╣");
                }
            }

            if (DividerHorizontal.Count > 0)
            {
                for (int i = 0; i < DividerHorizontal.Count; i++)
                {
                    Console.SetCursorPosition(DividerHorizontal[i][0] + offsetLeft, DividerHorizontal[i][1] + offsetTop);
                    Console.Write("╦");
                    for (int j = 0; j < DividerHorizontal[i][2] - DividerHorizontal[i][1] - 1; j++)
                    {
                    Console.SetCursorPosition(DividerHorizontal[i][0] + offsetLeft, DividerHorizontal[i][1] + offsetTop + j + 1);
                        Console.Write("║");
                    }
                    Console.SetCursorPosition(DividerHorizontal[i][0] + offsetLeft, DividerHorizontal[i][2] + offsetTop);
                    Console.Write("╩");
                }
            }
        }

        public void AddVerticalDivider(int row)
        {
            DividerVertical.Add(row);
        }

        public void AddHorizontalDivider(int collumn,int start, int stop)
        {
            int[] output = { collumn, start, stop };
            DividerHorizontal.Add(output);
        }
    }
}
