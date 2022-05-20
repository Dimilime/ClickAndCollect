using ClickAndCollect.DAL;
using ClickAndCollect.DAL.IDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class Product
    {
        public int NumProduct { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public float Price { get; set; }

        public Product()
        {

        }

        public static List<Product> GetProducts(IProductDAL productDAL, Product produit)
        {
            return productDAL.GetProducts(produit);
        }
        
        public static List<Product> GetCategories(IProductDAL productDAL)
        {
            return productDAL.GetCategories();
        }

        public Product GetInfoProduct(IProductDAL productDAL)
        {
            return productDAL.GetInfoProduct(this);
        }
    }
}
