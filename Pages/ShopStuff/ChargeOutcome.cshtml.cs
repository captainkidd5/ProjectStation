using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;
using Stripe.Checkout;

namespace ProjectStation.Pages.ShopStuff
{
    public enum ChargeOutCome
    {
        Failed = 0,
        Succeeded = 1
    }

    public class ChargeOutcomeModel : PageModel
    {
        public string ChargeOutcomeMessage { get; set; }
        const string secret = "whsec_wKgJiX3S2yEPMqqagexK9D2tUWSTlFs6";

        public void OnGet()
        {

        }

        [HttpPost]
        public async Task<IActionResult> OnPost()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], secret);

                // Handle the checkout.session.completed event
                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;

                    // Fulfill the purchase...
                   // HandleCheckoutSession(session);
                }
                else
                {
                    return Page();
                }
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
            return Page();
        }
    }
}