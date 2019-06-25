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
        public static void MainWindow()
        {
            Frame frameMain = new Frame(frameDim);
            frameMain.AddHorizontalDivider(2, 0, frameDim[0]);
            frameMain.AddHorizontalDivider(35, 0, frameDim[0]);
            frameMain.AddVerticalDivider(32, 2, 35);

            Navigation menuSelection = new Navigation(frameMain, 3, 5, new string[] { "Customer Overview", "Car Overview", "Create Customer", "Exit Program" });

            bool run = true;
            do
            {
                int selectedCustomerIndex;
                string selectedCarVin;
                int selectedCarIndex;

                Console.Clear();
                frameMain.Print();
                switch (menuSelection.Selector())
                {
                    case 0:
                        string customerTitle = Tool.FormatString(Customer.buffer, new string[] { "#ID", "Name", "Addresse", "City", "PhoneNumber", "Email", "Customer Since" });

                        List<string> customerFormattedList = new List<string>();
                        foreach (var item in Customer.customerList)
                            customerFormattedList.Add(item.StringFormat);
                        Navigation navigationCustomer = new Navigation(frameMain, 34, 6, customerFormattedList.ToArray(), customerTitle);

                        navigationCustomer.PrintTitle();
                        selectedCustomerIndex = navigationCustomer.Selector(28, Customer.customerList);
                        if (selectedCustomerIndex >= 0)
                        {
                            Windows.CustomerWindow(selectedCustomerIndex);
                        }
                        break;
                    case 1:
                        string carTitle = Tool.FormatString(Car.buffer, new string[] { "#ID", "VIN Number", "Number plate", "Brand", "Model", "Year", "Kilometers", "Fuel Type", "Registration Date" });

                        List<string> carFormattedList = new List<string>();
                        foreach (var item in Car.carList)
                            carFormattedList.Add(item.StringFormat);
                        Navigation navigationCar = new Navigation(frameMain, 34, 6, carFormattedList.ToArray(), carTitle);

                        navigationCar.PrintTitle();
                        selectedCarVin = navigationCar.Selector(5, Car.carList);
                        selectedCarIndex = Car.carList.FindIndex(car => car.VinNumber == selectedCarVin);
                        if (selectedCarIndex >= 0)
                        {
                            Windows.CustomerWindow(Car.carList[selectedCarIndex].CustomerID);
                        }
                        break;
                    case 2:
                        Customer.Create(frameMain, 35, 3);
                        break;
                    case 3:
                        run = false;
                        Console.Clear();
                        break;
                    case -1:
                        run = false;
                        Console.Clear();
                        break;
                    default:
                        break;
                }
            } while (run);
        }

        public static void CustomerWindow(int selectedCustomerID)
        {
            int selectedCustomerIndex = Customer.customerList.FindIndex(customer => customer.CustomerID == selectedCustomerID);
            Console.Clear();

            // Makes the frame of the window.
            Frame frameCustomer = new Frame(frameDim);
            frameCustomer.AddHorizontalDivider(2, 0, frameDim[0]);
            frameCustomer.AddHorizontalDivider(35, 0, frameDim[0]);
            frameCustomer.AddHorizontalDivider(26, 32, frameDim[0]);
            frameCustomer.AddVerticalDivider(32, 2, 35);
            frameCustomer.AddVerticalDivider(110, 2, 26);
            Customer.Read(frameCustomer, 35, 3, Customer.customerList[selectedCustomerIndex]);

            // Creates a list of the selected customers cars
            List<Car> selectedCustomerCarList = new List<Car>();
            selectedCustomerCarList = Car.carList.Where(id => id.CustomerID == selectedCustomerID).ToList();
            string carTitle = Tool.FormatString(Car.buffer, new string[] { "#ID", "VIN Number", "Number plate", "Brand", "Model", "Year", "Kilometers", "Fuel Type", "Registration Date" });

            // Makes the menu to choose from.
            Navigation menuSelection = new Navigation(frameCustomer, 3, 5, new string[] { "Select Car", "Open Shop Visits", "Edit Customer", "Remove Customer", "Create Car", "Edit Car", "Remove Car", "Back" });

            // Default values set before going into the loop
            string selectedCarVin = "";
            int selectedCarIndex = -1;
            bool run = true;
            do
            {
                // Makes a list of the current selected customers cars, and populates it.
                if (selectedCustomerCarList.Count > 0)
                    Car.Overview(frameCustomer, 34, 27, carTitle, selectedCustomerCarList, 5);
                else
                {
                    Console.SetCursorPosition((frameDim[0] + 34 - 10) / 2, 28);
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("< No Cars >");
                    Console.ForegroundColor = color;
                }
                Customer.Read(frameCustomer, 35, 3, Customer.customerList[selectedCustomerIndex]);

                frameCustomer.Print();
                switch (menuSelection.Selector())
                {
                    case 0: // Select Car
                        if (selectedCustomerCarList.Count > 0)
                        {
                            List<string> carFormattedList = new List<string>();
                            foreach (var item in selectedCustomerCarList)
                                carFormattedList.Add(item.StringFormat);
                            Navigation navigationCar = new Navigation(frameCustomer, 34, 30, carFormattedList.ToArray(), carTitle);
                            //int selectedCustomerIndex = Customer.customerList.FindIndex(customer => customer.CustomerID == customerID);

                            navigationCar.PrintTitle();
                            selectedCarVin = navigationCar.Selector(5, selectedCustomerCarList);
                            selectedCarIndex = selectedCustomerCarList.FindIndex(car => car.VinNumber == selectedCarVin);
                            if (selectedCarIndex >= 0)
                            {
                                Car.Read(frameCustomer, selectedCustomerCarList[selectedCarIndex], 113, 3);
                            }
                        }
                        break;
                    case 1: // Open Shop Visits
                        if (selectedCarVin != "")
                        {
                            Console.Clear();
                            ShopVisitWindow(selectedCustomerCarList[selectedCarIndex].VinNumber);
                        }
                        break;
                    case 2: // Edit Customer
                        Customer.Update(frameCustomer, Customer.customerList[selectedCustomerIndex], 35, 3);
                        break;
                    case 3: // Remove Customer
                        // Need a work -- remove cars and vists first.
                        SQL.DeleteCustomer(selectedCustomerID);
                        run = false;
                        break;
                    case 4: // Create Car
                        Car.Create(frameCustomer, 113, 3, selectedCustomerID);
                        selectedCustomerCarList.Clear();
                        selectedCustomerCarList = Car.carList.Where(id => id.CustomerID == selectedCustomerID).ToList();
                        Car.Overview(frameCustomer, 34, 27, carTitle, selectedCustomerCarList, 5);
                        break;
                    case 5: // Edit Car
                        if (selectedCarIndex >= 0)
                        {
                            Car.Update(frameCustomer, selectedCustomerCarList[selectedCarIndex], 113, 3);
                            selectedCustomerCarList.Clear();
                            selectedCustomerCarList = Car.carList.Where(id => id.CustomerID == selectedCustomerID).ToList();
                            Car.Overview(frameCustomer, 34, 27, carTitle, selectedCustomerCarList, 5);
                        }
                        break;
                    case 6: // Remove Car
                        if (selectedCarVin != "")
                        {
                            SQL.DeleteCar(selectedCarVin);
                            selectedCustomerCarList.Clear();
                            selectedCustomerCarList = Car.carList.Where(id => id.CustomerID == selectedCustomerID).ToList();
                            Car.Overview(frameCustomer, 34, 27, carTitle, selectedCustomerCarList, 5);
                            selectedCarVin = "";
                            selectedCarIndex = -1;
                            Console.Clear();
                        }
                        break;
                    case 7:
                        Console.Clear();
                        run = false;
                        break;
                    case -1:
                        Console.Clear();
                        run = false;
                        break;
                    default:
                        break;
                }

            } while (run);
        }

        public static void ShopVisitWindow(string vinNumber)
        {
            int selectedCarIndex = Car.carList.FindIndex(car => car.VinNumber == vinNumber);
            Console.Clear();

            Frame frameVisit = new Frame(frameDim);
            frameVisit.AddHorizontalDivider(2, 0, frameDim[0]);
            frameVisit.AddHorizontalDivider(35, 0, frameDim[0]);
            frameVisit.AddHorizontalDivider(26, 32, frameDim[0]);
            frameVisit.AddVerticalDivider(32, 2, 35);
            frameVisit.Print();

            //Car.Read(frameVisit, 35, 3, Customer.customerList[selectedCarIndex]);

            List<ShopVisit> selectedShopVisitList = new List<ShopVisit>();
            selectedShopVisitList = ShopVisit.shopVisitList.Where(car => car.VinNumber == vinNumber).ToList();
            string shopVisitTitle = Tool.FormatString(ShopVisit.buffer, new string[] { "#ID", "Mechanic", "VIN", "KM Count", "Issue", "Note", "Date" });

            // Makes the menu to choose from.
            Navigation menuSelection = new Navigation(frameVisit, 3, 5, new string[] { "Select ShopVisit", "Create ShopVisit", "Edit ShopVisit", "Remove ShopVisit", "Back" });

            int selectedShopVisitIndex = -1;
            int selectedShopVisitID = -1;
            bool run = true;
            do
            {
                if (selectedShopVisitList.Count > 0)
                    ShopVisit.Overview(frameVisit, 34, 27, shopVisitTitle, selectedShopVisitList, 5);
                else
                {
                    Console.SetCursorPosition((frameDim[0] + 34 - 10) / 2, 28);
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("< No Entries >");
                    Console.ForegroundColor = color;
                }
                frameVisit.Print();
                switch (menuSelection.Selector())
                {
                    case 0: // Select
                        if (selectedShopVisitList.Count > 0)
                        {
                            List<string> shopVisitFormattedList = new List<string>();
                            foreach (var item in selectedShopVisitList)
                                shopVisitFormattedList.Add(item.StringFormat);
                            Navigation navigationCar = new Navigation(frameVisit, 34, 30, shopVisitFormattedList.ToArray(), shopVisitTitle);

                            navigationCar.PrintTitle();
                            selectedShopVisitID = navigationCar.Selector(5, selectedShopVisitList);
                            selectedShopVisitIndex = selectedShopVisitList.FindIndex(shopVisit => shopVisit.VisitID == selectedShopVisitID);
                            if (selectedCarIndex >= 0)
                            {
                                ShopVisit.Read(frameVisit, selectedShopVisitList[selectedShopVisitIndex], 35, 3);
                            }
                        }
                        break;
                    case 1: // Create
                        ShopVisit.Create(frameVisit, 35, 3);
                        selectedShopVisitList.Clear();
                        selectedShopVisitList = ShopVisit.shopVisitList.Where(car => car.VinNumber == vinNumber).ToList();
                        ShopVisit.Overview(frameVisit, 34, 27, shopVisitTitle, selectedShopVisitList, 5);
                        break;
                    case 2: // Update
                        if (selectedShopVisitIndex >= 0)
                        {
                            ShopVisit.Update(frameVisit, selectedShopVisitList[selectedShopVisitIndex], 35, 3);
                            selectedShopVisitList.Clear();
                            selectedShopVisitList = ShopVisit.shopVisitList.Where(car => car.VinNumber == vinNumber).ToList();
                            ShopVisit.Overview(frameVisit, 34, 27, shopVisitTitle, selectedShopVisitList, 5);
                        }
                        break;
                    case 3: // Delete
                        if (selectedShopVisitID > 0)
                        {
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
                        Console.Clear();
                        break;
                    case -1: // Escape
                        run = false;
                        Console.Clear();
                        break;
                    default:
                        break;
                }

            } while (run);
        }
    }
}