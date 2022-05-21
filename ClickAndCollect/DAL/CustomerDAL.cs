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

            try
            {
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
            catch(Exception)
            {
                return result;
            }

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

                    string type = "Customer";
                    cmd.Parameters.AddWithValue("LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("Email", customer.Email);
                    cmd.Parameters.AddWithValue("Password", customer.Password);
                    cmd.Parameters.AddWithValue("Type", type);
                    cmd2.Parameters.AddWithValue("DoB", customer.DoB);
                    cmd2.Parameters.AddWithValue("PhoneNumber", customer.PhoneNumber);

                    connection.Open();
                    int res = cmd.ExecuteNonQuery();
                    int res2 = cmd2.ExecuteNonQuery();
                    success = res > 0 && res2 > 0;
                }
                return success;
            }
            catch (Exception)
            {
                return success;
            }

        }

        public Customer GetInfoCustomer(int id)
        {
            Customer customer = new Customer();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "select p.LastName, p.FirstName, c.PhoneNumber from customer c inner join Person p on p.IdPerson= c.Idperson where c.IdPerson=@Id";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("Id", id);

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customer.Id = id;
                            customer.LastName = reader.GetString("LastName");
                            customer.FirstName = reader.GetString("FirstName");
                            customer.PhoneNumber = reader.GetInt32("PhoneNumber");
                        }
                    }
                    
                }
                return customer;
            }
            catch (Exception)
            {
                return null;
            }

        }

    }
}
