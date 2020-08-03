using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services;

namespace ProjectStation.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly IClientRepository userRepository;

        public DetailsModel(IClientRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public Client UserEntity { get; private set; }

        public IActionResult OnGet(int id )
        {

            UserEntity = userRepository.GetClient(id);
            if(UserEntity==null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }
    }
}