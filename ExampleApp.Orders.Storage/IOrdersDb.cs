using ExampleApp.Orders.Models;

namespace ExampleApp.Orders.Storage
{
    public interface IOrdersDb
    {
        void Upsert(Order order);
    }
}
