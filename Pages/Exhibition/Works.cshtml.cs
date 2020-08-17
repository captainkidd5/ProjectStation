using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Models;
using Services.ArtWork;

namespace ProjectStation.Pages.Exhibition
{
    public class WorksModel : PageModel
    {

        public ArtPiece ArtPiece { get; set; }
        private readonly IArtPieceRepository artPieceRepository;
        public string MessageSent { get; set; }

        [BindProperty]
        public ContactForm ContactForm { get; set; }

        public IEmailSender EmailSender { get; }
        public WorksModel(IArtPieceRepository artPieceRepository, IEmailSender emailSender)
        {
            this.artPieceRepository = artPieceRepository;
            this.EmailSender = emailSender;
        }
        public void OnGet(int id)
        {
            RetrieveArtPiece(id);

            ContactForm = new ContactForm();
            MessageSent = null;
        }

        private void RetrieveArtPiece(int id)
        {
            if (id > 0)
            {
                this.ArtPiece = artPieceRepository.GetArtPiece(id);
                this.ArtPiece.PhotoPath = "~/SiteAssets/exhibitions/" + ArtPiece.PhotoPath;
                this.ArtPiece.QRPath = "~/SiteAssets/exhibitions/" + ArtPiece.QRPath;
            }
        }

        public void OnPost(int id)
        {
            RetrieveArtPiece(id);
            bool state = ContactForm.AgreePolicy;
            if (ModelState.IsValid)
            {
                string msg = "Name: " + ContactForm.Name + "\n" +
               "Name(Reading): " + ContactForm.NameReading + "\n" +
               "Phone: " + ContactForm.PhoneNumber + "\n" +
               "Email: " + ContactForm.Email + "\n" +
               "Address: " + ContactForm.Address + "\n" +
                "CompanyName: " + ContactForm.CompanyName + "\n" +
                 "Referal: " + ContactForm.Referal.ToString() + "\n" +
                 "Inquiry about: " + ArtPiece.Name + " " + ContactForm.Inquiry + "\n";
                EmailSender.SendEmailAsync("tucker.higgins1@gmail.com", "Inquiry from " + ContactForm.Name + " " +
                    ContactForm.NameReading, msg);
                MessageSent = "Thank you, we have received your message.";
            }
        }
    }
}