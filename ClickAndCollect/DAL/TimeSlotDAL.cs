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

        public TimeSlot GetTimeSlot(int id)
        {
            TimeSlot timeSlot = new TimeSlot()
            {
                IdCanva = id
                
            };
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Canva WHERE IdCanva = @IdCanva", connection);

                    cmd.Parameters.AddWithValue("IdCanva", id);

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            timeSlot.Start = (TimeSpan)reader.GetValue("Start");
                            timeSlot.End = (TimeSpan)reader.GetValue("End");
                        }
                    }

                    return timeSlot;
                }
            }
            catch(Exception)
            {
                return null;
            }

        }

        public int CheckIfAvalaible(TimeSlot timeSlot, Shop shop)
        {
            int nbr = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM TimeSlot WHERE Start = @Start and Days = @Day and ShopId = @ShopId ", connection);

                    cmd.Parameters.AddWithValue("Start", timeSlot.Start);
                    cmd.Parameters.AddWithValue("Day", timeSlot.Day);
                    cmd.Parameters.AddWithValue("ShopId", shop.ShopId);

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nbr++;
                        }
                    }

                    return nbr;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
