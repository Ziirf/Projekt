using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Customer : IObjects
    {

        public static List<Customer> customerList = new List<Customer>();
        public static int[] buffer = { 0, 7, 30, 50, 73, 87, 112 };

        public int CustomerID { get; set; }

        string firstname;
        public string Firstname
        {
            get { return firstname; }
            // Anytime this value is set, it will take the value that is given, run it through "BuildPascalCase" first to ensure it is correctly formatted
            set { firstname = Tool.BuildPascalCase(value); }
        }

        string lastname;
        public string Lastname
        {
            get { return lastname; }
            // Anytime this value is set, it will take the value that is given, run it through "BuildPascalCase" first to ensure it is correctly formatted
            set { lastname = Tool.BuildPascalCase(value); }
        }

        string address;
        public string Address
        {
            get { return address; }
            // Anytime this value is set, it will take the value that is given, run it through "BuildPascalCase" first to ensure it is correctly formatted
            set { address = Tool.BuildPascalCase(value); }
        }

        public int ZipCode { get; set; }

        public string City { get; set; }

        public int PhoneNumber { get; set; }

        string eMail;
        public string EMail
        {
            get { return eMail; }
            // set the value ToLower to make sure it is correctly formatted
            set { eMail = value.ToLower(); }
        }

        public DateTime CreatedDate { get; set; }

        public string StringFormat { get; set; }

        public Customer(int customerID, string firstname, string lastname, string address, int zipCode, string city, int phoneNumber, string eMail, DateTime createdDate)
        {
            CustomerID = customerID;
            Firstname = firstname;
            Lastname = lastname;
            Address = address;
            ZipCode = zipCode;
            City = city;
            PhoneNumber = phoneNumber;
            EMail = eMail;
            CreatedDate = createdDate;
            StringFormat = Tool.FormatString(buffer, Info());
        }

        // Takes the values in the customer and add converts it into an array, the bool determinds if the names and city/zip should be combined in the final array
        public string[] Info(bool comb = true)
        {

            List<string> CustomerInfoList = new List<string>();
            string nameComb = Lastname + ", " + Firstname;
            string cityComb = ZipCode + ", " + City;

            if (comb == true)
                CustomerInfoList.AddRange(new string[] { CustomerID.ToString(), nameComb, Address, cityComb, PhoneNumber.ToString(), EMail, CreatedDate.ToString("dd-MM-yyyy") });
            else
                CustomerInfoList.AddRange(new string[] { CustomerID.ToString(), Firstname, Lastname, Address, ZipCode.ToString(), City, PhoneNumber.ToString(), EMail, CreatedDate.ToString("dd-MM-yyyy") });

            return CustomerInfoList.ToArray();
        }
        
        // Create a customer
        public static void Create(Frame frame, int left, int top)
        {
            string[] information = { "Firstname", "Lastname", "Address", "Zip code", "Phone number", "E-Mail" };
            top += frame.OffsetTop;
            left += frame.OffsetLeft;

            Console.SetCursorPosition(left, top);
            Console.WriteLine("Customer Form:");

            // Writes out the infomation like "Firstname: " - as to know what you are supposed to fill out
            for (int i = 0; i < information.Length; i++)
            {
                Console.SetCursorPosition(left, top + 2 + i);
                Console.Write(information[i] + ": ");
            }

            // sets the Value to the input that the user puts in
            string Firstname = Tool.BuildString(left + information[0].Length + 2, top + 2, 20);
            string Lastname = Tool.BuildString(left + information[1].Length + 2, top + 3, 20);
            string address = Tool.BuildString(left + information[2].Length + 2, top + 4, 40);
            int ZipCode = Tool.BuildInt(left + information[3].Length + 2, top + 5, 4);
            int phoneNumber = Tool.BuildInt(left + information[4].Length + 2, top + 6, 8);
            string email = Tool.BuildEmail(left + information[5].Length + 2, top + 7, 30).ToLower();

            // Runs it through the SQL Create fuction
            SQL.CreateCustomer(Firstname, Lastname, address, ZipCode, phoneNumber, email);
        }

        // Prints out the infomation about the Customer object to the screen
        public void Read(Frame frame, int offsetLeft, int offsetTop)
        {
            string[] information = { "ID", "Firstname", "Lastname", "Address", "Zip code", "City", "Phone number", "E-Mail" };
            string[] data = { CustomerID.ToString() ,Firstname, Lastname, Address, ZipCode.ToString(), City, PhoneNumber.ToString(), EMail };
            offsetTop += frame.OffsetTop;
            offsetLeft += frame.OffsetLeft;

            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.WriteLine("Customer Form:");

            for (int i = 0; i < information.Length; i++)
            {
                Console.SetCursorPosition(offsetLeft, offsetTop + 2 + i);
                Console.Write(information[i] + ": " + data[i]);
            }
        }

        // A combination of the above two, writes out all the information and allows you to edit it
        public void Update(Frame frame, int offsetLeft, int offsetTop)
        {
            string[] information = { "ID", "Firstname", "Lastname", "Address", "Zip code", "City", "Phone number", "E-Mail" };
            string[] data = { CustomerID.ToString(), Firstname, Lastname, Address, ZipCode.ToString(), City, PhoneNumber.ToString(), EMail };
            offsetTop += frame.OffsetTop;
            offsetLeft += frame.OffsetLeft;

            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.WriteLine("Customer Form:");

            for (int i = 0; i < information.Length; i++)
            {
                Console.SetCursorPosition(offsetLeft, offsetTop + 2 + i);
                Console.Write(information[i] + ": " + data[i]);
            }

            Tool.Write(offsetLeft + information[0].Length + 2, offsetTop + 2, CustomerID.ToString(), ConsoleColor.DarkGray);
            Tool.Write(offsetLeft + information[5].Length + 2, offsetTop + 7, City, ConsoleColor.DarkGray);
            string newFirstname = Tool.BuildString(offsetLeft + information[1].Length + 2, offsetTop + 3, 20, data[1]);
            string newLastname = Tool.BuildString(offsetLeft + information[2].Length + 2, offsetTop + 4, 20, data[2]);
            string newAddress = Tool.BuildString(offsetLeft + information[3].Length + 2, offsetTop + 5, 40, data[3]);
            int newZipCode = Tool.BuildInt(offsetLeft + information[4].Length + 2, offsetTop + 6, 4, data[4]);
            int newPhoneNumber = Tool.BuildInt(offsetLeft + information[6].Length + 2, offsetTop + 8, 8, data[6]);
            string newEmail = Tool.BuildEmail(offsetLeft + information[7].Length + 2, offsetTop + 9, 30, data[7]).ToLower();

            // calls the SQL funktion
            SQL.UpdateCustomer(newFirstname, newLastname, newAddress, newZipCode, newPhoneNumber, newEmail, CustomerID);
        }
    }
}