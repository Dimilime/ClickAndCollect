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
            List<Product> categorys = Product.GetCategorys(_productDAL); //ies pour categories
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

            if(Nbr <= 0)
            {
                TempData["Minimum"] = "Vous devez ajouter minimum 1 article !";
                return Redirect("Index");
            }

            if(HttpContext.Session.GetString("OrderExist") == "false")
            {
                HttpContext.Session.SetString("OrderExist", "True");

                int id = (int)HttpContext.Session.GetInt32("Id");
                Customer customer = new Customer();
                customer.Id = id;

                Order order = new Order(customer);

                Dictionary<int, int> dictionary = new Dictionary<int, int>();
                dictionary.Add(NumProduct, Nbr);

                OrderDicoViewModels orderDicoViewModels = new OrderDicoViewModels(order, dictionary);
                HttpContext.Session.SetString("CurrentOrder", JsonConvert.SerializeObject(orderDicoViewModels));

            }
            else
            {
                var obj = HttpContext.Session.GetString("CurrentOrder");
                OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj); 

                if (orderDicoViewModels.Dictionary.ContainsKey(NumProduct))
                {
                    int oldNbr = orderDicoViewModels.Dictionary[NumProduct];
                    int newNbr = oldNbr + Nbr;
                    orderDicoViewModels.Dictionary[NumProduct] = newNbr;
                }
                else
                {
                    orderDicoViewModels.Dictionary.Add(NumProduct, Nbr);
                }

                HttpContext.Session.SetString("CurrentOrder", JsonConvert.SerializeObject(orderDicoViewModels));

            }
            TempData["Add"] = "L'ajout a été réalisé avec succès !";
            return Redirect("Index");
        }

        public IActionResult Basket()
        {
            var obj = HttpContext.Session.GetString("CurrentOrder");

            if(obj is null)
            {
                TempData["BasketEmpty"] = "Votre panier est vide :(";
                return View("BasketEmpty");
            }

            OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);
            orderDicoViewModels.Order.DictionaryProducts = new Dictionary<Product, int>();

            foreach (int key in orderDicoViewModels.Dictionary.Keys)
            {
                Product p = new Product();
                p.NumProduct = key;
                p = p.GetInfoProduct(_productDAL);

                int Nbr = orderDicoViewModels.Dictionary[key];

                orderDicoViewModels.Order.DictionaryProducts.Add(p, Nbr);

            }

            double SoldePanier = orderDicoViewModels.Order.GetOrderAmount();

            TempData["OrderAmount"] = SoldePanier;

            return View(orderDicoViewModels);
        }

        public IActionResult Delete(int Key)
        {
            var obj = HttpContext.Session.GetString("CurrentOrder");
            OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);

            orderDicoViewModels.Dictionary.Remove(Key);

            HttpContext.Session.SetString("CurrentOrder", JsonConvert.SerializeObject(orderDicoViewModels));

            return Redirect("Basket");
        }

    }


}
