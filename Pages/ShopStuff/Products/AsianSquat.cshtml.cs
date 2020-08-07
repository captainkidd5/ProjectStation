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


        public AsianSquatModel(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
            
        }

        public void OnGet()
        {

            this.Product = productRepository.GetProduct(1);
            this.Price = string.Format(Product.Price.ToString(), "{0:C}", CultureInfo.CreateSpecificCulture("ja-JP"));
        }
    }
}