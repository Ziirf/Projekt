using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Projekt;

namespace Projekt
{
    class SQL
    {
        static readonly string path = Path.GetFullPath(Path.Combine(Path.Combine(Environment.CurrentDirectory), @"..\..\"));
        static readonly SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= " + path + "MechanicDB.mdf ;Integrated Security=True");
        //static SqlConnection con = new SqlConnection("Data Source=DESKTOP-8IO9ER9;Initial Catalog=AutoDB;Integrated Security=True");

        public static void ReadCustomerToObj()
        {
            con.Open();

            string query = "SELECT customerID, firstname, lastname, [address], Customer.zipCode, ZipAndCity.city, phoneNumber, eMail, createdDate FROM Customer left join ZipAndCity on Customer.zipCode = ZipAndCity.zipCode;";

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
                string city = dr["city"].ToString();
                int phoneNumber = Convert.ToInt32(dr["phoneNumber"]);
                string eMail = dr["eMail"].ToString();
                DateTime createdDate = Convert.ToDateTime(dr["createdDate"]);
                Customer.customerList.Add(new Customer(customerID, firstName, lastName, address, zipCode, city, phoneNumber, eMail, createdDate));
            }

            con.Close();
        }

        public static void CreateCustomer(string firstname, string lastname, string address, int zipCode, int phoneNumber, string eMail)
        {
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
            query = "SELECT TOP 1 customerID, firstname, lastname, [address], Customer.zipCode, ZipAndCity.city, phoneNumber, eMail, createdDate FROM Customer left join ZipAndCity on Customer.zipCode = ZipAndCity.zipCode ORDER BY CustomerID DESC ";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            DataRow dr = dt.Rows[0];
            int customerID = Convert.ToInt32(dr["customerID"]);
            DateTime creationDate = Convert.ToDateTime(dr["createdDate"]);
            string city = Convert.ToString(dr["city"]);

            // Creates an object and adds it to the customer list
            Customer.customerList.Add(new Customer(customerID, firstname, lastname, address, zipCode, city, phoneNumber, eMail, creationDate));
            con.Close();
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
            }
            con.Close();
        }


        public static void ReadShopVisitToObj()
        {
            con.Open();

            string query = "SELECT ShopVisit.visitID, ShopVisit.dateTimeVisit, ShopVisit.mechanic, ShopVisit.vinNumber, ShopVisit.kmCount, ShopVisit.issue, ShopVisit.notes FROM ShopVisit;";

            // Inserts the query into a data table
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            // A loop used to export data from the SQL database into objects
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                int visitID = Convert.ToInt32(dr["visitID"]);
                DateTime dateTimeVisit = Convert.ToDateTime(dr["dateTimeVisit"]);
                string mechanic = dr["mechanic"].ToString();
                string vinNumber = dr["vinNumber"].ToString();
                int kmCount = Convert.ToInt32(dr["kmCount"]);
                string issue = dr["issue"].ToString();
                string notes = dr["notes"].ToString();

                ShopVisit.ShopVisitList.Add(new ShopVisit(visitID, dateTimeVisit, mechanic, vinNumber, kmCount, issue, notes));
            }

            con.Close();
        }


        public static void CreateShopVisit(string mechanic, string vinNumber, int kmCount, string issue, string notes)
        {
            // Opens the connection
            SqlCommand cmd;
            con.Open();
            string query = "INSERT INTO ShopVisit(dateTimeVisit, mechanic, vinNumber, kmCount, issue, notes) VALUES (GETDATE(), @mechanic, @vinNumber, @kmCount, @issue, @notes);";

            // Using SqlCommand to inject the variables into the query string
            using (cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@mechanic", mechanic);
                cmd.Parameters.AddWithValue("@vinNumber", vinNumber);
                cmd.Parameters.AddWithValue("@kmCount", kmCount);
                cmd.Parameters.AddWithValue("@issue", issue);
                cmd.Parameters.AddWithValue("@notes", notes);
            }
            // Executes the query, and are therefore inserted into the database
            cmd.ExecuteNonQuery();

            // Pulls out the data for the newly made customer, to get back the information for the ID, date and status which created in SQL
            query = "SELECT TOP 1 ShopVisit.visitID, ShopVisit.dateTimeVisit, ShopVisit.mechanic, ShopVisit.vinNumber, ShopVisit.kmCount, ShopVisit.issue, ShopVisit.notes FROM ShopVisit ORDER BY visitID DESC;";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            DataRow dr = dt.Rows[0];
            int visitID = Convert.ToInt32(dr["visitID"]);
            DateTime dateTimeVisit = Convert.ToDateTime(dr["dateTimeVisit"]);


            // Creates an object and adds it to the customer list

            ShopVisit.ShopVisitList.Add(new ShopVisit(visitID, dateTimeVisit, mechanic, vinNumber, kmCount, issue, notes));
            con.Close();
        }


