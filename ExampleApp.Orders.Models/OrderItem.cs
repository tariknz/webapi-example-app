namespace ExampleApp.Orders.Models
{
    public class OrderItem
    {
        /// <summary>
        /// Product code for the hardware being ordered
        /// </summary>
        public string HardwareProductCode { get; set; }
        /// <summary>
        /// Quantity specified
        /// </summary>
        public int Quantity { get; set; }
    }
}