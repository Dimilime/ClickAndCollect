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
    public class ProductController : Controller
    {
        private readonly IProductDAL _productDAL;

        public ProductController(IProductDAL productDAL)
        {
            _productDAL = productDAL;
        }
        public IActionResult Index()
        {
            List<Product> categorys = Product.GetCategorys(_productDAL);
            return View(categorys);
        }

        public IActionResult Details(Product produit)
        {
            List<Product> produits = Product.GetProducts(_productDAL,produit);
            int Nbr = 0;

            ProductNbrViewModels pnvm = new ProductNbrViewModels(produits,Nbr);

            return View(pnvm);
        }

        public IActionResult AddProduct(int NumProduct, int Nbr)
        {

            if(HttpContext.Session.GetString("OrderExist") == "false")
            {
                HttpContext.Session.SetString("OrderExist", "True");

                Order order = new Order();

                Dictionary<int, int> dictionary = new Dictionary<int, int>();
                dictionary.Add(NumProduct, Nbr);
                OrderDicoViewModels orderDicoViewModels = new OrderDicoViewModels(order, dictionary);

                HttpContext.Session.SetString("CurrentOrder", JsonConvert.SerializeObject(orderDicoViewModels));

            }
            else
            {
                var obj = HttpContext.Session.GetString("CurrentOrder");
                OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);

                /****/
                //AJOUT AU DICO 
                //1. Est ce que le produuit a deja été ajouté ? si oui on additionne a la quantité
                // Si non, on ajoute un element au dico et on le reserialize 

                if (orderDicoViewModels.Dictionary.ContainsKey(NumProduct))
                {
                    //ajouter de la quantité 

                }
                else
                {
                    orderDicoViewModels.Dictionary.Add(NumProduct, Nbr);
                }

                HttpContext.Session.SetString("CurrentOrder", JsonConvert.SerializeObject(orderDicoViewModels));

            }

            return Redirect("/Product/Index");
        }

    }
}