        public static void UpdateShopVisit(int visitID, DateTime dateTimeVisit, string mechanic, string vinNumber, int kmCount, string issue, string notes)
        {
            con.Open();
            string query = "UPDATE ShopVisit SET dateTimeVisit = @dateTimeVisit, mechanic = @mechanic, vinNumber = @vinNumber, kmCount = @kmCount, issue = @issue, notes = @notes WHERE visitID = @visitID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@dateTimeVisit", dateTimeVisit);
                cmd.Parameters.AddWithValue("@mechanic", mechanic);
                cmd.Parameters.AddWithValue("@vinNumber", vinNumber);
                cmd.Parameters.AddWithValue("@kmCount", kmCount);
                cmd.Parameters.AddWithValue("@issue", issue);
                cmd.Parameters.AddWithValue("@notes", notes);
                cmd.Parameters.AddWithValue("@visitID", visitID);

                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        public static void DeleteShopVisit(int visitID)
        {
            con.Open();
            string query = "DELETE FROM Car WHERE visitID = @visitID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@visitID", visitID);

                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        public static void ReadCarToObj()
        {
            con.Open();

            string query = "SELECT car.customerID, car.vinNumber, car.numberPlate, car.carBrand, car.carModel, car.productionYear, car.kmCount, car.fuelType, car.createdDate FROM Car;";

            // Inserts the query into a data table
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            // A loop used to export data from the SQL database into objects
            for (int i = 0; i < dt.Rows.Count; i++)
            { //int , string , string , string , string , int , int , string 
                DataRow dr = dt.Rows[i];
                int customerID = Convert.ToInt32(dr["customerID"]);
                string vinNumber = dr["vinNumber"].ToString();
                string numberPlate = dr["numberPlate"].ToString();
                string carBrand = dr["carBrand"].ToString();
                string carModel = dr["carModel"].ToString();
                int productionYear = Convert.ToInt32(dr["productionYear"]);
                int kmCount = Convert.ToInt32(dr["kmCount"]);
                string fuelType = dr["fuelType"].ToString();                
                DateTime createdDate = Convert.ToDateTime(dr["createdDate"]);

                Car.CarList.Add(new Car(customerID, vinNumber, numberPlate, carBrand, carModel, productionYear, kmCount, fuelType, createdDate));
            }

            con.Close();
        }


        public static void CreateCar(int customerID, string vinNumber, string numberPlate, string carBrand, string carModel, int productionYear, int kmCount, string fuelType)
        {
            // Opens the connection
            SqlCommand cmd;
            con.Open();
            string query = "INSERT INTO Car(customerID, vinNumber, numberPlate, carBrand, carModel, productionYear, kmCount, fuelType, createdDate) VALUES (@customerID, @vinNumber, @numberPlate, @carBrand, @carModel, @productionYear, @kmCount, @fuelType, GETDATE());";

            // Using SqlCommand to inject the variables into the query string
            using (cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@customerID", customerID);
                cmd.Parameters.AddWithValue("@vinNumber", vinNumber);
                cmd.Parameters.AddWithValue("@numberPlate", numberPlate);
                cmd.Parameters.AddWithValue("@carBrand", carBrand);
                cmd.Parameters.AddWithValue("@carModel", carModel);
                cmd.Parameters.AddWithValue("@productionYear", productionYear);
                cmd.Parameters.AddWithValue("@kmCount", kmCount);
                cmd.Parameters.AddWithValue("@fuelType", fuelType);

            }
            // Executes the query, and are therefore inserted into the database
            cmd.ExecuteNonQuery();

            // Pulls out the data for the newly made customer, to get back the information for the ID, date and status which created in SQL

            query = "SELECT TOP 1 customerID, vinNumber, numberPlate, carBrand, carModel, productionYear, kmCount, fuelType, createdDate FROM Car WHERE vinNumber = @vinNumber;";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            sda.SelectCommand.Parameters.AddWithValue("@vinNumber", vinNumber);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            DataRow dr = dt.Rows[0];

            DateTime createdDate = Convert.ToDateTime(dr["createdDate"]);


            // Creates an object and adds it to the customer list

            Car.CarList.Add(new Car(customerID, vinNumber, numberPlate, carBrand, carModel, productionYear, kmCount, fuelType, createdDate));
            con.Close();
        }


        public static void UpdateCar(int customerID, string vinNumber,string numberPlate, string carBrand, string carModel, int productionYear, int kmCount, string fuelType, DateTime createdDate)
        {
            con.Open();
            string query = "UPDATE Car SET customerID = @customerID, vinNumber = @ vinNumber, numberPlate = @numberPlate, carBrand = @carBrand, carModel = @carModel, productionYear = @productionYear, kmCount = @kmCount, fuelType = @fuelType, createdDate = @createdDate WHERE vinNumber = @vinNumber";
            
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@customerID", customerID);
                cmd.Parameters.AddWithValue("@vinNumber", vinNumber);
                cmd.Parameters.AddWithValue("@numberPlate", numberPlate);
                cmd.Parameters.AddWithValue("@carBrand", carBrand); 
                cmd.Parameters.AddWithValue("@carModel", carModel);
                cmd.Parameters.AddWithValue("@productionYear", productionYear);
                cmd.Parameters.AddWithValue("@kmCount", kmCount);
                cmd.Parameters.AddWithValue("@fuelType", fuelType);
                cmd.Parameters.AddWithValue("@createdDate", createdDate);                

                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        public static void DeleteCar(string vinNumber)
        {
            con.Open();
            string query = "DELETE FROM Car WHERE vinNumber = @vinNumber";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@vinNumber", vinNumber);

                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
    }
}
