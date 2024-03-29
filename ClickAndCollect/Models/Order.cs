﻿using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.Interface;
using ClickAndCollect.ViewModels;
using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [Display(Name = "Prêt?")]
        public Boolean Ready { get; set; }

        [Display(Name = "Entrer le nombre de caisse utilisé")]
        [Required(ErrorMessage = "Le nombre de caisse obligatoire !")]
        [Min(0,ErrorMessage ="Le nombre de caisse doit être plus grand ou égal à 0")]
        public int NumberOfBoxUsed { get; set; }
        [Display(Name = "Entrer le nombre de caisse retourné")]
        [Required(ErrorMessage = "Le nombre de caisse obligatoire !")]
        [Min(0, ErrorMessage = "Le nombre de caisse doit être plus grand ou égal à 0")]
        public int NumberOfBoxReturned { get; set; }
        [Display(Name = "Repris?")]
        public Boolean Receipt { get; set; }
        public static double ServiceFees { get; set; } = 5.95;
        public static double BoxesFees { get; set; } = 5.95;
        public Dictionary<Product, int> Products { get; set; }
        public Customer Customer { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public Shop Shop { get; set; }

        public Order()
        {
            Products = new Dictionary<Product, int>();
        }
        public Order(Customer customer)
        {
            Customer = customer;
            Products = new Dictionary<Product, int>();
        }

        public bool MakeOrder(IOrderDAL orderDAL, OrderDicoViewModels orderDicoViewModels)
        {
            return orderDAL.MakeOrder(this, orderDicoViewModels);
        }

        public double GetOrderAmount()
        {
            return CalculAmount();
        }

        public double CalculAmount()
        {
            double prix;
            double total = 0;

            foreach (var item in Products)
            {
                prix = item.Key.Price * item.Value ;
                total += prix;
            }

            return total + ServiceFees + (NumberOfBoxUsed - NumberOfBoxReturned) * BoxesFees;

        }
        public static Order GetDetails(IOrderDAL orderDAL, int id)
        {
            return orderDAL.GetOrder(id);
        }
        
        
        public bool ModifyReady(IOrderDAL orderDAL)
        {
            return orderDAL.OrderReady(this);
        }

        public bool ModifyReceipt(IOrderDAL orderDAL)
        {
            return orderDAL.OrderReceipt(this);
        }  
    }
}
