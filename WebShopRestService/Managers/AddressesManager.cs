using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopRestService.Interfaces; // Ensure this is included for IAddressesRepository
using WebShopRestService.Models;

namespace WebShopRestService.Managers
{
    public class AddressesManager
    {
        private readonly IAddressesRepository _addressesRepository;

        public AddressesManager(IAddressesRepository addressesRepository)
        {
            _addressesRepository = addressesRepository;
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            return await _addressesRepository.GetAllAddressesAsync();
        }

        public async Task<Address> Get(int id)
        {
            return await _addressesRepository.GetAddressByIdAsync(id);
        }

        public async Task Update(int id, Address address)
        {
            await _addressesRepository.UpdateAddressAsync(address);
        }

        public async Task<Address> Create(Address address)
        {
            await _addressesRepository.AddAddressAsync(address);
            // Assuming the repository method does the SaveChangesAsync and returns the added entity
            return address;
        }

        public async Task Delete(int id)
        {
            var address = await _addressesRepository.GetAddressByIdAsync(id);
            if (address != null)
            {
                await _addressesRepository.DeleteAddressAsync(address);
            }
        }
    }
}