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

        public List<Product> GetCategories()
        {
            List<Product> categorys = new List<Product>();

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
                            Product produit = new Product();
                            produit.Name = null;
                            produit.Price = 0;
                            produit.Category = reader.GetString("Category");
                            categorys.Add(produit);
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

        public List<Product> GetProducts(Product product)
        {

            List<Product> produits = new List<Product>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE Category = @Category", connection);

                    cmd.Parameters.AddWithValue("Category", product.Category);

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product produit = new Product();
                            produit.NumProduct = reader.GetInt32("NumProduct");
                            produit.Name = reader.GetString("Name");
                            produit.Price = (float)reader.GetDouble("Price");
                            produit.Category = reader.GetString("Category");
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
                            product.Category = reader.GetString("Category");
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
