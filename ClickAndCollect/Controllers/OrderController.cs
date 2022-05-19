﻿using ClickAndCollect.DAL.IDAL;
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
            var obj = HttpContext.Session.GetString("CurrentOrder");
            OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);
            OrderDicoViewModels orderDicoViewModels2 = orderDicoViewModels;

            if (orderDicoViewModels.Order.MakeOrder(_orderDAL, orderDicoViewModels2) == true)
            {
                TempData["SuccessOrder"] = "Felicitation ta commande a été validé !";
                return Redirect("/Product/Index");
            }

            TempData["ErrorOrder"] = "La commande n'a pas abouti !!";
            return Redirect("/Product/Index");
        }

        public IActionResult History()
        {
            int Id = (int)HttpContext.Session.GetInt32("Id");
            Customer customer = new Customer();
            customer.Id = Id;

            List<OrderTimeSlotOrderProductViewModel> orders = Order.GetOrders(_orderDAL, customer);

            return View(orders);
        }

    }
}
