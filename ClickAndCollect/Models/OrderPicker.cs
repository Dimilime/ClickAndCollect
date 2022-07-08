using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class OrderPicker : Person, IEmployees
    {
        private Shop shop;
        public Shop Shop { get => shop; set => shop=value; }
        public OrderPicker()
        {

        }
        public OrderPicker(Shop s)
        {
            Shop = s;
        }

        public static OrderPicker GetOrderPicker(IOrderPickerDAL orderPickerDAL,int id)
        {
            return orderPickerDAL.GetOrderPicker(id);
        }

    }
}
