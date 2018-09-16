using AutoFixture;
using ExampleApp.Orders.Business;
using ExampleApp.Orders.Business.Exceptions;
using ExampleApp.Orders.Models;
using ExampleApp.Orders.Service.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ExampleApp.Orders.UnitTests.Service
{
    [TestClass]
    public class OrdersControllerTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void WhenCreatingOrder_ShouldCreateOrderSuccessfully()
        {
            // ARRANGE
            var newOrder = _fixture.Create<OrderCreate>();
            var ordersService = new Mock<IOrders>();
            var controller = new OrdersController(ordersService.Object);

            // ACT
            var result = controller.Post(newOrder) as OkObjectResult;

            // ASSERT
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void WhenThrowsCustomerCanNotBeNullException_ShouldReturnBadRequest()
        {
            // ARRANGE
            var ordersService = new Mock<IOrders>();
            ordersService
                .Setup(o => o.CreateOrder(It.IsAny<OrderCreate>()))
                .Throws<CustomerCanNotBeNullException>();

            var controller = new OrdersController(ordersService.Object);

            // ACT
            var result = controller.Post(_fixture.Create<OrderCreate>()) as BadRequestObjectResult;

            // ASSERT
            Assert.AreEqual(result?.StatusCode, 400, "Response should return 400 Bad Request");
        }

        [TestMethod]
        public void WhenThrowsMustContainOrderItemException_ShouldReturn400()
        {
            // ARRANGE
            var ordersService = new Mock<IOrders>();
            ordersService
                .Setup(o => o.CreateOrder(It.IsAny<OrderCreate>()))
                .Throws<MustContainOrderItemException>();

            var controller = new OrdersController(ordersService.Object);

            // ACT
            var result = controller.Post(_fixture.Create<OrderCreate>()) as BadRequestObjectResult;

            // ASSERT
            Assert.AreEqual(result?.StatusCode, 400, "Response should return 400 Bad Request");
        }


        [TestMethod]
        public void WhenThrowsOrderCreateException_ShouldReturn500()
        {
            // ARRANGE
            var ordersService = new Mock<IOrders>();
            ordersService
                .Setup(o => o.CreateOrder(It.IsAny<OrderCreate>()))
                .Throws<OrderCreateException>();

            var controller = new OrdersController(ordersService.Object);

            // ACT
            var result = controller.Post(_fixture.Create<OrderCreate>()) as ObjectResult;

            // ASSERT
            Assert.AreEqual(result?.StatusCode, 500, "Response should return 500 internal server error");
        }
    }
}
