using System.Collections.Generic;
using WebShopRestService.Interfaces;
using WebShopRestService.Models;

namespace WebShopRestService.Managers
{
    public class CategoriesManager
    {
        private readonly ICategoriesRepository _categoryRepository;

        public CategoriesManager(ICategoriesRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _categoryRepository.GetCategoryByIdAsync(categoryId);
        }

        public async Task AddCategoryAsync(Category category)
        {
            // Additional business logic can be added here before adding the category
            await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            // Additional business logic can be added here before updating the category
            await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            // Additional business logic can be added here before deleting the category
            await _categoryRepository.DeleteCategoryAsync(categoryId);
        }
        // Your manager class acts as a layer of abstraction over your repository.
        // It encapsulates how data operations are handled and provides a clear interface to the rest of your application.
        // This means if you ever need to add business logic or change how data is handled,
        // you can do so in the manager class without affecting other parts of your application.

    }
}