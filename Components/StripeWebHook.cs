using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Shopping;
using Stripe;
using Models;
using Models.Models;
using Models.Models.ShoppingStuff;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Stripe.Checkout;

namespace ProjectStation.Components
{
    [Route("api/[controller]")]
    public class StripeWebHook : Controller
    {
        // You can find your endpoint's secret in your webhook settings
        const string secret = "whsec_wKgJiX3S2yEPMqqagexK9D2tUWSTlFs6";
        private readonly IOrderRepository orderRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly SignInManager<IdentityUser> signInManager;

        public StripeWebHook(IOrderRepository orderRepository,
            IShoppingCartRepository shoppingCartRepository, SignInManager<IdentityUser> signInManager)
        {
            this.orderRepository = orderRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.signInManager = signInManager;
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
                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    
                    var session = stripeEvent.Data.Object as Session;

                    // Fulfill the purchase...
                    // HandleCheckoutSession(session);
                    CompleteSession(session);
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
            string userId = string.Empty;
            ShoppingCart cart = new ShoppingCart();
            if (signInManager.IsSignedIn(User))
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                cart = shoppingCartRepository.GetCart(userId);
            }
            else
            {
                cart = shoppingCartRepository.GetCart(null, HttpContext);
            }

            Models.Models.Order order = new Models.Models.Order()
            {
                Id = session.Id,
                CustomerId = userId,
                FirstName = session.Shipping.Name,
                LastName = session.Shipping.Name,
                Country = session.Shipping.Address.Country,
                StreetNumber = session.Shipping.Address.Line1 + session.Shipping.Address.Line2,
                State = session.Shipping.Address.State,
                ZipCode = session.Shipping.Address.PostalCode,
                
                
                DateTime = DateTime.Now,
                OrderStatus = OrderStatus.Shipped



            };
            if(orderRepository.Add(order))
            {
                shoppingCartRepository.SetCartToCheckedOut(cart);
            }

        
        }
    }
}
