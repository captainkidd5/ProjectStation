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

        private IUserRepository userRepository;

        public IEnumerable<User> Users { get; set; }

        public IndexModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            
        }


        public void OnGet()
        {
            Users = userRepository.GetAllClients();
        }
    }
}