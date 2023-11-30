using WebShopRestService.Interfaces;
using WebShopRestService.Models;

namespace WebShopRestService.Managers
{
    public class OrderItemManager
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemManager(IProductRepository productRepository, IOrderItemRepository orderItemRepository)
        {
            _productRepository = productRepository;
            _orderItemRepository = orderItemRepository;
        }

        //In this method, you retrieve the product by its ID and then compare the price in the OrderItem against the price of the product.
        //If they don't match, an exception is thrown, indicating a validation failure.
        public void ValidateAndAddOrderItem(OrderItem orderItem)
        {
            var product = _productRepository.GetProductById(orderItem.ProductId);
            if (product == null)
            {
                throw new InvalidOperationException("Product does not exist.");
            }

            if (orderItem.Price != product.Price)
            {
                throw new InvalidOperationException("The price of the order item does not match the current product price.");
            }

            // Additional business logic here
            _orderItemRepository.AddOrderItem(orderItem);
        }
    }

}
