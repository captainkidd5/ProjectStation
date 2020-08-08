using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Models;
using Services;

namespace ProjectStation.Pages.ShopStuff.Products
{
    public class AsianSquatModel : PageModel
    {
        public Product Product { get; set; }
        private readonly IProductRepository productRepository;
        public string Price { get; set; }

        public List<string> ImagePaths { get; set; }

        public AsianSquatModel(IProductRepository productRepository)
        {
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
    }
}