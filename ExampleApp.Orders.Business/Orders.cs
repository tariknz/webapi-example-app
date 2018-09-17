using ExampleApp.Orders.Models;
using ExampleApp.Orders.Storage;
using System;
using System.Linq;
using ExampleApp.Orders.Business.Exceptions;

namespace ExampleApp.Orders.Business
{
    /// <summary>
    /// Business logic for Orders
    /// </summary>
    public class Orders : IOrders
    {
        private readonly IOrdersDb _ordersDb;

        public Orders(IOrdersDb ordersDb)
        {
            _ordersDb = ordersDb;
        }

        /// <summary>
        /// Creates a new order and returns the order
        /// </summary>
        /// <param name="newOrder"></param>
        /// <returns></returns>
        /// <exception cref="OrderCreateException">When failed to create in database</exception>
        public Order CreateOrder(OrderCreate newOrder)
        {
            // Some basic validation
            if (newOrder.Customer == null) throw new CustomerCanNotBeNullException();
            if (newOrder.OrderItems == null || !newOrder.OrderItems.Any()) throw new MustContainOrderItemException();

            // There is probably a lot more validations to consider i.e. does the item code exist,
            // each order item should have quantity > 0 and customer details are not null (this only checks for the object)
            // I would use a validation framework like fluent validator or the built-in MVC validators if they are simple static ones
            
            // Create business object from a new order with new ID and Order create date
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CreateDateTime = DateTime.Now,
                OrderItems = newOrder.OrderItems,
                Customer = newOrder.Customer
            };
            
            // Creates in DB
            try
            {
                _ordersDb.Upsert(order);
            }
            catch (Exception)
            {
                // Just basic exception for now
                throw new OrderCreateException();
            }

            return order;
        }
    }
}
