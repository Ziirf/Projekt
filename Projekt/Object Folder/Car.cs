using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Car
    {
        public static List<Car> carList = new List<Car>();
        public static int[] buffer = { 0, 7, 22, 38, 48, 48, 58, 73, 86 };

        private int customerID;
        public int CustomerID
        {
            get { return customerID; }
            private set { customerID = value; }
        }

        private string vinNumber;
        public string VinNumber
        {
            get { return vinNumber; }
            private set { vinNumber = value; }
        }

        private string numberPlate;
        public string NumberPlate
        {
            get { return numberPlate; }
            private set { numberPlate = value; }
        }

        private string carBrand;
        public string CarBrand
        {
            get { return carBrand; }
            private set { carBrand = value; }
        }

        private string carModel;
        public string CarModel
        {
            get { return carModel; }
            private set { carModel = value; }
        }

        private int productionYear;
        public int ProductionYear
        {
            get { return productionYear; }
            private set { productionYear = value; }
        }

        private int kmCount;
        public int KmCount
        {
            get { return kmCount; }
            private set { kmCount = value; }
        }

        private string fuelType;
        public string FuelType
        {
            get { return fuelType; }
            private set { fuelType = value; }
        }

        private DateTime createdDate;
        public DateTime CreatedDate
        {
            get { return createdDate; }
            private set { createdDate = value; }
        }

        private string stringFormat;
        public string StringFormat
        {
            get { return stringFormat; }
            set { stringFormat = value; }
        }

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
            stringFormat = Tool.FormatString(buffer, Info());
        }

        public string[] Info()
        {
            string[] car = { CustomerID.ToString(), VinNumber, NumberPlate, CarBrand, CarModel, ProductionYear.ToString(), KmCount.ToString(), FuelType, CreatedDate.ToString("dd-MM-yyyy") };

            return car;
        }

        public static void Create(Frame frame, int offsetLeft, int offsetTop)
        {
            string[] information = { "Owner ID", "VIN", "Number plate", "Car Brand", "Car Model", "Production Year", "KM Counter", "Fuel type"};
            offsetTop += frame.OffsetTop;
            offsetLeft += frame.OffsetLeft;

            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.WriteLine("Car Form:");

            for (int i = 0; i < information.Length; i++)
            {
                Console.SetCursorPosition(offsetLeft, offsetTop + 2 + i);
                Console.Write(information[i] + ": ");
            }
            int id = Tool.BuildInt(offsetLeft + information[0].Length + 2, offsetTop + 2, 20);
            string vin = Tool.BuildString(offsetLeft + information[1].Length + 2, offsetTop + 3, 20);
            string numberPlate = Tool.BuildString(offsetLeft + information[2].Length + 2, offsetTop + 4, 20);
            string brand = Tool.BuildString(offsetLeft + information[3].Length + 2, offsetTop + 5, 20);
            string model = Tool.BuildString(offsetLeft + information[4].Length + 2, offsetTop + 6, 20);
            int productionYear = Tool.BuildInt(offsetLeft + information[5].Length + 2, offsetTop + 7, 4);
            int kmCount = Tool.BuildInt(offsetLeft + information[6].Length + 2, offsetTop + 8, 20);
            string fuelType = Tool.BuildString(offsetLeft + information[7].Length + 2, offsetTop + 9, 20);

            SQL.CreateCar(id, vin, numberPlate, brand, model, productionYear, kmCount, fuelType);
        }
    }
}
