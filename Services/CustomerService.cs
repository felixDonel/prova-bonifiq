using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Models.Interfaces;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class CustomerService : BaseService<Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        public CustomerService(ICustomerRepository customerRepository, IOrderRepository orderRepository, TestDbContext ctx) : base(ctx){
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            await Validate(customerId, purchaseValue);

            if (!CanPurchaseInThisMonth(customerId))
                return false;
            if (_customerRepository.Havepurchasedbefore(customerId) && purchaseValue > 100)
                return false;

            return true;
        }

        private async Task Validate(int customerId, decimal purchaseValue)
        {
            if (customerId <= 0)
                throw new ArgumentOutOfRangeException(nameof(customerId));

            if (purchaseValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            var customer = await _customerRepository.GetCustomer(customerId);
            if (customer == null)
                throw new InvalidOperationException($"Customer Id {customerId} does not exist");

        }
        private bool CanPurchaseInThisMonth(int customerId)
        {
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = _orderRepository.GetOrdersInMonth(customerId, baseDate);
            return ordersInThisMonth == 0;
        }
    }
}
