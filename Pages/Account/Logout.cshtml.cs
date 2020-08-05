using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace ProjectStation.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly IAccountRepository accountRepository;
        private readonly UserManager<IdentityUser> userManager;

        public SignInManager<IdentityUser> SignInManager { get; }

        public LogoutModel(IAccountRepository accountRepository, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.accountRepository = accountRepository;
            this.userManager = userManager;
            this.SignInManager = signInManager;
        }

        public void OnGet()
        {

        }
        //Runescape1!
        public async Task<IActionResult> OnPost()
        {
            await SignInManager.SignOutAsync();
            return RedirectToPage("/Shop");
        }
    }
}