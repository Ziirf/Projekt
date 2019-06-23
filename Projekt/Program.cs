using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Program
    {
        static int[] winSize = { 190, 40 };            // Height, Width
        static int[] frameMainDim = { 190, 39, 0, 1 }; // Height, Width, offsetLeft, OffsetTop

        static void Main(string[] args)
        {
            StartUp();
            Frame FrameMain = new Frame(frameMainDim);
            FrameMain.AddVerticalDivider(2, 0, frameMainDim[0]);
            FrameMain.AddVerticalDivider(35, 0, frameMainDim[0]);
            FrameMain.AddHorizontalDivider(32, 2, 35);

            Navigation menuSelection = new Navigation(FrameMain, 3, 5, new string[] { "Customer Overview", "Car Overview", "Create Customer", "Search" });

            //int[] customerBuffer = { 0, 7, 30, 50, 73, 87, 112 };
            //string customerTitle = Tool.FormatString(customerBuffer, new string[] { "#ID", "Name", "Addresse", "City", "PhoneNumber", "Email", "Customer Since" });
            //List<string> customerFormattedList = new List<string>();
            //foreach (var item in Customer.customerList)
            //    customerFormattedList.Add(Tool.FormatString(customerBuffer, item.Info()));
            //Navigation customerSelection = new Navigation(FrameMain, 34, 6, customerFormattedList.ToArray(), customerTitle);

            //int[] carBuffer = { 0, 7, 22, 38, 48, 48, 58, 73, 86 };
            //string carTitle = Tool.FormatString(carBuffer, new string[] { "#ID", "VIN Number", "Number plate", "Brand", "Model", "Year", "Kilometers", "Fuel Type", "Registration Date" });
            //List<string> carFormattedList = new List<string>();
            //foreach (var item in Car.carList)
            //    carFormattedList.Add(Tool.FormatString(carBuffer, item.Info()));
            //Navigation carSelection = new Navigation(FrameMain, 34, 6, carFormattedList.ToArray(), carTitle);

            do
            {
                Console.Clear();
                FrameMain.Print();
                switch (menuSelection.Selector())
                {
                    case 0:
                        int[] customerBuffer = { 0, 7, 30, 50, 73, 87, 112 };
                        string customerTitle = Tool.FormatString(customerBuffer, new string[] { "#ID", "Name", "Addresse", "City", "PhoneNumber", "Email", "Customer Since" });
                        List<string> customerFormattedList = new List<string>();
                        foreach (var item in Customer.customerList)
                            customerFormattedList.Add(Tool.FormatString(customerBuffer, item.Info()));
                        Navigation customerSelection = new Navigation(FrameMain, 34, 6, customerFormattedList.ToArray(), customerTitle);

                        customerSelection.PrintTitle();
                        customerSelection.Selector();
                        break;
                    case 1:
                        int[] carBuffer = { 0, 7, 22, 38, 48, 48, 58, 73, 86 };
                        string carTitle = Tool.FormatString(carBuffer, new string[] { "#ID", "VIN Number", "Number plate", "Brand", "Model", "Year", "Kilometers", "Fuel Type", "Registration Date" });
                        List<string> carFormattedList = new List<string>();
                        foreach (var item in Car.carList)
                            carFormattedList.Add(Tool.FormatString(carBuffer, item.Info()));
                        Navigation carSelection = new Navigation(FrameMain, 34, 6, carFormattedList.ToArray(), carTitle);

                        carSelection.PrintTitle();
                        carSelection.Selector();
                        break;
                    case 2:
                        Customer.Create(FrameMain, 35, 3);
                        break;
                    default:
                        break;
                }
            } while (true);









            //int[] bufferCustomerOverview = { 0, 7, 30, 50, 73, 87, 112 };
            //string[] titlesCustomerOverview = { "#ID", "Name", "Addresse", "City", "PhoneNumber", "Email", "Customer Since" };
            //List<string> customerFormattedList = new List<string>();
            //foreach (var item in Customer.customerList)
            //    customerFormattedList.Add(item.FormatString(bufferCustomerOverview));

            //Customer.CustomerOverview(FrameMain, 34, 3, bufferCustomerOverview, titlesCustomerOverview);
            //int selectedCustomer = Navigation.Selector(FrameMain, 34, 6, customerFormattedList.ToArray());


            //string[] titlesCarOverview = { "#ID", "VIN Number", "Number plate", "Brand", "Model", "Year", "Kilometers", "Fuel Type", "Registration Date" };
            //int[] bufferCarOverview = { 0, 7, 22, 38, 48, 48, 58, 73, 86 };
            //FrameMain.CarOverview(34, 3, bufferCarOverview, titlesCarOverview);

            //ShopVisit.Create(FrameMain, 34, 3);
            //Car.Create(FrameMain, 34, 3);
            //Customer.Create(FrameMain, 34, 3);
        }

        static void StartUp()
        {
            // Sets the console size
            Console.SetWindowSize(winSize[0], winSize[1]);
            Console.SetBufferSize(winSize[0], winSize[1]);
            Console.SetWindowPosition(0, 0);
            Console.CursorVisible = false;

            // Reads all the information from the SQL server into the project
            SQL.ReadCustomerToObj();
            SQL.ReadCarToObj();
            SQL.ReadShopVisitToObj();
        }
    }
}
