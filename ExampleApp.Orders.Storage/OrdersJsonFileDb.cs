using System.IO;
using System.Reflection;
using ExampleApp.Orders.Models;
using Newtonsoft.Json;

namespace ExampleApp.Orders.Storage
{
    /// <summary>
    /// Storage for orders to store as JSON documents in the file system
    /// </summary>
    public class OrdersJsonFileDb : IOrdersDb
    {
        private readonly string _path;
        private const string JsonFormat = "json";

        public OrdersJsonFileDb()
        {
            _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/orders";

            Directory.CreateDirectory(_path);
        }

        public void Upsert(Order order)
        {
            var id = order.Id;
            var serialized = JsonConvert.SerializeObject(order);

            File.WriteAllText($"{_path}/{id}.{JsonFormat}", serialized);
        }
    }
}
