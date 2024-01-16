using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShopRestService.Managers;
using WebShopRestService.Models;

namespace WebShopRestService.Controllers
{
    [Route("sql/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly OrderItemsManager _orderItemsManager;

        public OrderItemsController(OrderItemsManager orderItemsManager)
        {
            _orderItemsManager = orderItemsManager;
        }

        // GET: api/OrderItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems()
        {
            var orderItems = await _orderItemsManager.GetAllOrderItemsAsync();

            if (orderItems == null)
            {
                return NotFound();
            }

            return Ok(orderItems);
        }

        // GET: api/OrderItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetOrderItem(int id)
        {
            var orderItem = await _orderItemsManager.GetOrderItemByIdAsync(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return orderItem;
        }

        // PUT: api/OrderItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderItem(int id, OrderItem orderItem)
        {
            if (id != orderItem.OrderItemId)
            {
                return BadRequest();
            }

            try
            {
                await _orderItemsManager.UpdateOrderItemAsync(orderItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _orderItemsManager.GetOrderItemByIdAsync(id) != null;
                if (!exists)
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

        // POST: api/OrderItems
        [HttpPost]
        public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItem orderItem)
        {
            try
            {
                await _orderItemsManager.ValidateAndAddOrderItem(orderItem);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetOrderItem), new { id = orderItem.OrderItemId }, orderItem);
        }

        // DELETE: api/OrderItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            try
            {
                await _orderItemsManager.DeleteOrderItemAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
