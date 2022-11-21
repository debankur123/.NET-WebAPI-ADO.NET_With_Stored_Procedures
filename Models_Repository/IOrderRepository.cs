using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models_Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrder();
        Order GetOrderById(int id);
        Order AddOrder(Order _order);
        Order UpdateOrder(Order _order);
        void DeleteOrder(int id);
    }
}
