using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class Customer : Person
    {
        private DateTime doB;
        private int phoneNumber;
        private List<Order> orders;

        public Customer (string ln, string fn, string e, string p, DateTime d, int pn)
            :base(ln,fn,e,p)
        {
            doB = d;
            phoneNumber = pn;
            orders = new List<Order>();
        }

        public override void LogOut()
        {
            throw new NotImplementedException();
        }

        public override void Register()
        {
            throw new NotImplementedException();
        }
    }
}
