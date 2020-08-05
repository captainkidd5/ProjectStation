using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Models;
using Services;

namespace ProjectStation.Pages.Account
{
    public class LoginViewModel : PageModel
    {
        private readonly IAccountRepository accountRepository;
        private readonly UserManager<IdentityUser> userManager;

        public LoginViewModel(IAccountRepository accountRepository, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.accountRepository = accountRepository;
            this.userManager = userManager;
            this.SignInManager = signInManager;
        }
        [BindProperty]
        public UserAccount UserAccount { get; set; }

        [BindProperty]
        public bool RememberMe { get; set; }
        public SignInManager<IdentityUser> SignInManager { get; }

        public void OnGet()
        {
            UserAccount = new UserAccount();


        }

 

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {

                var result = await SignInManager.PasswordSignInAsync(UserAccount.Email, UserAccount.Password, RememberMe, false);


                if (result.Succeeded)
                {
                    return RedirectToPage("/Shop");
                }

                    ModelState.AddModelError("", "Invalid Login Attempt");
                

            }
            return Page();
        }
    }
}