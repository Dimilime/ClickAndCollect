using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class OrderPicker : Person
    {
        private Shop shop;
        
        public OrderPicker(string ln, string fn, string e, string p, Shop s)
            : base(ln, fn, e, p)
        {
            shop = s;
        }

        public override void LogOut()
        {
            throw new NotImplementedException();
        }

        public override void Register()
        {
            throw new NotImplementedException();
        }

        public void ViewOrders()
        {

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
