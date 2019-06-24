using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Windows
    {
        static int[] frameDim = { 190, 39, 0, 1 };
        public static void MainWindow()
        {
            Frame FrameMain = new Frame(frameDim);
            FrameMain.AddHorizontalDivider(2, 0, frameDim[0]);
            FrameMain.AddHorizontalDivider(35, 0, frameDim[0]);
            FrameMain.AddVerticalDivider(32, 2, 35);

            Navigation menuSelection = new Navigation(FrameMain, 3, 5, new string[] { "Customer Overview", "Car Overview", "Create Customer", "Exit Program" });

            do
            {
                int selectedCustomer;
                int selectedCar;

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
                        selectedCustomer = navigationCustomer.Selector(28, Customer.customerList);
                        if (selectedCustomer >= 0)
                        {

                        }
                        break;
                    case 1:
                        string carTitle = Tool.FormatString(Car.buffer, new string[] { "#ID", "VIN Number", "Number plate", "Brand", "Model", "Year", "Kilometers", "Fuel Type", "Registration Date" });

                        List<string> carFormattedList = new List<string>();
                        foreach (var item in Car.carList)
                            carFormattedList.Add(item.StringFormat);
                        Navigation navigationCar = new Navigation(FrameMain, 34, 6, carFormattedList.ToArray(), carTitle);

                        navigationCar.PrintTitle();
                        selectedCar = navigationCar.Selector(5);
                        if (selectedCar >= 0)
                        {

                        }
                        break;
                    case 2:
                        Customer.Create(FrameMain, 35, 3);
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
            } while (true);
        }

        public static void CustomerWindow()
        {
            Frame frameCustomer = new Frame(frameDim);
            frameCustomer.AddHorizontalDivider(2, 0, frameDim[0]);
            frameCustomer.AddHorizontalDivider(35, 0, frameDim[0]);
            frameCustomer.AddHorizontalDivider(26, 32, frameDim[0]);
            frameCustomer.AddVerticalDivider(32, 2, 35);

            Navigation menuSelection = new Navigation(frameCustomer, 3, 5, new string[] { "Select Car", "Open Shop Visits", "Edit Customer", "Remove Customer", "Edit Car", "Remove Car" });
            do
            {
                int selectedCustomer;
                int selectedCar;

                Console.Clear();
                frameCustomer.Print();
                switch (menuSelection.Selector())
                {
                    case 0:
                        string carTitle = Tool.FormatString(Car.buffer, new string[] { "#ID", "VIN Number", "Number plate", "Brand", "Model", "Year", "Kilometers", "Fuel Type", "Registration Date" });

                        List<string> carFormattedList = new List<string>();
                        foreach (var item in Car.carList)
                            carFormattedList.Add(item.StringFormat);
                        Navigation navigationCar = new Navigation(frameCustomer, 34, 30, carFormattedList.ToArray(), carTitle);

                        navigationCar.PrintTitle();
                        selectedCar = navigationCar.Selector(2);
                        if (selectedCar >= 0)
                        {

                        }
                        break;
                    default:
                        break;
                }

            } while (true);
        }
    }
}
