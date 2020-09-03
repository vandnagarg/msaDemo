using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly ILogger<AdminController> log;
        public ProductsController(ILogger<AdminController> _log)
        {
            log = _log;
        }
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            try
            {
                log.LogInformation("Get all the products.");
                return ProductDb.products;
            }
            catch (Exception ex)
            {
                log.LogInformation("Cant Get all the products.");
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            try
            {
                log.LogInformation("Get all the product by ID.");
                return ProductDb.products.Where(x => x.id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.LogInformation("Can't get all the product by ID.");
                throw ex;
            }

        }
        
    }
}
