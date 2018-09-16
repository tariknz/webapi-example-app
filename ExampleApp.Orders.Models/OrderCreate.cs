using System.Collections.Generic;

namespace ExampleApp.Orders.Models
{
    /// <summary>
    /// Order create object that does not include system fields (such as ID, and create date)
    /// Ideally this should contain API validation attributes here
    /// </summary>
    public class OrderCreate
    {
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
