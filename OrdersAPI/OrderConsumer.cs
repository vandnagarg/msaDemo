using MassTransit;
using Nancy.Json;
using OrdersAPI.Models;
using OrdersAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersAPI
{
    [Serializable]

    public class OrderConsumer : IConsumer<Common.OrderDetails>
    {
        public static string received;

        public async Task Consume(ConsumeContext<Common.OrderDetails> context)
        {
            var receivedmessage = ((MassTransit.Context.ConsumeContextScope<Common.OrderDetails>)context).Message;
            OrderManagement.DeliveryStatusUpdate(receivedmessage);
        }
    }
}
