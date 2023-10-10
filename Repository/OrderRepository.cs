using Microsoft.EntityFrameworkCore;
using ProvaPub.Models.Interfaces;

namespace ProvaPub.Repository
{
    public class OrderRepository : IOrderRepository
    {
        TestDbContext _ctx;

        public OrderRepository(TestDbContext ctx)
        {
            _ctx = ctx;
        }
        public int GetOrdersInMonth(int customerId, DateTime baseDate)
        {
            return _ctx.Customers.Count(s => s.Id == customerId && s.Orders.Any());
        }
    }
}
