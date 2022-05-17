using ClickAndCollect.DAL.IDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private Customer customer { get; set; }
        private TimeSlot timeSlot { get; set; }
        public Shop shop { get; set; }

        public Order()
        {

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
                prix = item.Key.Prix * item.Value;
                total += prix;
            }

            return total;
        public static Order GetDetails(int id,IOrderDAL orderDAL)
        {
            return orderDAL.GetOrder(id);
        }
        public static List<Order> GetOrders(IOrderDAL orderDAL)
        {
            return orderDAL.GetOrders(); 
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
