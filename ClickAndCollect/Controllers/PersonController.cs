using ClickAndCollect.DAL;
using ClickAndCollect.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonDAL _personDAL;

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
            if (ModelState.IsValid)
            {
                if (person.VerifierCompte(_personDAL) != true)
                {
                    return View("View/Person/Success");
                }
                return View("View/Person/Error");
            }
            return View();
        }
    }
}
