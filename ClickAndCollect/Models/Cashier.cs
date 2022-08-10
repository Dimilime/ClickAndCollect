using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class Cashier : Person , IEmployees
    {

        public Shop Shop { get; set; }

        public Cashier()
        {

        }
        public Cashier(Shop shop)
        {
            Shop = shop;
        }

        public static Cashier GetCashier (ICashierDAL cashierDAL, int id)
        {
            return cashierDAL.GetCashier(id);
        }

        public void GetOrders(IShopDAL shopDAL)
        {
            Shop.GetOrders(shopDAL, this);
        }

        public void GetInfoShop(IShopDAL shopDAL)
        {
            Shop = Shop.GetInfoShop(shopDAL, Shop.ShopId);
        }
    }
}
