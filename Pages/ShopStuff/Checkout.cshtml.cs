using System;
using System.Collections.Generic;
using System.IO;
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

        private readonly string WebHookSecret = "webhookSecret";

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

        public IActionResult ChargeChange()
        {
            var json = new StreamReader(HttpContext.Request.Body).ReadToEnd();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], WebHookSecret, throwOnApiVersionMismatch: true);
                Charge charge = (Charge)stripeEvent.Data.Object;
                switch (charge.Status)
                {
                    case "succeeded":
                        //This is an example of what to do after a charge is successful
                        charge.Metadata.TryGetValue("Product", out string Product);
                        charge.Metadata.TryGetValue("Quantity", out string Quantity);
                        //   Database.ReduceStock(Product, Quantity);
                        RedirectToPage("/shopstuff/ChargeOutcome", new { id = 1 });
                        break;
                    case "failed":
                        //Code to execute on a failed charge
                        RedirectToPage("/shopstuff/ChargeOutcome", new { id = 0 });
                        break;
                }
            }
            catch (Exception e)
            {
              //  e.Ship(HttpContext);
                return BadRequest();
            }
            return Page();
        }

        public void OnGet()
        {
           // this.Customer = new Customer();
        }
    }
}