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

        }

        public static void UpdateCustomer()
        {

        }

        public static void DeleteCustomer()
        {

        }
    }
}
