using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.ViewModels
{
    public class OrderDicoViewModels
    {
        public Order Order{get;set;}
        public Dictionary<int, int> Dictionary { get; set; }

        public OrderDicoViewModels(Order order, Dictionary<int, int> dictionary)
        {
            Order = order;
            Dictionary = dictionary;
        }
    }
}
