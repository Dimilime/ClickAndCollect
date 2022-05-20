using ClickAndCollect.DAL;
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
        private readonly IShopDAL _shopDAL;
        private readonly ICustomerDAL _customerDAL;
        public OrderController(IOrderDAL orderDAL, IShopDAL shopDAL, ICustomerDAL customerDAL)
        {
            _orderDAL = orderDAL;
            _shopDAL = shopDAL;
        }
        public IActionResult Orders(OrderPicker orderPicker)
        {
            try
            {
                int IdShop = (int)HttpContext.Session.GetInt32("IdShop");
                Shop shop = new Shop();
                shop.ShopId = IdShop;
                shop = Shop.GetInfoShop(_shopDAL, shop);
                orderPicker.Shop = shop;

                List<Order> orders = Order.GetOrders(_orderDAL, orderPicker);
                orderPicker.Shop.Orders = orders;
                return View(orderPicker.Shop.Orders);
            }
            catch (Exception)
            {
                TempData["ErrorIdShop"] = "Erreur reconnectez vous!";
                return View("/Person/Authenticate.cshtml");
            }
            
        }
        public IActionResult Details(int id)
        {
            /*Order order = Order.GetDetails(id, _orderDAL, new OrderPicker());
            if (order == null)
            {
                ViewData["Error"] = "Commande introuvable!";
            }*/
            return View();
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
                TempData["ErrorSession"] = "Reconnectez vous pour voir l'historique!";
                return View("Views/Person/Authenticate.cshtml");
            }

        }

        
    }
}