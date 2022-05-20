using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL.IDAL
{
    public interface ITimeSlotDAL
    {
        public TimeSlot GetTimeSlot(TimeSlot timeSlot);
        public int CheckIfAvalaible(TimeSlot timeSlot, Shop shop);
    }
}
