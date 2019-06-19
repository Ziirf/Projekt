using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Tool
    {
        public static void FillOut()
        {
            // Takes the info from the textbox and stores it in variables
            Console.Write("Customer firstname:");
            string firstname = Console.ReadLine();
            Console.Write("Customer lastname:");
            string lastname = Console.ReadLine();
            Console.Write("Customer address:");
            string address = Console.ReadLine();
            Console.Write("Customer zip code:");
            int zipCode = Convert.ToInt32(Console.ReadLine());
            Console.Write("Customer Phonenumber:");
            int phoneNumber = Convert.ToInt32(Console.ReadLine());
            Console.Write("Customer e-mail:");
            string eMail = Console.ReadLine();
            SQL.CreateCustomer(firstname, lastname, address, zipCode, phoneNumber, eMail);
        }
    }
}
