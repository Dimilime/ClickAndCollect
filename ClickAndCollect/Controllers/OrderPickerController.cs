using ClickAndCollect.DAL.IDAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClickAndCollect.DAL;
using ClickAndCollect.Models;
using Microsoft.AspNetCore.Http;

namespace ClickAndCollect.Controllers
{
    public class OrderPickerController : Controller
    {
        private readonly IOrderPickerDAL _orderPickerDAL;
        private readonly IShopDAL _shopDAL;
        private readonly IOrderDAL _orderDAL;
        public OrderPickerController(IOrderPickerDAL orderPickerDAL, IShopDAL shopDAL, IOrderDAL orderDAL)
        {
            _orderPickerDAL = orderPickerDAL;
            _shopDAL = shopDAL;
            _orderDAL = orderDAL;
        }
        
        public IActionResult Orders()
        {
            try
            {
                int oPId = (int)HttpContext.Session.GetInt32("IdOp");// get orderpicker id
                OrderPicker orderPicker = OrderPicker.GetOrderPicker(_orderPickerDAL, oPId);
                orderPicker.GetInfoShop(_shopDAL);
                orderPicker.GetOrders(_shopDAL);
                if (orderPicker.Shop.Orders == null)
                {
                    ViewData["ErrorOrder"] = "Erreur liste de commande non trouvé!";
                }
                else if (orderPicker.Shop.Orders.Count == 0)
                {
                    ViewData["EmptyOrder"] = "Pas de commande pour demain!";
                }
                return View(orderPicker.Shop.Orders);
            }
            catch (Exception)
            {
                TempData["ErrorSession"] = "Erreur reconnectez vous!";
                return Redirect("/Person/Authenticate");
            }


        }
        
    }
}
