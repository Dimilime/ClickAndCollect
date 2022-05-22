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
        public Shop Shop { get; set; }
        public OrderPicker()
        {

        }

        public static OrderPicker GetOrderPicker(IOrderPickerDAL orderPickerDAL,int id)
        {
            return orderPickerDAL.GetOrderPicker(id);
        }

    }
}
