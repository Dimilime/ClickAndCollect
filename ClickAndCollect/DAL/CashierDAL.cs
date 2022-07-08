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
    public class CashierDAL : ICashierDAL
    {
        private string connectionString;

        public CashierDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Cashier GetCashier(int id)
        {
            Cashier cashier = null;
            Shop shop = new Shop();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "select * from Cashier c inner join Person p on p.IdPerson=c.IdPerson where c.IdPerson=@Id;";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("Id", id);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            shop.ShopId = reader.GetInt32("ShopId");
                            cashier = new Cashier(shop);
                            cashier.Id = reader.GetInt32("IdPerson");
                            cashier.LastName = reader.GetString("LastName");
                            cashier.FirstName = reader.GetString("FirstName");
                            cashier.Email = reader.GetString("Email");
                            cashier.Password = reader.GetString("Password");
                            
                            
                        }

                    }
                }
                return cashier;
            }
            catch (Exception)
            {

                return null;
            }

        }
    }
}
