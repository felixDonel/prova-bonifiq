using ProvaPub.Models;
using System.Linq.Expressions;

namespace ProvaPub.Services
{
    public class OrderService
    {
        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
        {
            switch (paymentMethod)
            {
                case "pix":
                    return await new PixPayment().Paying(paymentValue, customerId);
                case "creditcard":
                    return await new CreditCardPayment().Paying(paymentValue, customerId);
                case "paypal":
                    return await new PaypalPayment().Paying(paymentValue, customerId);
                default:
                    throw new ArgumentOutOfRangeException(nameof(paymentMethod), $"{paymentMethod} payment method not found");
            }
        }
    }
}
