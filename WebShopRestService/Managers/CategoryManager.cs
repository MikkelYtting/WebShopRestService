using System.Collections.Generic;
using WebShopRestService.Interfaces;
using WebShopRestService.Models;

namespace WebShopRestService.Managers
{
    public class CategoryManager
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _categoryRepository.GetCategoryById(categoryId);
        }

        public void AddCategory(Category category)
        {
            // Additional business logic can be added here before adding the category
            _categoryRepository.AddCategory(category);
        }

        public void UpdateCategory(Category category)
        {
            // Additional business logic can be added here before updating the category
            _categoryRepository.UpdateCategory(category);
        }

        public void DeleteCategory(int categoryId)
        {
            // Additional business logic can be added here before deleting the category
            _categoryRepository.DeleteCategory(categoryId);
        }
        // Your manager class acts as a layer of abstraction over your repository.
        // It encapsulates how data operations are handled and provides a clear interface to the rest of your application.
        // This means if you ever need to add business logic or change how data is handled,
        // you can do so in the manager class without affecting other parts of your application.

    }
}