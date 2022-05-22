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

        public List<OrderTimeSlotOrderProductViewModel> GetOrders(Customer customer)
        {
            List<OrderTimeSlotOrderProductViewModel> orderTimeSlotOrderProductViewModel = new List<OrderTimeSlotOrderProductViewModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand
                        ("select o.OrderId from [Order] o " +
                        "full join TimeSlot t on o.TimeSlotId = t.TimeSlotId " +
                        "full join OrderProducts op on o.OrderId = op.OrderId " +
                        "where o.IdPerson = @IdPerson group by o.OrderId", connection);

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
                            //orderTimeSlotOrderProductViewModel1.TimeSlot.Day = (DateTime)reader.GetValue("Days");

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
                    cmd2.Parameters.AddWithValue("Start", order.TimeSlot.Start);
                    cmd2.Parameters.AddWithValue("End", order.TimeSlot.End);
                    cmd2.Parameters.AddWithValue("ShopId", order.Shop.ShopId);
                    cmd2.Parameters.AddWithValue("Days", order.TimeSlot.Day);
                    res2 = cmd2.ExecuteNonQuery();

                    //2eme insert 
                    SqlCommand cmd3 = new SqlCommand("INSERT INTO [Order] (Ready, TimeSlotId, IdPerson) VALUES ('false', ident_current('TimeSlot'), @IdPerson)", connection);
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
                foreach (KeyValuePair<int, int> kvp in orderDicoViewModels2.Dictionary)
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

            Order order = new Order();
            order.OrderId = id;
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
                            Product product = new Product();
                            product.NumProduct = reader.GetInt32("NumProduct");
                            product.Name = reader.GetString("Name");
                            product.Price = (float)reader.GetDouble("Price");
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

        public List<Order> GetOrders(IEmployees employee)
        {
            List<Order> orders = new List<Order>();
            int nb = 0;
            string sql2 = "";
            if (employee is OrderPicker) 
            {
                nb = 1;
            }
            else
            {
                sql2 = "and Receipt=0";
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string sql = "select o.OrderId, o.IdPerson, o.Ready, t.Days, t.Start, t.[End] from [Order] o inner join TimeSlot t on o.TimeSlotId = t.TimeSlotId " +
                    $"where o.TimeSlotId in (select TimeSlotId from TimeSlot where ShopId = @shopId) and Days = Convert(varchar(10),GETDATE()+{nb},103) {sql2}";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("shopId", employee.Shop.ShopId);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order order = new Order();
                            order.OrderId = reader.GetInt32("OrderId");
                            order.Ready = reader.GetBoolean("Ready");
                            order.TimeSlot = new TimeSlot();
                            order.TimeSlot.Start = (TimeSpan)reader.GetValue("Start");
                            order.TimeSlot.End = (TimeSpan)reader.GetValue("End");
                            order.TimeSlot.Day = reader.GetDateTime("Days");
                            order.TimeSlot.Shop = employee.Shop;
                            order.Customer = new Customer();
                            order.Customer.Id = reader.GetInt32("IdPerson");
                            
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


    
    

