using ClickAndCollect.Models;
using ClickAndCollect.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickAndCollect.DAL.IDAL
{
    public interface IOrderDAL
    {
        public bool MakeOrder(Order order, OrderDicoViewModels orderDicoViewModels2);
        public List<OrderTimeSlotOrderProductViewModel> GetOrders(Customer customer);
        public bool InsertOrderProductWithQuantity(int OrderId, int NumProduct, int Quantity);
    }
}
