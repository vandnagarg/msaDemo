using OrdersAPI.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersAPI.Services
{
    public class OrderManagement
    {
        public static void DeliveryStatusUpdate(Common.OrderDetails orderDetails)
        {
            if(OrderDb.orders.Where(o=>o.id == orderDetails.OrderId && o.userID == orderDetails.UserId).FirstOrDefault() != null)
            OrderDb.orders.Where(o => o.id == orderDetails.OrderId && o.userID == orderDetails.UserId).FirstOrDefault().isDeliveryDone = orderDetails.IsDeliverySuccessful;
        }
    }
}
