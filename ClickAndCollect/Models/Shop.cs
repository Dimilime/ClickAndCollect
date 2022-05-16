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
        private List<Order> orders;
        private List<Cashier> cashiers;
        private List<OrderPicker> orderPickers;
        private List<TimeSlot> timeSlots;

        public Shop()
        {

        }
        public Shop(int p, Cashier c, OrderPicker op, TimeSlot t)
        {

            postCode = p;
            orders = new List<Order>();
            cashiers = new List<Cashier>();
            cashiers.Add(c);
            orderPickers = new List<OrderPicker>();
            orderPickers.Add(op);
            timeSlots = new List<TimeSlot>();
            timeSlots.Add(t);
        }

        public int PostCode
        {
            get { return postCode; }
            set { postCode = value; }
        }

        public void GetDailyCustomers()
        {

        }

        public void GetOrders()
        {

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
        }

    }
}
