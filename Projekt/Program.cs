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
            SQL.ReadCustomerToObj();
            SQL.ReadShopVisitToObj();
            foreach (Customer customer in Customer.customerList)
            {
                Console.Write(customer.CustomerID + "\t ");
                Console.Write(customer.Firstname + "\t ");
                Console.WriteLine(customer.City);
            }
            SQL.CreateShopVisit("dan", "asd456789dsa", 35000, "test", "det virker");
            foreach (ShopVisit visit in ShopVisit.ShopVisitList)
            {
                Console.Write(visit.VinNumber + "\t");
                Console.WriteLine(visit.VisitID);
            }
            

            Console.ReadKey();
        }
    }
}
