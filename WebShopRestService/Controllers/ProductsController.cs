using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShopRestService.Data;
using WebShopRestService.DTO;
using WebShopRestService.Managers;
using WebShopRestService.Models;

namespace WebShopRestService.Controllers
{
    [Route("sql/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsManager _productsManager;
        private readonly IMapper _mapper;

        private readonly MyDbContext _spmanager;

        public ProductsController(ProductsManager productsManager, IMapper mapper, MyDbContext ctx)
        {
            _productsManager = productsManager;
            _mapper = mapper;
            _spmanager = ctx;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _productsManager.GetAll();
            if (products == null)
            {
                return NotFound();
            }
            var results = _mapper.Map<IList<ProductDTO>>(products);
            return Ok(results);
        }

        // GET: api/Products/lowQuantity
        [HttpGet("lowQuantity")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetLowProducts()
        {
            var products = _spmanager.Products.FromSqlRaw("Execute dbo.get_products_with_low_quantity").AsEnumerable();

            if (products == null)
            {
                return NotFound();
            }
            var results = _mapper.Map<IList<ProductDTO>>(products);
            return Ok(results);
        }
        // GET: api/Products/sortByCategory
        [HttpGet("sortByCategory")]
        public async Task<ActionResult<IEnumerable<SortProductDTO>>> GetProductsByCategory()
        {
            var products = _spmanager.SortProducts.FromSqlRaw("Execute GetProductsByCategory").AsEnumerable();

            if (products == null)
            {
                return NotFound();
            }
            var results = _mapper.Map<IList<SortProductDTO>>(products);
            return Ok(results);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _productsManager.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<ProductDTO>(product);
            return Ok(result);
        }

        // PUT: api/Products/5
        //[Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, [FromBody] ProductDTO updates)
        {
            if (id != updates.ProductId)
            {
                return BadRequest();
            }

            var productToUpdate = await _productsManager.Get(id);
            if (productToUpdate == null)
            {
                return NotFound();
            }
            productToUpdate.Name = updates.Name;
            productToUpdate.Description = updates.Description;
            productToUpdate.Img = updates.Img;
            productToUpdate.Price = updates.Price;
            productToUpdate.StockQuantity = updates.StockQuantity;
            productToUpdate.CategoryId = updates.CategoryId;

            //_mapper.Map(updates, productToUpdate);
            await _productsManager.Update(id, productToUpdate);

            return Ok(productToUpdate);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct([FromBody] ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var createdProduct = await _productsManager.Create(product);
            var result = _mapper.Map<ProductDTO>(createdProduct);

            return CreatedAtAction(nameof(GetProduct), new { id = result.ProductId }, result);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productsManager.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productsManager.Delete(id);

            return NoContent();
        }
    }
}
