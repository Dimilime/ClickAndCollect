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

        public List<OrderTimeSlotOrderProductViewModel> GetOrders(Customer customer)
        {
            List<OrderTimeSlotOrderProductViewModel> orderTimeSlotOrderProductViewModel = new List<OrderTimeSlotOrderProductViewModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand
                        ("select o.OrderId, o.Receipt, t.Days, op.NumProduct, op.Quantity from [Order] o " +
                        "full join TimeSlot t on o.TimeSlotId = t.TimeSlotId " +
                        "full join OrderProducts op on o.OrderId = op.OrderId " +
                        "where o.IdPerson = @IdPerson", connection);

                    cmd.Parameters.AddWithValue("IdPerson", customer.Id);

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order order = new Order(customer);
                            TimeSlot timeSlot = new TimeSlot();
                            Dictionary<int, int> dictonary = new Dictionary<int, int>();
                            OrderTimeSlotOrderProductViewModel orderTimeSlotOrderProductViewModel1 = new OrderTimeSlotOrderProductViewModel(order, timeSlot, dictonary);
                            
                            orderTimeSlotOrderProductViewModel1.Order.OrderId = reader.GetInt32("OrderId");
                            //orderTimeSlotOrderProductViewModel1.Order.Receipt = (bool)reader.GetValue("Receipt");
                            orderTimeSlotOrderProductViewModel1.TimeSlot.Day = (DateTime)reader.GetValue("Days");

                            orderTimeSlotOrderProductViewModel.Add(orderTimeSlotOrderProductViewModel1);

                            //orderTimeSlotOrderProductViewModel.OrderProduct.Keys = reader.GetValue("NumProduct");
                            //orderTimeSlotOrderProductViewModel.OrderProduct.Values = reader.GetValue("Quantity");
                        }
                    }
                }

                return orderTimeSlotOrderProductViewModel;
            }
            catch (Exception)
            {
                return null;
            }
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
