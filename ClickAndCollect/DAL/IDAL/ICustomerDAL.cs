using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL
{
    public interface ICustomerDAL
    {
        public void AddCustomer(Customer c);

        public bool EmailExists(Customer c);

    }
}
