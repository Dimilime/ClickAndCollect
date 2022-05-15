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

        public List<Products> GetCategorys()
        {
            List<Products> categorys = new List<Products>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Category FROM Products GROUP BY Category", connection);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Products p = new Products();
                        p.Name = null;
                        p.Prix = 0;
                        p.Category = reader.GetString("Category");
                        categorys.Add(p);
                    }
                }
            }

            return categorys;
        }

        public List<Products> GetProducts(Products product)
        {

            List<Products> ps = new List<Products>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE Category = @Category", connection);

                cmd.Parameters.AddWithValue("Category", product.Category);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Products p = new Products();
                        p.NumProduct = reader.GetInt32("NumProduct");
                        p.Name = reader.GetString("Name");
                        p.Prix = (float)reader.GetDouble("Prix");
                        p.Category = reader.GetString("Category");
                        ps.Add(p);
                    }
                }
            }
            return ps;
        }

        public Products InfoPro(Products product)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE NumProduct = @NumProduct", connection);

                cmd.Parameters.AddWithValue("NumProduct", product.NumProduct);

                connection.Open();
                Products p = new Products();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        p.NumProduct = reader.GetInt32("NumProduct");
                        p.Name = reader.GetString("Name");
                        p.Prix = (float)reader.GetDouble("Prix");
                        p.Category = reader.GetString("Category");
                    }
                }

                return p;
            }
        }
    }
}
