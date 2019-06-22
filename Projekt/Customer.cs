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

        private int customerID;
        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        private string firstname;
        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }

        private string lastname;
        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private int zipCode;
        public int ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; }
        }

        private string city;
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        
        private int phoneNumber;
        public int PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        private string eMail;
        public string EMail
        {
            get { return eMail; }
            set { eMail = value; }
        }

        private DateTime createdDate;
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        #endregion Properties

        private Customer()
        {

        }

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

        public void Print()
        {
            Console.WriteLine(CustomerID);
            Console.WriteLine(Firstname);
            Console.WriteLine(Lastname);
            Console.WriteLine(Address);
            Console.WriteLine(ZipCode);
            Console.WriteLine(City);
            Console.WriteLine(PhoneNumber);
            Console.WriteLine(EMail);
            Console.WriteLine(CreatedDate.ToString("dd-MM-yyyy"));
        }

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
            string firstname = Tool.BuildString(offsetLeft + information[0].Length + 2, offsetTop + 2, 20);
            string lastname = Tool.BuildString(offsetLeft + information[1].Length + 2, offsetTop + 3, 20);
            string address = Tool.BuildString(offsetLeft + information[2].Length + 2, offsetTop + 4, 40);
            int zipcode = Tool.BuildInt(offsetLeft + information[3].Length + 2, offsetTop + 5, 4);
            int phoneNumber = Tool.BuildInt(offsetLeft + information[4].Length + 2, offsetTop + 6, 8);
            string email = Tool.BuildEmail(offsetLeft + information[5].Length + 2, offsetTop + 7, 30);

            SQL.CreateCustomer(firstname, lastname, address, zipcode, phoneNumber, email);
        }
    }
}
