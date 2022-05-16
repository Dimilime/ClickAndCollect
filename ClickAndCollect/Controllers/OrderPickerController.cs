using ClickAndCollect.DAL.IDAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Controllers
{
    public class OrderPickerController : Controller
    {
        private readonly IOrderPickerDAL _orderPickerDAL;
        public OrderPickerController(IOrderPickerDAL orderPickerDAL)
        {
            _orderPickerDAL = orderPickerDAL;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
