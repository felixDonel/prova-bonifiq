﻿using ProvaPub.Models.Interfaces;

namespace ProvaPub.Models
{
    public class PixPayment : IPayment
    {
        public Task<Order> Paying(decimal paymentValue, int customerId)
        {
            return Task.FromResult(new Order()
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                Value = paymentValue
            });
        }
    }
}
