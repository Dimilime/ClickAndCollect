using ClickAndCollect.DAL;
using ClickAndCollect.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerDAL _customerDAL;

        public CustomerController(CustomerDAL customerDAL)
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
                if(customer.VerifierMail(_customerDAL) != false)
                {
                    customer.Register(_customerDAL);
                    return View("Views/Customer/Succes");
                }
                return View("View/Customer/Error");

            }
            return View();
        }
    }
}
