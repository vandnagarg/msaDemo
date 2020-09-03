using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Models
{
    public class PaymentModel
    {
        public int id { get; set; }
        public int orderId { get; set; }
        public string userId { get; set; }
        public string PaymentMethod { get; set; }
        public bool Status { get; set; }

        public PaymentModel(int id,int orderId, string userId, string PaymentMethod,bool status)
        {
            this.id = id;
            this.orderId = orderId;
            this.userId = userId;
            this.PaymentMethod = PaymentMethod;
            this.Status = status;
        }
    }
}
