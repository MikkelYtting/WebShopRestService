using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShopRestService.Data;
using WebShopRestService.DTO;
using WebShopRestService.Interfaces;
using WebShopRestService.Models;

namespace WebShopRestService.Controllers
{
    [Route("sql/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static IProductsRepository _manager;
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(MyDbContext context, IMapper mapper)
        {
            _manager = new ProductsRepository(context);
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
          var products = await _context.Products.ToListAsync();
          var results = _mapper.Map<IList<ProductDTO>>(products);
          return Ok(results);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
          var product = await _context.Products.FindAsync(id);
          var result = _mapper.Map<ProductDTO>(product);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, [FromBody] ProductDTO updates)
        {
            if (id != updates.ProductId)
            {
                return BadRequest();
            }
            Product product = _context.Products.Find(id);
            product.Name = updates.Name;
            product.Description = updates.Description;
            product.Img = updates.Img;
            product.Price = updates.Price;
            product.StockQuantity = updates.StockQuantity;
            product.CategoryId = updates.CategoryId;
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody]Product product)
        {
          if (_context.Products == null)
          {
              return Problem("Entity set 'MyDbContext.Products'  is null.");
          }
          _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
