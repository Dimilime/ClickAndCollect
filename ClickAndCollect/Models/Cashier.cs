using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class Cashier : Person
    {

        private Shop shop;

        public Cashier(string ln, string fn, string e, string p, Shop s)
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

        public void AccesDailyCustomers()
        {

        }

        public void EnterBoxesReturned()
        {

        }

        public void ViewAmountOrder()
        {

        }

        public void ValidateReceipt()
        {

        }


    }
}
