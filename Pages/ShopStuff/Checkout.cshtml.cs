using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Razor;
using Models;
using Models.Models;
using Models.Models.ShoppingStuff;
using Services;
using Services.Shopping;
using Stripe;
using Stripe.Checkout;

namespace ProjectStation.Pages.ShopStuff
{
    [RequireHttps]
    public class CheckoutModel : PageModel
    {


        private readonly IShoppingCartRepository cartRepository;
        private readonly IProductRepository productRepository;
        private readonly SignInManager<IdentityUser> signInManager;

        public Session Session { get; set; }


        public ShoppingCart ShoppingCart { get; set; }

        public List<CartItem> CartItems { get; set; }

        public float TotalCost { get; set; }

        public bool Empty { get; set; }

        public double TotalAfterTax { get; set; }



        public CheckoutModel(IShoppingCartRepository cartRepository,
            IProductRepository productRepository, SignInManager<IdentityUser> signInManager)
        {
            this.cartRepository = cartRepository;
            this.productRepository = productRepository;
            this.signInManager = signInManager;
        }


        public void OnGet()
        {
            this.ShoppingCart = GetShoppingCart();
            this.CartItems = GetCartItems(this.ShoppingCart);
            this.TotalCost = GetTotalCost(this.ShoppingCart);

            if (CartItems.Count <= 0)
            {
                this.Empty = true;
            }
            List<SessionLineItemOptions> convertedOptions = new List<SessionLineItemOptions>();
            foreach (CartItem item in CartItems)
            {
                convertedOptions.Add(ConvertProductToStripe(item));
            }

            var options = new SessionCreateOptions
            {
                BillingAddressCollection = "required",
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                {
                    AllowedCountries = new List<string>
                 {
                "US",
                "CA",
                "JP",
                "KR",
                "CN",
                 },
                },
                LineItems = convertedOptions,
                Mode = "payment",
                SuccessUrl = "https://projectstation.azurewebsites.net/shopstuff/PaymentSuccess",
                CancelUrl = "https://projectstation.azurewebsites.net/shopstuff/PaymentFailure",

                
               
                
            };

            var service = new SessionService();
            Session session = service.Create(options);

            this.Session = session;
        }

        private SessionLineItemOptions ConvertProductToStripe(CartItem cartItem)
        {
            Models.Models.Product product = GetProduct(cartItem.ProductId);
            return new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "jpy",
                    Product = "prod_HpaIhTMMKDrtID",
                    UnitAmount = product.Price,
                },
                Quantity = cartItem.Quantity,
            };
        }

        public Models.Models.Product GetProduct(int id)
        {
            return productRepository.GetProduct(id);
        }

        private ShoppingCart GetShoppingCart()
        {
            ShoppingCart cart = new ShoppingCart();
            if (signInManager.IsSignedIn(User))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                cart = cartRepository.GetCart(userId);
            }
            else
            {
                cart = cartRepository.GetCart(null, HttpContext);
            }
            return cart;
        }

        private List<CartItem> GetCartItems(ShoppingCart shoppingCart)
        {
            List<CartItem> cartItems = new List<CartItem>();
            if (signInManager.IsSignedIn(User))
            {
                cartItems = cartRepository.GetItems(shoppingCart.CartId);
            }
            else
            {
                cartItems = cartRepository.GetItems(shoppingCart.CartId, HttpContext);
            }
            return cartItems;
        }

        private float GetTotalCost(ShoppingCart shoppingCart)
        {
            float cost = 0f;
            if (signInManager.IsSignedIn(User))
            {
                cost = cartRepository.TotalCost(shoppingCart.CartId, productRepository);
            }
            else
            {
                cost = cartRepository.TotalCost(shoppingCart.CartId, productRepository, HttpContext);
            }
            return cost;
        }
    }
}