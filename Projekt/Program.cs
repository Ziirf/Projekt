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
            foreach (Customer customer in Customer.customerList)
            {
                Console.Write(customer.CustomerID + " ");
                Console.WriteLine(customer.Firstname);
            }


            Console.ReadKey();
        }
    }
}
