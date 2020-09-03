using ProductsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI
{
    public class ProductDb
    {
        public static List<Product> products = new List<Product>() { new Product(1, "Phone",20000.00), new Product(2, "Bed",10000.00), new Product(3, "Desk",2000.00) };

        public ProductDb()
        {
        }
    }
}
