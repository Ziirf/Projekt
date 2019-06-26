using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class ShopVisit : IObjects
    {
        public static List<ShopVisit> shopVisitList = new List<ShopVisit>();
        public static int[] buffer = { 0, 12, 24, 44, 54, 80, 112 };

        public int VisitID { get; set; }

        public DateTime DateTimeVisit { get; set; }

        public string Mechanic { get; set; }

        public string VinNumber { get; set; }

        public int KmCount { get; set; }

        public string Issue { get; set; }

        public string Notes { get; set; }

        public string StringFormat { get; set; }

        public ShopVisit(int visitID, DateTime dateTimeVisit, string mechanic, string vinNumber, int kmCount, string issue, string notes)
        {
            VisitID = visitID;
            DateTimeVisit = dateTimeVisit;
            Mechanic = mechanic;
            VinNumber = vinNumber;
            KmCount = kmCount;
            Issue = issue;
            Notes = notes;
            StringFormat = Tool.FormatString(buffer, Info());
        }

        public string[] Info()
        {
            string[] car = { VisitID.ToString(), Mechanic, VinNumber, KmCount.ToString(), Issue, Notes, DateTimeVisit.ToString("dd-MM-yyyy") };

            return car;
        }

        public static void Create(Frame frame, int left, int top, string vin)
        {
            string[] information = { "Mechanic", "VIN", "KM Count", "Issue", "Note" };
            top += frame.OffsetTop;
            left += frame.OffsetLeft;

            Console.SetCursorPosition(left, top);
            Console.WriteLine("Shop Visit Form:");

            for (int i = 0; i < information.Length; i++)
            {
                Console.SetCursorPosition(left, top + 2 + i);
                Console.Write(information[i] + ": " + new string(' ', 50));
            }
            Tool.Write(left + information[1].Length + 2, top + 3, vin, ConsoleColor.DarkGray);
            string mechanic = Tool.BuildString(left + information[0].Length + 2, top + 2, 20);
            int kmCount = Tool.BuildInt(left + information[2].Length + 2, top + 4, 20);
            string issue = Tool.BuildString(left + information[3].Length + 2, top + 5, 20);
            string note = Tool.BuildString(left + information[4].Length + 2, top + 6, 50);

            SQL.CreateShopVisit(mechanic, vin, kmCount, issue, note);
        }

        public void Read(Frame frame, int offsetLeft, int offsetTop)
        {
            string[] information = { "Visit ID", "Date", "Mechanic", "VIN", "KM Count", "Issue", "Note" };
            string[] data = { VisitID.ToString(), DateTimeVisit.ToString("dd-MM-yyyy"), Mechanic, VinNumber, KmCount.ToString(), Issue, Notes };
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
        public void Update(Frame frame, int offsetLeft, int offsetTop)
        {
            string[] information = { "Visit ID", "Date", "Mechanic", "VIN", "KM Count", "Issue", "Note" };
            string[] data = { VisitID.ToString(), DateTimeVisit.ToString("dd-MM-yyyy"), Mechanic, VinNumber, KmCount.ToString(), Issue, Notes };
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
            string newMechanic = Tool.BuildString(offsetLeft + information[2].Length + 2, offsetTop + 4, 20, data[2]);
            string newVinNumber = Tool.BuildString(offsetLeft + information[3].Length + 2, offsetTop + 5, 20, data[3]);
            int newKmCount = Tool.BuildInt(offsetLeft + information[4].Length + 2, offsetTop + 6, 20, data[4]);
            string newIssue = Tool.BuildString(offsetLeft + information[5].Length + 2, offsetTop + 7, 20, data[5]);
            string newNote = Tool.BuildString(offsetLeft + information[6].Length + 2, offsetTop + 8, 50, data[6]);

            SQL.UpdateShopVisit(VisitID, DateTimeVisit, newMechanic, newVinNumber, newKmCount, newIssue, newNote);
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
    }
}
