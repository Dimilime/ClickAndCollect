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
                int idShop = orderPicker.Shop.ShopId;
                orderPicker.Shop = Shop.GetInfoShop(_shopDAL, idShop);
                orderPicker.Shop.Orders = Order.GetOrders(_orderDAL, orderPicker);
                if (orderPicker.Shop.Orders == null)
                {
                    ViewData["ErrorOrder"] = "Erreur liste de commande non trouvé!";
                }
                return View(orderPicker.Shop.Orders);
            }
            catch (Exception)
            {
                TempData["ErrorIdShop"] = "Erreur reconnectez vous!";
                return Redirect("/Person/Authenticate");
            }


        }
        
    }
}
