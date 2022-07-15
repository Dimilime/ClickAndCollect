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
    public class ProductDAL : IProductDAL
    {
        private string connectionString;

        public ProductDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<string> GetCategories()
        {
            List<string> categorys = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT Category FROM Products GROUP BY Category", connection);

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string cat = reader.GetString("Category");
                            categorys.Add(cat);
                        }
                    }
                }

                return categorys;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public List<Product> GetProducts(string category)
        {

            List<Product> produits = new List<Product>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE Category = @Category", connection);

                    cmd.Parameters.AddWithValue("Category", category);

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product produit = new Product
                            {
                                NumProduct = reader.GetInt32("NumProduct"),
                                Name = reader.GetString("Name"),
                                Price = (float)reader.GetDouble("Price"),
                                Category = (Category)Enum.Parse(typeof(Category), reader.GetString("Category"))
                            };
                            produits.Add(produit);
                        }
                    }
                }
                return produits;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Product GetInfoProduct(Product product)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE NumProduct = @NumProduct", connection);

                    cmd.Parameters.AddWithValue("NumProduct", product.NumProduct);

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            product.NumProduct = reader.GetInt32("NumProduct");
                            product.Name = reader.GetString("Name");
                            product.Price = (float)reader.GetDouble("Price");
                            product.Category = (Category)Enum.Parse(typeof(Category), reader.GetString("Category"));
                        }
                    }

                    return product;
                }
            }
            catch(Exception)
            {
                return null;
            }

        }
    }
}
