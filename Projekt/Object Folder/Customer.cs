using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Customer
    {
        #region Properties

        public static List<Customer> customerList = new List<Customer>();
        public static int[] buffer = { 0, 7, 30, 50, 73, 87, 112 };

        private int customerID;
        public int CustomerID
        {
            get { return customerID; }
            private set { customerID = value; }
        }

        private string firstname;
        public string Firstname
        {
            get { return firstname; }
            private set { firstname = value; }
        }

        private string lastname;
        public string Lastname
        {
            get { return lastname; }
            private set { lastname = value; }
        }

        private string address;
        public string Address
        {
            get { return address; }
            private set { address = value; }
        }

        private int zipCode;
        public int ZipCode
        {
            get { return zipCode; }
            private set { zipCode = value; }
        }

        private string city;
        public string City
        {
            get { return city; }
            private set { city = value; }
        }
        
        private int phoneNumber;
        public int PhoneNumber
        {
            get { return phoneNumber; }
            private set { phoneNumber = value; }
        }

        private string eMail;
        public string EMail
        {
            get { return eMail; }
            private set { eMail = value; }
        }

        private DateTime createdDate;
        public DateTime CreatedDate
        {
            get { return createdDate; }
            private set { createdDate = value; }
        }

        private string stringFormat;
        public string StringFormat
        {
            get { return stringFormat; }
            set { stringFormat = value; }
        }


        #endregion Properties

        //private Customer()
        //{

        //}

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
            stringFormat = Tool.FormatString(buffer, Info());
        }

        public string[] Info(bool comb = true)
        {
            List<string> CustomerInfoList = new List<string>();
            string nameComb = lastname + ", " + firstname;
            string cityComb = zipCode + ", " + city;

            if (comb == true)
                CustomerInfoList.AddRange(new string[] { customerID.ToString(), nameComb, address, cityComb, phoneNumber.ToString(), eMail, createdDate.ToString("dd-MM-yyyy") });
            else
                CustomerInfoList.AddRange(new string[] { customerID.ToString(), firstname, lastname, address, zipCode.ToString(), city, phoneNumber.ToString(), eMail, createdDate.ToString("dd-MM-yyyy") });

            return CustomerInfoList.ToArray();
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

        public static void Create(Frame frame, int offsetLeft, int offsetTop)
        {
            string[] information = { "Firstname", "Lastname", "Address", "Zip code", "Phone number", "E-Mail" };
            offsetTop += frame.OffsetTop;
            offsetLeft += frame.OffsetLeft;

            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.WriteLine("Customer Form:");

            for (int i = 0; i < information.Length; i++)
            {
                Console.SetCursorPosition(offsetLeft, offsetTop + 2 + i);
                Console.Write(information[i] + ": ");
            }
            ConsoleColor[] palette = { Console.ForegroundColor, Console.BackgroundColor, ConsoleColor.Black, ConsoleColor.White };

            string firstname = Tool.BuildString(offsetLeft + information[0].Length + 2, offsetTop + 2, 20);
            string lastname = Tool.BuildString(offsetLeft + information[1].Length + 2, offsetTop + 3, 20);
            string address = Tool.BuildString(offsetLeft + information[2].Length + 2, offsetTop + 4, 40);
            int zipcode = Tool.BuildInt(offsetLeft + information[3].Length + 2, offsetTop + 5, 4);
            int phoneNumber = Tool.BuildInt(offsetLeft + information[4].Length + 2, offsetTop + 6, 8);
            string email = Tool.BuildEmail(offsetLeft + information[5].Length + 2, offsetTop + 7, 30);

            SQL.CreateCustomer(firstname, lastname, address, zipcode, phoneNumber, email);
        }

        //private static void ColorChange(ConsoleColor[] palette, bool oldColor = true)
        //{
        //    ConsoleColor[] colorOld = { Console.ForegroundColor, Console.BackgroundColor };
        //    ConsoleColor[] color = { ConsoleColor.Black, ConsoleColor.White };

        //    if (oldColor == true)
        //    {
        //        Console.ForegroundColor = palette[0];
        //        Console.BackgroundColor = palette[1];
        //    }
        //    else
        //    {
        //        Console.ForegroundColor = palette[3];
        //        Console.BackgroundColor = palette[4];
        //    }
        //}
    }
}
