using ClickAndCollect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProduct(int NumProduct)
        {
            string sessionProducts = HttpContext.Session.GetString("Order");
            sessionProducts = "," + NumProduct.ToString();
            HttpContext.Session.SetString("Order", sessionProducts);
            var res = HttpContext.Session.GetString("Order");
            Console.WriteLine(res);

            return View();
        }
    }
}
