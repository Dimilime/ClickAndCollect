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
            List<string> categories = Product.GetCategories(_productDAL);
            List<Category> cats = new List<Category>();
            for (int i = 0; i < categories.Count; i++)
            {
                cats.Add((Category)Enum.Parse(typeof(Category), categories[i]));
            }
            return View(cats);
        }

        public IActionResult Details(string category)
        {
            List<Product> produits = Product.GetProducts(_productDAL,category);
            int Nbr = 0;
            ProductNbrViewModels pnvm = new ProductNbrViewModels(produits,Nbr);
            TempData["Category"] =category;
            return View(pnvm);
        }

        
        public IActionResult AddProduct(int NumProduct, int Nbr)
        {
            string category= (string)TempData["Category"];
            try
            {
                if (Nbr <= 0)
                {
                    TempData["Minimum"] = "Vous devez ajouter minimum 1 article !";
                    return Redirect($"Details?category={category}");
                    
                }
                else if (HttpContext.Session.GetString("CurrentOrder") == null)
                {
                    //HttpContext.Session.SetString("OrderExist", "True");
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
                    string obj = HttpContext.Session.GetString("CurrentOrder");
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
                TempData["Add"] = "Produit ajouté dans le panier !";
                return Redirect($"Details?category={category}");
            }
            catch (Exception)
            {
                TempData["Error"] = "Erreur session, reconnectez vous!";
                return Redirect("Index");
            }

        }

        public IActionResult Basket()
        {
            try
            {
                string obj = HttpContext.Session.GetString("CurrentOrder");

                if (obj is null)
                {
                    ViewData["BasketEmpty"] = "Votre panier est vide :(";
                    return View();
                }

                OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);
                orderDicoViewModels.Order.Products = new Dictionary<Product, int>();

                foreach (int key in orderDicoViewModels.Dictionary.Keys)
                {
                    Product p = new Product();
                    p.NumProduct = key;
                    p = p.GetInfoProduct(_productDAL);

                    int Nbr = orderDicoViewModels.Dictionary[key];

                    orderDicoViewModels.Order.Products.Add(p, Nbr);

                }

                double SoldePanier = orderDicoViewModels.Order.GetOrderAmount();

                ViewData["OrderAmount"] = SoldePanier;

                return View(orderDicoViewModels);
            }
            catch (Exception)
            {
                TempData["Error"] = "Erreur session, reconnectez vous!";
                return Redirect("/Product/Index");
            }
        }

        public IActionResult Delete(int Key)
        {
            try
            {
                string obj = HttpContext.Session.GetString("CurrentOrder");
                OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);

                orderDicoViewModels.Dictionary.Remove(Key);

                HttpContext.Session.SetString("CurrentOrder", JsonConvert.SerializeObject(orderDicoViewModels));

                return Redirect("Basket");
            }
            catch (Exception)
            {
                TempData["Error"] = "Erreur session, reconnectez vous!";
                return Redirect("/Product/Index");
            }
        }

        public IActionResult Summary()
        {
            try
            {
                string obj = HttpContext.Session.GetString("CurrentOrder");
                OrderDicoViewModels orderDicoViewModels = JsonConvert.DeserializeObject<OrderDicoViewModels>(obj);

                orderDicoViewModels.Order.Products = new Dictionary<Product, int>();

                foreach (int key in orderDicoViewModels.Dictionary.Keys)
                {
                    Product p = new Product
                    {
                        NumProduct = key
                    };
                    p = p.GetInfoProduct(_productDAL);

                    int Nbr = orderDicoViewModels.Dictionary[key];

                    orderDicoViewModels.Order.Products.Add(p, Nbr);

                }

                double SoldePanier = orderDicoViewModels.Order.GetOrderAmount();

                ViewData["OrderAmount"] = SoldePanier;


                return View(orderDicoViewModels);
            }
            catch (Exception)
            {
                TempData["Error"] = "Erreur session, reconnectez vous!";
                return Redirect("/Product/Index");
            }
        }
    }
}
