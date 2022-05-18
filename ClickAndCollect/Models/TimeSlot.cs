using ClickAndCollect.DAL.IDAL;
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
        public Shop shop { get; set; }
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


        public static TimeSlot GetTimeSlot(ITimeSlotDAL timeSlotDAL, TimeSlot timeSlot)
        {
            return timeSlotDAL.GetTimeSlot(timeSlot);
        }

        public int CheckIfAvalaible(ITimeSlotDAL timeSlotDAL, Shop shop)
        {
            return timeSlotDAL.CheckIfAvalaible(this, shop);
        }
        public void AddMaxOrder()
        {

        }
    }
}
