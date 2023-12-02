
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopRestService.Models;

namespace WebShopRestService.Interfaces
{
    public interface ICustomersRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int customerId);
        Task<bool> Exists(int customerId);
    }
}
