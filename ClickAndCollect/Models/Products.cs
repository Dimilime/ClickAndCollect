using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class Products
    {
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

        public static void GetProducts()
        {

        }

        public void AddToList()
        {

        }
    }
}
