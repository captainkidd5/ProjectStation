using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public ClientType? ClientType { get; set; }
    }
}
