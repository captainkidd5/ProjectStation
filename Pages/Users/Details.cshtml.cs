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
        private readonly IUserRepository userRepository;

        public DetailsModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User UserEntity { get; private set; }

        public IActionResult OnGet(int id )
        {

            UserEntity = userRepository.GetUser(id);
            if(UserEntity==null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }
    }
}