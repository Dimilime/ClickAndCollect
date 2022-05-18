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
    public class TimeSlotDAL : ITimeSlotDAL
    {
        private string connectionString;

        public TimeSlotDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public TimeSlot GetTimeSlot(TimeSlot timeSlot)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Canva WHERE IdCanva = @IdCanva", connection);

                cmd.Parameters.AddWithValue("IdCanva", timeSlot.IdCanva);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        timeSlot.IdCanva = reader.GetInt32("IdCanva");
                        timeSlot.Start = (TimeSpan)reader.GetValue("Start");
                        timeSlot.End = (TimeSpan)reader.GetValue("End");
                    }
                }

                return timeSlot;
            }
        }

        public int CheckIfAvalaible(TimeSlot timeSlot, Shop shop)
        {
            int nbr = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT count(ShopId) FROM TimeSlot WHERE Start = @Start and ShopId = @ShopId group by ShopId", connection);

                cmd.Parameters.AddWithValue("Start", timeSlot.Start);
                cmd.Parameters.AddWithValue("ShopId", shop.ShopId);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nbr = nbr + 1;
                    }
                }

                return nbr;
            }
        }
    }
}
