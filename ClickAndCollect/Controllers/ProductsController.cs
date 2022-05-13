using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Models;
using ClickAndCollect.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            List<Products> categorys = Products.GetCategorys(_productsDAL);
            return View(categorys);
        }

        public IActionResult Details(Products p)
        {
            List<Products> ps = Products.GetProducts(_productsDAL,p);
            int Nbr = 0;

            OrderProductsViewModels opvm = new OrderProductsViewModels(ps,Nbr);

            return View(opvm);
        }

        public IActionResult AddProduct(int NumProduct, int Nbr)
        {

            if(HttpContext.Session.GetString("OrderExist") == "false")
            {
                HttpContext.Session.SetString("OrderExist", "True");

                Order o = new Order();
                Products p = new Products();

                p.NumProduct = NumProduct;
                p.GetInfoProduct(_productsDAL);

                o.Products = new Dictionary<Products, int>();
                o.Products.Add(p, Nbr);

                //TempData["CurrentOrder"] = JsonConvert.SerializeObject(o);
                HttpContext.Session.SetString("CurrentOrder", JsonConvert.SerializeObject(o));
            }
            else
            {
                //Console.WriteLine(TempData["CurrentOrder"]);
                Order o = JsonConvert.DeserializeObject<Order>(TempData["CurrentOrder"].ToString());
                //var value = HttpContext.Session.GetString("CurrentOrder");
                //Order o = JsonConvert.DeserializeObject<Order>(value);

                Products p = new Products();
                p.GetInfoProduct(_productsDAL);

                //o.Products.Add(p, Nbr);

            }

            return Redirect("/Products/Index");
        }

    }
}
