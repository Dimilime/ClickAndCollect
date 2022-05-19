using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Models;
using ClickAndCollect.ViewModels;
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

        public bool MakeOrder(Order order, OrderDicoViewModels orderDicoViewModels2)
        {
            bool success = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlCommand cmd2 = new SqlCommand("INSERT INTO TimeSlot(Start, [End], ShopId, Days) VALUES (@Start, @End, @ShopId, @Days)", connection);
                    SqlCommand cmd3 = new SqlCommand("INSERT INTO [Order] (Ready, TimeSlotId, IdPerson) VALUES ('false', ident_current('TimeSlot'), @IdPerson)", connection);

                    using (SqlConnection connection2 = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd4 = new SqlCommand("SELECT TOP 1 OrderId FROM[Order] ORDER BY OrderId DESC", connection2);
                        connection2.Open();

                        using (SqlDataReader reader = cmd4.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                order.OrderId = reader.GetInt32("OrderId") + 1;
                            }
                        }
                    }

                    SqlCommand cmd = new SqlCommand("INSERT INTO OrderProducts (OrderId, NumProduct, Quantity) VALUES ( @OrderId, @NumProduct, @Quantity)", connection);

                    foreach (KeyValuePair<int, int> kvp in orderDicoViewModels2.Dictionary)
                    {


                        cmd.Parameters.AddWithValue("OrderId", order.OrderId);
                        cmd.Parameters.AddWithValue("NumProduct", kvp.Key);
                        cmd.Parameters.AddWithValue("Quantity", kvp.Value);
                    }
                   
                    cmd2.Parameters.AddWithValue("Start", order.timeSlot.Start);
                    cmd2.Parameters.AddWithValue("End", order.timeSlot.End);
                    cmd2.Parameters.AddWithValue("ShopId", order.shop.ShopId);
                    cmd2.Parameters.AddWithValue("Days", order.timeSlot.Day);
                    cmd3.Parameters.AddWithValue("IdPerson", order.customer.Id);

                    connection.Open();
                    int res = cmd.ExecuteNonQuery(); 
                    int res2 = cmd2.ExecuteNonQuery();
                    int res3 = cmd3.ExecuteNonQuery();
                    success = res > 0 && res2 > 0 && res3 > 0;
                }

                return success;
            }
            catch(Exception)
            {
                return success;
            }

        }
    }
}
