using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL.IDAL
{
    public interface IProductDAL
    {
        public List<Product> GetProducts(string cat);
        public List<string> GetCategories();
        public Product GetInfoProduct(Product p);
    }
}
