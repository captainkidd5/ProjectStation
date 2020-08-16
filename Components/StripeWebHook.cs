using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Shopping;
using Stripe;
using Stripe.Checkout;
using Models;
using Models.Models;
using Models.Models.ShoppingStuff;

namespace ProjectStation.Components
{
    [Route("api/[controller]")]
    public class StripeWebHook : Controller
    {
        // You can find your endpoint's secret in your webhook settings
        const string secret = "whsec_wKgJiX3S2yEPMqqagexK9D2tUWSTlFs6";
        private readonly IOrderRepository orderRepository;

        public StripeWebHook(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], secret);

                // Handle the checkout.session.completed event
                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var session = stripeEvent.Data.Object as Session;

                    // Fulfill the purchase...
                    // HandleCheckoutSession(session);
                    return RedirectToPage("/shopstuff/chargeoutcome", "WebHook");
                }
                else if(stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    return RedirectToPage("/shopstuff/chargeoutcome", "WebHook");
                }
                else
                {
                    return Ok();

                }
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
            
        }

        public void CompleteSession(Session session)
        {
            Models.Models.Order order = new Models.Models.Order()
            {
                Id = session.Id,
                FirstName = session.Customer.Name.Split(" ")[0],
                LastName = session.Customer.Name.Split(" ")[1],
                Country = session.Shipping.Address.Country,
                StreetNumber = session.Shipping.Address.Line1 + session.Shipping.Address.Line2,
                State = session.Shipping.Address.State,
                ZipCode = session.Shipping.Address.PostalCode,



            }
            orderRepository.Add()
        }
    }
}
