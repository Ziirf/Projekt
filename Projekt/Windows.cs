using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Windows
    {
        static readonly int[] frameDim = { 190, 39, 0, 1 };
        static readonly string customerTitle = Tool.FormatString(Customer.buffer, new string[] { "#ID", "Name", "Addresse", "City", "PhoneNumber", "Email", "Customer Since" });
        static readonly string carTitle = Tool.FormatString(Car.buffer, new string[] { "#ID", "VIN Number", "Number plate", "Brand", "Model", "Year", "Kilometers", "Fuel Type", "Registration Date" });
        static readonly string shopVisitTitle = Tool.FormatString(ShopVisit.buffer, new string[] { "#ID", "Mechanic", "VIN", "KM Count", "Issue", "Note", "Date" });

        public static void MainWindow()
        {
            // Creates a Frame and names it frameMain, There there is added dividers to seperate the Window which the frame makes.
            Frame frameMain = new Frame(frameDim);
            frameMain.AddHorizontalDivider(2, 0, frameDim[0]);
            frameMain.AddHorizontalDivider(35, 0, frameDim[0]);
            frameMain.AddVerticalDivider(32, 2, 35);

            // Makes a Menu selection in the left side of the frame
            Navigation menuSelection = new Navigation(frameMain, 3, 5, new string[] { "Customer Overview", "Car Overview", "Create Customer", "Exit Program" });

            // variables used in the loop, with selfexplainatiory names
            int selectedCustomerID;
            string selectedCarVin;
            int selectedCarIndex;
            bool run = true;
            do
            {
                // Prints out the frame
                Console.Clear();
                frameMain.Print();
                Information(2, 31, "[Arrow Up] Navigate Up", "[Arrow Down] Navigate Down", "[Enter] Select", "[ESC] To Exit");
                switch (menuSelection.Selector())
                {
                    case 0: // Select Customer
                        if (Customer.customerList.Count > 0)
                        {
                            Information(2, 30, "[S] Sort List");
                            // Makes a list of strings and populates it
                            List<string> customerFormattedList = new List<string>();
                            foreach (var item in Customer.customerList)
                                customerFormattedList.Add(item.StringFormat);

                            // Instanciate a Nagation object
                            Navigation CustomerSelection = new Navigation(frameMain, 35, 6, customerFormattedList.ToArray(), customerTitle);
                            CustomerSelection.PrintTitle();

                            // Uses the object, and return the ID of the selected customer, if escape is pressed it will return -1.
                            selectedCustomerID = CustomerSelection.Selector(28, Customer.customerList);
                            if (selectedCustomerID >= 0)
                                Windows.CustomerWindow(selectedCustomerID);
                        }
                        else
                        {
                            NoListAvaiable(107, 4, "< No Customers >");
                            System.Threading.Thread.Sleep(2000);
                        }
                        break;
                    case 1: // Select Car
                        if (Car.carList.Count > 0)
                        {
                            Information(2, 30, "[S] Sort List");
                            // Makes a list of strings and populates it
                            List<string> carFormattedList = new List<string>();
                            foreach (var item in Car.carList)
                                carFormattedList.Add(item.StringFormat);

                            // Instanciate a Nagation object
                            Navigation navigationCar = new Navigation(frameMain, 35, 6, carFormattedList.ToArray(), carTitle);
                            navigationCar.PrintTitle();

                            // Uses the object, and return the VIN number of the selected Car, if escape is pressed it will return -1.
                            selectedCarVin = navigationCar.Selector(28, Car.carList);
                            selectedCarIndex = Car.carList.FindIndex(car => car.VinNumber == selectedCarVin);
                            if (selectedCarIndex >= 0)
                                Windows.CustomerWindow(Car.carList[selectedCarIndex].CustomerID, selectedCarVin);
                        }
                        else
                        {
                            NoListAvaiable(107, 4, "< No Cars >");
                            System.Threading.Thread.Sleep(2000);
                        }
                        break;
                    case 2: // Create Customer
                        Customer.Create(frameMain, 35, 3);
                        break;
                    case 3: // Exit
                        run = false;
                        Console.Clear();
                        break;
                    case -1: // Escape
                        run = false;
                        Console.Clear();
                        break;
                }
            } while (run);
        }

        public static void CustomerWindow(int selectedCustomerID, string selectedCarVin = null)
        {
            int selectedCustomerIndex = Customer.customerList.FindIndex(customer => customer.CustomerID == selectedCustomerID);

            // Makes the frame of the window.
            Frame frameCustomer = new Frame(frameDim);
            frameCustomer.AddHorizontalDivider(2, 0, frameDim[0]);
            frameCustomer.AddHorizontalDivider(35, 0, frameDim[0]);
            frameCustomer.AddHorizontalDivider(26, 32, frameDim[0]);
            frameCustomer.AddVerticalDivider(32, 2, 35);
            frameCustomer.AddVerticalDivider(110, 2, 26);

            // Creates a list of the selected customers cars
            List<Car> selectedCustomerCarList = new List<Car>();
            selectedCustomerCarList = Car.carList.Where(id => id.CustomerID == selectedCustomerID).ToList();

            // Makes the menu to choose from.
            Navigation menuSelection = new Navigation(frameCustomer, 3, 5, new string[] { "Select Car", "Open Shop Visits", "Edit Customer", "Remove Customer", "Create Car", "Edit Car", "Remove Car", "Back" });

            // Default values set before going into the loop
            int selectedCarIndex;
            if (selectedCarVin == null)
                selectedCarIndex = -1;
            else
                selectedCarIndex = selectedCustomerCarList.FindIndex(car => car.VinNumber == selectedCarVin);

            bool run = true;
            do
            {
                // Prints out the frame and customer Information
                Console.Clear();
                frameCustomer.Print();
                Information(2, 31, "[Arrow Up] Navigate Up", "[Arrow Down] Navigate Down", "[Enter] Select", "[ESC] Back");
                Customer.customerList[selectedCustomerIndex].Read(frameCustomer, 35, 3);

                // If the customer has any cars, a list of them will be shown, else it will show an error message with "No cars"
                if (selectedCustomerCarList.Count > 0)
                    Car.Overview(frameCustomer, 34, 27, carTitle, selectedCustomerCarList, 5);
                else
                    NoListAvaiable(107, 28, "< No Cars >");
                // If the a car is selected it will show in the car detail window
                if (selectedCarIndex >= 0)
                    selectedCustomerCarList[selectedCarIndex].Read(frameCustomer, 113, 3);

                switch (menuSelection.Selector())
                {
                    case 0: // Select Car case
                        // If the customer doesn't have a car, then this case wont do anything.
                        if (selectedCustomerCarList.Count > 0)
                        {
                            Information(2, 30, "[S] Sort List");
                            // Makes a list of the strings of formatted car 
                            List<string> carFormattedList = new List<string>();
                            foreach (var item in selectedCustomerCarList)
                                carFormattedList.Add(item.StringFormat);
                            Navigation navigationCar = new Navigation(frameCustomer, 34, 30, carFormattedList.ToArray(), carTitle);

                            navigationCar.PrintTitle();
                            selectedCarVin = navigationCar.Selector(5, selectedCustomerCarList);
                            selectedCarIndex = selectedCustomerCarList.FindIndex(car => car.VinNumber == selectedCarVin);
                        }
                        break;
                    case 1: // Open Shop Visits case
                        // If there isn't a selected Car then this case wont do anything
                        if (selectedCarVin != null)
                        {
                            ShopVisitWindow(selectedCustomerCarList[selectedCarIndex].VinNumber);
                        }
                        break;
                    case 2: // Edit Customer case
                        Customer.customerList[selectedCustomerIndex].Update(frameCustomer, 35, 3);
                        break;
                    case 3: // Remove Customer case
                        // Remove the customer, all his cars and all his shop visits
                        if (ConfirmDelete() == true)
                        {
                            SQL.DeleteCustomer(selectedCustomerID);
                            run = false;
                        }
                        //SQL.DeleteCustomer(selectedCustomerID);
                        //run = false;
                        break;
                    case 4: // Create Car case
                        // Creates a car, and refreshes the list of the customers cars
                        Car.Create(frameCustomer, 113, 3, selectedCustomerID);
                        selectedCustomerCarList.Clear();
                        selectedCustomerCarList = Car.carList.Where(id => id.CustomerID == selectedCustomerID).ToList();
                        Car.Overview(frameCustomer, 34, 27, carTitle, selectedCustomerCarList, 5);
                        break;
                    case 5: // Edit Car case
                        if (selectedCarIndex >= 0)
                        {
                            // Edit a car, and refreshes the list of the customers cars
                            selectedCustomerCarList[selectedCarIndex].Update(frameCustomer, 113, 3);
                            selectedCustomerCarList.Clear();
                            selectedCustomerCarList = Car.carList.Where(id => id.CustomerID == selectedCustomerID).ToList();
                            Car.Overview(frameCustomer, 34, 27, carTitle, selectedCustomerCarList, 5);
                        }
                        break;
                    case 6: // Remove Car case
                        if (selectedCarVin != null)
                        {
                            // Removes a car and its shop entry lists, updates the list and selection values
                            SQL.DeleteCar(selectedCarVin);
                            selectedCustomerCarList.Clear();
                            selectedCustomerCarList = Car.carList.Where(id => id.CustomerID == selectedCustomerID).ToList();
                            Car.Overview(frameCustomer, 34, 27, carTitle, selectedCustomerCarList, 5);
                            selectedCarVin = null;
                            selectedCarIndex = -1;
                        }
                        break;
                    case 7: // Back case
                        run = false;
                        break;
                    case -1: // If escape is pressed
                        run = false;
                        break;
                }

            } while (run);
        }

        public static void ShopVisitWindow(string vinNumber)
        {
            int selectedCarIndex = Car.carList.FindIndex(car => car.VinNumber == vinNumber);

            Frame frameVisit = new Frame(frameDim);
            frameVisit.AddHorizontalDivider(2, 0, frameDim[0]);
            frameVisit.AddHorizontalDivider(35, 0, frameDim[0]);
            frameVisit.AddHorizontalDivider(26, 32, frameDim[0]);
            frameVisit.AddVerticalDivider(32, 2, 35);
            

            List<ShopVisit> selectedShopVisitList = new List<ShopVisit>();
            selectedShopVisitList = ShopVisit.shopVisitList.Where(car => car.VinNumber == vinNumber).ToList();

            // Makes the menu to choose from.
            Navigation menuSelection = new Navigation(frameVisit, 3, 5, new string[] { "Select ShopVisit", "Create ShopVisit", "Edit ShopVisit", "Remove ShopVisit", "Back" });

            // variables used in the loop, with selfexplainatiory names
            int selectedShopVisitIndex = -1;
            int selectedShopVisitID = -1;
            bool run = true;
            do
            {
                // Prints out the frame and customer Information
                Console.Clear();
                frameVisit.Print();
                Information(2, 31, "[Arrow Up] Navigate Up", "[Arrow Down] Navigate Down", "[Enter] Select", "[ESC] Back");
                if (selectedShopVisitList.Count > 0)
                    ShopVisit.Overview(frameVisit, 34, 27, shopVisitTitle, selectedShopVisitList, 5);
                else
                    NoListAvaiable(107, 28, "< No Entries >");
                // If a entry is selected, show in entry detail window
                if (selectedShopVisitIndex >= 0)
                    selectedShopVisitList[selectedShopVisitIndex].Read(frameVisit, 35, 3);

                switch (menuSelection.Selector())
                {
                    case 0: // Select
                        if (selectedShopVisitList.Count > 0)
                        {
                            Information(2, 30, "[S] Sort List");
                            // Makes a list of the strings of formatted Shop Visits 
                            List<string> shopVisitFormattedList = new List<string>();
                            foreach (var item in selectedShopVisitList)
                                shopVisitFormattedList.Add(item.StringFormat);
                            Navigation navigationCar = new Navigation(frameVisit, 34, 30, shopVisitFormattedList.ToArray(), shopVisitTitle);

                            navigationCar.PrintTitle();
                            selectedShopVisitID = navigationCar.Selector(5, selectedShopVisitList);
                            selectedShopVisitIndex = selectedShopVisitList.FindIndex(shopVisit => shopVisit.VisitID == selectedShopVisitID);
                        }
                        break;
                    case 1: // Create Entry
                        // Creates a entry into shop visit and refreshes the cars shop visit list
                        ShopVisit.Create(frameVisit, 35, 3, vinNumber);
                        selectedShopVisitList.Clear();
                        selectedShopVisitList = ShopVisit.shopVisitList.Where(car => car.VinNumber == vinNumber).ToList();
                        ShopVisit.Overview(frameVisit, 34, 27, shopVisitTitle, selectedShopVisitList, 5);
                        break;
                    case 2: // Edit Entry
                        if (selectedShopVisitIndex >= 0)
                        {
                            // Edit a entry, and refreshes the list the cars shop visit list
                            selectedShopVisitList[selectedShopVisitIndex].Update(frameVisit, 35, 3);
                            selectedShopVisitList.Clear();
                            selectedShopVisitList = ShopVisit.shopVisitList.Where(car => car.VinNumber == vinNumber).ToList();
                            ShopVisit.Overview(frameVisit, 34, 27, shopVisitTitle, selectedShopVisitList, 5);
                        }
                        break;
                    case 3: // Delete Entry
                        if (selectedShopVisitID > 0)
                        {
                            // Removes a shop entry list and refreshes the list the cars shop visit list
                            SQL.DeleteShopVisit(selectedShopVisitID);
                            selectedShopVisitList.Clear();
                            selectedShopVisitList = ShopVisit.shopVisitList.Where(car => car.VinNumber == vinNumber).ToList();
                            ShopVisit.Overview(frameVisit, 34, 27, shopVisitTitle, selectedShopVisitList, 5);
                            selectedShopVisitID = -1;
                            selectedShopVisitIndex = -1;
                        }
                        break;
                    case 4: // Back
                        run = false;
                        break;
                    case -1: // If escape is pressed
                        run = false;
                        break;
                }

            } while (run);
        }

        // Generic metode man kunne have brugt -- Bruger den dog ikke da jeg er den eneste der kan forsvare hvad den gør
        private static List<string> BuildList<T>(List<T> importList) where T : IObjects
        {
            List<string> outputList = new List<string>();
            foreach (var item in importList)
                outputList.Add(item.StringFormat);
            return outputList;
        }

        private static void NoListAvaiable(int left, int top, string message)
        {
            var color = Console.ForegroundColor;
            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = color;
        }

        private static void Information(int left, int top, params string[] text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.SetCursorPosition(left, top + i);
                Console.Write(text[i]);
            }
        }

        private static bool ConfirmDelete()
        {
            int[] frameSize = { 40, 7 };
            int[] pos = { ((Console.WindowWidth - frameSize[0]) / 2), ((Console.WindowHeight - frameSize[1]) / 2) };
            Frame frame = new Frame(frameSize[0], frameSize[1], pos[0], pos[1]);
            for (int i = 0; i < frameSize[1] - 1; i++)
            {
                Tool.Write(pos[0], pos[1] + i, new string(' ', frameSize[0]));
            }

            string message = "Confirm Deletion";
            Tool.Write(pos[0] + ((frameSize[0] - message.Length) / 2), pos[1] + 2, message);
            message = "[Enter] Yes / [Esc] No";
            Tool.Write(pos[0] + ((frameSize[0] - message.Length) / 2), pos[1] + 3, message);
            frame.Print();

            ConsoleKeyInfo cki;
            bool output;

            do
            {
                cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                    output = true;
                else
                    output = false;

            } while (cki.Key != ConsoleKey.Enter && cki.Key != ConsoleKey.Escape);


            return output;
        }
    }
}