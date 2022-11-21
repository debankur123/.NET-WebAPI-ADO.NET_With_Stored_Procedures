using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models_Repository;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepository _orderRepository;
        public OrderController(IOrderRepository _repo)
        {
            _orderRepository = _repo;
        }
        [HttpGet]
        public IEnumerable<Order> returnAllOrders()
        {
            return _orderRepository.GetAllOrder().ToList();
        }
        [HttpGet("{id}")]
        public Order orderById(int id)
        {
            return _orderRepository.GetOrderById(id);
        }
        [HttpPost]
        public Order createOrder([FromBody] Order _Order)
        {
            return _orderRepository.AddOrder(_Order);
        }
        [HttpPatch]
        public Order changeOrderDetails([FromBody] Order _order)
        {
            return _orderRepository.UpdateOrder(_order);
        }
        [HttpDelete("{id}")]
        public void deleteOrderDetails(int id)
        {
            _orderRepository.DeleteOrder(id);
        }
    }
}
