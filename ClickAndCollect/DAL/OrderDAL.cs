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
    public class OrderDAL: IOrderDAL
    {
        private string connectionString;

        public OrderDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Order GetOrder(int id)
        {
            /*
            Customer customer = new();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Order WHERE OrderId = @Id'", connection);

                cmd.Parameters["@Id"].Value = id;
                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Order order = new Order();
                        order.Customer =
                    }
                }
            }*/
            Order order = new Order();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql1 = "select IdPerson, o.OrderId, NumProduct, Quantity  from [Order] o inner join OrderProducts op on o.OrderId = op.OrderId";
                string sql2 = $"select IdPerson, o.OrderId, p.Name , Quantity from Products p inner join ({sql1}) o on p.NumProduct =o.NumProduct;";
                SqlCommand cmd = new SqlCommand(sql2, connection);
                
                cmd.Parameters["@Id"].Value = id;
                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        
                        
                        
                    }
                }
                
            }
            return order;
            
            

            
        }

        public List<Order> GetOrders(Shop shop)
        {
            List<Order> orders = new List<Order>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql1 = "select TimeSlotId from TimeSlot where ShopId = @shopId";
                    string sql = $"select OrderId,DateOfReceipt, Ready FROM [Order] where TimeSlotId in {sql1};";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("shopId", shop.ShopId);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order order = new Order();
                            order.OrderId = reader.GetInt32("OrderId"); 
                            order.Ready = reader.GetBoolean("Ready");
                            order.DateOfReceipt = reader.GetDateTime("DateOfReceipt");
                            orders.Add(order);
                        }
                    
                    }
                }
                return orders;
            }
            catch (Exception)
            {

                return null;
            }
            
        }
    }
}
