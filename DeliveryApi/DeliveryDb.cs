using DeliveryApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApi
{
    public class DeliveryDb
    {
        public static List<DeliveryModel> deliveryList;

        public DeliveryDb()
        {
            deliveryList = new List<DeliveryModel>();
        }
    }
}
