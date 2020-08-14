﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public class CartItem
    {
        [Key]
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public System.DateTime DateCreated { get; set; }

        
    }
}
