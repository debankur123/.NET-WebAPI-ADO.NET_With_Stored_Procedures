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
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository _custRepository;
        //Dependency Injection is used above.
        //Creating an instance of ICustomerRepository
        public CustomerController(ICustomerRepository repo)
        {
            _custRepository = repo;
        }
        [HttpGet]
        public IEnumerable<Customers> returnAllCustomers()
        {
            return _custRepository.GetAllCustomer().ToList();
        }
        [HttpGet("{id}")]
        public Customers CustomerById(int id)
        {
            return _custRepository.GetCustomerById(id);
        }
        [HttpPost]
        public Customers CreateCustomers([FromBody] Customers _customers)
        {
            return _custRepository.AddCustomer(_customers);
        }
        [HttpPatch]
        public Customers Update([FromBody] Customers _customers)
        {
            return _custRepository.UpdateCustomer(_customers);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _custRepository.DeleteCustomer(id);
        }
    }
}
