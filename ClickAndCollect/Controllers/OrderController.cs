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
            var obj = HttpContext.Session.GetString("CurrentOrder");
            OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);

            //var obj2 = HttpContext.Session.GetString("Produits");
            //Product[] produits = JsonConvert.DeserializeObject<Product[]>(obj2);

            //orderDicoViewModels.Order.DictionaryProducts = new Dictionary<Product, int>();

            //foreach (int key in orderDicoViewModels.Dictionary.Keys)
            //{
            //    for (int i = 0; i<produits.Length; i++)
            //    {
            //        Product p = produits[i];

            //        int Nbr = orderDicoViewModels.Dictionary[key];

            //        orderDicoViewModels.Order.DictionaryProducts.Add(p, Nbr);
            //    }

            //}

            OrderDicoViewModels orderDicoViewModels2 = orderDicoViewModels;

            if (orderDicoViewModels.Order.MakeOrder(_orderDAL, orderDicoViewModels2) == true)
            {
                TempData["SuccessOrder"] = "Felicitation ta commande a été validé !";
                return Redirect("/Product/Index");
            }

            return View();
        }

    }
}
