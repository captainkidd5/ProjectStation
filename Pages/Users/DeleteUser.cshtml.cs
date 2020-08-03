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
    public class DeleteUserModel : PageModel
    {
        private readonly IClientRepository clientRepository;

        [BindProperty]
        public Client Client{ get; set; }

        public DeleteUserModel(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }


        public IActionResult OnGet(int id)
        {
            Client = clientRepository.GetClient(id);

            if(Client==null)
            {
                return RedirectToPage("/NotFound");

            }

            return Page();
        }

        public IActionResult OnPost()
        {
            Client deletedClient = clientRepository.Delete(Client.Id);

            if(deletedClient == null)
            {
                return RedirectToPage("/NotFound");
            }
            return RedirectToPage("Index");

        }
    }
}