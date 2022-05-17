using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL
{
    public interface ICustomerDAL
    {
        public bool Register(Customer c);

        public bool CheckIfEmailCustomerExists(Customer c);

    }
}
