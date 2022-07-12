using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Interface
{
    public interface IEmployees
    {
        public Shop Shop { get; set; }

        public void GetOrders(IShopDAL shopDAL);

        public void GetInfoShop(IShopDAL shopDAL);
    }
}
