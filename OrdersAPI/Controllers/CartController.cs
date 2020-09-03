using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrdersAPI.Db;
using OrdersAPI.Models;

namespace OrdersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CartController : ControllerBase
    {
        public string userId = String.Empty;
        readonly ILogger<CartController> _log;

        public CartController(ILogger<CartController> log)
        {
            _log = log;
        }
        
        // POST: api/Cart
        [HttpPost]
        public ActionResult Post( int id)
        {
            try
            {
                //get price of the product 
                var accessToken = Request.Headers["Authorization"];
                string _apiUrl = "https://localhost:44338/api/products/" + id;
                var response = Common.HttpClientService.getAsyncMethod(_apiUrl, accessToken.ToString());
                double price = Convert.ToDouble(response.Result.Content.ReadAsStringAsync().Result);
                var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
                userId = identity.Claims.FirstOrDefault(c => c.Type == "userId").Value;
                if (CartDb.cart.Where(x => x.UserId == userId).FirstOrDefault() != null)
                {
                    CartDb.cart.Where(x => x.UserId == userId).FirstOrDefault().ProductsIDs.Add(id);
                    CartDb.cart.Where(x => x.UserId == userId).FirstOrDefault().TotalPrice += price;
                }
                else
                {
                    CartDb.cart.Add(new Cart(1, id, userId, price));
                }
                return Ok("Product has been added to the cart.");
            }
            catch (Exception ex)
            {
                _log.LogInformation("Product cant be added in the cart.");
                throw ex;
            }

        }

        [HttpDelete]
        public ActionResult Delete( int id)
        {
            try
            {
                var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
                userId = identity.Claims.FirstOrDefault(c => c.Type == "userId").Value;
                if (CartDb.cart.Where(x => x.UserId == userId).FirstOrDefault() != null)
                {
                    CartDb.cart.Where(x => x.UserId == userId).FirstOrDefault().ProductsIDs.Remove(id);
                }
                else
                {
                    _log.LogInformation("Product was not in the cart.");
                    return Ok("Product was not in the cart.");
                }
                _log.LogInformation("Product has been removed from the cart.");
                return Ok("Product has been removed from the cart.");
            }
            catch (Exception ex)
            {
                _log.LogInformation("Product cant be removed from the cart.");
                throw ex;
            }
            
        }


    }
}
