using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Models;
using Services;

namespace ProjectStation.Pages.Shared
{
    public class _AccountRegistryModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        public _AccountRegistryModel( UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.SignInManager = signInManager;
        }
        [BindProperty]
        public UserAccount UserAccount { get; set; }
        public SignInManager<IdentityUser> SignInManager { get; }

        public void OnGet(bool logout)
        {
            UserAccount = new UserAccount();

            if (logout)
            {

            }
        }

        public async Task<IActionResult> OnLogout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToPage("Shop");
        }


        public async Task<IActionResult> OnPost(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = userAccount.Email, Email = userAccount.Email };
                var result = await userManager.CreateAsync(user, userAccount.Password);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToPage("/Shop");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return Page();
        }
    }
}