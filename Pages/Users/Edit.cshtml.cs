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
        private readonly IClientRepository userRepository;

        public EditModel(IClientRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        

        public IActionResult OnGet(int id)
        {
            Client = userRepository.GetClient(id);
            if(Client == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }
        [BindProperty]
        public Client Client { get; set; }

        public IActionResult OnPost(Client client)
        {
            Client = userRepository.Update(client);
            return RedirectToPage("Index");
        }
    }
}