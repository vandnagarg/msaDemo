﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common
{
    public class DeliveryDetails
    {
        public int id { get; set; }
        public int OrderID { get; set; }
        public bool IsDeliveryDone { get; set; }
    }
}
