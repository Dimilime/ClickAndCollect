using ClickAndCollect.DAL.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL
{
    public class TimeSlotDAL : ITimeSlotDAL
    {
        private string connectionString;

        public TimeSlotDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
