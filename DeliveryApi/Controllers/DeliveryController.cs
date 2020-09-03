using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryApi.Models;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DeliveryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Delivery")]

    public class DeliveryController : ControllerBase
    {
        private readonly IBusControl _bus;
        private readonly IConfiguration _config;

        readonly ILogger<DeliveryController> _log;

        public DeliveryController(IBusControl bus, IConfiguration config, ILogger<DeliveryController> log)
        {
            _bus = bus;
            _log = log;
            _config = config;
        }

        [HttpPost]
        public async Task<ActionResult> DeliverProduct([FromBody] DeliveryModel deliveryModel)
        {
            try
            {
                //deliver product
                var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
                var userId = identity.Claims.FirstOrDefault(c => c.Type == "userId").Value;
                DeliveryDb.deliveryList.Add(new DeliveryModel(DeliveryDb.deliveryList.Count + 1, deliveryModel.orderId, userId, deliveryModel.DeliveredBy, deliveryModel.DeliveryStatus));

                //change status in order
                Uri uri = new Uri($"rabbitmq://{_config.GetValue<string>("RabbitMQHostName")}/delStatus");
                var endPoint = await _bus.GetSendEndpoint(uri);
                var order = new Common.OrderDetails();
                order.OrderId = deliveryModel.orderId;
                order.UserId = deliveryModel.userId;
                order.IsDeliverySuccessful = deliveryModel.DeliveryStatus;
                await endPoint.Send(order);
                _log.LogInformation("Order has been delivered.");
                return Ok("Order Delivered");
            }
            catch(Exception ex)
            {
                _log.LogInformation("Order can't be delivered.");
                return Ok("Order can't be delivered.");
            }
            
        }
    }
}