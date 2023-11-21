using ProvaPub.Models;
using ProvaPub.Models.Interfaces;
using System.Linq.Expressions;

namespace ProvaPub.Services
{
    public class OrderService
    {
        private readonly PixPayment _pixPayment;
        private readonly CreditCardPayment _CreditCardPayment;
        private readonly PaypalPayment _PaypalPayment;
        public OrderService(PixPayment pixPayment, CreditCardPayment creditCardPayment, PaypalPayment paypalPayment)
        {
            _pixPayment = pixPayment;
            _CreditCardPayment = creditCardPayment;
            _PaypalPayment = paypalPayment;
        }
        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
        {
            IPayment payment;
            switch (paymentMethod)
            {
                case "pix":
                    payment = _pixPayment;
                    break;
                case "creditcard":
                    payment = _CreditCardPayment;
                    break;
                case "paypal":
                    payment = _PaypalPayment;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(paymentMethod), $"{paymentMethod} payment method not found");
            }

            return await payment.Paying(paymentValue, customerId);
        }
    }
}
