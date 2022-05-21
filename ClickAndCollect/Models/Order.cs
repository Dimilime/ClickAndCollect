using ClickAndCollect.DAL.IDAL;
using ClickAndCollect.ViewModels;
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
        public int NumberOfBoxUsed { get; set; }
        [Display(Name = "Entrer le nombre de caisse")]
        [Required(ErrorMessage = "Le nombre de caisse obligatoire !")]
        public int NumberOfBoxReturned { get; set; }
        public Boolean Receipt { get; set; }
        public static double ServiceFees { get; set; } = 5.95;
        public double BoxesFees { get; set; } = 5.95;
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
        public Order(DateTime dOreceipt, Customer customer, Shop shop, TimeSlot timeSlot)
        {
            Customer = customer;
            Shop = shop;
            TimeSlot = timeSlot;
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
                prix = item.Key.Price * item.Value;
                total += prix;
            }

            return total;

        }
        public static Order GetDetails(int id,IOrderDAL orderDAL)
        {
            return orderDAL.GetOrder(id);
        }
        public static List<Order> GetOrders(IOrderDAL orderDAL, OrderPicker orderPicker)
        {
            return orderDAL.GetOrders(orderPicker); 
        }
        public bool ModifyReady(IOrderDAL orderDAL)
        {
            return orderDAL.OrderReady(this);
        }

        public void EnterNumberOfBoxUsed()
        {

        }

        public void EnterNumberOfBoxesReturned()
        {

        }

        public void ModifyReceipt()
        {
            
        }

        
        
    }
}
