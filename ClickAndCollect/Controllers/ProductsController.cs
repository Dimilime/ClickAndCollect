using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsDAL _productsDAL;

        public ProductsController(IProductsDAL productsDAL)
        {
            _productsDAL = productsDAL;
        }
        public IActionResult Index()
        {
            List<Products> products = _productsDAL.GetProducts();
            return View(products);
        }
    }
}
