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

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public string SubmitMessage { get; set; }

        public ContactModel()
        {
            Customer = new Customer();
        }

        public void OnGet()
        {

        }

        public void OnPost(int id)
        {
            Customer.Name = Name;
            Customer.Email = Email;
            SubmitMessage = "We have received your information, thanky you!" + Customer.Name + Customer.Email;
            if (EmailNotify)
            {
                SubmitMessage += " /n you have been added to our mailing list!";
            }
            else
            {
                SubmitMessage += " /n you have opted out of our mailing list!";
            }

           
        }
    }
}