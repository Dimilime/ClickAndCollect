using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL
{
    public class CustomerDAL: ICustomerDAL
    {
        private string connectionString;

        public CustomerDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Boolean EmailExists(Customer c)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * from Person WHERE EMAIL = @Email AND TYPE = 'Customer'", connection);
                connection.Open();
                //using (SqlDataReader reader = cmd.ExecuteReader())
                //{
                //    while (reader.Read())
                //    {
                //        result = true;
                //    }
                //}
            }

            return result;
        }
        public void AddCustomer(Customer c)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Person (LastName, FirstName, Email, Password, Type) VALUES ('tit', 'oio', 'jnkd@fds.dd', 'lol', 'Customer')", connection);
                //SqlCommand cmd2 = new SqlCommand("INSERT INTO Customer(DoB, PhoneNumber) VALUES (@Dob, @PhoneNumber)", connection);
                cmd.Parameters.AddWithValue("LastName", c.LastName);
                cmd.Parameters.AddWithValue("FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("Email", c.Email);
                cmd.Parameters.AddWithValue("Password", c.Password);
                //cmd2.Parameters.AddWithValue("DoB", c.DoB);
                //cmd2.Parameters.AddWithValue("PhoneNumber", c.PhoneNumber);
                connection.Open();
            }
        }
    }
}
