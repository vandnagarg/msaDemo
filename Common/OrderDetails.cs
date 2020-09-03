using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common
{
    public class OrderDetails
    {
        public int id { get; set; }
        public string UserId { get; set; }
        public int OrderId { get; set; }
        public bool IsPaymentSuccessful { get; set; }
        public bool IsDeliverySuccessful { get; set; }
    }
}

