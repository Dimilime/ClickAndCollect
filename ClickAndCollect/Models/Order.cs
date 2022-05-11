using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.Models
{
    public class Order
    {
        private Boolean ready;
        private int numberOfBoxUsed;
        private int numberOfBoxReturned;
        private Boolean receipt;
        private static double serviceFees;
        private double aDeposit;
        private Dictionary<Products, int> product;
        private Customer customer;
        private TimeSlot timeSlot;
        private Shop shop;

        public Order()
        {
            product = new Dictionary<Products, int>();
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
