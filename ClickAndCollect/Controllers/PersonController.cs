using ClickAndCollect.DAL;
using ClickAndCollect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Authenticate(Person person)
        {
            if (person.VerifierCompte(_personDAL) == true)
            {
                person.GetUser(_personDAL);

                if (person.Type == "Customer")
                {
                    if(string.IsNullOrEmpty(HttpContext.Session.GetString("Id")))
                    {
                        HttpContext.Session.SetInt32("Id", person.Id);
                        HttpContext.Session.SetString("State", "connected");
                        HttpContext.Session.SetString("OrderExist", "false");
                        return Redirect("/Products/Index");
                    }
                }
                if (person.Type == "OrderPicker")
                {
                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("Id")))
                    {
                        HttpContext.Session.SetInt32("Id", person.Id);
                        HttpContext.Session.SetString("State", "connected");
                        HttpContext.Session.SetString("OrderExist", "false");
                        return View("View/Person/SuccessOrderPicker");
                    }
                }
                if (person.Type == "Cashier")
                {
                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("Id")))
                    {
                        HttpContext.Session.SetInt32("Id", person.Id);
                        HttpContext.Session.SetString("State", "connected");
                        HttpContext.Session.SetString("OrderExist", "false");
                        return View("View/Person/SuccessCashier");
                    }
                }

            }
            return View("View/Products/Index");
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            TempData["State"] = "Disconnect";
            return Redirect("/Products/Index");
        }

    }
}
