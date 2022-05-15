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
                p=p.GetInfoProduct(_productsDAL);

                Dictionary<int, int> dico = new Dictionary<int, int>();
                dico.Add(NumProduct, Nbr);
                OrderDicoViewModels orderDicoViewModels = new OrderDicoViewModels(o, dico);

                HttpContext.Session.SetString("CurrentOrder", JsonConvert.SerializeObject(orderDicoViewModels));

            }
            else
            {
                //Console.WriteLine(TempData["CurrentOrder"]);
                var obj = HttpContext.Session.GetString("CurrentOrder");
                OrderDicoViewModels o = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);
                //var value = HttpContext.Session.GetString("CurrentOrder");
                //Order o = JsonConvert.DeserializeObject<Order>(value);

                /****/
                //AJOUT AU DICO 
                //1. Est ce que le produuit a deja été ajouté ? si oui on additionne a la quantité
                // Si non, on ajoute un element au dico et on le reserialize 

                if (o.Dico.ContainsKey(NumProduct))
                {
                    //ajouter de la quantité 

                }
                else
                {
                    o.Dico.Add(NumProduct, Nbr);
                }

                //Products p = new Products();
                ////p.GetInfoProduct(_productsDAL);

                //o.Products.Add(p, Nbr);
                HttpContext.Session.SetString("CurrentOrder", JsonConvert.SerializeObject(o));

            }

            return Redirect("/Products/Index");
        }

    }
}
