using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Program
    {

        static void Main(string[] args)
        {
            int[] winSize = { 190, 40 };            // Height, Width
            int[] frameMainDim = { 190, 39, 0, 1 }; // Height, Width, offsetLeft, OffsetTop

            // Sets the console size
            Console.SetWindowSize(winSize[0], winSize[1]);
            Console.SetBufferSize(winSize[0], winSize[1]);
            Console.SetWindowPosition(0, 0);

            // Reads all the information from the SQL server into the project
            SQL.ReadCustomerToObj();
            SQL.ReadCarToObj();
            SQL.ReadShopVisitToObj();

            Frame FrameMain = new Frame(frameMainDim);
            FrameMain.AddVerticalDivider(2, 0, frameMainDim[0]);
            FrameMain.AddVerticalDivider(35, 0, frameMainDim[0]);
            FrameMain.AddHorizontalDivider(32, 2, 35);
            FrameMain.Print();

            //string[] titlesCustomerOverview = { "#ID", "Name", "Addresse", "City", "PhoneNumber", "Email", "Customer Since" };
            //int[] bufferCustomerOverview = { 0, 7, 30, 50, 73, 87, 112 };
            //FrameMain.CustomerOverview(34, 3, bufferCustomerOverview, titlesCustomerOverview);

            //string[] titlesCarOverview = { "#ID", "VIN Number", "Number plate", "Brand", "Model", "Year", "Kilometers", "Fuel Type", "Registration Date" };
            //int[] bufferCarOverview = { 0, 7, 22, 38, 48, 48, 58, 73, 86 };
            //FrameMain.CarOverview(34, 3, bufferCarOverview, titlesCarOverview);

            //string firstname, string lastname, string address, int zipCode, int phoneNumber, string eMail
            string[] infoCustomer = {"Firstname", "Lastname", "Address", "Zip code", "Phone number", "E-Mail"};
            FrameMain.CreateCustomer(34, 3, infoCustomer);

            Console.ReadKey();
        }
    }
}
