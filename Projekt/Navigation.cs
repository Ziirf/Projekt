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
        public string[] Options { get; set; }   // The options to choose from during a selection
        public string Title { get; set; }       // the titlebar

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

        // Prints out the title
        public void PrintTitle()
        {
            Console.SetCursorPosition(OffsetLeft, OffsetTop - 2);
            Console.Write(Title);
        }

        // selects a option, and outputs the index of the options
        public int Selector()
        {
            var cursorState = Console.CursorVisible;    // Looks at what the current cursor state is, and saves it.
            Console.CursorVisible = false;              // makes the cursor invisible
            int selectorPos = 0;
            int selectorPos_old = 1;
            var color = Console.ForegroundColor;        // saves the current foreground color
            ConsoleKeyInfo cki;

            // Writes out the menu options
            for (int i = 0; i < Options.Length; i++)
            {
                Console.SetCursorPosition(OffsetLeft, OffsetTop + i);
                Console.WriteLine(Options[i]);
            }

            // starts the selector which keeps running until you hit enter
            do
            {
                // Changes the color, as to highlight the current selected option
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

                // read key press
                cki = Console.ReadKey(true);
                selectorPos_old = selectorPos;

                // goes up or down in the menu depending on the key press, using escape will return "-1" which we use in our switch case to break a while loop
                if (cki.Key == ConsoleKey.UpArrow && selectorPos > 0)
                    selectorPos--;
                if (cki.Key == ConsoleKey.DownArrow && selectorPos < Options.Length - 1)
                    selectorPos++;
                if (cki.Key == ConsoleKey.Escape)
                    return -1;

            } while (cki.Key != ConsoleKey.Enter);

            // The selected option will look grey as to show which menu you are current in
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(OffsetLeft, OffsetTop + selectorPos);
            Console.WriteLine(Options[selectorPos]);
            Console.ForegroundColor = color;

            // returns the cursor states before ending the method
            Console.CursorVisible = cursorState;
            return selectorPos; // returns the index of the option
        }

        // an overload for the above to compensate for a Customer List, "maxEntries" indicates the highest amount of entries at any given time
        public int Selector(int maxEntries, List<Customer> list)
        {
            // Checks if the max value is higher than what the list contains, if it is, it will make the maxEntries equal the list-count
            if (maxEntries > list.Count)
                maxEntries = list.Count;

            var cursorState = Console.CursorVisible;
            Console.CursorVisible = false;
            int selectorPos = 0;
            int min = 0;
            int sort = 1;                   // The initial Sorting
            string[] sortText = { "Customer ID", "Lastname    ", "Zip Code     " }; // string array that stores the sort options (made it quickly, so it is coded ugly)
            ConsoleColor color = Console.ForegroundColor;
            ConsoleKeyInfo cki;

            Tool.Write(2, 2, "Sorted by: " + sortText[0], ConsoleColor.Gray); // writes sorted by in the top cornor
            // starts the selector which keeps running until you hit enter
            do
            {
                // the scroll function, if the selector is higher than what the maxEntries allows for, it will increase min with one. this is used later
                if (selectorPos >= maxEntries + min)
                    min++;
                else if (selectorPos < min)
                    min--;

                // a for loop that have i = min, which is default at 0, but will increase with the scroll function, and the highest value will be maxEntries + min,
                // which means the max entries also will go one up in this scenario. - this for loop prints out all the entries in the list
                for (int i = min; i < maxEntries + min; i++)
                {
                    Console.SetCursorPosition(OffsetLeft, OffsetTop + i - min);
                    Console.WriteLine(list[i].StringFormat);
                }

                // Will show a little "V" if it is posible to scroll down, and an "A" if it is possible to scroll up.
                if (min + maxEntries < list.Count)
                    Tool.Write(OffsetLeft + 130,OffsetTop + maxEntries - 1, "V", ConsoleColor.Gray);
                else
                    Tool.Write(OffsetLeft + 130, OffsetTop + maxEntries - 1, " ", ConsoleColor.Gray);
                if (min > 0)
                    Tool.Write(OffsetLeft + 130, OffsetTop - 1, "A", ConsoleColor.Gray);
                else
                    Tool.Write(OffsetLeft + 130, OffsetTop - 1, " ", ConsoleColor.Gray);

                // Changes the color, as to highlight the current selected option
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
                // if it is "S" it will sort the list, reset the selector position and min values, as well as change the sorted by text, goes through ID, Lastname and Zipcode
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
            if (maxEntries > list.Count)
                maxEntries = list.Count;

            var cursorState = Console.CursorVisible;    // Saves the blinking status of the cursor.
            Console.CursorVisible = false;              // Makes cursor invisible.
            int selectorPos = 0;
            int min = 0;
            int sort = 1;                   // The initial Sorting
            string[] sortText = { "Customer ID", "VIN Number  ", "Car Model  " }; // string array that stores the sort options (made it quickly, so it is coded ugly)
            ConsoleColor color = Console.ForegroundColor;
            ConsoleKeyInfo cki;

            Tool.Write(2, 2, "Sorted by: " + sortText[0], ConsoleColor.Gray); // writes sorted by in the top cornor
            // starts the selector which keeps running until you hit enter
            do
            {
                // the scroll function, if the selector is higher than what the maxEntries allows for, it will increase min with one. this is used later
                if (selectorPos >= maxEntries + min)
                    min++;
                else if (selectorPos < min)
                    min--;

                // a for loop that have i = min, which is default at 0, but will increase with the scroll function, and the highest value will be maxEntries + min,
                // which means the max entries also will go one up in this scenario. - this for loop prints out all the entries in the list
                for (int i = min; i < maxEntries + min; i++)
                {
                    Console.SetCursorPosition(OffsetLeft, OffsetTop + i - min);
                    Console.WriteLine(list[i].StringFormat);
                }

                // Will show a little "V" if it is posible to scroll down, and an "A" if it is possible to scroll up.
                if (min + maxEntries < list.Count)
                    Tool.Write(OffsetLeft + 130, OffsetTop + maxEntries - 1, "V", ConsoleColor.Gray);
                else
                    Tool.Write(OffsetLeft + 130, OffsetTop + maxEntries - 1, " ", ConsoleColor.Gray);
                if (min > 0)
                    Tool.Write(OffsetLeft + 130, OffsetTop - 1, "A", ConsoleColor.Gray);
                else
                    Tool.Write(OffsetLeft + 130, OffsetTop - 1, " ", ConsoleColor.Gray);

                // Changes the color, as to highlight the current selected option
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
                // if it is "S" it will sort the list, reset the selector position and min values, as well as change the sorted by text, goes through ID, VinNumber and CarModel
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
            // Checks if the max value is higher than what the list contains, if it is, it will make the maxEntries equal the list-count
            if (maxEntries > list.Count)
                maxEntries = list.Count;

            var cursorState = Console.CursorVisible;    // Saves the blinking status of the cursor.
            Console.CursorVisible = false;              // Makes cursor invisible.
            int selectorPos = 0;
            int min = 0;
            int sort = 1;                       // The initial Sorting
            string[] sortText = { "Mechanic   ", "VIN Number  ", "Date       " }; // string array that stores the sort options (made it quickly, so it is coded ugly)
            ConsoleColor color = Console.ForegroundColor;
            ConsoleKeyInfo cki;

            Tool.Write(2, 2, "Sorted by: " + sortText[0], ConsoleColor.Gray); // writes sorted by in the top cornor
            // starts the selector which keeps running until you hit enter
            do
            {
                // the scroll function, if the selector is higher than what the maxEntries allows for, it will increase min with one. this is used later
                if (selectorPos >= maxEntries + min)
                    min++;
                else if (selectorPos < min)
                    min--;

                // a for loop that have i = min, which is default at 0, but will increase with the scroll function, and the highest value will be maxEntries + min,
                // which means the max entries also will go one up in this scenario. - this for loop prints out all the entries in the list
                for (int i = min; i < maxEntries + min; i++)
                {
                    Console.SetCursorPosition(OffsetLeft, OffsetTop + i - min);
                    Console.WriteLine(list[i].StringFormat);
                }

                // Will show a little "V" if it is posible to scroll down, and an "A" if it is possible to scroll u
                if (min + maxEntries < list.Count)
                    Tool.Write(OffsetLeft + 130, OffsetTop + maxEntries - 1, "V", ConsoleColor.Gray);
                else
                    Tool.Write(OffsetLeft + 130, OffsetTop + maxEntries - 1, " ", ConsoleColor.Gray);
                if (min > 0)
                    Tool.Write(OffsetLeft + 130, OffsetTop - 1, "A", ConsoleColor.Gray);
                else
                    Tool.Write(OffsetLeft + 130, OffsetTop - 1, " ", ConsoleColor.Gray);

                // Changes the color, as to highlight the current selected option
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
                // if it is "S" it will sort the list, reset the selector position and min values, as well as change the sorted by text, goes through Mechanic, VinNumber and DateTimevisit
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

        // Generic liste <-- Not Implemented as not the entire group knows it -->
        public string SelectorGeneric<T>(int maxEntries, List<T> list) where T : IObjects
        {
            if (maxEntries > list.Count)
                maxEntries = list.Count;

            var cursorState = Console.CursorVisible;    
            Console.CursorVisible = false;              
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

                cki = Console.ReadKey(true);

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
            } while (cki.Key != ConsoleKey.Enter);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(OffsetLeft, OffsetTop + selectorPos);
            Console.WriteLine(list[selectorPos].StringFormat);
            Console.ForegroundColor = color;

            Console.CursorVisible = cursorState;
            return list[selectorPos].ToString();
        }
    }
}
