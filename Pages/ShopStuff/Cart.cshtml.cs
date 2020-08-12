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
using Models.Models;
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
        public List<CartItem> CartItems{ get; set; }


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
                ShoppingCart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("ShoppingCart");
                CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("ShoppingCart");
            }
            else
            {
                ShoppingCart = new ShoppingCart()
                { CartId = Guid.NewGuid().ToString(), DateCreated = DateTime.Now, UserId = null };
                HttpContext.Session.SetObjectAsJson("ShoppingCart", ShoppingCart);
                List<CartItem> cartItems = new List<CartItem>();
                HttpContext.Session.SetObjectAsJson("ShoppingCart", cartItems);
                this.ShoppingCart = ShoppingCart;
            }

            
           
        }
    }
}