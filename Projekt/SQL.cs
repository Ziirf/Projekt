using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Projekt
{
    class SQL
    {
        static SqlConnection con = new SqlConnection("Data Source=DESKTOP-8IO9ER9;Initial Catalog=AutoDB;Integrated Security=True");

        public static void ReadCustomerToObj()
        {

            // int customerID, string firstname, string lastname, string address, int zipCode, string city, int phoneNumber, string eMail
            con.Open();

            string query = "SELECT customerID, firstname, lastname, [address], zipCode, phoneNumber, eMail, createdDate FROM Customer";

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
                string address = dr["[address]"].ToString();
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
            string query = "INSERT INTO Customer(firstname, lastname, [address], zipCode, phoneNumber, email, ) VALUES (@firstName, @lastName, @phoneNumber, @eMail, GETDATE());";

            // Using SqlCommand to inject the variables into the query string
            using (cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                cmd.Parameters.AddWithValue("@Email", email);
            }
            // Executes the query, and are therefore inserted into the database
            cmd.ExecuteNonQuery();

            // Pulls out the data for the newly made customer, to get back the information for the ID, date and status which created in SQL
            query = "SELECT TOP 1 * FROM Customer ORDER BY CustomerID DESC";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            DataRow dr = dt.Rows[0];
            int customerID = Convert.ToInt32(dr["CustomerID"]);
            DateTime creationDate = Convert.ToDateTime(dr["CreationDate"]);
            string status = Convert.ToString(dr["Status"]);

            // Creates an object and adds it to the customer list
            Program.customerList.Add(new Customer(customerID, firstName, lastName, creationDate, phoneNumber, email, status));
        }

        public static void UpdateCustomer()
        {

        }

        public static void DeleteCustomer()
        {

        }
    }
}
