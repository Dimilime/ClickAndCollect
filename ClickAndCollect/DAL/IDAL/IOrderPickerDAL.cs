﻿using ClickAndCollect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL.IDAL
{
    public interface IOrderPickerDAL
    {
        OrderPicker GetOrderPicker(int id);
    }
}
