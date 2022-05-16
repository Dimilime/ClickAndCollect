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
            List<Shop> shops = Shop.GetShops(_shopDAL);
            return View(shops);
        }

        public IActionResult Select(int ShopId)
        {
            Shop shop = new Shop();
            shop.ShopId = ShopId;
            shop.GetInfoShop(_shopDAL);

            var obj = HttpContext.Session.GetString("CurrentOrder");
            OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);

            orderDicoViewModels.Order.shop = shop;

            return View();
        }
    }
}
