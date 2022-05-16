using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.ViewModels
{
    public class ProductNbrViewModels
    {
        public List<Product> Product { get; set; }
        public int Nbr { get; set; }

        public ProductNbrViewModels(List<Product> product, int nbr)
        {
            Product = product;
            Nbr = nbr;
        } 
    }
}
