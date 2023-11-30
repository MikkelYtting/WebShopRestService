using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopRestService.Models;

namespace WebShopRestService.Interfaces
{
    public interface IOrderTableRepository
    {
        Task<IEnumerable<OrderTable>> GetAllOrdersAsync();
        Task<OrderTable> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(OrderTable order);
        Task UpdateOrderAsync(OrderTable order);
        Task DeleteOrderAsync(int orderId);
        // Additional methods can be added as per business requirements
    }
}