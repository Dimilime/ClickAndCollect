using ClickAndCollect.DAL;
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
        private readonly IOrderDAL _orderDAL;
        private readonly ICustomerDAL _customerDAL;
        public CashierController(ICashierDAL cashierDAL, IShopDAL shopDAL, IOrderDAL orderDAL, ICustomerDAL customerDAL) 
        {
            _cashierDAL = cashierDAL;
            _shopDAL = shopDAL ;
            _orderDAL = orderDAL;
            _customerDAL = customerDAL;
        }

        public IActionResult DailyCustomer()
        {
            try
            {
                int cId = (int)HttpContext.Session.GetInt32("IdC");// get cashier id
                Cashier cashier = Cashier.GetCashier(_cashierDAL, cId);
                int idShop = cashier.Shop.ShopId;
                cashier.Shop = Shop.GetInfoShop(_shopDAL, idShop);
                cashier.Shop.Orders = Order.GetOrders(_orderDAL, cashier);
                
                if (cashier.Shop.Orders != null)
                {
                    for (int i = 0; i < cashier.Shop.Orders.Count; i++)
                    {
                        cashier.Shop.Orders[i].Customer = Customer.GetInfoCustomer(_customerDAL, cashier.Shop.Orders[i].Customer.Id);
                    }
                }
                else
                {
                    ViewData["ErrorCustomer"] = "Erreur liste de client non trouvé!";
                }
                if (cashier.Shop.Orders != null && cashier.Shop.Orders.Count == 0 )
                {
                    ViewData["EmptyList"] = "Plus de client pour aujourd'hui !";
                }
                return View(cashier.Shop.Orders);
            }
            catch (Exception)
            {
                TempData["ErrorSession"] = "Erreur reconnectez vous!";
                return Redirect("/Person/Authenticate");
            }
        }

        public IActionResult ValidateReceipt(int id)
        {
            Order order = Order.GetDetails(id, _orderDAL);
            if (order == null)
            {
                ViewData["Error"] = "Commande introuvable!";
            }
            if ((string)TempData["ReceiptValid"] == "Commande repris!")
            {
                ViewData["Total"] = order.GetOrderAmount();
            }
            return View(order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DailyCustomer(Order order)
        {
            if(order != null)
            {
                if (ModelState.IsValid)
                {
                    if (order.ModifyReceipt(_orderDAL))
                    {
                        if (order.Receipt)
                        {
                            TempData["ReceiptValid"] = "Commande repris!";
                        }
                        else
                        {
                            TempData["ReceiptValid"] = "Attention! Veuillez indiquer la commande comme reprise";
                        }

                    }
                    else
                    {
                        TempData["ReceiptValid"] = "L'insertion s'est mal déroulé veuillez réessayer!";
                    }
                }
                return Redirect($"ValidateReceipt/{order.OrderId}");
            }
            ViewData["OrderNotFound"] = "Commande non trouvé!";
            return View();
            
        }

    }
}
