using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Frame
    {
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

        private int offsetVertical;
        public int OffsetVertical
        {
            get { return offsetVertical; }
            set { offsetVertical = value; }
        }

        private int offsetHorizontal;
        public int OffsetHorizontal
        {
            get { return offsetHorizontal; }
            set { offsetHorizontal = value; }
        }

        public Frame(int height, int width)
        {
            Height = height;
            Width = width;
            OffsetVertical = 0;
            OffsetHorizontal = 0;
        }

        public Frame(int height, int width, int offsetVertical, int offsetHorizontal)
        {
            Height = height;
            Width = width;
            OffsetVertical = offsetVertical;
            OffsetHorizontal = offsetHorizontal;
        }

        public void HorizontalDivider()
        {

        }

        public void Print()
        {
            Console.SetCursorPosition(offsetHorizontal, offsetVertical);
            Console.Write("╔");
            for (int i = 0; i < width - 2; i++)
            {
                Console.Write("═");
            }
            Console.Write("╗");
            for (int i = 0; i < height - 2; i++)
            {
                Console.SetCursorPosition(offsetHorizontal, offsetVertical + i + 1);
                Console.WriteLine("║");
                Console.SetCursorPosition(offsetHorizontal + width - 1, offsetVertical + i + 1);
                Console.WriteLine("║");
            }
            Console.SetCursorPosition(offsetHorizontal, offsetVertical + height - 1);
            Console.Write("╚");
            for (int i = 0; i < width - 2; i++)
            {
                Console.Write("═");
            }
            Console.Write("╝");
        }
    }
}
