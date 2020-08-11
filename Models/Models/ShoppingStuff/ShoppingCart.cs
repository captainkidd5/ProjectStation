using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public System.DateTime DateCreated { get; set; }
    }
}
