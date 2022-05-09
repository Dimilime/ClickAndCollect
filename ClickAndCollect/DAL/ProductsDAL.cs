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
    public class ProductsDAL : IProductsDAL
    {
        private string connectionString;

        public ProductsDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Products> GetProducts()
        {

            List<Products> products = new List<Products>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Name FROM Products", connection);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Products p = new Products();
                        p.Name = reader.GetString("Name");
                        //p.Category = reader.GetString("Category");
                        products.Add(p);
                    }
                }
            }

            return products;
        }
    }
}
