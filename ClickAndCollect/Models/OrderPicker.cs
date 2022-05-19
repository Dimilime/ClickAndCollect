using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class OrderPicker : Person
    {
        public Shop shop { get; set; }
        public OrderPicker()
        {

        }

        public OrderPicker(string ln, string fn, string e, string p, Shop s)
            : base(ln, fn, e, p)
        {

        }

        public void ViewOrders()
        {
            shop.GetOrders();
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
