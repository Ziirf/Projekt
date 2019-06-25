using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt;

namespace Projekt
{
    class ShopVisit
    {
        public static List<ShopVisit> shopVisitList = new List<ShopVisit>();
        public static int[] buffer = { 0, 12, 24, 44, 54, 80, 112 };

        private int visitID;
        public int VisitID
        {
            get { return visitID; }
            set { visitID = value; }
        }

        private DateTime dateTimeVisit;
        public DateTime DateTimeVisit
        {
            get { return dateTimeVisit; }
            set { dateTimeVisit = value; }
        }

        private string mechanic;
        public string Mechanic
        {
            get { return mechanic; }
            set { mechanic = value; }
        }

        private string vinNumber;
        public string VinNumber
        {
            get { return vinNumber; }
            set { vinNumber = value; }
        }

        private int kmCount;
        public int KmCount
        {
            get { return kmCount; }
            set { kmCount = value; }
        }

        private string issue;
        public string Issue
        {
            get { return issue; }
            set { issue = value; }
        }

        private string notes;
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        private string stringFormat;
        public string StringFormat
        {
            get { return stringFormat; }
            set { stringFormat = value; }
        }

        public ShopVisit(int visitID, DateTime dateTimeVisit, string mechanic, string vinNumber, int kmCount, string issue, string notes)
        {
            VisitID = visitID;
            DateTimeVisit = dateTimeVisit;
            Mechanic = mechanic;
            VinNumber = vinNumber;
            KmCount = kmCount;
            Issue = issue;
            Notes = notes;
            stringFormat = Tool.FormatString(buffer, Info());
        }

        public string[] Info()
        {
            string[] car = { VisitID.ToString(), Mechanic, VinNumber, KmCount.ToString(), Issue, Notes, DateTimeVisit.ToString("dd-MM-yyyy") };

            return car;
        }

        public static void Create(Frame frame, int offsetLeft, int offsetTop)
        {
            string[] information = { "Mechanic", "VIN", "KM Count", "Issue", "Note" };
            offsetTop += frame.OffsetTop;
            offsetLeft += frame.OffsetLeft;

            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.WriteLine("Shop Visit Form:");

            for (int i = 0; i < information.Length; i++)
            {
                Console.SetCursorPosition(offsetLeft, offsetTop + 2 + i);
                Console.Write(information[i] + ": ");
            }
            string mechanic = Tool.BuildString(offsetLeft + information[0].Length + 2, offsetTop + 2, 20);
            string vin = Tool.BuildString(offsetLeft + information[1].Length + 2, offsetTop + 3, 20);
            int kmCount = Tool.BuildInt(offsetLeft + information[2].Length + 2, offsetTop + 4, 20);
            string issue = Tool.BuildString(offsetLeft + information[3].Length + 2, offsetTop + 5, 20);
            string note = Tool.BuildString(offsetLeft + information[4].Length + 2, offsetTop + 6, 50);

            SQL.CreateShopVisit(mechanic, vin, kmCount, issue, note);
        }

        public static void Overview(Frame frame, int left, int top, string title, List<ShopVisit> list, int max)
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

        //int visitID, DateTime dateTimeVisit, string mechanic, string vinNumber, int kmCount, string issue, string notes
        public static void Read(Frame frame, ShopVisit shopVisit, int offsetLeft, int offsetTop)
        {
            string[] information = { "Visit ID", "Date", "Mechanic", "VIN", "KM Count", "Issue", "Note" };
            string[] data = { shopVisit.VisitID.ToString(), shopVisit.DateTimeVisit.ToString("dd-MM-yyyy"), shopVisit.Mechanic, shopVisit.vinNumber, shopVisit.kmCount.ToString(), shopVisit.issue, shopVisit.notes };
            offsetTop += frame.OffsetTop;
            offsetLeft += frame.OffsetLeft;

            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.WriteLine("Shop Visit Form:");

            for (int i = 0; i < information.Length; i++)
            {
                Console.SetCursorPosition(offsetLeft, offsetTop + 2 + i);
                Console.Write(information[i] + ": " + data[i]);
            }
        }

