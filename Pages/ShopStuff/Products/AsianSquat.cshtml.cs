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
        [BindProperty]
        public Product Product { get; private set; }

        [BindProperty]
        public int Quantity { get; set; }

        private readonly IShoppingCartRepository cartRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IProductRepository productRepository;

        [BindProperty]
        public string Price { get; 
            private set; }

        [TempData]
        public string AddToCartMessage { get; set; }

        public AsianSquatModel(IShoppingCartRepository cartRepository, SignInManager<IdentityUser> signInManager, IProductRepository productRepository)
        {
            this.cartRepository = cartRepository;
            this.signInManager = signInManager;
            this.productRepository = productRepository;

        }

        public void OnGet()
        {
            //ImagePaths = new List<string>()
            //{
            //     "~/SiteAssets/shopstuff/Products/AsianSquat/Asian-Squat_01.jpg",
            //     "~/SiteAssets/shopstuff/Products/AsianSquat/Asian-Squat_02.jpg",
            //     "~/SiteAssets/shopstuff/Products/AsianSquat/Asian-Squat_03.jpg",
            //     "~/SiteAssets/shopstuff/Products/AsianSquat/Asian-Squat_04.jpg",
            //     "~/SiteAssets/shopstuff/Products/AsianSquat/Asian-Squat_05.jpg",
            //};
            Product = productRepository.GetProduct(1);
            Price = string.Format(Product.Price.ToString("C", CultureInfo.CreateSpecificCulture("ja-JP")));
        }
        public ShoppingCart ShoppingCart { get; set; }
        public IActionResult OnPost()
        {
            OnGet();

            if (signInManager.IsSignedIn(User))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                ShoppingCart = cartRepository.GetCart(userId);
            }
            else
            {
                ShoppingCart = cartRepository.GetCart(null, HttpContext);
            }

            this.Product = productRepository.GetProduct(1);

            if (signInManager.IsSignedIn(User))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                cartRepository.AddItem(ShoppingCart, Product.ID, Quantity, userId,null);
            }
            else
            {
                cartRepository.AddItem(ShoppingCart, Product.ID, Quantity,null, HttpContext);
            }
            AddToCartMessage = "Your items have been added to the cart!";
            return Page();
           
        }
    }
}