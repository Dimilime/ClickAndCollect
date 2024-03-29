﻿using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL
{
    public interface ICustomerDAL
    {
        bool Register(Customer c);
        Customer GetInfoCustomer(int id);
        bool CheckIfEmailCustomerExists(Customer c);

    }
}
