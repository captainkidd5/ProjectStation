using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectStation.Pages.ShopStuff
{
    public enum ChargeOutCome
    {
        Failed = 0,
        Succeeded = 1
    }

    public class ChargeOutcomeModel : PageModel
    {
        public string ChargeOutcomeMessage { get; set; }

        public void OnGet(int chargeOutCome)
        {
            if ((ChargeOutCome)chargeOutCome == ChargeOutCome.Failed)
            {
                ChargeOutcomeMessage = "Payment failed, please try again.";
            }
            else if ((ChargeOutCome)chargeOutCome == ChargeOutCome.Succeeded)
            {
                ChargeOutcomeMessage = "Your payment has been received and your order is being processed!";
            }
            else
            {
                throw new Exception("Not a valid enum value for chargeoutcome");
            }
        }
    }
}