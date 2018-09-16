using System;
using System.Collections.Generic;

namespace ExampleApp.Orders.Models
{
    public class Order
    {
        /// <summary>
        /// Order ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Date & time when the order was created
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// Order Items
        /// </summary>
        public IEnumerable<OrderItem> OrderItems { get; set; }
        /// <summary>
        /// Customer details
        /// </summary>
        public Customer Customer { get; set; }
    }
}
