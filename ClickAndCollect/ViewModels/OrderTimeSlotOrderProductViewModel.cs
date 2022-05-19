using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.ViewModels
{
    public class OrderTimeSlotOrderProductViewModel
    {
        public Order Order { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public Dictionary<int, int> OrderProduct { get; set; }

        public OrderTimeSlotOrderProductViewModel (Order order, TimeSlot timeSlot, Dictionary<int,int> orderProduct)
        {
            Order = order;
            TimeSlot = timeSlot;
            OrderProduct = orderProduct;
        }
    }
}
