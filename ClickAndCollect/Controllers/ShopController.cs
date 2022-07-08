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
        
        public IActionResult SelectShop()
        {
            try
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
            catch (Exception)
            {
                TempData["Error"] = "Erreur session";
                return Redirect("/Product/Index");
            }
        }


        public IActionResult SelectDay(int ShopId)
        {
            try
            {
                var obj = HttpContext.Session.GetString("CurrentOrder");
                OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);

                Shop shop = Shop.GetInfoShop(_shopDAL, ShopId);

                orderDicoViewModels.Order.Shop = shop;
                HttpContext.Session.SetString("CurrentOrder", JsonConvert.SerializeObject(orderDicoViewModels));

                return View();
            }
            catch (Exception)
            {
                TempData["Error"] = "Erreur session";
                return Redirect("/Product/Index");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectDay(TimeSlot ts)
        {
            if (ts.Day <= DateTime.Today)
            {
                TempData["Today"] = "La date de retrait doit être une date futur!!";
                return View();
            }

            try
            {
                var obj = HttpContext.Session.GetString("CurrentOrder");
                OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);

                TimeSlot timeSlotJour = ts;

                orderDicoViewModels.Order.TimeSlot = timeSlotJour;

                HttpContext.Session.SetString("CurrentOrder", JsonConvert.SerializeObject(orderDicoViewModels));

                return Redirect("SelectCanva");
            }
            catch (Exception)
            {
                TempData["Error"] = "Erreur session, reconnectez-vous!";
                return Redirect("/Product/Index");
            }
        }

        public IActionResult SelectCanva()
        {
            try
            {
                var obj = HttpContext.Session.GetString("CurrentOrder");
                OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);

                Shop shop = orderDicoViewModels.Order.Shop;

                List<TimeSlot> timeSlots = shop.GetTimeSlots(_shopDAL);

                HttpContext.Session.SetString("CurrentOrder", JsonConvert.SerializeObject(orderDicoViewModels));

                return View(timeSlots);
            }
            catch (Exception)
            {
                TempData["Error"] = "Erreur session, reconnectez-vous!";
                return Redirect("/Product/Index");
            }
        }


        
        
    }
}
