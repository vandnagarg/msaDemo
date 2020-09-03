using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
    public class OrderController : ControllerBase
    {
        public string userId = String.Empty;
        readonly ILogger<OrderController> _log;

        public OrderController(ILogger<OrderController> log)
        {
            _log = log;
        }
        [HttpPost]
        public ActionResult CreateOrder()
        {
            try
            {
                var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
                userId = identity.Claims.FirstOrDefault(c => c.Type == "userId").Value;
                var cart = CartDb.cart.Where(x => x.UserId == userId).FirstOrDefault();
                if (cart != null)
                {
                    CartDb.cart.Remove(cart);
                    int count = OrderDb.orders.Count;
                    OrderDb.orders.Add(new Order(++count, userId, cart.id, cart.ProductsIDs.Count, cart.TotalPrice));
                    _log.LogInformation("Order Done");
                    return Ok("Order Done");

                }
                else
                {
                    _log.LogInformation("Current user doesnt contain any cart.");
                    return Ok("Current user doesnt contain any cart.");
                }
            }
            catch (Exception ex)
            {

                _log.LogInformation("Order cant be created.");
                throw ex;
            }

        }

        [HttpGet]
        public List<Order> GetOrders()
        {
            try
            {
                _log.LogInformation("To get all the orders for the user.");
                var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
                userId = identity.Claims.FirstOrDefault(c => c.Type == "userId").Value;
                return OrderDb.orders.Where(o => o.userID == userId).ToList();
            }
            catch (Exception ex)
            {
                _log.LogInformation("Error in getting orders.");
                throw ex;
            }
        }

        [HttpPut]
        [Route("SetPaymentStatus")]
        public bool SetPaymentStatus(int orderId)
        {
            try
            {
                var order = OrderDb.orders.Where(o => o.id == orderId).FirstOrDefault();
                if (order == null)
                {
                    _log.LogInformation("Order is not present or payment status cant be marked as successful.");
                    return false;
                }
                else
                {
                    _log.LogInformation("Payment has been marked as successful for the order");
                    OrderDb.orders.Where(o => o.id == orderId).FirstOrDefault().isPaymnetSuccess = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                _log.LogInformation("Payment cant be marked as successful for the order");
                return false;
            }
        }
    }
}