        public static void Update(Frame frame, ShopVisit shopVisit, int offsetLeft, int offsetTop)
        {
            string[] information = { "Visit ID", "Date", "Mechanic", "VIN", "KM Count", "Issue", "Note" };
            string[] data = { shopVisit.VisitID.ToString(), shopVisit.DateTimeVisit.ToString("dd-MM-yyyy"), shopVisit.Mechanic, shopVisit.vinNumber, shopVisit.kmCount.ToString(), shopVisit.issue, shopVisit.notes };
            offsetTop += frame.OffsetTop;
            offsetLeft += frame.OffsetLeft;

            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.WriteLine("Shop Visit Form:");

            for (int i = 0; i < information.Length; i++)
            {
                Console.SetCursorPosition(offsetLeft, offsetTop + 2 + i);
                Console.Write(information[i] + ": " + data[i]);
            }
            Tool.Write(offsetLeft + information[0].Length + 2, offsetTop + 2, data[0].ToString(), ConsoleColor.DarkGray);
            Tool.Write(offsetLeft + information[1].Length + 2, offsetTop + 3, data[1].ToString(), ConsoleColor.DarkGray);
            string mechanic = Tool.BuildString(offsetLeft + information[2].Length + 2, offsetTop + 4, 20, data[2]);
            string vin = Tool.BuildString(offsetLeft + information[3].Length + 2, offsetTop + 5, 20, data[3]);
            int kmCount = Tool.BuildInt(offsetLeft + information[4].Length + 2, offsetTop + 6, 20, data[4]);
            string issue = Tool.BuildString(offsetLeft + information[5].Length + 2, offsetTop + 7, 20, data[5]);
            string note = Tool.BuildString(offsetLeft + information[6].Length + 2, offsetTop + 8, 50, data[6]);

            SQL.UpdateShopVisit(shopVisit.VisitID, shopVisit.DateTimeVisit, mechanic, vin, kmCount, issue, note);
        }

        //public static void Update(Frame frame, Car car, int offsetLeft, int offsetTop)
        //{
        //    string[] information = { "Owner ID", "VIN", "Number plate", "Car Brand", "Car Model", "Production Year", "KM Counter", "Fuel type" };
        //    string[] data = { car.customerID.ToString(), car.vinNumber, car.numberPlate, car.carBrand, car.carModel, car.productionYear.ToString(), car.kmCount.ToString(), car.fuelType };
        //    offsetTop += frame.OffsetTop;
        //    offsetLeft += frame.OffsetLeft;

        //    Console.SetCursorPosition(offsetLeft, offsetTop);
        //    Console.WriteLine("Car Form:");

        //    for (int i = 0; i < information.Length; i++)
        //    {
        //        Console.SetCursorPosition(offsetLeft, offsetTop + 2 + i);
        //        Console.Write(information[i] + ": " + data[i]);
        //    }
        //    Tool.Write(offsetLeft + information[0].Length + 2, offsetTop + 2, data[0].ToString(), ConsoleColor.DarkGray);
        //    string vin = Tool.BuildString(offsetLeft + information[1].Length + 2, offsetTop + 3, 20, data[1]).ToUpper();
        //    string numberPlate = Tool.BuildString(offsetLeft + information[2].Length + 2, offsetTop + 4, 7, data[2]).ToUpper();
        //    string brand = Tool.BuildString(offsetLeft + information[3].Length + 2, offsetTop + 5, 20, data[3]);
        //    string model = Tool.BuildString(offsetLeft + information[4].Length + 2, offsetTop + 6, 20, data[4]);
        //    int productionYear = Tool.BuildInt(offsetLeft + information[5].Length + 2, offsetTop + 7, 4, data[5]);
        //    int kmCount = Tool.BuildInt(offsetLeft + information[6].Length + 2, offsetTop + 8, 20, data[6]);
        //    string fuelType = Tool.BuildString(offsetLeft + information[7].Length + 2, offsetTop + 9, 20, data[7]);

        //    SQL.UpdateCar(car.customerID, vin, numberPlate, brand, model, productionYear, kmCount, fuelType, car.VinNumber);
        //}
    }
}
