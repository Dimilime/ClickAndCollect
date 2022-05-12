using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.ViewModels
{
    public class OrderProductsViewModels
    {
        public List<Products> Products { get; set; }
        public int Nbr { get; set; }

        public OrderProductsViewModels(List<Products> products, int nbr)
        {
            Products = products;
            Nbr = nbr;
        } 
    }
}
