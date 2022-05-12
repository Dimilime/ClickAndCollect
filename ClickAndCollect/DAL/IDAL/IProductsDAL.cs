using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL.IDAL
{
    public interface IProductsDAL
    {
        public List<Products> GetProducts(Products p);
        public List<Products> GetCategorys();

        public Products InfoPro(Products p);
    }
}
