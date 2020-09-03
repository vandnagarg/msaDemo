using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApi.Models
{
    public class DeliveryModel
    {
        public int id { get; set; }
        public int orderId { get; set; }
        public string userId { get; set; }
        public string DeliveredBy { get; set; }
        public bool DeliveryStatus { get; set; }

        public DeliveryModel(int id, int orderId, string userId, string deliveredBy, bool deliveryStatus)
        {
            this.id = id;
            this.orderId = orderId;
            this.userId = userId;
            this.DeliveredBy = deliveredBy;
            this.DeliveryStatus = deliveryStatus;
        }
    }
}
