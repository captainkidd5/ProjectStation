using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class ShoppingCart
    {
        [Key]
        public string Id { get; set; }
        public int UserId { get; set; }
        public System.DateTime DateCreated { get; set; }
    }
}
