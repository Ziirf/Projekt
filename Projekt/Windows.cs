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
            Frame FrameMain = new Frame(frameDim);
            FrameMain.AddHorizontalDivider(2, 0, frameDim[0]);
            FrameMain.AddHorizontalDivider(35, 0, frameDim[0]);
            FrameMain.AddVerticalDivider(32, 2, 35);

            Navigation menuSelection = new Navigation(FrameMain, 3, 5, new string[] { "Customer Overview", "Car Overview", "Create Customer", "Exit Program" });

            bool run = true;
            do
            {
                int selectedCustomerIndex;
                string selectedCarVin;
                int selectedCarIndex;

                Console.Clear();
                FrameMain.Print();
                switch (menuSelection.Selector())
                {
                    case 0:
                        string customerTitle = Tool.FormatString(Customer.buffer, new string[] { "#ID", "Name", "Addresse", "City", "PhoneNumber", "Email", "Customer Since" });

                        List<string> customerFormattedList = new List<string>();
                        foreach (var item in Customer.customerList)
                            customerFormattedList.Add(item.StringFormat);
                        Navigation navigationCustomer = new Navigation(FrameMain, 34, 6, customerFormattedList.ToArray(), customerTitle);

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
                        Navigation navigationCar = new Navigation(FrameMain, 34, 6, carFormattedList.ToArray(), carTitle);

                        navigationCar.PrintTitle();
                        selectedCarVin = navigationCar.Selector(5, Car.carList);
                        selectedCarIndex = Car.carList.FindIndex(car => car.VinNumber == selectedCarVin);
                        if (selectedCarIndex >= 0)
                        {
                            Windows.CustomerWindow(Car.carList[selectedCarIndex].CustomerID);
                        }
                        break;
                    case 2:
                        Customer.Create(FrameMain, 35, 3);
                        break;
                    case 3:
                        run = false;
                        break;
                    case -1:
                        run = false;
                        break;
                    default:
                        break;
                }
            } while (run);
        }


        public static void CustomerWindow(int selectedCustomerID = 0)
        {
            //int accIndex = Program.accountList.FindIndex(account => account.AccountNumber == Utility.SelectedAccountNumber);
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

            //// Makes a list of the current selected customers cars, and populates it.
            //List<Car> selectedCustomerCarList = new List<Car>();
            //selectedCustomerCarList = Car.carList.Where(id => id.CustomerID == selectedCustomerID).ToList();
            //string carTitle = Tool.FormatString(Car.buffer, new string[] { "#ID", "VIN Number", "Number plate", "Brand", "Model", "Year", "Kilometers", "Fuel Type", "Registration Date" });
            //if (selectedCustomerCarList.Count > 0)
            //    Car.Overview(frameCustomer, 34, 27, carTitle, selectedCustomerCarList, 5);
            //else
            //{
            //    Console.SetCursorPosition((frameDim[0] + 34 - 10) / 2, 28);
            //    var color = Console.ForegroundColor;
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine("< No Cars >");
            //    Console.ForegroundColor = color;
            //}
            //Customer.Read(frameCustomer, 35, 3, Customer.customerList[selectedCustomerIndex]);
            // Makes a list of the current selected customers cars, and populates it.

            List<Car> selectedCustomerCarList = new List<Car>();
            selectedCustomerCarList = Car.carList.Where(id => id.CustomerID == selectedCustomerID).ToList();
            string carTitle = Tool.FormatString(Car.buffer, new string[] { "#ID", "VIN Number", "Number plate", "Brand", "Model", "Year", "Kilometers", "Fuel Type", "Registration Date" });

            // Makes the menu to choose from.
            Navigation menuSelection = new Navigation(frameCustomer, 3, 5, new string[] { "Select Car", "Open Shop Visits", "Edit Customer", "Remove Customer", "Create Car", "Edit Car", "Remove Car", "Back" });

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
                        run = false;
                        break;
                    case -1:
                        run = false;
                        break;
                    default:
                        break;
                }

            } while (run);
        }
    }
}
