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
        public Shop shop { get; set; }

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

        [Display(Name = "Date de retrait")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La date de retrait est obligatoire !")]
        public DateTime Day
        {
            get { return day; }
            set { day = value; }
        }

        public static TimeSlot GetTimeSlot(ITimeSlotDAL timeSlotDAL, TimeSlot timeSlot)
        {
            return timeSlotDAL.GetTimeSlot(timeSlot);
        }

        public int CheckIfAvalaible(ITimeSlotDAL timeSlotDAL, Shop shop)
        {
            this.shop = shop;
            return timeSlotDAL.CheckIfAvalaible(this, shop);
        }
    }
}
