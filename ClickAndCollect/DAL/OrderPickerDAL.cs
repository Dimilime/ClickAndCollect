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
    public class OrderPickerDAL : IOrderPickerDAL
    {
        private string connectionString;

        public OrderPickerDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Order> ViewOrders(OrderPicker orderPicker)
        {
            Shop shop = new Shop();
            
            List<Order> orders = new List<Order>();
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "select OrderId,DateOfReceipt, Ready FROM [Order] ;";
                SqlCommand cmd = new SqlCommand(sql, connection);

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
    }
}
