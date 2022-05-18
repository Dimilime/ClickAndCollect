using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class Order
    {
        public Boolean Ready { get; set; }
        public int NumberOfBoxUsed { get; set; }
        public int NumberOfBoxReturned { get; set; }
        public Boolean Receipt { get; set; }
        public static double ServiceFees { get; set; }
        public double BoxesFees { get; set; }
        public Dictionary<Product, int> DictionaryProducts { get; set; }
        public Customer customer { get; set; }
        public TimeSlot timeSlot { get; set; }
        public Shop shop { get; set; }

        public Order(Customer customer)
        {
            this.customer = customer;
        }

        public void MakeOrder()
        {

        }

        public void AddTimeSlot()
        {

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

        public void GetDetails()
        {

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

        public static void GetAllShopOrders()
        {

        }
        public void CustomerToList()
        {

        }
        
        public static void GetCustomerList()
        {

        }
    }
}
