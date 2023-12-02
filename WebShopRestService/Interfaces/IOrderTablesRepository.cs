using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopRestService.Models;

namespace WebShopRestService.Interfaces
{
    public interface IOrderTablesRepository
    {
        Task<IEnumerable<OrderTable>> GetAllOrdersAsync();
        Task<OrderTable> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(OrderTable order);
        Task UpdateOrderAsync(OrderTable order);
        Task DeleteOrderAsync(int orderId);

        /// <summary>
        /// Finds an order that is similar to the provided order.
        /// </summary>
        /// <param name="order">The order to compare with existing orders.</param>
        /// <returns>A similar order if found, otherwise null.</returns>
        Task<IEnumerable<OrderTable>> GetOrdersByCustomerAndDateAsync(int customerId, DateTime start, DateTime end);
        Task DeleteOrderAsync(OrderTable order);
    }
}