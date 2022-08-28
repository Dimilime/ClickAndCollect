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
        bool MakeOrder(Order order, OrderDicoViewModels orderDicoViewModels);
        
        bool InsertOrderProductWithQuantity(int OrderId, int NumProduct, int Quantity);
        Order GetOrder(int id);
        bool OrderReady(Order order);
        bool OrderReceipt(Order order);
    }
}
