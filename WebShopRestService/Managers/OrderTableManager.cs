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
    public class OrderTableManager : IOrderTableManager
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IOrderTableRepository _orderTableRepository;
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Constructs an OrderTableManager with required repositories.
        /// </summary>
        /// <param name="customerRepository">Repository for customer operations.</param>
        /// <param name="addressRepository">Repository for address operations.</param>
        /// <param name="orderTableRepository">Repository for order table operations.</param>
        /// <param name="productRepository">Repository for product operations.</param>
        public OrderTableManager(ICustomerRepository customerRepository,
                                 IAddressRepository addressRepository,
                                 IOrderTableRepository orderTableRepository,
                                 IProductRepository productRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _orderTableRepository = orderTableRepository ?? throw new ArgumentNullException(nameof(orderTableRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        /// <summary>
        /// Validates the given order and adds it to the repository if validation is successful.
        /// </summary>
        /// <param name="order">The order to validate and add.</param>
        public async Task ValidateAndAddOrderAsync(OrderTable order)
        {
            ValidateCustomerExists(order.CustomerId);
            ValidateAddressExists(order.DeliveryAddressId);
            ValidateOrderDate(order.OrderDate);
            await ValidateDuplicateOrderAsync(order);
            ValidateOrderItems(order.OrderItems);

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
        private void ValidateCustomerExists(int customerId)
        {
            if (!_customerRepository.Exists(customerId))
            {
                throw new InvalidOperationException("Customer does not exist.");
            }
        }

        /// <summary>
        /// Checks if an address with the given ID exists.
        /// </summary>
        /// <param name="addressId">The address ID to check.</param>
        private void ValidateAddressExists(int addressId)
        {
            if (!_addressRepository.Exists(addressId))
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
        private void ValidateOrderItems(IEnumerable<OrderItem> orderItems)
        {
            if (!orderItems.Any())
            {
                throw new InvalidOperationException("Order must contain at least one item.");
            }

            foreach (var item in orderItems)
            {
                if (!_productRepository.Exists(item.ProductId))
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
        /// Compares two sets of order items to determine if they are identical.
        /// </summary>
        /// <param name="items1">First set of order items to compare.</param>
        /// <param name="items2">Second set of order items to compare.</param>
        /// <returns>true if the order items are identical; otherwise, false.</returns>
        private bool DoOrderItemsMatch(IEnumerable<OrderItem> items1, IEnumerable<OrderItem> items2)
        {
            var itemSet1 = items1.OrderBy(i => i.ProductId).ThenBy(i => i.Quantity);
            var itemSet2 = items2.OrderBy(i => i.ProductId).ThenBy(i => i.Quantity);

            return itemSet1.SequenceEqual(itemSet2, new OrderItemComparer());
        }

        /// <summary>
        /// A custom comparer for comparing order items.
        /// </summary>
        private class OrderItemComparer : IEqualityComparer<OrderItem>
        {
            public bool Equals(OrderItem x, OrderItem y)
            {
                return x.ProductId == y.ProductId && x.Quantity == y.Quantity;
            }

            public int GetHashCode(OrderItem obj)
            {
                unchecked // Overflow is fine, just wrap
                {
                    int hash = 17;
                    hash = hash * 23 + obj.ProductId.GetHashCode();
                    hash = hash * 23 + obj.Quantity.GetHashCode();
                    return hash;
                }
            }
        }
    }
}
