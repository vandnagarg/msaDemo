using PaymentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI
{
    public class PaymentDb
    {
        public static List<PaymentModel> paymentList = new List<PaymentModel>();

        public PaymentDb()
        {
            //paymentList = new List<PaymentModel>();
        }
    }
}
