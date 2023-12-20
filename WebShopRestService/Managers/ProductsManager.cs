
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopRestService.Interfaces; // Ensure to include the namespace for IProductsRepository
using WebShopRestService.Models;

namespace WebShopRestService.Managers
{
    public class ProductsManager
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsManager(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productsRepository.GetAllProductsAsync();
        }

        public async Task<Product> Get(int id)
        {
            return await _productsRepository.GetProductByIdAsync(id);
        }

        public async Task Update(int id, Product product)
        {
            await _productsRepository.UpdateProductAsync(product);
        }

        public async Task<Product> Create(Product product)
        {
            await _productsRepository.AddProductAsync(product);
            return product; // Assuming the repository handles SaveChangesAsync and returns the added entity
        }

        public async Task Delete(int id)
        {
            var product = await _productsRepository.GetProductByIdAsync(id);
            if (product != null)
            {
                await _productsRepository.DeleteProductAsync(product.ProductId); // Use the correct property name
            }
        }

    }
}
