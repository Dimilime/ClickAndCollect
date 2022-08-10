using ClickAndCollect.DAL.IDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class TimeSlot
    {
        public int IdCanva { get; set; }
        private TimeSpan start;
        private TimeSpan end;
        private DateTime day;
        public Shop Shop { get; set; }

        public TimeSlot()
        {

        }
        public TimeSlot(Shop shop)
        {
            Shop = shop;
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

        [Display(Name = "Date de retrait")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La date de retrait est obligatoire !")]
        public DateTime Day
        {
            get { return day; }
            set { day = value; }
        }

        public static TimeSlot GetTimeSlot(ITimeSlotDAL timeSlotDAL,int id)
        {
            return timeSlotDAL.GetTimeSlot(id);
        }

        public int CheckIfAvalaible(ITimeSlotDAL timeSlotDAL)
        {
            return timeSlotDAL.CheckIfAvalaible(this, Shop);
        }
    }
}
