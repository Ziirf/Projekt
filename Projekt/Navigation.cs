using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Navigation
    {
        public Frame Frame { get; set; }
        public int OffsetLeft { get; set; }

        public int OffsetTop { get; set; }

        public string[] Options { get; set; }
        public string Title { get; set; }

        public Navigation(Frame frame, int offsetLeft, int offsetTop, string[] options)
        {
            Frame = frame;
            OffsetLeft = offsetLeft;
            OffsetTop = offsetTop;
            Options = options;
        }

        public Navigation(Frame frame, int offsetLeft, int offsetTop, string[] options, string title)
        {
            Frame = frame;
            OffsetLeft = offsetLeft;
            OffsetTop = offsetTop;
            Options = options;
            Title = title;
        }

        public void PrintTitle()
        {
            Console.SetCursorPosition(OffsetLeft, OffsetTop - 2);
            Console.Write(Title);
        }

        public int Selector()
        {
            var cursorState = Console.CursorVisible;    // gemmer staten af cursorens blinken-status.
            Console.CursorVisible = false;              // gør cursoren usynlig.
            int selectorPos = 0;
            int selectorPos_old = 1;
            var color = Console.ForegroundColor;
            ConsoleKeyInfo cki;

            for (int i = 0; i < Options.Length; i++)
            {
                Console.SetCursorPosition(OffsetLeft, OffsetTop + i);
                Console.WriteLine(Options[i]);
            }

            // Starter vælgeren, som bliver ved med at køre igennem indtil man trykker enter.
            do
            {
                if (selectorPos != selectorPos_old)
                {
                    color = Console.ForegroundColor;
                    Console.SetCursorPosition(OffsetLeft, OffsetTop + selectorPos_old);
                    Console.Write(Options[selectorPos_old]);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(OffsetLeft, OffsetTop + selectorPos);
                    Console.WriteLine(Options[selectorPos]);

                    Console.ForegroundColor = color;
                }

                // læser tastede indput.
                cki = Console.ReadKey(true);
                selectorPos_old = selectorPos;

                // hopper op og ned i menuen alt efter hvad der bliver trykket.
                if (cki.Key == ConsoleKey.UpArrow && selectorPos > 0)
                    selectorPos--;
                if (cki.Key == ConsoleKey.DownArrow && selectorPos < Options.Length - 1)
                    selectorPos++;
                if (cki.Key == ConsoleKey.Escape)
                    return -1;

                // hopper ud af loopen hvis der bliver trykket på enter.
            } while (cki.Key != ConsoleKey.Enter);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(OffsetLeft, OffsetTop + selectorPos);
            Console.WriteLine(Options[selectorPos]);
            Console.ForegroundColor = color;

            // gendanner cursorens status til den tidligere status og derefter returnere værdien for det valgte emne.
            Console.CursorVisible = cursorState;
            return selectorPos;
        }

        public int Selector(int maxEntries, List<Customer> list)
        {
            if (maxEntries > list.Count)
                maxEntries = list.Count;

            var cursorState = Console.CursorVisible;    // Saves the blinking status of the cursor.
            Console.CursorVisible = false;              // Makes cursor invisible.
            int selectorPos = 0;
            int min = 0;
            int listCount = list.Count;
            int sort = 1;
            string[] sortText = { "Customer ID", "Lastname    ", "Zip Code     " };
            ConsoleColor color = Console.ForegroundColor;
            ConsoleKeyInfo cki;

            Tool.Write(2, 2, "Sorted by: " + sortText[0], ConsoleColor.Gray);
            do
            {

                if (selectorPos >= maxEntries + min)
                    min++;
                else if (selectorPos < min)
                    min--;

                for (int i = min; i < maxEntries + min; i++)
                {
                    Console.SetCursorPosition(OffsetLeft, OffsetTop + i - min);
                    Console.WriteLine(list[i].StringFormat);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(OffsetLeft, OffsetTop + selectorPos - min);
                Console.WriteLine(list[selectorPos].StringFormat);
                Console.ForegroundColor = color;

                if (min + maxEntries < listCount)
                    Tool.Write(OffsetLeft + 130,OffsetTop + maxEntries - 1, "V", ConsoleColor.Gray);
                else
                    Tool.Write(OffsetLeft + 130, OffsetTop + maxEntries - 1, " ", ConsoleColor.Gray);
                if (min > 0)
                    Tool.Write(OffsetLeft + 130, OffsetTop - 1, "A", ConsoleColor.Gray);
                else
                    Tool.Write(OffsetLeft + 130, OffsetTop - 1, " ", ConsoleColor.Gray);

                // Read key input.
                cki = Console.ReadKey(true);

                // Changes up and down in menu, depending on key press.
                if (cki.Key == ConsoleKey.UpArrow && selectorPos > 0)
                    selectorPos--;
                if (cki.Key == ConsoleKey.DownArrow && selectorPos < Options.Length - 1)
                    selectorPos++;
                if (cki.Key == ConsoleKey.Escape)
                    return -1;
                if (cki.Key == ConsoleKey.S)
                {
                    if (sort == 0)
                        list = list.OrderBy(order => order.CustomerID).ToList();
                    else if (sort == 1)
                        list = list.OrderBy(order => order.Lastname).ToList();
                    else if (sort == 2)
                        list = list.OrderBy(order => order.ZipCode).ToList();

                    Tool.Write(2, 2, "Sorted by: " + sortText[sort], ConsoleColor.Gray);
                    if (sort >= 2)
                        sort = 0;
                    else
                        sort++;

                    selectorPos = 0;
                    min = 0;
                }
                // Exits loop if enter is pressed.
            } while (cki.Key != ConsoleKey.Enter);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(OffsetLeft, OffsetTop + selectorPos);
            Console.WriteLine(list[selectorPos].StringFormat);
            Console.ForegroundColor = color;
                        
            //Reverts cursor to previous status and returns value for the chosen subject.
            Console.CursorVisible = cursorState;
            return list[selectorPos].CustomerID;
        }

        public string Selector(int maxEntries, List<Car> list)
        {
            if (list.Count == 0)

            if (maxEntries > list.Count)
                maxEntries = list.Count;

            var cursorState = Console.CursorVisible;    // Saves the blinking status of the cursor.
            Console.CursorVisible = false;              // Makes cursor invisible.
            int selectorPos = 0;
            int min = 0;
            int listCount = list.Count;
            int sort = 1;
            string[] sortText = { "Customer ID", "VIN Number  ", "Car Model  " };
            ConsoleColor color = Console.ForegroundColor;
            ConsoleKeyInfo cki;

            Tool.Write(2, 2, "Sorted by: " + sortText[0], ConsoleColor.Gray);
            do
            {
                if (selectorPos >= maxEntries + min)
                    min++;
                else if (selectorPos < min)
                    min--;

                for (int i = min; i < maxEntries + min; i++)
                {
                    Console.SetCursorPosition(OffsetLeft, OffsetTop + i - min);
                    Console.WriteLine(list[i].StringFormat);
                }

                if (min + maxEntries < listCount)
                    Tool.Write(OffsetLeft + 130, OffsetTop + maxEntries - 1, "V", ConsoleColor.Gray);
                else
                    Tool.Write(OffsetLeft + 130, OffsetTop + maxEntries - 1, " ", ConsoleColor.Gray);
                if (min > 0)
                    Tool.Write(OffsetLeft + 130, OffsetTop - 1, "A", ConsoleColor.Gray);
                else
                    Tool.Write(OffsetLeft + 130, OffsetTop - 1, " ", ConsoleColor.Gray);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(OffsetLeft, OffsetTop + selectorPos - min);
                Console.WriteLine(list[selectorPos].StringFormat);
                Console.ForegroundColor = color;

                // Read key input.
                cki = Console.ReadKey(true);

                // Changes up and down in menu, depending on key press.
                if (cki.Key == ConsoleKey.UpArrow && selectorPos > 0)
                    selectorPos--;
                if (cki.Key == ConsoleKey.DownArrow && selectorPos < Options.Length - 1)
                    selectorPos++;
                if (cki.Key == ConsoleKey.Escape)
                    return "-1";
                if (cki.Key == ConsoleKey.S)
                {
                    if (sort == 0)
                        list = list.OrderBy(order => order.CustomerID).ToList();
                    else if (sort == 1)
                        list = list.OrderBy(order => order.VinNumber).ToList();
                    else if (sort == 2)
                        list = list.OrderBy(order => order.CarModel).ToList();

                    Tool.Write(2, 2, "Sorted by: " + sortText[sort], ConsoleColor.Gray);
                    if (sort >= 2)
                        sort = 0;
                    else
                        sort++;

                    selectorPos = 0;
                    min = 0;
                }
                // Exits loop if enter is pressed.
            } while (cki.Key != ConsoleKey.Enter);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(OffsetLeft, OffsetTop + selectorPos);
            Console.WriteLine(list[selectorPos].StringFormat);
            Console.ForegroundColor = color;

            //Reverts cursor to previous status and returns value for the chosen subject.
            Console.CursorVisible = cursorState;
            return list[selectorPos].VinNumber;
        }
        public int Selector(int maxEntries, List<ShopVisit> list)
        {
            if (maxEntries > list.Count)
                maxEntries = list.Count;

            var cursorState = Console.CursorVisible;    // Saves the blinking status of the cursor.
            Console.CursorVisible = false;              // Makes cursor invisible.
            int selectorPos = 0;
            int min = 0;
            int listCount = list.Count;
            int sort = 1;
            string[] sortText = { "Mechanic   ", "VIN Number  ", "Date       " };
            ConsoleColor color = Console.ForegroundColor;
            ConsoleKeyInfo cki;

            Tool.Write(2, 2, "Sorted by: " + sortText[0], ConsoleColor.Gray);
            do
            {
                if (selectorPos >= maxEntries + min)
                    min++;
                else if (selectorPos < min)
                    min--;

                for (int i = min; i < maxEntries + min; i++)
                {
                    Console.SetCursorPosition(OffsetLeft, OffsetTop + i - min);
                    Console.WriteLine(list[i].StringFormat);
                }

                if (min + maxEntries < listCount)
                    Tool.Write(OffsetLeft + 130, OffsetTop + maxEntries - 1, "V", ConsoleColor.Gray);
                else
                    Tool.Write(OffsetLeft + 130, OffsetTop + maxEntries - 1, " ", ConsoleColor.Gray);
                if (min > 0)
                    Tool.Write(OffsetLeft + 130, OffsetTop - 1, "A", ConsoleColor.Gray);
                else
                    Tool.Write(OffsetLeft + 130, OffsetTop - 1, " ", ConsoleColor.Gray);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(OffsetLeft, OffsetTop + selectorPos - min);
                Console.WriteLine(list[selectorPos].StringFormat);
                Console.ForegroundColor = color;

                // Read key input.
                cki = Console.ReadKey(true);

                // Changes up and down in menu, depending on key press.
                if (cki.Key == ConsoleKey.UpArrow && selectorPos > 0)
                    selectorPos--;
                if (cki.Key == ConsoleKey.DownArrow && selectorPos < Options.Length - 1)
                    selectorPos++;
                if (cki.Key == ConsoleKey.Escape)
                    return -1;
                if (cki.Key == ConsoleKey.S)
                {
                    if (sort == 0)
                        list = list.OrderBy(order => order.Mechanic).ToList();
                    else if (sort == 1)
                        list = list.OrderBy(order => order.VinNumber).ToList();
                    else if (sort == 2)
                        list = list.OrderBy(order => order.DateTimeVisit).ToList();

                    Tool.Write(2, 2, "Sorted by: " + sortText[sort], ConsoleColor.Gray);
                    if (sort >= 2)
                        sort = 0;
                    else
                        sort++;

                    selectorPos = 0;
                    min = 0;
                }
                // Exits loop if enter is pressed.
            } while (cki.Key != ConsoleKey.Enter);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(OffsetLeft, OffsetTop + selectorPos);
            Console.WriteLine(list[selectorPos].StringFormat);
            Console.ForegroundColor = color;

            //Reverts cursor to previous status and returns value for the chosen subject.
            Console.CursorVisible = cursorState;
            return list[selectorPos].VisitID;
        }

        // Generic liste (uden for pensum) - Sådan her kunne man have gjort. -- Ikke implenteret da jeg er den eneste der har arbejdet med generics
        public string SelectorGeneric<T>(int maxEntries, List<T> list) where T : IObjects
        {
            if (maxEntries > list.Count)
                maxEntries = list.Count;

            var cursorState = Console.CursorVisible;    // Saves the blinking status of the cursor.
            Console.CursorVisible = false;              // Makes cursor invisible.
            int selectorPos = 0;
            int min = 0;
            ConsoleColor color = Console.ForegroundColor;
            ConsoleKeyInfo cki;

            do
            {
                if (selectorPos >= maxEntries + min)
                    min++;
                else if (selectorPos < min)
                    min--;

                for (int i = min; i < maxEntries + min; i++)
                {
                    Console.SetCursorPosition(OffsetLeft, OffsetTop + i - min);
                    Console.WriteLine(list[i].StringFormat);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(OffsetLeft, OffsetTop + selectorPos - min);
                Console.WriteLine(list[selectorPos].StringFormat);
                Console.ForegroundColor = color;

                // Read key input.
                cki = Console.ReadKey(true);

                // Changes up and down in menu, depending on key press.
                if (cki.Key == ConsoleKey.UpArrow && selectorPos > 0)
                    selectorPos--;
                if (cki.Key == ConsoleKey.DownArrow && selectorPos < Options.Length - 1)
                    selectorPos++;
                if (cki.Key == ConsoleKey.Escape)
                    return "1";
                if (cki.Key == ConsoleKey.S)
                {
                    selectorPos = 0;
                    min = 0;
                }
                // Exits loop if enter is pressed.
            } while (cki.Key != ConsoleKey.Enter);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(OffsetLeft, OffsetTop + selectorPos);
            Console.WriteLine(list[selectorPos].StringFormat);
            Console.ForegroundColor = color;

            //Reverts cursor to previous status and returns value for the chosen subject.
            Console.CursorVisible = cursorState;
            return list[selectorPos].ToString();
        }
    }
}
