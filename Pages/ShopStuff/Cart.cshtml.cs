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
using Services;
using Services.Shopping;
using Stripe.BillingPortal;

namespace ProjectStation.Pages.ShopStuff
{
    public class CartModel : PageModel
    {
        private readonly IShoppingCartRepository cartRepository;
        private readonly IProductRepository productRepository;
        private readonly SignInManager<IdentityUser> signInManager;

        public ShoppingCart ShoppingCart { get; set; }
        public List<CartItem> CartItems{ get; set; }

        public double TotalCost { get; set; }

        public CartModel(IShoppingCartRepository cartRepository,
            IProductRepository productRepository,SignInManager<IdentityUser> signInManager)
        {
            this.cartRepository = cartRepository;
            this.productRepository = productRepository;
            this.signInManager = signInManager;
        }

        public string GetProductImg(int id)
        {
            return productRepository.GetProduct(id).PhotoPath;
        }

        public string GetProductName(int id)
        {
            return productRepository.GetProduct(id).Name;
        }

        public Product GetProduct(int id)
        {
            return productRepository.GetProduct(id);
        }

        public void OnGet()
        {
            if (signInManager.IsSignedIn(User))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                ShoppingCart = cartRepository.GetCart(userId);
                CartItems = cartRepository.GetItems(ShoppingCart.CartId);
                this.TotalCost = cartRepository.TotalCost(ShoppingCart.CartId, productRepository);
            }
            else
            {
               ShoppingCart = cartRepository.GetCart(null, HttpContext);
                CartItems = cartRepository.GetItems(ShoppingCart.CartId, HttpContext);
                this.TotalCost = cartRepository.TotalCost(ShoppingCart.CartId, productRepository, HttpContext);
            }             
        }

        public void OnPostUpdateCart()
        {
            OnGet();

        }
    }
}