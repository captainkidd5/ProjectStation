using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services;

namespace ProjectStation.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly IClientRepository userRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EditModel(IClientRepository userRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.userRepository = userRepository;
            this.webHostEnvironment = webHostEnvironment;
        }



        public IActionResult OnGet(int id)
        {
            Client = userRepository.GetClient(id);
            if (Client == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }
        [BindProperty]
        public Client Client { get; set; }


        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public bool Notify { get; set; }

        public string Message { get; set; }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {


                if (Photo != null)
                {
                    if (Client.PhotoPath != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                            "siteassets/users", Client.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    Client.PhotoPath = ProcessUploadedFile();
                }

                Client = userRepository.Update(Client);
                return RedirectToPage("Index");
            }
            return Page();
        }

        public IActionResult OnPostUpdateNotificationPreferences(int id)
        {
            if (Notify)
            {
                Message = "You have turned ON email notifications.";
            }
            else
            {
                Message = "You have turned OFF email notifications.";
            }
            TempData["message"] = Message; //once temp data is read, it is deleted on reload.

            return RedirectToPage("Details", new { id = id });

        }

        public string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadsFolder =
                    Path.Combine(webHostEnvironment.WebRootPath, "siteassets/users");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(Photo.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }

            }
            return uniqueFileName;
        }
    }
}