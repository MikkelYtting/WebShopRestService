using System;
using System.Threading.Tasks;
using WebShopRestService.Interfaces;
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

        // We asynchronously retrieve the product by its ID and then compare the price in the OrderItem against the price of the product.
        // If they don't match, an exception is thrown, indicating a validation failure.
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
            // For example, adding the validated order item to the repository
            // await _orderItemRepository.AddOrderItemAsync(orderItem);
        }
    }
}