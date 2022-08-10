using ClickAndCollect.DAL;
using ClickAndCollect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonDAL _personDAL;

        public PersonController(IPersonDAL personDAL)
        {
            _personDAL = personDAL;
        }
        

        public IActionResult Authenticate()
        {   
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Authenticate(Person person)
        {
            if (person.CheckIfAccountExists(_personDAL) == true)
            {
                person = person.GetAllFromUser(_personDAL);

                if (person is Customer)
                {
                    if(string.IsNullOrEmpty(HttpContext.Session.GetString("Id")))
                    {
                        HttpContext.Session.SetInt32("Id", person.Id);
                        HttpContext.Session.SetString("State", "connected");
                        //HttpContext.Session.SetString("OrderExist", "false");
                        HttpContext.Session.SetString("Type", "Customer");
                        TempData["Type"] = HttpContext.Session.GetString("Type");
                        TempData["State"] = HttpContext.Session.GetString("State");
                       
                        return Redirect("/Product/Index");
                    }
                }
                if (person is OrderPicker)
                {
                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("Id")))
                    {
                        HttpContext.Session.SetInt32("IdOp", person.Id);
                        HttpContext.Session.SetString("State", "connected");
                        HttpContext.Session.SetString("Type", "OrderPicker");
                        TempData["Type"] = HttpContext.Session.GetString("Type");
                        TempData["State"] = HttpContext.Session.GetString("State");
                        return Redirect("/OrderPicker/Orders");
                    }
                }
                if (person is Cashier)
                {
                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("Id")))
                    {
                        HttpContext.Session.SetInt32("IdC", person.Id);
                        HttpContext.Session.SetString("State", "connected");
                        HttpContext.Session.SetString("Type", "Cashier");
                        TempData["Type"] = HttpContext.Session.GetString("Type");
                        TempData["State"] = HttpContext.Session.GetString("State");
                        
                        return Redirect("/Cashier/DailyCustomer");
                    }
                }

            }
            TempData["ErrorAuthenticate"] = "L'email ou le mot de passe est incorrecte!";
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            TempData["State"] = "Disconnect";
            return RedirectToAction("Authenticate");
        }

    }
}
