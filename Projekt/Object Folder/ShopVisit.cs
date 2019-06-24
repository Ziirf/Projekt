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
            private set { visitID = value; }
        }

        private DateTime dateTimeVisit;
        public DateTime DateTimeVisit
        {
            get { return dateTimeVisit; }
            private set { dateTimeVisit = value; }
        }

        private string mechanic;
        public string Mechanic
        {
            get { return mechanic; }
            private set { mechanic = value; }
        }

        private string vinNumber;
        public string VinNumber
        {
            get { return vinNumber; }
            private set { vinNumber = value; }
        }

        private int kmCount;
        public int KmCount
        {
            get { return kmCount; }
            private set { kmCount = value; }
        }

        private string issue;
        public string Issue
        {
            get { return issue; }
            private set { issue = value; }
        }

        private string notes;
        public string Notes
        {
            get { return notes; }
            private set { notes = value; }
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
    }
}
