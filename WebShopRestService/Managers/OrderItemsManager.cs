
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopRestService.Interfaces; // Ensure to include the namespace for IOrderItemsRepository
using WebShopRestService.Models;

namespace WebShopRestService.Managers
{
    public class OrderItemsManager
    {
        private readonly IProductsRepository _productRepository;
        private readonly IOrderItemsRepository _orderItemRepository;

        public OrderItemsManager(IProductsRepository productRepository, IOrderItemsRepository orderItemRepository)
        {
            _productRepository = productRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task ValidateAndAddOrderItem(OrderItem orderItem)
        {
            var product = await _productRepository.GetProductByIdAsync(orderItem.ProductId);
            if (product == null)
            {
                throw new InvalidOperationException("Product does not exist.");
            }

            if (orderItem.Price != product.Price)
            {
                throw new InvalidOperationException("The price of the order item does not match the current product price.");
            }

            // Additional business logic here
            await _orderItemRepository.AddOrderItemAsync(orderItem);
        }

        // Create operation
        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            await _orderItemRepository.AddOrderItemAsync(orderItem);
        }

        // Read operation
        public async Task<OrderItem> GetOrderItemByIdAsync(int orderItemId)
        {
            return await _orderItemRepository.GetOrderItemByIdAsync(orderItemId);
        }

        // Update operation
        public async Task UpdateOrderItemAsync(OrderItem orderItem)
        {
            await _orderItemRepository.UpdateOrderItemAsync(orderItem);
        }

        // Delete operation
        public async Task DeleteOrderItemAsync(int orderItemId)
        {
            await _orderItemRepository.DeleteOrderItemAsync(orderItemId);
        }

        // Additional method to get all order items
        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        {
            return await _orderItemRepository.GetAllOrderItemsAsync();
        }
    }
}
