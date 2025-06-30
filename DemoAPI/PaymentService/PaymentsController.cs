using PaymentService.Interfaces;
using PaymentService.Models;
using DemoAPI.Common.Errors;
using DemoAPI.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace PaymentService.Controllers
{
    public class PaymentsController : BaseAPIController
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration; 

        public PaymentsController(IPaymentService paymentService, IConfiguration configuration)
        {
            _paymentService = paymentService;
            _configuration = configuration;
        }

        [HttpGet("{BasketId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<CustomerBasket>> GetOrUpdatePaymentIntent(string BasketId)
        {
            var Basket = await _paymentService.CreateOrUpdatePaymentIntentAsync(BasketId);

            if (Basket == null)
            {
                return BadRequest(new ApiResponse(400, "Problem with your basket"));
            }
            return Ok(Basket);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _configuration["Stripe:WebhookSecret"]);

            var PaymentIntent = stripeEvent.Data.Object as PaymentIntent;

            

            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentSucceeded:  

                    await _paymentService.UpdatePaymentIntentSucceededOrFailed(PaymentIntent.Id, true);

                    break;
                case EventTypes.PaymentIntentPaymentFailed: 
                    await _paymentService.UpdatePaymentIntentSucceededOrFailed(PaymentIntent.Id , false);

                    break;
                default:
                    return BadRequest(new ApiResponse(400, "Unhandled event type"));
            }
            return Ok();
        }
    }
}
