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
        [Display(Name = "Date d'enlèvement")]
        public DateTime DateOfReceipt { get; set; }
        public Boolean Ready { get; set; }
        public int NumberOfBoxUsed { get; set; }
        public int NumberOfBoxReturned { get; set; }
        public Boolean Receipt { get; set; }
        public static double ServiceFees { get; set; } = 5.95;
        public double BoxesFees { get; set; } = 5.95;
        public Dictionary<Product, int> DictionaryProducts { get; set; }
        private Customer Customer { get; set; }
        private TimeSlot TimeSlot { get; set; }
        public Shop Shop { get; set; }

        public Order()
        {
            DictionaryProducts = new Dictionary<Product, int>();
        }
        public Order(Customer customer)
        {
            this.customer = customer;
            DictionaryProducts = new Dictionary<Product, int>();
        }
        public Order(DateTime dOreceipt, Customer customer, Shop shop, TimeSlot timeSlot)
        {
            DateOfReceipt = dOreceipt;
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
        public static List<Order> GetOrders(IOrderDAL orderDAL, Shop shop)
        {
            return orderDAL.GetOrders(shop); 
        }
        public void ModifyReady()
        {

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
