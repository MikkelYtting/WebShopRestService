using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopRestService.Data;
using WebShopRestService.Interfaces;
using WebShopRestService.Models;

public class OrderItemsRepository : IOrderItemsRepository
{
    private readonly MyDbContext _context;

    public OrderItemsRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
    {
        return await _context.OrderItems.ToListAsync();
    }

    public async Task<OrderItem> GetOrderItemByIdAsync(int orderItemId)
    {
        return await _context.OrderItems.FindAsync(orderItemId);
    }

    public async Task AddOrderItemAsync(OrderItem orderItem)
    {
        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOrderItemAsync(OrderItem orderItem)
    {
        _context.Entry(orderItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderItemAsync(int orderItemId)
    {
        var orderItem = await _context.OrderItems.FindAsync(orderItemId);
        if (orderItem != null)
        {
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}