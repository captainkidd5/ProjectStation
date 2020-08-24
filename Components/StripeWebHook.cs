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
                    return RedirectToPage("/shopstuff/paymentfailure", "WebHook");
                }
                else if(stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    return RedirectToPage("/shopstuff/paymentsuccess", "WebHook");
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
            var customerInfo = new CustomerService();
            var customer = customerInfo.Get(session.CustomerId);

            string userId = string.Empty;
            ShoppingCart cart = new ShoppingCart();
            if (signInManager.IsSignedIn(User))
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                cart = shoppingCartRepository.GetCart(userId);
            }
            else
            {
                cart = shoppingCartRepository.GetCart(null, HttpContext); //get cookie cart
                var cartItems = shoppingCartRepository.GetItems(cart.CartId, HttpContext); //get the cookie items



                shoppingCartRepository.CreateCart("tempUser", cart.CartId); //now add card to database because order has gone thru.
                
                cart = shoppingCartRepository.GetCart(cart.CartId); //get the sql cart
                foreach (CartItem item in cartItems) //add cookie items to Sql cart.
                {
                    shoppingCartRepository.AddItem(cart, item.ProductId, item.Quantity, "tempUser");
                }

                HttpContext.Session.Remove("ShoppingCart"); //get rid of card and items so that the user will create a brand
                //new cart.
                HttpContext.Session.Remove("CartItems");
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
                EmailAddress = customer.Email,
                City = session.Shipping.Address.City,
                PhoneNumber = customer.Phone,

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
