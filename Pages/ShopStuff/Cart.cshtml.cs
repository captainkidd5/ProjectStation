using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services.Shopping;

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
            if (Request.Cookies["CartCookie"] == null)
            {
                Response.Cookies.Append(
                    "CartCookie",
                    new Guid().ToString(), new CookieOptions()
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(30)
                    });

            }

            if(signInManager.IsSignedIn)
            {

            }
            ShoppingCart = new ShoppingCart();
        }
    }
}