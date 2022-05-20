﻿using ClickAndCollect.DAL.IDAL;
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
            int res2, res3;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //1er insert
                    connection.Open();
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO TimeSlot(Start, [End], ShopId, Days) VALUES (@Start, @End, @ShopId, @Days)", connection);
                    cmd2.Parameters.AddWithValue("Start", order.timeSlot.Start);
                    cmd2.Parameters.AddWithValue("End", order.timeSlot.End);
                    cmd2.Parameters.AddWithValue("ShopId", order.shop.ShopId);
                    cmd2.Parameters.AddWithValue("Days", order.timeSlot.Day);
                    res2 = cmd2.ExecuteNonQuery();

                    //2eme insert 
                    SqlCommand cmd3 = new SqlCommand("INSERT INTO [Order] (Ready, TimeSlotId, IdPerson) VALUES ('false', ident_current('TimeSlot'), @IdPerson)", connection);
                    cmd3.Parameters.AddWithValue("IdPerson", order.customer.Id);
                    res3 = cmd3.ExecuteNonQuery();

                    //recuperation Id
                    SqlCommand cmd4 = new SqlCommand("SELECT TOP 1 OrderId FROM[Order] ORDER BY OrderId DESC", connection);
                    using (SqlDataReader reader = cmd4.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            order.OrderId = reader.GetInt32("OrderId");
                        }
                    }
                }

                //3ème insert
                bool res4=false;
                foreach (KeyValuePair<int, int> kvp in orderDicoViewModels2.Dictionary)
                {            
                    res4 = InsertOrderProductWithQuantity(order.OrderId, kvp.Key, kvp.Value);
                }
     
                success = res4 ==true && res2 > 0 && res3 > 0;

                return success;
            }
            catch(Exception)
            {
                return success;
            }
        }


        public bool InsertOrderProductWithQuantity(int OrderId, int NumProduct, int Quantity )
        {
            int res=0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO OrderProducts (OrderId, NumProduct, Quantity) VALUES ( @OrderId, @NumProduct, @Quantity)", connection);
                cmd.Parameters.AddWithValue("OrderId", OrderId);
                cmd.Parameters.AddWithValue("NumProduct", NumProduct);
                cmd.Parameters.AddWithValue("Quantity", Quantity);
                connection.Open();
                res = cmd.ExecuteNonQuery();
            }
            return res >0;
        }
    }

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
