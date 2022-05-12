using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderDAL _orderDAL;

        public OrderController(IOrderDAL orderDAL)
        {
            _orderDAL = orderDAL;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Basket()
        {
            Order o = JsonConvert.DeserializeObject<Order>(TempData["CurrentOrder"].ToString());

            return View(o);
        }

    }
}
