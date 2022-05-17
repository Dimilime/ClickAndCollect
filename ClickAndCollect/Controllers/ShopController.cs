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
    public class ShopController : Controller
    {
        private readonly IShopDAL _shopDAL;

        public ShopController(IShopDAL shopDAL)
        {
            _shopDAL = shopDAL;
        }
        
        public IActionResult Index()
        {
            var obj = HttpContext.Session.GetString("CurrentOrder");
            OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);

            if (orderDicoViewModels.Dictionary.Count == 0)
            {
                TempData["BasketEmpty"] = "Votre panier est vide :(";
                return Redirect("/Product/Basket");
            }

            List<Shop> shops = Shop.GetShops(_shopDAL);
            return View(shops);
        }

        public IActionResult Select(int ShopId)
        {
            var obj = HttpContext.Session.GetString("CurrentOrder");
            OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);

            Shop shop = new Shop();
            shop.ShopId = ShopId;
            shop.GetInfoShop(_shopDAL);

            orderDicoViewModels.Order.shop = shop;

            HttpContext.Session.SetString("CurrentOrder", JsonConvert.SerializeObject(orderDicoViewModels));

            return View();
        }
    }
}
