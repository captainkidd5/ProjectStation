using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProjectStation.Models;
using ProjectStation.Services;

namespace ProjectStation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFileProductService ProductService;
        public IEnumerable<Product> Products { get; private set; }

        public IndexModel(ILogger<IndexModel> logger,
            JsonFileProductService productService) //ask for services here from asp net (From configure services in startup)
        {
            _logger = logger;
            this.ProductService = productService;
        }

        public void OnGet() //when someone gets this page what should we do?
        {
            Products = ProductService.GetProducts();
        }
    }
}
