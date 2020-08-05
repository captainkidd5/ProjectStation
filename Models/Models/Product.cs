using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string PhotoPath { get; set; }
    }
}
