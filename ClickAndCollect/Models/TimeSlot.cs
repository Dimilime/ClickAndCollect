using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class TimeSlot
    {
        public int IdCanva { get; set; }
        private TimeSpan start;
        private TimeSpan end;
        private int maxOrder;
        private Shop shop;
        private List<Order> orders;

        public TimeSlot()
        {

        }

        public TimeSpan Start
        {
            get { return start; }
            set { start = value; }
        }

        public TimeSpan End
        {
            get { return end; }
            set { end = value; }
        }


        public static void GetTimeSlots()
        {

        }

        public void AddMaxOrder()
        {

        }
    }
}
