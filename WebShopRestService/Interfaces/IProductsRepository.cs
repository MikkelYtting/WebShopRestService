
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopRestService.Models;

namespace WebShopRestService.Interfaces
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int productId);
        Task<bool> ProductExistsAsync(int productId);
    }
}
