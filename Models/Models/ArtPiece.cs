using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Models
{
    public class ArtPiece
    {
        public int Id { get; set; }
        public string Exhibition { get; set; }
        public string Name { get; set; }
 
        public string Description { get; set; }

        public string PhotoPath { get; set; }

        public string QRPath { get; set; }
    }
}
