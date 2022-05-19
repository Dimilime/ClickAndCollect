using ClickAndCollect.DAL;
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
        private readonly IShopDAL _shopDAL;
        private readonly ICustomerDAL _customerDAL;
        public OrderController(IOrderDAL orderDAL, IShopDAL shopDAL, ICustomerDAL customerDAL)
        {
            _orderDAL = orderDAL;
            _shopDAL = shopDAL;
            _customerDAL = customerDAL;
        }
        public IActionResult Orders()
        {

            int idOrderPicker = (int)HttpContext.Session.GetInt32("Id");
            
            return View(orders);
        }
        public IActionResult Details(int id)
        {
            Order order = Order.GetDetails(id,_orderDAL);
            if (order == null)
            {
                ViewData["Error"] = "Commande introuvable!";
            }
            return View(order);
        }

    }
}
