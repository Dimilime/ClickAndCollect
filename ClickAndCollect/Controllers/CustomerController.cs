using ClickAndCollect.DAL;
using ClickAndCollect.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ClickAndCollect.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerDAL _customerDAL;
        

        public CustomerController(ICustomerDAL customerDAL)
        {
            _customerDAL = customerDAL;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Customer customer)
        {
            if(ModelState.IsValid)
            {
                if(customer.CheckIfEmailCustomerExists(_customerDAL) != true)
                {
                    
                    if(customer.Register(_customerDAL) == true)
                    {
                        TempData["AccountCreate"] = "Votre compte a été cré !";
                        return Redirect("/Product/Index");
                    }

                }
                TempData["EmailExists"] = "L'adresse email a déjà un compte";
                return View();

            }
            return View();
        }

    }
}
