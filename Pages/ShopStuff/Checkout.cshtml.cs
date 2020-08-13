using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Razor;
using Models.Models.ShoppingStuff;
using Stripe;

namespace ProjectStation.Pages.ShopStuff
{
    public class CheckoutModel : PageModel
    {
        //[BindProperty]
        //public Customer Customer { get; set; }

        public CheckoutModel()
        {

        }

        public IActionResult OnPostCharge(string stripeEmail, string stripeToken)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            var customer = customerService.Create(new CustomerCreateOptions()
            {
                Email = stripeEmail,
                Source = stripeToken,
            });

            var charge = chargeService.Create(new ChargeCreateOptions()
            {
                Amount = 500,
                Description = "test",
                Currency = "usd",
                Customer = customer.Id

            }) ;

            return Page();
        }

        public void OnGet()
        {
           // this.Customer = new Customer();
        }
    }
}