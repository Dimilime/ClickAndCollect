using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Controllers
{
    public class CashierController : Controller
    {
        private readonly ICashierDAL _cashierDAL;
        private readonly IShopDAL _shopDAL;
        public CashierController(ICashierDAL cashierDAL, IShopDAL shopDAL)
        {
            _cashierDAL = cashierDAL;
            _shopDAL = shopDAL ;
        }

    public IActionResult DailyCustomer()
        {
            try
            {
                int cId = (int)HttpContext.Session.GetInt32("IdC");// get cashier id
                Cashier cashier = Cashier.GetCashier(_cashierDAL, cId);
                int idShop = cashier.Shop.ShopId;
                HttpContext.Session.SetInt32("IdShop", idShop);
                cashier.Shop= Shop.GetInfoShop(_shopDAL, idShop); //get his shop  
                return RedirectToAction("DailyCustomer", "Order", cashier);
            }
            catch (Exception)
            {
                TempData["ErrorSession"] = "Erreur reconnectez vous!";
                return Redirect("/Person/Authenticate");
            }
        }
    }
}
