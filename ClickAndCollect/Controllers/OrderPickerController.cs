using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClickAndCollect.DAL;
using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Models;
using Microsoft.AspNetCore.Http;

namespace ClickAndCollect.Controllers
{
    public class OrderPickerController : Controller
    {
        private readonly IOrderPickerDAL _orderPickerDAL;
        public OrderPickerController(IOrderPickerDAL orderPickerDAL)
        {
            _orderPickerDAL = orderPickerDAL;
        }
        public IActionResult Orders()
        {
            return Redirect("/order/OrdersList");
        }
        public IActionResult OrderDetails(int id)
        {
            
            return Redirect($"/order/Details/{id}");
        }
    }
}
