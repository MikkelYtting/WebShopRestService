using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopRestService.Models;

namespace WebShopRestService.Interfaces
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int categoryId);
    }
}