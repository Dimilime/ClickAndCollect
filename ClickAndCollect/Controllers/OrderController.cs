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
            _customerDAL = customerDAL;
        }
        public IActionResult Orders(OrderPicker orderPicker)
        {
            try
            {
                int idShop = (int)HttpContext.Session.GetInt32("IdShop");
                orderPicker.Shop = Shop.GetInfoShop(_shopDAL, idShop);

                List<Order> orders = Order.GetOrders(_orderDAL, orderPicker);
                orderPicker.Shop.Orders = orders;
                if(orderPicker.Shop.Orders == null)
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
        public IActionResult Details(int id)
        {
            Order order = Order.GetDetails(id, _orderDAL);
            if (order == null)
            {
                ViewData["Error"] = "Commande introuvable!";
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OrderReady(Order order)
        {
            if (ModelState.IsValid)
            {
                if (order.ModifyReady(_orderDAL))
                {
                    if (order.Ready)
                    {
                        TempData["OrderValid"] = "Commande prête!";
                    }
                    else
                    {
                        TempData["OrderValid"] = "Attention! La commande n'est pas prête, veuillez indiquer que c'est prêt";
                    }
                    
                }else
                {
                    TempData["OrderValid"] = "L'insertion s'est mal déroulé veuillez réessayer!";
                }
            }
            return Redirect($"details/{order.OrderId}");
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
                TempData["Error"] = "Erreur session";
                return Redirect("/Product/Index");
            }
        }

        public IActionResult DailyCustomer(Cashier cashier)
        {
            int idShop = (int)HttpContext.Session.GetInt32("IdShop");
            cashier.Shop = Shop.GetInfoShop(_shopDAL, idShop);
            cashier.Shop.Orders= Order.GetOrders(_orderDAL,cashier);
            for (int i = 0; i < cashier.Shop.Orders.Count; i++)
            {
                cashier.Shop.Orders[i].Customer = Customer.GetInfoCustomer(_customerDAL, cashier.Shop.Orders[i].Customer.Id);
            }
            if (cashier.Shop.Orders == null)
            {
                ViewData["ErrorCustomer"] = "Erreur liste de client non trouvé!";
            }
            if(cashier.Shop.Orders.Count == 0)
            {
                ViewData["EmptyList"] = "Plus de client pour aujourd'hui !";
            }
            return View(cashier.Shop.Orders);

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
            if (order == null)
            {
                ViewData["Error"] = "Commande introuvable!";
            }
            return Redirect($"ValidateReceipt/{order.OrderId}");

        }


    }
}