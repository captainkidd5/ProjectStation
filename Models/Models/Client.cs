using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace Models
{
    public class Client
    {
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage ="Name is required.")]
        [MinLength(3,ErrorMessage ="Name should contain at least 3 characters.")]
        public string Name { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage ="Invalid email format.")]
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public ClientType? ClientType { get; set; }
    }
}
