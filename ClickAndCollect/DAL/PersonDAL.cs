using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL
{
    public class PersonDAL : IPersonDAL
    {
        private string connectionString;

        public PersonDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool CheckIfAccountExists(Person p)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Person WHERE EMAIL = @Email AND PASSWORD = @Password", connection);

                cmd.Parameters.AddWithValue("Email", p.Email);
                cmd.Parameters.AddWithValue("Password", p.Password);

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

        public Person GetAllFromUser(Person p)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string type;

                SqlCommand cmd = new SqlCommand("Select * from Person p full join Customer c on p.IdPerson = c.IdPerson where Email = @Email and Password = @Password", connection);

                cmd.Parameters.AddWithValue("Email", p.Email);
                cmd.Parameters.AddWithValue("Password", p.Password);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        p.Id = reader.GetInt32("IdPerson");
                        p.LastName = reader.GetString("LastName");
                        p.FirstName = reader.GetString("FirstName");
                        p.Email = reader.GetString("Email");
                        p.Password = reader.GetString("Password");
                        type = reader.GetString("Type");

                        if( type == "Customer")
                        {
                            Customer c = new Customer();
                            c.Id = p.Id;
                            c.LastName = p.LastName;
                            c.FirstName = p.FirstName;
                            c.Email = p.Email;
                            c.Password = p.Password;
                            c.DoB = reader.GetDateTime("DoB");
                            c.PhoneNumber = reader.GetInt32("PhoneNumber");
                             return c;
                        }

                        if( type == "OrderPicker")
                        {
                            OrderPicker o = new OrderPicker();
                            o.Id = reader.GetInt32("IdPerson");
                            o.LastName = reader.GetString("LastName");
                            o.FirstName = reader.GetString("FirstName");
                            o.Email = reader.GetString("Email");
                            o.Password = reader.GetString("Password");
                            return o;
                        }

                        if( type == "Casher")
                        {
                            Cashier c = new Cashier();
                            c.Id = reader.GetInt32("IdPerson");
                            c.LastName = reader.GetString("LastName");
                            c.FirstName = reader.GetString("FirstName");
                            c.Email = reader.GetString("Email");
                            c.Password = reader.GetString("Password");
                            return c;
                        }
                    }
                }
                return null;
            }
        }
    }
}
