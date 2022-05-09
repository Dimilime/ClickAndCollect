using ClickAndCollect.DAL;
using ClickAndCollect.Models;
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
        
        public IActionResult HomePage()
        {
            return View();
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
                    return View("View/Person/SuccessCustomer");
                }
                if (person.Type == "OrderPicker")
                {
                    return View("View/Person/SuccessOrderPicker");
                }
                if (person.Type == "Cashier")
                {
                    return View("View/Person/SuccessCashier");
                }

            }
            return View("View/Person/Error");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
