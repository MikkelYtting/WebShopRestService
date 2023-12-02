using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopRestService.Interfaces;
using WebShopRestService.Models;

namespace WebShopRestService.Managers
{
    /// <summary>
    /// Manages business logic operations for order tables.
    /// </summary>
    public class OrderTablesManager
    {
        private readonly ICustomersRepository _customerRepository;
        private readonly IAddressesRepository _addressRepository;
        private readonly IOrderTablesRepository _orderTableRepository;
        private readonly IProductsRepository _productRepository;

        /// <summary>
        /// Constructs an OrderTableManager with required repositories.
        /// </summary>
        /// <param name="customerRepository">Repository for customer operations.</param>
        /// <param name="addressRepository">Repository for address operations.</param>
        /// <param name="orderTableRepository">Repository for order table operations.</param>
        /// <param name="productRepository">Repository for product operations.</param>
        public OrderTablesManager(ICustomersRepository customerRepository,
                                 IAddressesRepository addressRepository,
                                 IOrderTablesRepository orderTableRepository,
                                 IProductsRepository productRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _orderTableRepository = orderTableRepository ?? throw new ArgumentNullException(nameof(orderTableRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }


        public async Task<IEnumerable<OrderTable>> GetAllOrdersAsync()
        {
            // You can add any business logic here if necessary
            return await _orderTableRepository.GetAllOrdersAsync();
        }

        public async Task<OrderTable> GetOrderByIdAsync(int orderId)
        {
            // Add any pre-retrieval logic here
            var order = await _orderTableRepository.GetOrderByIdAsync(orderId);
            // Add any post-retrieval logic here
            return order;
        }

        public async Task UpdateOrderAsync(OrderTable order)
        {
            // Add any validation or business logic before updating
            await _orderTableRepository.UpdateOrderAsync(order);
            // Add any logic after updating, if necessary
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            // Add any logic needed before deletion
            var order = await _orderTableRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new InvalidOperationException("Order not found.");
            }
            await _orderTableRepository.DeleteOrderAsync(order);
            // Add any logic needed after deletion
        }

        /// <summary>
        /// Validates the given order and adds it to the repository if validation is successful.
        /// </summary>
        /// <param name="order">The order to validate and add.</param>
        public async Task ValidateAndAddOrderAsync(OrderTable order)
        {
            await ValidateCustomerExists(order.CustomerId);
            await ValidateAddressExists(order.DeliveryAddressId);
            ValidateOrderDate(order.OrderDate);
            await ValidateDuplicateOrderAsync(order);
            await ValidateOrderItems(order.OrderItems);

            decimal calculatedTotal = CalculateOrderTotal(order.OrderItems);
            if (order.TotalAmount != calculatedTotal)
            {
                throw new InvalidOperationException("Total amount does not match the sum of order items.");
            }

            await _orderTableRepository.AddOrderAsync(order);
        }


        /// <summary>
        /// Checks if a customer with the given ID exists.
        /// </summary>
        /// <param name="customerId">The customer's ID to check.</param>
        private async Task ValidateCustomerExists(int customerId)
        {
            bool exists = await _customerRepository.Exists(customerId);
            if (!exists) // This line checks if exists is false. The ! operator is a logical NOT operator, which inverts the value of exists
            {
                throw new InvalidOperationException("Customer does not exist.");
            }
        }

        /// <summary>
        /// Checks if an address with the given ID exists.
        /// </summary>
        /// <param name="addressId">The address ID to check.</param>
        private async Task ValidateAddressExists(int addressId)
        {
            bool addressExists = await _addressRepository.AddressExistsAsync(addressId);
            if (!addressExists)
            {
                throw new InvalidOperationException("Delivery address does not exist.");
            }
        }

        /// <summary>
        /// Ensures that the order date is not in the future.
        /// </summary>
        /// <param name="orderDate">The date of the order to validate.</param>
        private void ValidateOrderDate(DateTime orderDate)
        {
            if (orderDate > DateTime.Now)
            {
                throw new InvalidOperationException("Order date cannot be in the future.");
            }
        }

        /// <summary>
        /// Validates that there are no duplicate orders within a 15-minute window with the same customer and items.
        /// </summary>
        /// <param name="order">The order to check for duplicates.</param>
        private async Task ValidateDuplicateOrderAsync(OrderTable order)
        {
            var startTime = order.OrderDate.AddMinutes(-15);
            var endTime = order.OrderDate.AddMinutes(15);

            var potentialDuplicateOrders = await _orderTableRepository.GetOrdersByCustomerAndDateAsync(order.CustomerId, startTime, endTime);

            foreach (var existingOrder in potentialDuplicateOrders)
            {
                if (existingOrder.OrderId != order.OrderId && DoOrderItemsMatch(existingOrder.OrderItems, order.OrderItems))
                {
                    throw new InvalidOperationException("A similar order already exists.");
                }
            }
        }

        /// <summary>
        /// Validates the list of order items for an order.
        /// </summary>
        /// <param name="orderItems">The order items to validate.</param>
        private async Task ValidateOrderItems(IEnumerable<OrderItem> orderItems)
        {
            if (!orderItems.Any())
            {
                throw new InvalidOperationException("Order must contain at least one item.");
            }

            foreach (var item in orderItems)
            {
                // Await the asynchronous operation and get the boolean result
                bool productExists = await _productRepository.ProductExistsAsync(item.ProductId);
                if (!productExists)  
                {
                    throw new InvalidOperationException($"Product with ID {item.ProductId} does not exist.");
                }

                if (item.Quantity <= 0)
                {
                    throw new InvalidOperationException("Order item quantity must be positive.");
                }
            }
        }


        /// <summary>
        /// Calculates the total amount of an order based on its items.
        /// </summary>
        /// <param name="orderItems">The items of the order.</param>
        /// <returns>The total amount.</returns>
        private decimal CalculateOrderTotal(IEnumerable<OrderItem> orderItems)
        {
            return orderItems.Sum(item => item.Quantity * item.Price);
        }

     
        /// <summary>
        /// Determines if two sequences of order items are identical by comparing their ProductId and Quantity.
        /// </summary>
        /// <param name="items1">The first sequence of order items to compare.</param>
        /// <param name="items2">The second sequence of order items to compare.</param>
        /// <returns>True if both sequences contain the same items with the same quantities; otherwise, false.</returns>
        private bool DoOrderItemsMatch(IEnumerable<OrderItem> items1, IEnumerable<OrderItem> items2)
        {
            // Sort both sequences to ensure items are in the same order for comparison
            var itemSet1 = items1.OrderBy(i => i.ProductId).ThenBy(i => i.Quantity);
            var itemSet2 = items2.OrderBy(i => i.ProductId).ThenBy(i => i.Quantity);

            // Use a custom comparer to determine if both sequences are equal
            return itemSet1.SequenceEqual(itemSet2, new OrderItemComparer());
        }

        /// <summary>
        /// A comparer to check if two order items are equal based on ProductId and Quantity.
        /// </summary>
        private class OrderItemComparer : IEqualityComparer<OrderItem>
        {
            // Determines if two order items are equal by comparing ProductId and Quantity
            public bool Equals(OrderItem x, OrderItem y)
            {
                return x.ProductId == y.ProductId && x.Quantity == y.Quantity;
            }

            // Generates a hash code based on ProductId and Quantity
            /// <summary>
            /// Produces a hash code for an order item based on its ProductId and Quantity.
            /// This hash code is used by hash-based collections to quickly organize and access objects.
            /// </summary>
            /// <param name="obj">The order item for which to generate a hash code.</param>
            /// <returns>An integer hash code representing the order item.</returns>
            public int GetHashCode(OrderItem obj)
            {
                unchecked // Allows overflow without throwing an exception, which is fine for hash codes
                {
                    int hash = 17;
                    // Multiply the hash by a prime number (23) and add the hash of the ProductId.
                    hash = hash * 23 + obj.ProductId.GetHashCode();
                    // Multiply the hash by the same prime number (23) and add the hash of the Quantity.
                    hash = hash * 23 + obj.Quantity.GetHashCode();
                    return hash;
                }
            }

        }

    }
}
