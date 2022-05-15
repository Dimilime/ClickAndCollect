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
        public string Name { get; set; }
        public string Category { get; set; }
        public float Prix { get; set; }
        private List<Order> Orders { get; set; }

        public Products()
        {

        }

        //public Products(string n, string c)
        //{
        //    Name = n;
        //    Category = c;
        //    Orders = new List<Order>();
        //}
        
        //public string Name
        //{
        //    get { return name; }
        //    set { name = value; }
        //}

        //public string Category
        //{
        //    get { return category; }
        //    set { category = value; }
        //}

        //public float Prix
        //{
        //    get { return prix; }
        //    set { prix = value; }
        //}

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
