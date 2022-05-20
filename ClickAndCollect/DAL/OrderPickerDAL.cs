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
        
        public OrderPicker GetOrderPicker(int id)
        {
            OrderPicker orderPicker = new OrderPicker();
            Shop shop = new Shop();
            //try
            //{
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "select * from OrderPicker op inner join Person p on p.IdPerson=op.IdPerson where op.IdPerson=@Id;";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("Id", id);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            orderPicker.Id = reader.GetInt32("IdPerson");
                            orderPicker.LastName = reader.GetString("LastName");
                            orderPicker.FirstName = reader.GetString("FirstName");
                            orderPicker.Email = reader.GetString("Email");
                            orderPicker.Password = reader.GetString("Password");
                            shop.ShopId= reader.GetInt32("ShopId");
                            orderPicker.Shop= shop;
                            
                        }

                    }
                }
                return orderPicker;
            //}
            //catch (Exception)
            //{

               // return null;
            //}
            
        }
    }
}
