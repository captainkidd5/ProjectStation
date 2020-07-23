using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectStation.Models;

namespace ProjectStation.Pages
{
    public class ContactModel : PageModel
    {
        public Customer Customer { get; set; }

        [BindProperty]
        public bool EmailNotify { get; set; }

        public string SubmitMessage { get; set; }

        public void OnGet()
        {

        }

        public void OnSubmitContactForm(int id)
        {
            if(EmailNotify)
            {
                SubmitMessage = "We have received your information, thanky you!";
            }
            else
            {

            }
        }
    }
}