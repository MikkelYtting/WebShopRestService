using System;
using System.Linq;
using System.Threading.Tasks;
using WebShopRestService.Interfaces;
using WebShopRestService.Models;

namespace WebShopRestService.Managers
{
    public class OrderTableManager : IOrderTableManager
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IOrderTableRepository _orderTableRepository; // Using IOrderTableRepository
        private readonly IProductRepository _productRepository;

        public OrderTableManager(ICustomerRepository customerRepository,
                                 IAddressRepository addressRepository,
                                 IOrderTableRepository orderTableRepository, // Parameter updated
                                 IProductRepository productRepository)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _orderTableRepository = orderTableRepository;
            _productRepository = productRepository;
        }

        public async Task ValidateAndAddOrderAsync(OrderTable order)
        {
            ValidateCustomerExists(order.CustomerId);
            ValidateAddressExists(order.DeliveryAddressId);
            ValidateOrderDate(order.OrderDate);
            ValidateDuplicateOrder(order);
            ValidateOrderItems(order);

            decimal calculatedTotal = CalculateOrderTotal(order.OrderItems);
            if (order.TotalAmount != calculatedTotal)
            {
                throw new InvalidOperationException("Total amount does not match the sum of order items.");
            }

            await _orderTableRepository.AddOrderAsync(order); // Async call
        }

        private void ValidateCustomerExists(int customerId)
        {
            if (!_customerRepository.Exists(customerId))
            {
                throw new InvalidOperationException("Customer does not exist.");
            }
        }

        private void ValidateAddressExists(int addressId)
        {
            if (!_addressRepository.Exists(addressId))
            {
                throw new InvalidOperationException("Delivery address does not exist.");
            }
        }

        private void ValidateOrderDate(DateTime orderDate)
        {
            if (orderDate > DateTime.Now)
            {
                throw new InvalidOperationException("Order date cannot be in the future.");
            }
        }

        private void ValidateDuplicateOrder(OrderTable order)
        {
            // Assuming 'FindSimilarOrder' method is implemented in IOrderTableRepository
            var existingOrder = _orderTableRepository.FindSimilarOrder(order);
            if (existingOrder != null)
            {
                throw new InvalidOperationException("A similar order already exists.");
            }
        }

        private void ValidateOrderItems(OrderTable order)
        {
            if (!order.OrderItems.Any())
            {
                throw new InvalidOperationException("Order must contain at least one item.");
            }

            foreach (var item in order.OrderItems)
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

        private decimal CalculateOrderTotal(IEnumerable<OrderItem> orderItems)
        {
            return orderItems.Sum(item => item.Quantity * item.Price);
        }
    }
}
