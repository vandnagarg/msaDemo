using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        readonly ILogger<AdminController> log;
        public AdminController(ILogger<AdminController> _log)
        {
            log = _log;
        }
        [HttpGet]
        [ActionName("get_Pro")]
        public void Get()
        {

        }

        [HttpPost]
        [ActionName("Add_Products")]
        public IEnumerable<Product> Add_Products([FromBody]Product product)
        {
            try
            {
                log.LogInformation("Admin has added a product" + product.Name);
                ProductDb.products.Add(new Product(ProductDb.products.Count + 1, product.Name, product.Price));
                return ProductDb.products.ToList();
            }
            catch (Exception ex)
            {
                log.LogInformation("Admin cant add product" + product.Name);
                throw ex;
            }
        }

        [HttpDelete]
        [ActionName("Remove_Products")]
        public IEnumerable<Product> Remove_Products([FromBody]Product product)
        {
            try
            {
                log.LogInformation("Admin has removed a product" + product.Name);
                var p = ProductDb.products.Where(xp => xp.id == product.id).FirstOrDefault();
                if (p != null)
                    ProductDb.products.Remove(p);
                return ProductDb.products.ToList();
            }
            catch (Exception ex)
            {
                log.LogInformation("Admin cant remove a product" + product.Name);
                throw ex;
            }

        }


    }
}
