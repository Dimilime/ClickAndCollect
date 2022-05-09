using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public enum Categorys
    {
        DairyProduct, FruitAndVegetable, FrozenFood, OilAndSpice, OrganicFood, Drinks, StarchyFood
    }

    public class Products
    {
        private string name;
        private Categorys category;
        private List<Order> orders;

        public Products()
        {

        }

        public Products(string n, Categorys c)
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

        public Categorys Category
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
