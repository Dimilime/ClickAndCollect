using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductDAL _productDAL;

        public ProductController(IProductDAL productsDAL)
        {
            _productDAL = productsDAL;
        }
        public IActionResult Index()
        {
            List<Product> categorys = _productDAL.GetCategorys();
            return View(categorys);
        }

        public IActionResult Details(Product p)
        {
            List<Product> ps = _productDAL.GetProducts(p);
            return View(ps);
        }

    }
}
