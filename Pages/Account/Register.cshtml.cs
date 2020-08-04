using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Models;
using Services;

namespace ProjectStation.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountRepository accountRepository;

        public RegisterModel(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        [BindProperty]
        public UserAccount UserAccount { get; set; }
        public void OnGet()
        {
            UserAccount = new UserAccount();
        }
    }
}