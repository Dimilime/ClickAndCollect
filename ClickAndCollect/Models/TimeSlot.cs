using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class TimeSlot
    {
        private DateTime start;
        private DateTime end;
        private int maxOrder;
        private Shop shop;
        private List<Order> orders;

        public TimeSlot(DateTime s, DateTime e, int m, Shop shop)
        {
            start = s;
            end = e;
            maxOrder = m;
            this.shop = shop;
            orders = new List<Order>();
        }

        public static void GetTimeSlots()
        {

        }

        public void AddMaxOrder()
        {

        }
    }
}
