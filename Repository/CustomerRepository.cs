using Bogus;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Models.Interfaces;

namespace ProvaPub.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        TestDbContext _ctx;

        public CustomerRepository(TestDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Customer> GetCustomer(int customerId)
        {
            return await _ctx.Customers.Where(c => c.Id == customerId).FirstOrDefaultAsync();
        }

        public bool Havepurchasedbefore(int customerId)
        {
            return _ctx.Customers.Where(s => s.Id == customerId && s.Orders.Any()).Any();
        }
    }
}
