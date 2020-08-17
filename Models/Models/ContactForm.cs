using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public enum Referal
    {
        Gallery = 1,
        Friend = 2,
        Facebook = 3,
        Instagram = 4,
        Other = 5
    }
    public class ContactForm
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "Name should contain at least 3 characters.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Name (reading)")]
        public string NameReading { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage ="Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        public string Address { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Max message length is 500 characters.")]
        public string Inquiry { get; set; }

        [Required]
        public Referal? Referal { get; set; }

        [Display(Name = "Privacy Policy")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must review the privacy policy to submit an inquiry.")]
        public bool AgreePolicy { get; set; }
    }
}
