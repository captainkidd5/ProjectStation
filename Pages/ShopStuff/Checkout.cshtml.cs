using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Models.ShoppingStuff;

namespace ProjectStation.Pages.ShopStuff
{
    public class CheckoutModel : PageModel
    {
        [BindProperty]
        public Customer Customer { get; set; }

        public CheckoutModel()
        {

        }

        public void OnGet()
        {
            this.Customer = new Customer();
        }
    }
}