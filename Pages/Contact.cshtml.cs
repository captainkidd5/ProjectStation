using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Models;

namespace ProjectStation.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty]
        public ContactForm ContactForm{ get; set; }

        [BindProperty]
        public bool EmailNotify { get; set; }

        public IEmailSender EmailSender { get; }

        public ContactModel(IEmailSender emailSender)
        {
            this.EmailSender = emailSender;
        }

        public void OnGet()
        {
            ContactForm = new ContactForm();
        }

        public void OnPost()
        {
            if(ModelState.IsValid)
            {

            }
        }
    }
}