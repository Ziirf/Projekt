using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Frame
    {
        List<int[]> DividerVertical = new List<int[]>();
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
                    Console.SetCursorPosition(OffsetLeft + DividerVertical[i][1], offsetTop + DividerVertical[i][0]);
                    Console.Write("╠");
                    for (int j = 0; j < DividerVertical[i][2] - DividerVertical[i][1] - 2; j++)
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

        public void AddVerticalDivider(int row, int start, int stop)
        {
            int[] output = { row, start, stop };
            DividerVertical.Add(output);
        }

        public void AddHorizontalDivider(int collumn, int start, int stop)
        {
            int[] output = { collumn, start, stop };
            DividerHorizontal.Add(output);
        }

        public void CustomerOverview(int offsetLeft, int offsetTop, int[] buffer, string[] titles)
        {
            offsetTop += this.OffsetTop;
            offsetLeft += this.OffsetLeft;

            for (int i = 0; i < titles.Length; i++)
            {
                Console.SetCursorPosition(offsetLeft + buffer[i], offsetTop);
                Console.Write(titles[i]);
            }

            for (int i = 0; i < Customer.customerList.Count; i++)
            {
                string[] strArrayCustomer = Customer.customerList[i].CustomerInfo();
                for (int j = 0; j < buffer.Length; j++)
                {
                Console.SetCursorPosition(offsetLeft + buffer[j], offsetTop + 2 + i);
                Console.WriteLine(strArrayCustomer[j]);
                }
            }
        }

        public void CarOverview(int offsetLeft, int offsetTop, int[] buffer, string[] titles)
        {
            offsetTop += this.OffsetTop;
            offsetLeft += this.OffsetLeft;

            for (int i = 0; i < titles.Length; i++)
            {
                Console.SetCursorPosition(offsetLeft + buffer[i], offsetTop);
                Console.Write(titles[i]);
            }

            for (int i = 0; i < Car.carList.Count; i++)
            {
                string[] strArrayCar = Car.carList[i].CarInfo();
                for (int j = 0; j < buffer.Length; j++)
                {
                    Console.SetCursorPosition(offsetLeft + buffer[j], offsetTop + 2 + i);
                    Console.WriteLine(strArrayCar[j]);
                }
            }
        }

        public void CreateCustomer(int offsetLeft, int offsetTop, string[] information)
        {
            offsetTop += this.OffsetTop;
            offsetLeft += this.OffsetLeft;

            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.WriteLine("Customer Infomartion:");
            string[] customerArray = Customer.customerList[0].CustomerInfo(false).ToArray();

            for (int i = 0; i < information.Length; i++)
            {
                Console.SetCursorPosition(offsetLeft, offsetTop + 2 + i);
                Console.Write(information[i] +  ": ");
            }
        }
    }
}
