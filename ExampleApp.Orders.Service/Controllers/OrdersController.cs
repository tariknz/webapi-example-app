using System;
using System.Net;
using ExampleApp.Orders.Business;
using ExampleApp.Orders.Business.Exceptions;
using ExampleApp.Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ExampleApp.Orders.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrders _orders;

        public OrdersController(IOrders orders)
        {
            _orders = orders;
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(Order))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(string))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, type: typeof(string))]
        public IActionResult Post([FromBody] OrderCreate newOrder)
        {
            try
            {
                return Ok(_orders.CreateOrder(newOrder));
            }
            catch (CustomerCanNotBeNullException)
            {
                return BadRequest("Customer details can not be null");
            }
            catch (MustContainOrderItemException)
            {
                return BadRequest("Must contain at-least one order item");
            }
            catch (OrderCreateException)
            {
                return StatusCode(500, "Order can not be created currently, please try again later");
            }
            catch (Exception)
            {
                return StatusCode(500, "There was an unexpected error, please try again later");
            }
        }
    }
}
