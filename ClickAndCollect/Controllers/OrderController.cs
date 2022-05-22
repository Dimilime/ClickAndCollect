using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Models;
using ClickAndCollect.ViewModels;
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

        public IActionResult Validate()
        {
            try
            {
                var obj = HttpContext.Session.GetString("CurrentOrder");
                OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);
                OrderDicoViewModels orderDicoViewModels2 = orderDicoViewModels;
                bool result = orderDicoViewModels.Order.MakeOrder(_orderDAL, orderDicoViewModels2); 
                if (result == true)
                {
                    HttpContext.Session.SetString("CurrentOrder", "False");

                    TempData["SuccessOrder"] = "Felicitation ta commande a été validé !";
                    return Redirect("/Product/Index");
                }

                TempData["ErrorOrder"] = "La commande n'a pas abouti !!";
                return Redirect("/Product/Index");
            }
            catch (Exception)
            {
                TempData["Error"] = "Erreur session";
                return Redirect("/Product/Index");
            }

        }

        public IActionResult History()
        {
            try
            {
                int Id = (int)HttpContext.Session.GetInt32("Id");
                Customer customer = new Customer();
                customer.Id = Id;

                List<OrderTimeSlotOrderProductViewModel> orders = Order.GetOrders(_orderDAL, customer);

                return View(orders);
            }
            catch (Exception)
            {
                TempData["Error"] = "Erreur session";
                return Redirect("/Product/Index");
            }
        }

        public IActionResult Details (int OrderId)
        {
            int Id = (int)HttpContext.Session.GetInt32("Id");
            Customer customer = new Customer();
            customer.Id = Id;
            Order order = new Order(customer);
            order.OrderId = OrderId;
            List<OrderTimeSlotOrderProductViewModel> orders = Order.GetOrderById(_orderDAL, customer, order);

            return View(orders);
        }

    }
}
