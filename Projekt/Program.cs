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
            // Sets the console size
            int[] winSize = { 190, 40 }; // Height, Width
            Console.SetWindowSize(winSize[0], winSize[1]);
            Console.SetBufferSize(winSize[0], winSize[1]);
            Console.SetWindowPosition(0, 0);
            Console.CursorVisible = false;

            // Reads all the information from the SQL server into the project
            SQL.ReadCustomerToObj();
            SQL.ReadCarToObj();
            SQL.ReadShopVisitToObj();

            //Windows.ShopVisitWindow("ASD123");
            Windows.MainWindow();
        }
    }
    interface ITester
    {
        
    }
}
