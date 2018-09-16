using System.Collections.Generic;
using AutoFixture;
using ExampleApp.Orders.Business.Exceptions;
using ExampleApp.Orders.Models;
using ExampleApp.Orders.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ExampleApp.Orders.UnitTests.Business
{
    [TestClass]
    public class OrdersTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void WhenOrderCreate_ShouldCreateWithNewIdAndCreateDate()
        {
            // ARRANGE
            var newOrder = _fixture.Create<OrderCreate>();
            var mockDb = new Mock<IOrdersDb>();
            var orders = new Orders.Business.Orders(mockDb.Object);

            // ACT
            var response = orders.CreateOrder(newOrder);

            // ASSERT
            Assert.IsNotNull(response.Id);
            Assert.IsNotNull(response.CreateDateTime);
        }

        [TestMethod]
        public void WhenCustomerIsNotSpecified_ShouldThrowCustomerCanNotBeNullException()
        {
            // ARRANGE
            var newOrder = _fixture
                .Build<OrderCreate>()
                .Without(o => o.Customer)
                .Create();

            var mockDb = new Mock<IOrdersDb>();
            var orders = new Orders.Business.Orders(mockDb.Object);

            // ASSERT
            // ACT
            Assert.ThrowsException<CustomerCanNotBeNullException>(() => orders.CreateOrder(newOrder));
        }

        [TestMethod]
        public void WhenOrderItemsIsNotSpecified_ShouldThrowCustomerCanNotBeNullException()
        {
            // ARRANGE
            var newOrder = _fixture
                .Build<OrderCreate>()
                .Without(o => o.OrderItems)
                .Create();

            var mockDb = new Mock<IOrdersDb>();
            var orders = new Orders.Business.Orders(mockDb.Object);

            // ASSERT
            // ACT
            Assert.ThrowsException<MustContainOrderItemException>(() => orders.CreateOrder(newOrder));
        }

        [TestMethod]
        public void WhenOrderItemsHasNoItems_ShouldThrowCustomerCanNotBeNullException()
        {
            // ARRANGE
            var newOrder = _fixture
                .Build<OrderCreate>()
                .With(o => o.OrderItems, new List<OrderItem>())
                .Create();

            var mockDb = new Mock<IOrdersDb>();
            var orders = new Orders.Business.Orders(mockDb.Object);

            // ASSERT
            // ACT
            Assert.ThrowsException<MustContainOrderItemException>(() => orders.CreateOrder(newOrder));
        }
    }
}
