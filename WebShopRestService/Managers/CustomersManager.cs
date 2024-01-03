
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopRestService.Interfaces; // Ensure to include the namespace for ICustomersRepositoryfwffwafa
using WebShopRestService.Models;

namespace WebShopRestService.Managers
{
    public class CustomersManager
    {
        private readonly ICustomersRepository _customersRepository;

        public CustomersManager(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _customersRepository.GetAllCustomersAsync();
        }

        public async Task<Customer> Get(int id)
        {
            return await _customersRepository.GetCustomerByIdAsync(id);
        }

        public async Task Update(int id, Customer customer)
        {
            await _customersRepository.UpdateCustomerAsync(customer);
        }

        public async Task<Customer> Create(Customer customer)
        {
            await _customersRepository.AddCustomerAsync(customer);
            return customer; // Assuming the repository handles SaveChangesAsync and returns the added entity
        }

        public async Task Delete(int id)
        {
            var customer = await _customersRepository.GetCustomerByIdAsync(id);
            if (customer != null)
            {
                await _customersRepository.DeleteCustomerAsync(customer.CustomerId); // Use the correct property name
            }
        }
    }
}
