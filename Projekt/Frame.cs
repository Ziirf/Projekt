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

        // Need further work
        public void CustomerOverview(int offsetLeft, int offsetTop, int[] buffer)
        {
            offsetTop += this.OffsetTop;
            offsetLeft += this.OffsetLeft;

            Console.SetCursorPosition(offsetLeft + buffer[0], offsetTop);
            Console.Write("#ID");
            Console.SetCursorPosition(offsetLeft + buffer[1], offsetTop);
            Console.Write("FirstName");
            Console.SetCursorPosition(offsetLeft + buffer[2], offsetTop);
            Console.Write("LastName");
            Console.SetCursorPosition(offsetLeft + buffer[3], offsetTop);
            Console.WriteLine("Zip Code");
            Console.SetCursorPosition(offsetLeft + buffer[4], offsetTop);
            Console.WriteLine("City");
            Console.SetCursorPosition(offsetLeft + buffer[5], offsetTop);
            Console.Write("Phone Number");
            Console.SetCursorPosition(offsetLeft + buffer[6], offsetTop);
            Console.Write("Email");
            Console.SetCursorPosition(offsetLeft + buffer[7], offsetTop);
            Console.Write("Customer Since");
            
            for (int i = 0; i < Customer.customerList.Count; i++)
            {
                Console.SetCursorPosition(offsetLeft + buffer[0], offsetTop + 2 + i);
                Console.WriteLine(Customer.customerList[i].CustomerID);
                Console.SetCursorPosition(offsetLeft + buffer[1], offsetTop + 2 + i);
                Console.WriteLine(Customer.customerList[i].Firstname);
                Console.SetCursorPosition(offsetLeft + buffer[2], offsetTop + 2 + i);
                Console.WriteLine(Customer.customerList[i].Lastname);
                Console.SetCursorPosition(offsetLeft + buffer[3], offsetTop + 2 + i);
                Console.WriteLine(Customer.customerList[i].ZipCode);
                Console.SetCursorPosition(offsetLeft + buffer[4], offsetTop + 2 + i);
                Console.WriteLine(Customer.customerList[i].City);
                Console.SetCursorPosition(offsetLeft + buffer[5], offsetTop + 2 + i);
                Console.WriteLine(Customer.customerList[i].PhoneNumber);
                Console.SetCursorPosition(offsetLeft + buffer[6], offsetTop + 2 + i);
                Console.WriteLine(Customer.customerList[i].EMail);
                Console.SetCursorPosition(offsetLeft + buffer[7], offsetTop + 2 + i);
                Console.WriteLine(Customer.customerList[i].CreatedDate.ToString("dd-MM-yyyy"));
            }
        }

        public void CarOverview(int offsetLeft, int offsetTop, int[] buffer)
        {
            offsetTop += this.OffsetTop;
            offsetLeft += this.OffsetLeft;

            Console.SetCursorPosition(offsetLeft + buffer[0], offsetTop);
            Console.Write("CusID");
            Console.SetCursorPosition(offsetLeft + buffer[1], offsetTop);
            Console.Write("VinNumber");
            Console.SetCursorPosition(offsetLeft + buffer[2], offsetTop);
            Console.Write("Number Plate");
            Console.SetCursorPosition(offsetLeft + buffer[3], offsetTop);
            Console.WriteLine("Car Brand");
            Console.SetCursorPosition(offsetLeft + buffer[4], offsetTop);
            Console.WriteLine("Car Model");
            Console.SetCursorPosition(offsetLeft + buffer[5], offsetTop);
            Console.Write("Production Year");
            Console.SetCursorPosition(offsetLeft + buffer[6], offsetTop);
            Console.Write("Km Count");
            Console.SetCursorPosition(offsetLeft + buffer[7], offsetTop);
            Console.Write("Fuel Type");

            for (int i = 0; i < Car.carList.Count; i++)
            {
                Console.SetCursorPosition(offsetLeft + buffer[0], offsetTop + 2 + i);
                Console.WriteLine(Car.carList[i].CustomerID);
                Console.SetCursorPosition(offsetLeft + buffer[1], offsetTop + 2 + i);
                Console.WriteLine(Car.carList[i].VinNumber);
                Console.SetCursorPosition(offsetLeft + buffer[2], offsetTop + 2 + i);
                Console.WriteLine(Car.carList[i].NumberPlate);
                Console.SetCursorPosition(offsetLeft + buffer[3], offsetTop + 2 + i);
                Console.WriteLine(Car.carList[i].CarBrand);
                Console.SetCursorPosition(offsetLeft + buffer[4], offsetTop + 2 + i);
                Console.WriteLine(Car.carList[i].CarModel);
                Console.SetCursorPosition(offsetLeft + buffer[5], offsetTop + 2 + i);
                Console.WriteLine(Car.carList[i].ProductionYear);
                Console.SetCursorPosition(offsetLeft + buffer[6], offsetTop + 2 + i);
                Console.WriteLine(Car.carList[i].KmCount);
                Console.SetCursorPosition(offsetLeft + buffer[7], offsetTop + 2 + i);
                Console.WriteLine(Car.carList[i].FuelType);
            }
        }
    }
}
