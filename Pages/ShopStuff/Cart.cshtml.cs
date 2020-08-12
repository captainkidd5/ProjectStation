using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using ProjectStation.Components;
using Services.Shopping;
using Stripe.BillingPortal;

namespace ProjectStation.Pages.ShopStuff
{
    public class CartModel : PageModel
    {
        private readonly IShoppingCartRepository cartRepository;
        private readonly SignInManager<IdentityUser> signInManager;

        public ShoppingCart ShoppingCart { get; set; }


        public CartModel(IShoppingCartRepository cartRepository, SignInManager<IdentityUser> signInManager)
        {
            this.cartRepository = cartRepository;
            this.signInManager = signInManager;
        }

        public void OnGet()
        {
            if (signInManager.IsSignedIn(User))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                ShoppingCart = cartRepository.GetCart(userId);
                if(ShoppingCart == null)
                {
                    string cartID = Guid.NewGuid().ToString();
                    ShoppingCart = cartRepository.CreateCart(userId, cartID);
                }
            }
            else if(HttpContext.Session.Get("ShoppingCart") != null)
            {
                ShoppingCart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("cart");
            }
            else
            {
                ShoppingCart = new ShoppingCart()
                { CartId = Guid.NewGuid().ToString(), DateCreated = DateTime.Now, UserId = null };
                HttpContext.Session.SetObjectAsJson("ShoppingCart", ShoppingCart);
                this.ShoppingCart = ShoppingCart;
            }

            
           
        }
    }
}