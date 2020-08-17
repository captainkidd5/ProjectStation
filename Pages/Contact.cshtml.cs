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
        public string MessageSent { get; set; }

        [BindProperty]
        public ContactForm ContactForm{ get; set; }

        public IEmailSender EmailSender { get; }

        public ContactModel(IEmailSender emailSender)
        {
            this.EmailSender = emailSender;
        }

        public void OnGet()
        {
            ContactForm = new ContactForm();
            MessageSent = null;
        }

        public void OnPost()
        {
           
            if (ModelState.IsValid)
            {
                string msg = "Name: " + ContactForm.Name + "\n" +
               "Name(Reading): " + ContactForm.NameReading + "\n" +
               "Phone: " + ContactForm.PhoneNumber + "\n" +
               "Email: " + ContactForm.Email + "\n" +
               "Address: " + ContactForm.Address + "\n" +
                "CompanyName: " + ContactForm.CompanyName + "\n" +
                 "Referal: " + ContactForm.Referal.ToString() + "\n" +
                 "Inquiry: " + ContactForm.Inquiry + "\n";
                EmailSender.SendEmailAsync("tucker.higgins1@gmail.com", "Inquiry from " + ContactForm.Name + " " +
                    ContactForm.NameReading, msg);
                MessageSent = "Thank you, we have received your message.";
            }
        }
    }
}