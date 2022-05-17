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
        private int orderID;
        private int idPerson ;
        private bool ready ;
        private int numberOfBoxesUsed ;
        private int numberOfBoxesReturned ;
        private bool receipt ;
        private static double serviceFees = 5.95;
        private Dictionary<Product, int> products ;
        private Customer customer ;
        private TimeSlot timeSlot ;
        private Shop shop ;

        public int OrderID { get => orderID; set => orderID=value; }
        public int IdPerson { get => idPerson; set => idPerson=value; }
        public bool Ready { get => ready; set => ready = value; }
        public int NumberOfBoxesUsed { get => numberOfBoxesUsed; set => numberOfBoxesUsed = value; }
        public int NumberOfBoxesReturned { get => numberOfBoxesReturned ; set => numberOfBoxesReturned = value; }
        public bool Receipt { get => receipt; set => receipt = value; }
        public static double ServiceFees { get => serviceFees ; set => serviceFees = value ; }
        public Dictionary<Product, int> Products { get => products; set => products = value; } 
        public Customer Customer { get => customer; set => customer = value ; }
        public TimeSlot TimeSlot { get => timeSlot; set => timeSlot = value ; }
        public Shop Shop { get => shop; set => shop = value; }

        [DataType(DataType.Date)]
        public DateTime DateOfReceipt { get; set; }

        public Order()
        {
            Products = new Dictionary<Product, int>();
        }
        public Order(Customer c)
        {
            Customer = c;
            Products = new Dictionary<Product, int>();
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

        private double CalculAmount()
        {
            double totalOrder=0.0;
            return (NumberOfBoxesUsed - NumberOfBoxesReturned) * ServiceFees + totalOrder;
        }

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
