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

        //public Boolean EmailExists(Customer c)
        //{
        //    bool result = false;

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("SELECT * from dbo.Person WHERE email = @Email AND TYPE = 'Customer'", connection);
        //        connection.Open();
        //        using (SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                result = true;
        //            }
        //        }
        //    }

        //    return result;
        //}
        public void AddCustomer(Customer c)
        {
            //using(SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlCommand cmd = new SqlCommand("INSERT INTO Person (LastName, FirstName, Email, Password, Type) VALUES (@LastName, @FirstName, @Email, @Password, 'Customer')", connection);
            //    cmd.Parameters.AddWithValue("LastName", c.LastName);
            //    connection.Open();
            //}
        }
    }
}
