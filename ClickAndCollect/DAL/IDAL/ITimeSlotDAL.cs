using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL.IDAL
{
    public interface ITimeSlotDAL
    {
        TimeSlot GetTimeSlot(int id);
        int CheckIfAvalaible(TimeSlot timeSlot, Shop shop);
    }
}
