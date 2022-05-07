using ClickAndCollect.DAL;
using ClickAndCollect.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
        public IActionResult Register(Customer customer)
        {
            if(ModelState.IsValid)
            {
                if(customer.VerifierMail(_customerDAL) != true)
                {
                    
                    customer.Register(_customerDAL);
                    return View("Views/Customer/Succes.cshtml");

                }
                return View("Views/Customer/Error.cshtml");

            }
            return View();
        }

        [HttpPost]
        public IActionResult Authenticate(Customer customer)
        {
            try
            {
                customer.VerifierCompte(_customerDAL);
            }
            catch
            {

            }
            return View();
        }
    }
}
