using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace ClickAndCollect.DAL
{
    public class CustomerDAL: ICustomerDAL
    {
        private string connectionString;

        public CustomerDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool CheckIfEmailCustomerExists(Customer customer)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Person WHERE EMAIL = @Email AND TYPE = 'Customer'", connection);
                
                cmd.Parameters.AddWithValue("Email", customer.Email);
                
                connection.Open();
                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        public bool Register(Customer customer)
        {
            bool success = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand("INSERT INTO Person (LastName, FirstName, Email, Password, Type) VALUES (@LastName, @FirstName, @Email, @Password, @Type)", connection);
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO Customer(IdPerson, DoB, PhoneNumber) VALUES (ident_current('Person'),@Dob, @PhoneNumber)", connection);

                customer.Type = "Customer";
                cmd.Parameters.AddWithValue("LastName", customer.LastName);
                cmd.Parameters.AddWithValue("FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("Email", customer.Email);
                cmd.Parameters.AddWithValue("Password", customer.Password);
                cmd.Parameters.AddWithValue("Type", customer.Type);
                cmd2.Parameters.AddWithValue("DoB", customer.DoB);
                cmd2.Parameters.AddWithValue("PhoneNumber", customer.PhoneNumber);

                    connection.Open();
                    int res = cmd.ExecuteNonQuery();
                    int res2 = cmd2.ExecuteNonQuery();
                    success = res > 0 && res2 > 0;

                }
            }
            catch (Exception e)
            {

                return false;
            }
            
            return success;
        }
    }
}
