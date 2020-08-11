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
        public UserAccount UserAccount { get; set; }

        [BindProperty]
        public bool RememberMe { get; set; }
        private readonly SignInManager<IdentityUser> SignInManager;

        public void OnGet()
        {
            UserAccount = new UserAccount();
        }
        public async Task<IActionResult> OnPost()
        {
            if (UserAccount.Email != null)
            {



                var user = await userManager.FindByEmailAsync(UserAccount.Email);
                if (user != null)
                {


                    //if (user != null && (await userManager.CheckPasswordAsync(user, Password))) 
                    //{
                    //    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    //    return Page();
                    //}

                    //if (user != null && !user.EmailConfirmed && (await userManager.CheckPasswordAsync(user, Password)))
                    //{
                    //    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    //    return Page();
                    //}
                    var result = await SignInManager.PasswordSignInAsync(user.UserName, UserAccount.Password, RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToPage("/Shop");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid Login Attempt");


            return Page();
        }
    }
}