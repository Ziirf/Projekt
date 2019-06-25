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
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Address { get; set; }

        public int ZipCode { get; set; }

        public string City { get; set; }

        public int PhoneNumber { get; set; }

        public string EMail { get; set; }

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
        
        public static void Create(Frame frame, int left, int top)
        {
            string[] information = { "Firstname", "Lastname", "Address", "Zip code", "Phone number", "E-Mail" };
            top += frame.OffsetTop;
            left += frame.OffsetLeft;

            Console.SetCursorPosition(left, top);
            Console.WriteLine("Customer Form:");

            for (int i = 0; i < information.Length; i++)
            {
                Console.SetCursorPosition(left, top + 2 + i);
                Console.Write(information[i] + ": ");
            }

            string Firstname = Tool.BuildString(left + information[0].Length + 2, top + 2, 20);
            string Lastname = Tool.BuildString(left + information[1].Length + 2, top + 3, 20);
            string address = Tool.BuildString(left + information[2].Length + 2, top + 4, 40);
            int ZipCode = Tool.BuildInt(left + information[3].Length + 2, top + 5, 4);
            int phoneNumber = Tool.BuildInt(left + information[4].Length + 2, top + 6, 8);
            string email = Tool.BuildEmail(left + information[5].Length + 2, top + 7, 30).ToLower();

            SQL.CreateCustomer(Firstname, Lastname, address, ZipCode, phoneNumber, email);
        }
        public void Read(Frame frame, int offsetLeft, int offsetTop)
        {
            string[] information = { "ID", "Firstname", "Lastname", "Address", "Zip code", "Phone number", "E-Mail" };
            string[] data = { CustomerID.ToString() ,Firstname, Lastname, Address, ZipCode.ToString(), PhoneNumber.ToString(), EMail };
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

        public void Update(Frame frame, int offsetLeft, int offsetTop)
        {
            string[] information = { "ID", "Firstname", "Lastname", "Address", "Zip code", "Phone number", "E-Mail" };
            string[] data = { CustomerID.ToString(), Firstname, Lastname, Address, ZipCode.ToString(), PhoneNumber.ToString(), EMail };
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
            string newFirstname = Tool.BuildString(offsetLeft + information[1].Length + 2, offsetTop + 3, 20, data[1]);
            string newLastname = Tool.BuildString(offsetLeft + information[2].Length + 2, offsetTop + 4, 20, data[2]);
            string newAddress = Tool.BuildString(offsetLeft + information[3].Length + 2, offsetTop + 5, 40, data[3]);
            int newZipCode = Tool.BuildInt(offsetLeft + information[4].Length + 2, offsetTop + 6, 4, data[4]);
            int newPhoneNumber = Tool.BuildInt(offsetLeft + information[5].Length + 2, offsetTop + 7, 8, data[5]);
            string newEmail = Tool.BuildEmail(offsetLeft + information[6].Length + 2, offsetTop + 8, 30, data[6]).ToLower();

            SQL.UpdateCustomer(newFirstname, newLastname, newAddress, newZipCode, newPhoneNumber, newEmail, CustomerID);
        }
    }
}



//public void Print(int offsetLeft, int offsetTop, int[] buffer)
//{
//    string[] strArrayCustomer = Info();
//    for (int i = 0; i < buffer.Length; i++)
//    {
//        Console.SetCursorPosition(offsetLeft + buffer[i], offsetTop + 2);
//        Console.WriteLine(strArrayCustomer[i]);
//    }
//}

//public string FormatString(int[] buffer)
//{
//    string output = "";
//    int currentStringLength = 0;

//    string[] InfoArray = Info();
//    for (int i = 0; i < buffer.Length; i++)
//    {
//        if (buffer[i] > currentStringLength)
//        {
//            int temp = buffer[i] - currentStringLength;
//            output += new string(' ', temp);
//        }
//        else if (buffer[i] < currentStringLength)
//        {
//            int temp = currentStringLength - buffer[i];
//            output = output.Substring(0, output.Length - temp);
//        }
//        output += InfoArray[i];
//        currentStringLength = output.Length;
//    }

//    return output;
//}

//public static void CustomerOverview(Frame frame, int offsetLeft, int offsetTop, int[] buffer, string[] titles)
//{
//    offsetTop += frame.OffsetTop;
//    offsetLeft += frame.OffsetLeft;

//    for (int i = 0; i < titles.Length; i++)
//    {
//        Console.SetCursorPosition(offsetLeft + buffer[i], offsetTop);
//        Console.Write(titles[i]);
//    }

//    for (int i = 0; i < customerList.Count; i++)
//    {
//        Console.SetCursorPosition(offsetLeft, offsetTop + i + 2);
//        Console.Write(Customer.customerList[i].FormatString(buffer));
//    }
//}


//public static void Read(Frame frame, int offsetLeft, int offsetTop)
//{
//    string[] information = { "Firstname", "Lastname", "Address", "Zip code", "Phone number", "E-Mail" };
//    offsetTop += frame.OffsetTop;
//    offsetLeft += frame.OffsetLeft;

//    Console.SetCursorPosition(offsetLeft, offsetTop);
//    Console.WriteLine("Customer Form:");

//    for (int i = 0; i < information.Length; i++)
//    {
//        Console.SetCursorPosition(offsetLeft, offsetTop + 2 + i);
//        Console.Write(information[i] + ": ");
//    }
//}