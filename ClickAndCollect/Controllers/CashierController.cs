using ClickAndCollect.DAL.IDAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Controllers
{
    public class CashierController : Controller
    {
        private readonly ICashierDAL _cashierDAL;

        public CashierController(ICashierDAL cashierDAL)
        {
            _cashierDAL = cashierDAL;
        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
