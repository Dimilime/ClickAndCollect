using ClickAndCollect.DAL.IDAL;
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

        [Display(Name = "Entrer le nombre de caisse")]
        [Required(ErrorMessage = "Le nombre de caisse obligatoire !")]
        [Min(0,ErrorMessage ="Le nombre de caisse doit être plus grand ou égal à 0")]
        public int NumberOfBoxUsed { get; set; }
        [Display(Name = "Entrer le nombre de caisse")]
        [Required(ErrorMessage = "Le nombre de caisse obligatoire !")]
        [Min(0, ErrorMessage = "Le nombre de caisse doit être plus grand ou égal à 0")]
        public int NumberOfBoxReturned { get; set; }
        [Display(Name = "Repris?")]
        public Boolean Receipt { get; set; }
        public static double ServiceFees { get; set; } = 5.95;
        public static double BoxesFees { get; set; } = 5.95;
        public Dictionary<Product, int> DictionaryProducts { get; set; }
        public Customer Customer { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public Shop Shop { get; set; }

        public Order()
        {
            DictionaryProducts = new Dictionary<Product, int>();
        }
        public Order(Customer customer)
        {
            this.Customer = customer;
            DictionaryProducts = new Dictionary<Product, int>();
        }
       
        public static List<OrderTimeSlotOrderProductViewModel> GetOrders (IOrderDAL orderDAL, Customer customer)
        {
            return orderDAL.GetOrders(customer);
        }
        public bool MakeOrder(IOrderDAL orderDAL, OrderDicoViewModels orderDicoViewModels2)
        {
            return orderDAL.MakeOrder(this, orderDicoViewModels2);
        }

        public double GetOrderAmount()
        {
            return CalculAmount();
        }

        private double CalculAmount()
        {
            double prix;
            double total = 0;

            foreach (var item in DictionaryProducts)
            {
                prix = item.Key.Price * item.Value ;
                total += prix;
            }

            return total + ServiceFees + (NumberOfBoxUsed - NumberOfBoxReturned) * BoxesFees;

        }
        public static Order GetDetails(int id,IOrderDAL orderDAL)
        {
            return orderDAL.GetOrder(id);
        }
        public static List<Order> GetOrders(IOrderDAL orderDAL, IEmployees employee)
        {
            return orderDAL.GetOrders(employee); 
        }
        /*public static List<Customer> GetOrderCustomers(IOrderDAL orderDAL, Shop shop)
        {
            //return orderDAL.GetOrderCustomers(shop); 
        }*/
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
