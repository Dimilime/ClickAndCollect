using ClickAndCollect.Interface;
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
        public bool MakeOrder(Order order, OrderDicoViewModels orderDicoViewModels);
        
        public bool InsertOrderProductWithQuantity(int OrderId, int NumProduct, int Quantity);
        public Order GetOrder(int id);
        public bool OrderReady(Order order);
        public bool OrderReceipt(Order order);
    }
}
