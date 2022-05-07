using ClickAndCollect.DAL.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL
{
    public class ProductsDAL : IProductsDAL
    {
        private string connectionString;

        public ProductsDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
