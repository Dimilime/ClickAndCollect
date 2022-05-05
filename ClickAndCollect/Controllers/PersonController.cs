using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult HomePage()
        {
            return View();
        }
    }
}
