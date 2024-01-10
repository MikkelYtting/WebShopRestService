using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopRestService.Managers;
using WebShopRestService.Models;

namespace WebShopRestService.Controllers
{
    [Route("sql/[controller]")]
    [ApiController]
    public class OrderTablesController : ControllerBase
    {
        private readonly OrderTablesManager _orderTablesManager;

        public OrderTablesController(OrderTablesManager orderTablesManager)
        {
            _orderTablesManager = orderTablesManager;
        }

        // GET: api/OrderTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderTable>>> GetOrders()
        {
            var orders = await _orderTablesManager.GetAllOrdersAsync();

            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        // GET: api/OrderTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderTable>> GetOrderTable(int id)
        {
            var orderTable = await _orderTablesManager.GetOrderByIdAsync(id);

            if (orderTable == null)
            {
                return NotFound();
            }

            return orderTable;
        }

        // PUT: api/OrderTables/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderTable(int id, OrderTable orderTable)
        {
            if (id != orderTable.OrderId)
            {
                return BadRequest();
            }

            try
            {
                await _orderTablesManager.UpdateOrderAsync(orderTable);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _orderTablesManager.GetOrderByIdAsync(id) != null;
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

        // POST: api/OrderTables
        [HttpPost]
        public async Task<ActionResult<OrderTable>> PostOrderTable(OrderTable orderTable)
        {
            try
            {
                await _orderTablesManager.ValidateAndAddOrderAsync(orderTable);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetOrderTable), new { id = orderTable.OrderId }, orderTable);
        }

        // DELETE: api/OrderTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderTable(int id)
        {
            try
            {
                await _orderTablesManager.DeleteOrderAsync(id);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
