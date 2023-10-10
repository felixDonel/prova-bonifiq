namespace ProvaPub.Models.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomer(int customerId);
        bool Havepurchasedbefore(int customerId);
    }
}
