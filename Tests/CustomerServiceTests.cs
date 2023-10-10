using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ProvaPub.Models.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using System;

public class CustomerServiceTests
{
    // LEIA POR FAVOR!!
    // To com problema pra fazer ele Rodar dentro do ProvaPub, mas em teoria seria isso, Só não esta executando o teste, como a data é hoje, esta aqui a ideia da solução, mas estou investigando e vou tentar resolver antes de ser avaliado.
    // LEIA POR FAVOR!!

    [Fact]
    public async Task CanPurchase_CustomerNotFound_ReturnsFalse()
    {
        // Arrange
        var customerId = 1;
        var purchaseValue = 10;
        var dbContext = new Mock<TestDbContext>();
        var customerRepository = new Mock<ICustomerRepository>();
        var orderRepository = new Mock<IOrderRepository>();
        customerRepository.Setup(repo => repo.GetCustomer(customerId)).ReturnsAsync(new Customer());
        var service = new CustomerService(customerRepository.Object, orderRepository.Object, dbContext.Object);

        // Act
        var result = await Assert.ThrowsAsync<InvalidOperationException>(async () =>await service.CanPurchase(customerId, purchaseValue));

        // Assert
        Assert.Equal($"Customer Id {customerId} does not exist", result.Message);
    }

    [Fact]
    public async Task CanPurchase_OrdersInLastMonth_ReturnsFalse()
    {
        // Arrange
        var customerId = 1;
        var purchaseValue = 10;
        var dbContext = new Mock<TestDbContext>();
        var customerRepository = new Mock<ICustomerRepository>();
        var orderRepository = new Mock<IOrderRepository>();
        customerRepository.Setup(repo => repo.GetCustomer(customerId)).ReturnsAsync(new Customer());
        customerRepository.Setup(repo => repo.Havepurchasedbefore(customerId)).Returns(true);
        var service = new CustomerService(customerRepository.Object, orderRepository.Object, dbContext.Object);

        // Act
        var result = await service.CanPurchase(customerId, purchaseValue);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task CanPurchase_FirstPurchaseOver100_ReturnsFalse()
    {
        // Arrange
        var customerId = 1;
        var purchaseValue = 101;
        var dbContext = new Mock<TestDbContext>();
        var customerRepository = new Mock<ICustomerRepository>();
        var orderRepository = new Mock<IOrderRepository>();
        customerRepository.Setup(repo => repo.GetCustomer(customerId)).ReturnsAsync(new Customer());
        orderRepository.Setup(repo => repo.GetOrdersInMonth(customerId, DateTime.Now)).Returns(0);
        customerRepository.Setup(repo => repo.Havepurchasedbefore(customerId)).Returns(false);
        var service = new CustomerService(customerRepository.Object, orderRepository.Object, dbContext.Object);

        // Act
        var result = await service.CanPurchase(customerId, purchaseValue);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task CanPurchase_ValidPurchase_ReturnsTrue()
    {
        // Arrange
        var customerId = 1;
        var purchaseValue = 10;
        var dbContext = new Mock<TestDbContext>();
        var customerRepository = new Mock<ICustomerRepository>();
        var orderRepository = new Mock<IOrderRepository>();
        customerRepository.Setup(repo => repo.GetCustomer(customerId)).ReturnsAsync(new Customer());
        orderRepository.Setup(repo => repo.GetOrdersInMonth(customerId, DateTime.Now)).Returns(0);
        customerRepository.Setup(repo => repo.Havepurchasedbefore(customerId)).Returns(true);
        var service = new CustomerService(customerRepository.Object, orderRepository.Object, dbContext.Object);

        // Act
        var result = await service.CanPurchase(customerId, purchaseValue);

        // Assert
        Assert.True(result);
    }
}
