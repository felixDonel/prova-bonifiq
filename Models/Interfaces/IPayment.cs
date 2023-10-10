namespace ProvaPub.Models.Interfaces
{
    public interface IPayment
    {
        Task<Order> Paying(decimal paymentValue, int customerId);
    }
}
