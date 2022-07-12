using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Interface;
using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL
{
    public class ShopDAL : IShopDAL
    {
        private string connectionString;

        public ShopDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

       
        public Shop GetInfoShop(int id)
        {
            Shop shop = new Shop();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Shop WHERE ShopId = @ShopId", connection);

                    cmd.Parameters.AddWithValue("ShopId",id);

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            shop.ShopId = reader.GetInt32("ShopId");
                            shop.PostCode = reader.GetInt32("PostCode");
                        }
                    }     
                }
                return shop;
            }
            catch(Exception)
            {
                return null;
            }
            
        }

        

        public List<Shop> GetShops()
        {
            List<Shop> shops = new List<Shop>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Shop", connection);

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Shop shop = new Shop
                            {
                                ShopId = reader.GetInt32("ShopId"),
                                PostCode = reader.GetInt32("PostCode")
                            };
                            shops.Add(shop);
                        }
                    }
                }

                return shops;
            }
            catch(Exception)
            {
                return null;
            }

        }

        public List<TimeSlot> GetTimeSlots(Shop shop)
        {
            List<TimeSlot> timeSlots = new List<TimeSlot>();

            try 
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Canva where ShopId = @ShopId", connection);

                    cmd.Parameters.AddWithValue("ShopId", shop.ShopId);

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TimeSlot timeSlot = new TimeSlot(shop)
                            {
                                IdCanva = reader.GetInt32("IdCanva"),
                                Start = (TimeSpan)reader.GetValue("Start"),
                                End = (TimeSpan)reader.GetValue("End")
                            };
                            timeSlots.Add(timeSlot);
                        }
                    }
                }

                return timeSlots;
            }
            catch(Exception)
            {
                return null;
            }
  
        }
        
        public List<Order> GetOrders(Shop shop, IEmployees employee)
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
                    $"where o.TimeSlotId in (select TimeSlotId from TimeSlot where ShopId = @shopId) and Days = Convert(varchar(10),GETDATE()+{nb},23) {sql2}";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("shopId",shop.ShopId);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order order = new Order
                            {
                                OrderId = reader.GetInt32("OrderId"),
                                Ready = reader.GetBoolean("Ready"),
                                TimeSlot = new TimeSlot(shop)
                                {
                                    Start = (TimeSpan)reader.GetValue("Start"),
                                    End = (TimeSpan)reader.GetValue("End"),
                                    Day = reader.GetDateTime("Days")
                                },
                                Customer = new Customer
                                {
                                    Id = reader.GetInt32("IdPerson")
                                }
                            };

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
