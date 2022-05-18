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
    public class ShopDAL : IShopDAL
    {
        private string connectionString;

        public ShopDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Shop GetInfoShop(Shop shop)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Shop WHERE ShopId = @ShopId", connection);

                cmd.Parameters.AddWithValue("ShopId", shop.ShopId);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        shop.ShopId = reader.GetInt32("ShopId");
                        shop.PostCode = reader.GetInt32("PostCode");
                    }
                }

                return shop;
            }
        }

        public List<Shop> GetShops()
        {
            List<Shop> shops = new List<Shop>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Shop", connection);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Shop shop = new Shop();
                        shop.ShopId = reader.GetInt32("ShopId");
                        shop.PostCode = reader.GetInt32("PostCode");
                        shops.Add(shop);
                    }
                }
            }

            return shops;
        }

        public List<TimeSlot> GetTimeSlots(Shop shop)
        {
            List<TimeSlot> timeSlots = new List<TimeSlot>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Canva where ShopId = @ShopId", connection);

                cmd.Parameters.AddWithValue("ShopId", shop.ShopId);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TimeSlot timeSlot = new TimeSlot();
                        timeSlot.IdCanva = reader.GetInt32("IdCanva");
                        timeSlot.Start = (TimeSpan)reader.GetValue("Start");
                        timeSlot.End = (TimeSpan)reader.GetValue("End");
                        timeSlots.Add(timeSlot);
                    }
                }
            }

            return timeSlots;
        }
    }
}
