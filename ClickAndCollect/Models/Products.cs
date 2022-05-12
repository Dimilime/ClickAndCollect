using ClickAndCollect.DAL.IDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class Products
    {
        public int NumProduct { get; set; }
        private string name;
        private string category;
        private List<Order> orders;

        public Products()
        {

        }

        public Products(string n, string c)
        {
            name = n;
            category = c;
            orders = new List<Order>();
        }
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public static List<Products> GetProducts(IProductsDAL productsDAL, Products p)
        {
            return productsDAL.GetProducts(p);
        }
        
        public static List<Products> GetCategorys(IProductsDAL productsDAL)
        {
            return productsDAL.GetCategorys();
        }

        public Products GetInfoProduct(IProductsDAL productsDAL)
        {
            return productsDAL.InfoPro(this);
        }
    }
}
