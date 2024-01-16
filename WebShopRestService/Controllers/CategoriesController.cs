using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopRestService.Managers;
using WebShopRestService.Models;

namespace WebShopRestService.Controllers
{
    [Route("sql/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesManager _categoriesManager;

        public CategoriesController(CategoriesManager categoriesManager)
        {
            _categoriesManager = categoriesManager;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoriesManager.GetAllCategoriesAsync();

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoriesManager.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, [FromBody] CategoryDTO updatedCategory)
        {
            if (id != updatedCategory.CategoryId)
            {
                return BadRequest("Category ID mismatch.");
            }

            var existingCategory = await _categoriesManager.GetCategoryByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            // Update the properties of the existing category
            existingCategory.Name = updatedCategory.Name;
            existingCategory.Description = updatedCategory.Description;

            try
            {
                await _categoriesManager.UpdateCategoryAsync(existingCategory);
            }
            catch (Exception ex) // Catch more specific exception if possible
            {
                // Log the exception details
                // e.g., _logger.LogError(ex, "Error updating category with ID {CategoryId}", id);
                return StatusCode(500, "An error occurred while updating the category.");
            }

            return NoContent(); // You can also return Ok(existingCategory) if you want to include the updated object in the response.
        }


        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            await _categoriesManager.AddCategoryAsync(category);

            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoriesManager.DeleteCategoryAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
