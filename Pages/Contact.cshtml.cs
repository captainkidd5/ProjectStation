using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Models.Models;
using Newtonsoft.Json.Linq;
using Services;

namespace ProjectStation.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IHttpClientFactory clientFactory;

        public CaptchaData CaptchaData { get; set; }

        public string MessageSent { get; set; }

        [BindProperty]
        public ContactForm ContactForm{ get; set; }

        public IEmailSender EmailSender { get; }

        public ContactModel(IEmailSender emailSender, IOptions<CaptchaData> captchaData, IHttpClientFactory clientFactory)
        {
            this.EmailSender = emailSender;
            this.clientFactory = clientFactory;
            this.CaptchaData = captchaData.Value;
        }

        public void OnGet()
        {
            ContactForm = new ContactForm();
            MessageSent = null;
        }

        public async Task OnPostAsync()
        {
            string recaptchaResponse = this.Request.Form["g-recaptcha-response"];

            var client = clientFactory.CreateClient();
            try
            {
                var parameters = new Dictionary<string, string>
            {
                {"secret", CaptchaData.PrivateKey},
                {"response", recaptchaResponse},
                {"remoteip", this.HttpContext.Connection.RemoteIpAddress.ToString()}
            };

                HttpResponseMessage response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(parameters));
                response.EnsureSuccessStatusCode();

                string apiResponse = await response.Content.ReadAsStringAsync();
                dynamic apiJson = JObject.Parse(apiResponse);
                if (apiJson.success != true)
                {
                    this.ModelState.AddModelError(string.Empty, "There was an unexpected problem processing this request. Please try again.");
                }
            }
            catch (HttpRequestException ex)
            {
               
            }




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
                 "Inquiry: " + ContactForm.Inquiry + "\n";
                EmailSender.SendEmailAsync("tucker.higgins1@gmail.com", "Inquiry from " + ContactForm.Name + " " +
                    ContactForm.NameReading, msg);
                MessageSent = "Thank you, we have received your message.";
            }
        }
    }
}