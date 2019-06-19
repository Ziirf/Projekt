using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace Projekt
{
    class SQL
    {
        static readonly string path = Path.GetFullPath(Path.Combine(Path.Combine(Environment.CurrentDirectory), @"..\..\"));
        static readonly SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= " + path + "MechanicDB.mdf ;Integrated Security=True");
        //static SqlConnection con = new SqlConnection("Data Source=DESKTOP-8IO9ER9;Initial Catalog=AutoDB;Integrated Security=True");

        public static void ReadCustomerToObj()
        {

            // int customerID, string firstname, string lastname, string address, int zipCode, string city, int phoneNumber, string eMail
            con.Open();

            string query = "SELECT Customer.customerID, Customer.firstname, Customer.lastname, Customer.[address], Customer.zipCode, ZipAndCity.city, Customer.phoneNumber, Customer.eMail, Customer.createdDate FROM Customer left join ZipAndCity on Customer.zipCode = ZipAndCity.zipCode;";
            //string query = "SELECT Customer.customerID, Customer.firstname, Customer.lastname, Customer.[address], Customer.zipCode, ZipAndCity.city, Customer.phoneNumber, Customer.eMail, Customer.createdDate FROM Customerleft join ZipAndCityon Customer.zipCode = ZipAndCity.zipCode; ";
            //string query = "SELECT customerID, firstname, lastname, [address], zipCode, phoneNumber, eMail, createdDate FROM Customer";

            // Inserts the query into a data table
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            // A loop used to export data from the SQL database into objects
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                int customerID = Convert.ToInt32(dr["customerID"]);
                string firstName = dr["firstName"].ToString();
                string lastName = dr["lastName"].ToString();
                string address = dr["address"].ToString();
                int zipCode = Convert.ToInt32(dr["zipCode"]);
                //string city = dr["city"].ToString();
                int phoneNumber = Convert.ToInt32(dr["phoneNumber"]);
                string eMail = dr["eMail"].ToString();
                DateTime createdDate = Convert.ToDateTime(dr["createdDate"]);
                Customer.customerList.Add(new Customer(customerID, firstName, lastName, address, zipCode, "placeholder", phoneNumber, eMail, createdDate));
            }

            con.Close();
        }

        public static void CreateCustomer()
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

            // Opens the connection
            SqlCommand cmd;
            con.Open();
            string query = "INSERT INTO Customer(firstname, lastname, [address], zipCode, phoneNumber, email, createdDate) VALUES (@firstName, @lastName, @address, @zipCode, @phoneNumber, @eMail, GETDATE());";

            // Using SqlCommand to inject the variables into the query string
            using (cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@firstName", firstname);
                cmd.Parameters.AddWithValue("@lastName", lastname);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@zipCode", zipCode);
                cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                cmd.Parameters.AddWithValue("@eMail", eMail);
            }
            // Executes the query, and are therefore inserted into the database
            cmd.ExecuteNonQuery();

            // Pulls out the data for the newly made customer, to get back the information for the ID, date and status which created in SQL
            // query = "SELECT TOP 1 * FROM Customer join ZipAndCityon Customer.zipCode = ZipAndCity.zipCode ORDER BY CustomerID DESC";
            query = "SELECT TOP 1 Customer.customerID, Customer.firstname, Customer.lastname, Customer.[address], Customer.zipCode, ZipAndCity.city, Customer.phoneNumber, Customer.eMail, Customer.createdDate FROM Customer left join ZipAndCity on Customer.zipCode = ZipAndCity.zipCode ORDER BY CustomerID DESC ";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            DataRow dr = dt.Rows[0];
            int customerID = Convert.ToInt32(dr["customerID"]);
            DateTime creationDate = Convert.ToDateTime(dr["createdDate"]);
            string city = Convert.ToString(dr["city"]);

            // Creates an object and adds it to the customer list
            Customer.customerList.Add(new Customer(customerID, firstname, lastname, address, zipCode, city, phoneNumber, eMail, creationDate));
            //Program.customerList.Add(new Customer(customerID, firstName, lastName, creationDate, phoneNumber, email, status));
        }

        public static void UpdateCustomer(string firstname, string lastname, string address, int zipCode, int phoneNumber, string eMail, int customerID)
        {
            con.Open();
                string query = "UPDATE Customer SET firstname = @firstname, lastname = @lastname, [address] = @address, zipCode = @zipCode, phoneNumber = @phoneNumber, eMail = @eMail WHERE customerID = @customerID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@firstname", firstname);
                    cmd.Parameters.AddWithValue("@lastname", lastname);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@PzipCode", zipCode);
                    cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@eMail", eMail);
                    cmd.Parameters.AddWithValue("@customerID", customerID);

                    cmd.ExecuteNonQuery();
         
                } 
            con.Close();
        }

        public static void DeleteCustomer(int customerID)
        {
            con.Open();
                string query = "DELETE FROM Customer WHERE customerID = @customerID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@customerID", customerID);

                    cmd.ExecuteNonQuery();
                //friis er stiv
                } 
            con.Close();
        }

        public static void ReadShopVisit()
        {
    
        }

        public static void CreateShopVisit()
        {
            
        }

        public static void UpdateShopVisit()
        {

        }

        public static void DeleteShopVisit()
        {

        }

         public static void ReadCar()
        {
    
        }

        public static void CreateCar()
        {
            
        }

        public static void UpdateCar()
        {

        }

        public static void DeleteCar()
        {

        }


    }
}
