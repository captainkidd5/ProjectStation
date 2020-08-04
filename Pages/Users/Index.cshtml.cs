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
    public class IndexModel : PageModel
    {

        private IClientRepository userRepository;

        public IEnumerable<Client> Users { get; set; }

        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }

        public IndexModel(IClientRepository userRepository)
        {
            this.userRepository = userRepository;
            
        }


        public void OnGet()
        {
            Users = userRepository.Search(SearchTerm);
        }
    }
}