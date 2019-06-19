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
            SQL.CreateCustomer();
            //Customer.Create();
            //Customer Nicolai = new Customer(1, "Nicolai", "Friis", "asdvej 28", 3450, "Allerød", 34502329, "asd@asd.asd", DateTime.Now);
            //Customer.customerList.Add(Nicolai);
            //Nicolai.Print();
            Customer.customerList[0].Print();


            //UserInterface.Frame First = new UserInterface.Frame(5, 10);
            //First.Print();
            Console.ReadKey();
        }
    }
}
