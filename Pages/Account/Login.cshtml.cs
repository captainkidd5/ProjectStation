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
    public class LoginModel : PageModel
    {
        private readonly IAccountRepository accountRepository;
        private readonly UserManager<IdentityUser> userManager;

        public LoginModel(IAccountRepository accountRepository, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.accountRepository = accountRepository;
            this.userManager = userManager;
            this.SignInManager = signInManager;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public bool RememberMe { get; set; }
        public SignInManager<IdentityUser> SignInManager { get; }

        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {

                var user = await userManager.FindByEmailAsync(Email);

                if (user != null && !user.EmailConfirmed && (await userManager.CheckPasswordAsync(user, Password))) 
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return Page();
                }
                var result = await SignInManager.PasswordSignInAsync(Email,Password, RememberMe, false);

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