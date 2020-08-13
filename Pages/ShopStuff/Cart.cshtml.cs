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

        [BindProperty]
        public List<CartItem> CartItems{ get; set; }

        public double TotalCost { get; set; }

        public bool Empty { get; set; }

        public CartModel(IShoppingCartRepository cartRepository,
            IProductRepository productRepository,SignInManager<IdentityUser> signInManager)
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
            
            if(CartItems.Count <= 0)
            {
                this.Empty = true;
            }
            
        }

        public void OnPostUpdateCart()
        {

            int[] newQuantities = new int[CartItems.Count];
            for (int i = 0; i < CartItems.Count; i++)
            {
                newQuantities[i] = CartItems[i].Quantity;
            }


            OnGet();

            for (int i = 0; i < CartItems.Count; i++)
            {
                if (signInManager.IsSignedIn(User))
                {
                    cartRepository.UpdateQuantity(ShoppingCart.CartId, CartItems[i].ProductId, newQuantities[i]); //use the new quantities
                }
                else
                {
                    cartRepository.UpdateQuantity(ShoppingCart.CartId, CartItems[i].ProductId, newQuantities[i], HttpContext);
                }
            }

            this.ShoppingCart = GetShoppingCart();
            foreach(CartItem cartItem in CartItems)
            {
                if (signInManager.IsSignedIn(User))
                {
                    cartRepository.UpdateQuantity(ShoppingCart.CartId, cartItem.ProductId, cartItem.Quantity);
                }
                else
                {
                    cartRepository.UpdateQuantity(ShoppingCart.CartId, cartItem.ProductId, cartItem.Quantity, HttpContext);
                }
                
            }
            OnGet();

        }


        public Product GetProduct(int id)
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

        private double GetTotalCost(ShoppingCart shoppingCart)
        {
            double cost = 0d;
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