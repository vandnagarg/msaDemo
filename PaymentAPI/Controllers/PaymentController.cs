using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PaymentAPI.Models;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]

    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClient;

        readonly ILogger<PaymentController> log;

        public PaymentController( IConfiguration config, IHttpClientFactory clientFactory, ILogger<PaymentController> _log)
        {
            _config = config;
            _httpClient = clientFactory;
            log = _log;
        }
        [HttpPost]
        public async Task<ActionResult> Make_Payment([FromBody]PaymentModel payment)
        {
            try
            {
                var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
                if (identity.Claims.FirstOrDefault(c => c.Type == "userId") != null)
                {
                    //do_payment Code

                    //add payment details in payment db
                    var userId = identity.Claims.FirstOrDefault(c => c.Type == "userId").Value;
                    var count = PaymentDb.paymentList == null ? 0 : PaymentDb.paymentList.Count;
                    PaymentDb.paymentList.Add(new PaymentModel(++count, payment.orderId, userId, payment.PaymentMethod, payment.Status));


                    //change payment status by OrderAPI
                    var accessToken = Request.Headers["Authorization"];
                    string _apiUrl = "https://localhost:44360/api/order/SetPaymentStatus";// + payment.orderId ;
                    var response = Common.HttpClientService.PutAsyncMethodCall(_apiUrl, accessToken.ToString(), payment.orderId);
                    if (response.Result.Content.ReadAsStringAsync().Result == "true")
                    {
                        log.LogInformation("Payment done");
                        return Ok("Payment Done");
                    }
                    else
                    {
                        log.LogInformation("Payment not successful");
                        return BadRequest("Payment not successfull.");
                    }
                }
                else
                {
                    log.LogInformation("Payment not successful");
                    return BadRequest("Payment not successful");
                }
            }
            catch (Exception ex)
            {
                log.LogInformation("Payment not successful");
                throw ex;
            }

        }
    }
}