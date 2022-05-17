using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class Shop
    {
        public int PostCode { get; set; }
        public List<Order> Orders { get; set; }
        public List<Cashier> Cashiers { get; set; }
        public List<OrderPicker> OrderPickers{ get; set; }
        public List<TimeSlot> TimeSlots{ get; set; }

        public Shop()
        {

        }
        public Shop(int p, Cashier c, OrderPicker op, TimeSlot t)
        {

            PostCode = p;
            Orders = new List<Order>();
            Cashiers = new List<Cashier>();
            Cashiers.Add(c);
            OrderPickers = new List<OrderPicker>();
            OrderPickers.Add(op);
            TimeSlots = new List<TimeSlot>();
            TimeSlots.Add(t);
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

        public static void GetShops()
        {

        }

    }
}
