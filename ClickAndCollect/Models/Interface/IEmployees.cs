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
        Shop Shop { get; set; }

        void GetOrders(IShopDAL shopDAL);

        void GetInfoShop(IShopDAL shopDAL);
    }
}
