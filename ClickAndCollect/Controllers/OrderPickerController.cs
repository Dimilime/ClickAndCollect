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
            int idOrderPicker = (int)HttpContext.Session.GetInt32("Id");// get orderpicker id
            
            OrderPicker orderPicker = OrderPicker.GetOrderPicker(_orderPickerDAL, idOrderPicker);
            int IdShop = orderPicker.Shop.ShopId;
            HttpContext.Session.SetInt32("IdShop", IdShop); 
            return RedirectToAction("Orders","Order",orderPicker);
            
        }
        public IActionResult OrderDetails(int id)
        {
            
            return Redirect($"/order/Details/{id}");
        }
    }
}
