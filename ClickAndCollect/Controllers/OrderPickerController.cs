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
        private readonly IOrderPickerDAL _shopDAL;
        public OrderPickerController(IOrderPickerDAL orderPickerDAL)
        {
            _orderPickerDAL = orderPickerDAL;
        }
        public IActionResult Orders()
        {
            OrderPicker orderPicker = new OrderPicker();
            orderPicker.Id = (int)HttpContext.Session.GetInt32("Id");
            orderPicker.Shop.GetInfoShop(_shopDal);
            List<Order> orders = orderPicker.ViewOrders(_orderPickerDAL);
            return View(orders);
        }
        public IActionResult OrderDetails(int id)
        {
            
            return Redirect($"/order/Details/{id}");
        }
    }
}
