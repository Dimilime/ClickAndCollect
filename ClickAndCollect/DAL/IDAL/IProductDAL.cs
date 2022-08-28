using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL.IDAL
{
    public interface IProductDAL
    {
        List<Product> GetProducts(string cat);
        List<string> GetCategories();
        Product GetInfoProduct(Product p);
    }
}
