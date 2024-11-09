using ClickAndCollect.Interface;
using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL.IDAL
{
    public interface IShopDAL
    {
        List<Shop> GetShops();
        Shop GetInfoShop(int id);
        List<TimeSlot> GetTimeSlots(Shop shop);
        List<Order> GetOrders(Shop shop, IEmployees employee);
    }
}
