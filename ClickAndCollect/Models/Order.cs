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
        public double ADeposit { get; set; }
        public Dictionary<Product, int> Products { get; set; }
        private Customer customer { get; set; }
        private TimeSlot timeSlot { get; set; }
        private Shop shop { get; set; }

        public Order()
        {

        }

        public void ViewProducts()
        {

        }

        public void AddProduct()
        {

        }

        public void MakeOrder()
        {

        }

        public void AddTimeSlot()
        {

        }

        public void AddPickUpPoint()
        {

        }

        public void GetOrderAmount()
        {

        }

        private void CalculAmount()
        {

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
