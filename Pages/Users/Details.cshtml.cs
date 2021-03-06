﻿using System;
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

        public Client Client { get; private set; }

        public IActionResult OnGet(int id )
        {

            Client = userRepository.GetClient(id);
            if(Client==null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }

        [TempData]
        public string Message { get; set; }
    }
}