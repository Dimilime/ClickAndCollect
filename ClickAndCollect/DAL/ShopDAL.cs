using ClickAndCollect.DAL.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL
{
    public class ShopDAL : IShopDAL
    {
        private string connectionString;

        public ShopDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
