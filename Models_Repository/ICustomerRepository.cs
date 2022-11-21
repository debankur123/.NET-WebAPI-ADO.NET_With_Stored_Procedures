using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models_Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<Customers> GetAllCustomer();
        Customers GetCustomerById(int id);
        Customers AddCustomer(Customers _customers);
        Customers UpdateCustomer(Customers _customers);
        void DeleteCustomer(int id);
    }
}
