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
        public Dictionary<int, int> Dico { get; set; }

        public OrderDicoViewModels(Order o, Dictionary<int, int> dico)
        {
            Order = o;
            Dico = dico;
        }
    }
}
