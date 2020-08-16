using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;
using Stripe.Checkout;

namespace ProjectStation.Pages.ShopStuff
{
    public enum ChargeOutCome
    {
        Failed = 0,
        Succeeded = 1
    }

    [RequireHttps]
    public class ChargeOutcomeModel : PageModel
    {
        public string ChargeOutcomeMessage { get; set; }


        public ChargeOutcomeModel()
        {

        }
        public void OnGetWebHook()
        {
            string hi = "hi";
        }

        public async Task<IActionResult> OnPost()
        {
            
            return Page();
        }
    }
}