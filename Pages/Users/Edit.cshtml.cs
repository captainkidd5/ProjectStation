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
    public class EditModel : PageModel
    {
        private readonly IUserRepository userRepository;

        public EditModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [BindProperty]
        public User UserEntity{ get; set; }


        public IActionResult OnGet(int id)
        {
            UserEntity = userRepository.GetUser(id);
            if(UserEntity == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }

        public IActionResult OnPost(User user)
        {
            UserEntity = userRepository.Update(user);
            return RedirectToPage("Index");
        }
    }
}