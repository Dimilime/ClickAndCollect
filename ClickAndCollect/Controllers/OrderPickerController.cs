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
        
        public OrderPickerController(IOrderPickerDAL orderPickerDAL, IShopDAL shopDAL)
        {
            _orderPickerDAL = orderPickerDAL;
        }
        public IActionResult Orders()
        {
            try
            {
                int oPId = (int)HttpContext.Session.GetInt32("IdOp");// get orderpicker id
                OrderPicker orderPicker = OrderPicker.GetOrderPicker(_orderPickerDAL, oPId);
                int IdShop = orderPicker.Shop.ShopId;
                HttpContext.Session.SetInt32("IdShop", IdShop); 
                return RedirectToAction("Orders","Order",orderPicker);
            }
            catch (Exception)
            {
                TempData["ErrorSession"] = "Erreur reconnectez vous!";
                return Redirect("/Person/Authenticate");
            }
            
            
        }
        
    }
}
