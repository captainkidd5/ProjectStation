using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ProjectStation.Pages
{
    public class ContactModel : PageModel
    {

        [BindProperty]
        public bool EmailNotify { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public string SubmitMessage { get; set; }




        public ContactModel( )
        {

        }

        public void OnGet()
        {

        }

        public void OnPost(int id)
        {

        }
    }
}