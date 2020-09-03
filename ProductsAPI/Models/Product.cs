using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.Models
{
    public class Product
    {
        public int id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Product(int id, string value,double price)
        {
            this.id = id;
            this.Name = value;
            this.Price = price;
        }
    }
}
