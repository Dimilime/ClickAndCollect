using ClickAndCollect.DAL.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class OrderPicker : Person
    {
        public Shop Shop { get; set; }
        public OrderPicker()
        {

        }
        public OrderPicker(Shop shop)
        {
            Shop = shop;
        }

        
        public List<Order> ViewOrders(IOrderPickerDAL orderPickerDal)
        {
            return orderPickerDal.ViewOrders(this);
        }

        public void ViewOrderDetails()
        {

        }

        public void ValidateOrder()
        {

        }

        public void EnterBoxesUsed()
        {

        }
    }
}
