using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Navigation
    {
        private Frame frame;
        public Frame Frame
        {
            get { return frame; }
            set { frame = value; }
        }

        private int offsetLeft;
        public int OffsetLeft
        {
            get { return offsetLeft; }
            set { offsetLeft = value; }
        }

        private int offsetTop;
        public int OffsetTop
        {
            get { return offsetTop; }
            set { offsetTop = value; }
        }

        private string[] options;
        public string[] Options
        {
            get { return options; }
            set { options = value; }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public Navigation(Frame frame, int offsetLeft, int offsetTop, string[] options)
        {
            this.frame = frame;
            this.offsetLeft = offsetLeft;
            this.offsetTop = offsetTop;
            this.options = options;
        }

        public Navigation(Frame frame, int offsetLeft, int offsetTop, string[] options, string title)
        {
            this.frame = frame;
            this.offsetLeft = offsetLeft;
            this.offsetTop = offsetTop;
            this.options = options;
            this.title = title;
        }

        public void PrintTitle()
        {
            Console.SetCursorPosition(offsetLeft, offsetTop - 2);
            Console.Write(title);
        }

        public int Selector()
        {
            var cursorState = Console.CursorVisible;    // gemmer staten af cursorens blinken-status.
            Console.CursorVisible = false;              // gør cursoren usynlig.
            int selectorPos = 0;
            int selectorPos_old = 1;
            var color = Console.ForegroundColor;
            ConsoleKeyInfo cki;

            for (int i = 0; i < options.Length; i++)
            {
                Console.SetCursorPosition(offsetLeft, offsetTop + i);
                Console.WriteLine(options[i]);
            }

            // Starter vælgeren, som bliver ved med at køre igennem indtil man trykker enter.
            do
            {
                if (selectorPos != selectorPos_old)
                {
                    color = Console.ForegroundColor;
                    Console.SetCursorPosition(offsetLeft, offsetTop + selectorPos_old);
                    Console.Write(options[selectorPos_old]);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(offsetLeft, offsetTop + selectorPos);
                    Console.WriteLine(options[selectorPos]);

                    Console.ForegroundColor = color;
                }

                // læser tastede indput.
                cki = Console.ReadKey(true);
                selectorPos_old = selectorPos;

                // hopper op og ned i menuen alt efter hvad der bliver trykket.
                if (cki.Key == ConsoleKey.UpArrow && selectorPos > 0)
                    selectorPos--;
                if (cki.Key == ConsoleKey.DownArrow && selectorPos < options.Length - 1)
                    selectorPos++;
                if (cki.Key == ConsoleKey.Escape)
                    return -1;

                // hopper ud af loopen hvis der bliver trykket på enter.
            } while (cki.Key != ConsoleKey.Enter);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(offsetLeft, offsetTop + selectorPos);
            Console.WriteLine(options[selectorPos]);
            Console.ForegroundColor = color;

            // gendanner cursorens status til den tidligere status og derefter returnere værdien for det valgte emne.
            Console.CursorVisible = cursorState;
            return selectorPos;
        }

        //public int Selector(int maxEntries)
        //{
        //    var cursorState = Console.CursorVisible;    // gemmer staten af cursorens blinken-status.
        //    Console.CursorVisible = false;              // gør cursoren usynlig.
        //    int selectorPos = 0;
        //    int min = 0;
        //    ConsoleColor color = Console.ForegroundColor;
        //    ConsoleKeyInfo cki;

        //    do
        //    {
        //        if (selectorPos >= maxEntries)
        //        {
        //            maxEntries++;
        //            min++;
        //        }
        //        else if (selectorPos < min)
        //        {
        //            maxEntries--;
        //            min--;
        //        }

        //        for (int i = min; i < maxEntries; i++)
        //        {
        //            Console.SetCursorPosition(offsetLeft, offsetTop + i - min);
        //            Console.WriteLine(options[i]);
        //        }

        //        Console.ForegroundColor = ConsoleColor.Green;
        //        Console.SetCursorPosition(offsetLeft, offsetTop + selectorPos - min);
        //        Console.WriteLine(options[selectorPos]);
        //        Console.ForegroundColor = color;

        //        // reads the key indput.
        //        cki = Console.ReadKey(true);

        //        // changes menu up and down depending on key press.
        //        if (cki.Key == ConsoleKey.UpArrow && selectorPos > 0)
        //            selectorPos--;
        //        if (cki.Key == ConsoleKey.DownArrow && selectorPos < options.Length - 1)
        //            selectorPos++;
        //        if (cki.Key == ConsoleKey.Escape)
        //            return -1;
        //        // stops loop if enter is pressed.
        //    } while (cki.Key != ConsoleKey.Enter);

        //    Console.ForegroundColor = ConsoleColor.DarkGray;
        //    Console.SetCursorPosition(offsetLeft, offsetTop + selectorPos);
        //    Console.WriteLine(options[selectorPos]);
        //    Console.ForegroundColor = color;

        //    // gendanner cursorens status til den tidligere status og derefter returnere værdien for det valgte emne.
        //    Console.CursorVisible = cursorState;
        //    return selectorPos;
        //}

        public int Selector(int maxEntries, List<Customer> list)
        {
            if (maxEntries > list.Count)
                maxEntries = list.Count;

            var cursorState = Console.CursorVisible;    // Saves the blinking status of the cursor.
            Console.CursorVisible = false;              // Makes cursor invisible.
            int selectorPos = 0;
            int min = 0;
            int sort = 1;
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
                    Console.SetCursorPosition(offsetLeft, offsetTop + i - min);
                    Console.WriteLine(list[i].StringFormat);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(offsetLeft, offsetTop + selectorPos - min);
                Console.WriteLine(list[selectorPos].StringFormat);
                Console.ForegroundColor = color;

                // Read key input.
                cki = Console.ReadKey(true);

                // Changes up and down in menu, depending on key press.
                if (cki.Key == ConsoleKey.UpArrow && selectorPos > 0)
                    selectorPos--;
                if (cki.Key == ConsoleKey.DownArrow && selectorPos < options.Length - 1)
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
            Console.SetCursorPosition(offsetLeft, offsetTop + selectorPos);
            Console.WriteLine(list[selectorPos].StringFormat);
            Console.ForegroundColor = color;
                        
            //Reverts cursor to previous status and returns value for the chosen subject.
            Console.CursorVisible = cursorState;
            return list[selectorPos].CustomerID;
        }

        public string Selector(int maxEntries, List<Car> list)
        {
            if (maxEntries > list.Count)
                maxEntries = list.Count;

            var cursorState = Console.CursorVisible;    // Saves the blinking status of the cursor.
            Console.CursorVisible = false;              // Makes cursor invisible.
            int selectorPos = 0;
            int min = 0;
            int sort = 1;
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
                    Console.SetCursorPosition(offsetLeft, offsetTop + i - min);
                    Console.WriteLine(list[i].StringFormat);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(offsetLeft, offsetTop + selectorPos - min);
                Console.WriteLine(list[selectorPos].StringFormat);
                Console.ForegroundColor = color;

                // Read key input.
                cki = Console.ReadKey(true);

                // Changes up and down in menu, depending on key press.
                if (cki.Key == ConsoleKey.UpArrow && selectorPos > 0)
                    selectorPos--;
                if (cki.Key == ConsoleKey.DownArrow && selectorPos < options.Length - 1)
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
            Console.SetCursorPosition(offsetLeft, offsetTop + selectorPos);
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
            int sort = 1;
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
                    Console.SetCursorPosition(offsetLeft, offsetTop + i - min);
                    Console.WriteLine(list[i].StringFormat);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(offsetLeft, offsetTop + selectorPos - min);
                Console.WriteLine(list[selectorPos].StringFormat);
                Console.ForegroundColor = color;

                // Read key input.
                cki = Console.ReadKey(true);

                // Changes up and down in menu, depending on key press.
                if (cki.Key == ConsoleKey.UpArrow && selectorPos > 0)
                    selectorPos--;
                if (cki.Key == ConsoleKey.DownArrow && selectorPos < options.Length - 1)
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
            Console.SetCursorPosition(offsetLeft, offsetTop + selectorPos);
            Console.WriteLine(list[selectorPos].StringFormat);
            Console.ForegroundColor = color;

            //Reverts cursor to previous status and returns value for the chosen subject.
            Console.CursorVisible = cursorState;
            return list[selectorPos].VisitID;
        }
    }
}
