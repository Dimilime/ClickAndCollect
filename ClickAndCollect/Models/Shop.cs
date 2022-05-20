using ClickAndCollect.DAL;
using ClickAndCollect.DAL.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class Shop
    {
        public int ShopId { get; set; }
        private int postCode;
        private List<Cashier> cashiers;
        private List<OrderPicker> orderPickers;
        public int PostCode { get => postCode; set => postCode=value; }
        public List<Order> Orders { get; set; }
        public List<Cashier> Cashiers { get; set; }
        public List<OrderPicker> OrderPickers{ get; set; }
        public List<TimeSlot> TimeSlots{ get; set; }

        
        public Shop()
        {
            Orders = new List<Order>();
            Cashiers = new List<Cashier>();
            OrderPickers = new List<OrderPicker>();
            TimeSlots = new List<TimeSlot>();
        }

        public void GetDailyCustomers()
        {

        }

        public void GetOrders( IOrderDAL orderDAL)
        {
            Orders = orderDAL.GetOrders(this);
        }

        public void GetOrderById()
        {
            
        }

        public static List<Shop> GetShops(IShopDAL shopDAL)
        {
            return shopDAL.GetShops();
        }

        public Shop GetInfoShop(IShopDAL shopDAL)
        {
            return shopDAL.GetInfoShop(this);
        }public static Shop GetInfoShop(IShopDAL shopDAL, Person person)
        {
            return shopDAL.GetInfoShop(person);
        }

        public static List<TimeSlot> GetTimeSlots(IShopDAL shopDAL, Shop shop)
        {
            return shopDAL.GetTimeSlots(shop);
        }

    }
}
