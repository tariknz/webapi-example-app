using ExampleApp.Orders.Models;

namespace ExampleApp.Orders.Business
{
    public interface IOrders
    {
        Order CreateOrder(OrderCreate newOrder);
    }
}