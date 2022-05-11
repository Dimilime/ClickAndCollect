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
                        HttpContext.Session.SetString("state", "connected");
                        TempData["State"] = HttpContext.Session.GetString("state");
                        return Redirect("/Products/Index");
                    }
                }
                if (person.Type == "OrderPicker")
                {
                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("Id")))
                    {
                        HttpContext.Session.SetInt32("Id", person.Id);
                        HttpContext.Session.SetString("state", "connected");
                        TempData["State"] = HttpContext.Session.GetString("state");
                        return View("View/Person/SuccessOrderPicker");
                    }
                }
                if (person.Type == "Cashier")
                {
                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("Id")))
                    {
                        HttpContext.Session.SetInt32("Id", person.Id);
                        HttpContext.Session.SetString("state", "connected");
                        TempData["State"] = HttpContext.Session.GetString("state");
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
