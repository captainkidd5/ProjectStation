using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
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

namespace ProjectStation.Pages.ShopStuff.Products
{
    public class AsianSquatModel : PageModel
    {
        public Product Product { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        private readonly IShoppingCartRepository cartRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IProductRepository productRepository;
        public string Price { get; 
            set; }

        public List<string> ImagePaths { get; set; }

        public AsianSquatModel(IShoppingCartRepository cartRepository, SignInManager<IdentityUser> signInManager, IProductRepository productRepository)
        {
            this.cartRepository = cartRepository;
            this.signInManager = signInManager;
            this.productRepository = productRepository;

        }

        public void OnGet()
        {
            ImagePaths = new List<string>()
            {
                 "~/SiteAssets/shopstuff/Products/AsianSquat/Asian-Squat_01.jpg",
                 "~/SiteAssets/shopstuff/Products/AsianSquat/Asian-Squat_02.jpg",
                 "~/SiteAssets/shopstuff/Products/AsianSquat/Asian-Squat_03.jpg",
                 "~/SiteAssets/shopstuff/Products/AsianSquat/Asian-Squat_04.jpg",
                 "~/SiteAssets/shopstuff/Products/AsianSquat/Asian-Squat_05.jpg",
            };
            this.Product = productRepository.GetProduct(1);
            this.Price = string.Format(Product.Price.ToString("C", CultureInfo.CreateSpecificCulture("ja-JP")));
        }
        public ShoppingCart ShoppingCart { get; set; }
        public void OnPost()
        {
            this.Product = productRepository.GetProduct(1);
            if (signInManager.IsSignedIn(User))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                ShoppingCart = cartRepository.GetCart(userId);
                if (ShoppingCart == null)
                {
                    string cartID = Guid.NewGuid().ToString();
                    ShoppingCart = cartRepository.CreateCart(userId, cartID);
                }

                cartRepository.AddItem(Product.ID, Quantity);
            }
            else if (HttpContext.Session.Get("ShoppingCart") != null)
            {
                AddToSessionCart();
            }
            else
            {
                ShoppingCart = new ShoppingCart()
                { CartId = Guid.NewGuid().ToString(), DateCreated = DateTime.Now, UserId = null };
                HttpContext.Session.SetObjectAsJson("ShoppingCart", ShoppingCart);
                List<CartItem> cartItems = new List<CartItem>();
                HttpContext.Session.SetObjectAsJson("CartItems", cartItems);
                this.ShoppingCart = ShoppingCart;

                AddToSessionCart();
            }
        }

        private void AddToSessionCart()
        {
            List<CartItem> cartItems = new List<CartItem>();
            ShoppingCart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("ShoppingCart");
            cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("CartItems");

            CartItem cartItem = cartItems.Find(x => x.ProductId == this.Product.ID);
            if (cartItem != null)
            {
                cartItem.Quantity += Quantity;
            }
            else
            {
                cartItems.Add(new CartItem()
                {
                    CartId = ShoppingCart.CartId,
                    DateCreated = DateTime.Now,
                    Id = this.Product.ID,
                    ProductId = this.Product.ID,
                    Quantity = this.Quantity
                });
            }
            
            HttpContext.Session.SetObjectAsJson("CartItems", cartItems);
        }
    }
}