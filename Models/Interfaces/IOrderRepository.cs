namespace ProvaPub.Models.Interfaces
{
    public interface IOrderRepository
    {
        int GetOrdersInMonth(int customerId, DateTime baseDate);
    }
}
