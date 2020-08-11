using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Models.Models;
using Services;

namespace ProjectStation.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountRepository accountRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<RegisterModel> logger;

        public RegisterModel(IAccountRepository accountRepository, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, ILogger<RegisterModel> logger)
        {
            this.accountRepository = accountRepository;
            this.userManager = userManager;
            this.SignInManager = signInManager;
            this.logger = logger;
        }
        [BindProperty]
        public UserAccount UserAccount { get; set; }
        private readonly SignInManager<IdentityUser> SignInManager;

        public void OnGet()
        {
            UserAccount = new UserAccount();


        }

        public async Task<IActionResult> OnLogout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToPage("Shop");
        }

        public async Task<IActionResult> ConfirmEmail(string userID, string token)
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

        [BindProperty]
        public string RegisterMessage { get; set; }



        public async Task<IActionResult> OnPost(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = userAccount.Email, Email = userAccount.Email };
                var result = await userManager.CreateAsync(user, userAccount.Password);

                if (result.Succeeded)
                {
                    var token = userManager.GenerateEmailConfirmationTokenAsync(user).Result;

                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme);

                    logger.Log(LogLevel.Warning, confirmationLink);

                    using (var client = new SmtpClient())
                    {

                        client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        string pickupstring = @"C:\Users\armad\Desktop\emailTexter";
                        client.PickupDirectoryLocation = pickupstring;

                        var message = new MailMessage();
                        message.To.Add("joe@gmail.com");
                        message.Subject = "Fourth Coffee - New Order";
                        message.Body = confirmationLink;
                        message.IsBodyHtml = true;
                        message.From = new MailAddress("sales@fourthcoffee.com");
                        await client.SendMailAsync(message);
                    }
                    
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