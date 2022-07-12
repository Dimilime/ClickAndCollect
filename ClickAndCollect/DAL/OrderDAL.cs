using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Interface;
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
    public class OrderDAL : IOrderDAL
    {
        private string connectionString;

        public OrderDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool MakeOrder(Order order, OrderDicoViewModels orderDicoViewModels)
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
                    cmd2.Parameters.AddWithValue("Start", order.TimeSlot.Start);
                    cmd2.Parameters.AddWithValue("End", order.TimeSlot.End);
                    cmd2.Parameters.AddWithValue("ShopId", order.Shop.ShopId);
                    cmd2.Parameters.AddWithValue("Days", order.TimeSlot.Day);
                    res2 = cmd2.ExecuteNonQuery();

                    //2eme insert 
                    SqlCommand cmd3 = new SqlCommand("INSERT INTO [Order] (Ready, NumberOfBoxUsed,NumberOfBoxReturned, Receipt,TimeSlotId, IdPerson) VALUES ('false', 0,0,'false',ident_current('TimeSlot'), @IdPerson)", connection);
                    cmd3.Parameters.AddWithValue("IdPerson", order.Customer.Id);
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
                bool res4 = false;
                foreach (KeyValuePair<int, int> kvp in orderDicoViewModels.Dictionary)
                {
                    res4 = InsertOrderProductWithQuantity(order.OrderId, kvp.Key, kvp.Value);
                }

                success = res4 == true && res2 > 0 && res3 > 0;

                return success;
            }
            catch (Exception)
            {
                return success;
            }
        }


        public bool InsertOrderProductWithQuantity(int OrderId, int NumProduct, int Quantity)
        {
            int res = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO OrderProducts (OrderId, NumProduct, Quantity) VALUES ( @OrderId, @NumProduct, @Quantity)", connection);
                    cmd.Parameters.AddWithValue("OrderId", OrderId);
                    cmd.Parameters.AddWithValue("NumProduct", NumProduct);
                    cmd.Parameters.AddWithValue("Quantity", Quantity);
                    connection.Open();
                    res = cmd.ExecuteNonQuery();
                }
                return res > 0;
            }
            catch (Exception)
            {

                return false;
            }
           
        }
        public Order GetOrder(int id)
        {

            Order order = new Order
            {
                OrderId = id
                                
            };
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "select o.OrderId, o.Receipt, o.NumberOfBoxUsed, o.NumberOfBoxReturned,o.Ready ,op.NumProduct, op.Quantity, p.Price,p.Name from [Order] o " +
                    "inner join OrderProducts op on op.OrderId=o.OrderId inner join Products p on p.NumProduct = op.NumProduct where o.OrderId=@Id";
                    SqlCommand cmd = new SqlCommand(sql, connection);

                    cmd.Parameters.AddWithValue("Id",id);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                NumProduct = reader.GetInt32("NumProduct"),
                                Name = reader.GetString("Name"),
                                Price = (float)reader.GetDouble("Price")
                            };
                            
                            order.DictionaryProducts.Add(product,reader.GetInt32("Quantity"));
                            order.Ready = reader.GetBoolean("Ready");
                            order.Receipt = reader.GetBoolean("Receipt");
                            order.NumberOfBoxUsed = reader.GetInt32("NumberOfBoxUsed");
                            order.NumberOfBoxReturned = reader.GetInt32("NumberOfBoxReturned");
                        }
                    }

                }
                return order;
            }
            catch (Exception)
            {

                return null;
            }
            

        }

        public bool OrderReady(Order order)
        {

            bool succes = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "UPDATE [Order] SET Ready = @ready, NumberOfBoxUsed = @nbBoxU WHERE OrderId = @Id";
                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("Id", order.OrderId);
                cmd.Parameters.AddWithValue("ready", order.Ready);
                cmd.Parameters.AddWithValue("nbBoxU", order.NumberOfBoxUsed);
                connection.Open();

                succes =  cmd.ExecuteNonQuery() > 0;
                
            }
            return succes;
    
        }

        public bool OrderReceipt(Order order)
        {

            bool succes = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "UPDATE [Order] SET Receipt = @receipt, NumberOfBoxReturned = @nbBoxR WHERE OrderId = @Id";
                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("Id", order.OrderId);
                cmd.Parameters.AddWithValue("receipt", order.Receipt);
                cmd.Parameters.AddWithValue("nbBoxR", order.NumberOfBoxReturned);
                connection.Open();

                succes = cmd.ExecuteNonQuery() > 0;

            }
            return succes;

        }
    }
}


    
    

