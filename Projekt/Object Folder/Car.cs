using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Car : IObjects
    {
        public static List<Car> carList = new List<Car>();
        public static int[] buffer = { 0, 7, 26, 42, 60, 78, 88, 103, 117 };

        public int CustomerID { get; set; }

        public string VinNumber { get; set; }

        public string NumberPlate { get; set; }

        public string CarBrand { get; set; }

        public string CarModel { get; set; }

        public int ProductionYear { get; set; }

        public int KmCount { get; set; }

        public string FuelType { get; set; }

        public DateTime CreatedDate { get; set; }

        public string StringFormat { get; set; }


        public Car(int customerID, string vinNumber, string numberPlate, string carBrand, string carModel, int productionYear, int kmCount, string fuelType, DateTime createdDate)
        {
            CustomerID = customerID;
            VinNumber = vinNumber;
            NumberPlate = numberPlate;
            CarBrand = carBrand;
            CarModel = carModel;
            ProductionYear = productionYear;
            KmCount = kmCount;
            FuelType = fuelType;
            CreatedDate = createdDate;
            StringFormat = Tool.FormatString(buffer, Info());
        }

        public string[] Info()
        {
            string[] car = { CustomerID.ToString(), VinNumber, NumberPlate, CarBrand, CarModel, ProductionYear.ToString(), KmCount.ToString(), FuelType, CreatedDate.ToString("dd-MM-yyyy") };

            return car;
        }

        public static void Create(Frame frame, int left, int top, int customerID)
        {
            string[] information = { "Owner ID", "VIN", "Number plate", "Car Brand", "Car Model", "Production Year", "KM Counter", "Fuel type"};
            top += frame.OffsetTop;
            left += frame.OffsetLeft;

            Console.SetCursorPosition(left, top);
            Console.WriteLine("Car Form:");

            for (int i = 0; i < information.Length; i++)
            {
                Console.SetCursorPosition(left, top + 2 + i);
                Console.Write(information[i] + ": ");
            }

            Tool.Write(left + information[0].Length + 2, top + 2, customerID.ToString(), ConsoleColor.DarkGray);
            string vin = Tool.BuildString(left + information[1].Length + 2, top + 3, 20).ToUpper();
            string numberPlate = Tool.BuildString(left + information[2].Length + 2, top + 4, 7).ToUpper();
            string brand = Tool.BuildString(left + information[3].Length + 2, top + 5, 20);
            string model = Tool.BuildString(left + information[4].Length + 2, top + 6, 20);
            int productionYear = Tool.BuildInt(left + information[5].Length + 2, top + 7, 4);
            int kmCount = Tool.BuildInt(left + information[6].Length + 2, top + 8, 20);
            string fuelType = Tool.BuildString(left + information[7].Length + 2, top + 9, 20);

            SQL.CreateCar(customerID, vin, numberPlate, brand, model, productionYear, kmCount, fuelType);
        }

        public void Read(Frame frame, int offsetLeft, int offsetTop)
        {
            string[] information = { "Owner ID", "VIN", "Number plate", "Car Brand", "Car Model", "Production Year", "KM Counter", "Fuel type" };
            string[] data = { CustomerID.ToString(), VinNumber, NumberPlate, CarBrand, CarModel, ProductionYear.ToString(), KmCount.ToString(), FuelType };
            offsetTop += frame.OffsetTop;
            offsetLeft += frame.OffsetLeft;

            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.WriteLine("Car Form:");

            for (int i = 0; i < information.Length; i++)
            {
                Console.SetCursorPosition(offsetLeft, offsetTop + 2 + i);
                Console.Write(information[i] + ": " + data[i]);
            }
        }

        public void Update(Frame frame, int offsetLeft, int offsetTop)
        {
            string[] information = { "Owner ID", "VIN", "Number plate", "Car Brand", "Car Model", "Production Year", "KM Counter", "Fuel type" };
            string[] data = { CustomerID.ToString(), VinNumber, NumberPlate, CarBrand, CarModel, ProductionYear.ToString(), KmCount.ToString(), FuelType };
            offsetTop += frame.OffsetTop;
            offsetLeft += frame.OffsetLeft;

            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.WriteLine("Car Form:");

            for (int i = 0; i < information.Length; i++)
            {
                Console.SetCursorPosition(offsetLeft, offsetTop + 2 + i);
                Console.Write(information[i] + ": " + data[i]);
            }
            Tool.Write(offsetLeft + information[0].Length + 2, offsetTop + 2, data[0].ToString(), ConsoleColor.DarkGray);
            string vin = Tool.BuildString(offsetLeft + information[1].Length + 2, offsetTop + 3, 20, data[1]).ToUpper();
            string numberPlate = Tool.BuildString(offsetLeft + information[2].Length + 2, offsetTop + 4, 7, data[2]).ToUpper();
            string brand = Tool.BuildString(offsetLeft + information[3].Length + 2, offsetTop + 5, 20, data[3]);
            string model = Tool.BuildString(offsetLeft + information[4].Length + 2, offsetTop + 6, 20, data[4]);
            int productionYear = Tool.BuildInt(offsetLeft + information[5].Length + 2, offsetTop + 7, 4, data[5]);
            int kmCount = Tool.BuildInt(offsetLeft + information[6].Length + 2, offsetTop + 8, 20, data[6]);
            string fuelType = Tool.BuildString(offsetLeft + information[7].Length + 2, offsetTop + 9, 20, data[7]);

            SQL.UpdateCar(CustomerID, vin, numberPlate, brand, model, productionYear, kmCount, fuelType, VinNumber);
        }

        public static void Overview(Frame frame, int left, int top,  string title, List<Car> list, int max)
        {
            top += frame.OffsetTop;
            left += frame.OffsetLeft;
            if (list.Count < max)
                max = list.Count;


            Console.SetCursorPosition(left, top);
            Console.Write(title);

            for (int i = 0; i < max; i++)
            {
                Console.SetCursorPosition(left, top + i + 2);
                Console.Write(list[i].StringFormat);
            }
        }
    }
}
