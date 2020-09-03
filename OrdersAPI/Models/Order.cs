using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersAPI.Models
{
    public class Order
    {
        public int id { get; set; }
        public string userID { get; set; }
        public int cartId { get; set; }
        public bool isPaymnetSuccess { get; set; }
        public bool isDeliveryDone { get; set; }
        public int totalProducts { get; set; }
        public double totalAmount { get; set; }


        public Order(int id,string userId,int cId,int totalP, double totAmt)
        {
            this.id = id;
            this.userID = userId;
            this.cartId = cId;
            this.isPaymnetSuccess = false;
            this.isDeliveryDone = false;
            this.totalProducts = totalP;
            this.totalAmount = totAmt;

        }
    }
}
