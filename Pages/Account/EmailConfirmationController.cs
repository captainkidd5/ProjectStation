using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ProjectStation.Pages.Account
{
    public class EmailConfirmationController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public IActionResult Index()
        {
            return View();
        }
        public EmailConfirmationController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnConfirmEmail(string userID, string token)
        {


            if (userID == null || token == null)
            {
                return RedirectToPage("/index");
            }

            var user = await userManager.FindByIdAsync(userID);

            if (user == null)
            {

            }
            var result = await userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return RedirectToPage("/Account/ConfirmEmail");
            }
            return RedirectToPage("/Account/ConfirmEmail");
        }
    }
}
