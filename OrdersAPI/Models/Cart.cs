using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersAPI.Models
{
    public class Cart
    {
        public int id { get; set; }
        public List<int> ProductsIDs { get; set; }
        public string UserId { get; set; }
        public double TotalPrice { get; set; }

        public Cart(int id,int pId,string uid,double totP)
        {
            this.id = id;
            this.ProductsIDs = new List<int>() { pId };
            this.UserId = uid;
            this.TotalPrice = totP;
        }
    }
}
