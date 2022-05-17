using ClickAndCollect.DAL;
using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult OrdersList()
        {
            List<Order> orders = Order.GetOrders(_orderDAL);
            
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
